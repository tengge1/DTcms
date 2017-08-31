using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:系统频道表
    /// </summary>
    public partial class site_channel
    {
        private string databaseprefix;//数据库表名前缀
        public site_channel(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from  " + databaseprefix + "site_channel");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据及其子表
        /// </summary>
        public int Add(Model.site_channel model)
        {
            //取得站点对应的导航，如果站点导航不存在则退出新增
            int parent_id = new DAL.sites(databaseprefix).GetSiteNavId(model.site_id);
            if (parent_id == 0)
            {
                return 0;
            }

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open(); //打开数据连接
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 写入频道表数据==================
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();//数据字段
                        StringBuilder str2 = new StringBuilder();//数据参数
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("insert into " + databaseprefix + "site_channel(");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键和List<T>类型则追加sql字符串
                            if (!pi.Name.Equals("id") && !typeof(System.Collections.IList).IsAssignableFrom(pi.PropertyType))
                            {
                                //判断属性值是否为空
                                if (pi.GetValue(model, null) != null)
                                {
                                    str1.Append(pi.Name + ",");//拼接字段
                                    str2.Append("@" + pi.Name + ",");//声明参数
                                    paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(model, null)));//对参数赋值
                                }
                            }
                        }
                        strSql.Append(str1.ToString().Trim(','));
                        strSql.Append(") values (");
                        strSql.Append(str2.ToString().Trim(','));
                        strSql.Append(") ");
                        strSql.Append(";select @@IDENTITY;");
                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), paras.ToArray());//带事务
                        model.id = Convert.ToInt32(obj);
                        #endregion

                        //写入扩展字段及创建频道数据表
                        FieldAdd(conn, trans, model);

                        #region 写入导航数据===============
                        int newNavId = new DAL.navigation(databaseprefix).Add(conn, trans, parent_id, "channel_" + model.name, model.title, "", model.sort_id, model.id, "Show");
                        new DAL.navigation(databaseprefix).Add(conn, trans, newNavId, "channel_" + model.name + "_list", "内容管理", "article/article_list.aspx", 99, model.id, "Show,View,Add,Edit,Delete,Audit");
                        new DAL.navigation(databaseprefix).Add(conn, trans, newNavId, "channel_" + model.name + "_category", "栏目类别", "article/category_list.aspx", 100, model.id, "Show,View,Add,Edit,Delete");
                        //开启评论则新增菜单
                        if (model.is_comment > 0)
                        {
                            new DAL.navigation(databaseprefix).Add(conn, trans, newNavId, "channel_" + model.name + "_comment", "评论管理", "article/comment_list.aspx", 103, model.id, "Show,View,Delete,Reply");
                        }
                        #endregion

                        trans.Commit();//提交事务
                    }
                    catch
                    {
                        trans.Rollback();//回滚事务
                        return 0;
                    }
                }
            }
            return model.id;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.site_channel model)
        {
            Model.site_channel oldModel = GetModel(model.id);//取得旧的频道数据
            int siteNavParentId = new DAL.sites(databaseprefix).GetSiteNavId(model.site_id);//取得站点对应的导航ID
            if (siteNavParentId == 0)
            {
                return false;
            }
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 修改频道表======================
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("update  " + databaseprefix + "site_channel set ");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键则追加sql字符串
                            //!pi.Name.Equals("channel_fields")
                            if (!pi.Name.Equals("id") && !typeof(System.Collections.IList).IsAssignableFrom(pi.PropertyType))
                            {
                                //判断属性值是否为空
                                if (pi.GetValue(model, null) != null)
                                {
                                    str1.Append(pi.Name + "=@" + pi.Name + ",");//声明参数
                                    paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(model, null)));//对参数赋值
                                }
                            }
                        }
                        strSql.Append(str1.ToString().Trim(','));
                        strSql.Append(" where id=@id ");
                        paras.Add(new SqlParameter("@id", model.id));
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), paras.ToArray());
                        #endregion

                        //删除已移除扩展字段及频道数据表列
                        FieldDelete(conn, trans, model, oldModel);
                        //编辑扩展字段及频道数据表
                        FieldUpdate(conn, trans, model, oldModel);

                        #region 编辑对应的导航==================
                        new DAL.navigation(databaseprefix).Update(conn, trans, "channel_" + oldModel.name, siteNavParentId, "channel_" + model.name, model.title, model.sort_id);
                        new DAL.navigation(databaseprefix).Update(conn, trans, "channel_" + oldModel.name + "_list", "channel_" + model.name + "_list"); //内容管理
                        new DAL.navigation(databaseprefix).Update(conn, trans, "channel_" + oldModel.name + "_category", "channel_" + model.name + "_category"); //栏目类别

                        //评论导航菜单处理
                        if (model.is_comment > 0 && oldModel.is_comment == 0)
                        {
                            //如开启评论而以前关闭的需新增菜单
                            new DAL.navigation(databaseprefix).Add(conn, trans, "channel_" + model.name, "channel_" + model.name + "_comment", "评论管理", "article/comment_list.aspx", 103, model.id, "Show,View,Add,Edit,Delete");
                        }
                        else if (model.is_comment == 0 && oldModel.is_comment > 0)
                        {
                            //如关闭评论而以前开启需删除菜单
                            new DAL.navigation(databaseprefix).Delete(conn, trans, "channel_" + oldModel.name + "_comment");
                        }
                        else if (model.is_comment > 0)
                        {
                            //如都是开启状态则修改菜单
                            new DAL.navigation(databaseprefix).Update(conn, trans, "channel_" + oldModel.name + "_comment", "channel_" + model.name + "_comment");
                        }
                        #endregion

                        trans.Commit();//提交事务
                    }
                    catch
                    {
                        trans.Rollback();//回滚事务
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            //取得频道的名称
            string channelName = GetChannelName(id);
            if (string.IsNullOrEmpty(channelName))
            {
                return false;
            }
            //取得要删除的所有导航ID
            string navIds = new navigation(databaseprefix).GetIds("channel_" + channelName);

            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //删除导航菜单表
                        if (!string.IsNullOrEmpty(navIds))
                        {
                            DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "navigation where id in(" + navIds + ")");
                        }

                        //删除扩展字段表
                        StringBuilder strSql1 = new StringBuilder();
                        strSql1.Append("delete from " + databaseprefix + "site_channel_field");
                        strSql1.Append(" where channel_id=@channel_id ");
                        SqlParameter[] parameters1 = {
					            new SqlParameter("@channel_id", SqlDbType.Int,4)};
                        parameters1[0].Value = id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql1.ToString(), parameters1);

                        //删除频道数据表
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("drop table " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channelName);
                        DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString());

                        //删除频道表
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("delete from  " + databaseprefix + "site_channel");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        
                        trans.Commit(); //提交事务
                    }
                    catch
                    {
                        trans.Rollback(); //回滚事务
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.site_channel GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.site_channel model = new Model.site_channel();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                //拼接字段，忽略List<T>
                if (!typeof(System.Collections.IList).IsAssignableFrom(p.PropertyType))
                {
                    str1.Append(p.Name + ",");
                }
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "site_channel");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 返回频道的ID和名称列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,[name] FROM  " + databaseprefix + "site_channel");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by sort_id asc,id desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM  " + databaseprefix + "site_channel");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "site_channel");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 查询是否存在该记录
        /// </summary>
        public bool Exists(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "site_channel");
            strSql.Append(" where name=@name ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50)};
            parameters[0].Value = name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.site_channel GetModel(string channel_name)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.site_channel model = new Model.site_channel();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                //拼接字段，忽略List<T>
                if (!typeof(System.Collections.IList).IsAssignableFrom(p.PropertyType))
                {
                    str1.Append(p.Name + ",");
                }
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "site_channel");
            strSql.Append(" where name=@channel_name ");
            SqlParameter[] parameters = {
					new SqlParameter("@channel_name", SqlDbType.VarChar,50)};
            parameters[0].Value = channel_name;

            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];
            if (dt.Rows.Count > 0)
            {
                return DataRowToModel(dt.Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "site_channel set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "site_channel");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 返回频道名称
        /// </summary>
        public string GetChannelName(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 name from " + databaseprefix + "site_channel");
            strSql.Append(" where id=" + id);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return Convert.ToString(obj);
            }
            return string.Empty;
        }

        /// <summary>
        /// 返回频道ID
        /// </summary>
        public int GetChannelId(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "site_channel");
            strSql.Append(" where name=@name ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50)};
            parameters[0].Value = name;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        /// <summary>
        /// 返回指定扩展字段频道列表
        /// </summary>
        public DataSet GetFieldList(SqlConnection conn, SqlTransaction trans, int field_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select C.id,C.name");
            strSql.Append(" FROM " + databaseprefix + "site_channel as C INNER JOIN " + databaseprefix + "site_channel_field as F");
            strSql.Append(" on F.channel_id=C.id and F.field_id=" + field_id);
            return DbHelperSQL.Query(conn, trans, strSql.ToString());
        }

        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.site_channel DataRowToModel(DataRow row)
        {
            Model.site_channel model = new Model.site_channel();
            if (row != null)
            {
                #region 主表信息======================
                //利用反射获得属性的所有公共属性
                Type modelType = model.GetType();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    PropertyInfo proInfo = modelType.GetProperty(row.Table.Columns[i].ColumnName);
                    if (proInfo != null && row[i] != DBNull.Value)
                    {
                        proInfo.SetValue(model, row[i], null);//用索引值设置属性值
                    }
                }
                #endregion

                #region 子表信息======================
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from " + databaseprefix + "site_channel_field");
                strSql.Append(" where channel_id=@channel_id");
                SqlParameter[] parameters = {
					    new SqlParameter("@channel_id", SqlDbType.Int,4)};
                parameters[0].Value = model.id;
                DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];

                if (dt.Rows.Count > 0)
                {
                    int rowsCount = dt.Rows.Count;
                    List<Model.site_channel_field> models = new List<Model.site_channel_field>();
                    Model.site_channel_field modelt;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        modelt = new Model.site_channel_field();
                        Type modeltType = modelt.GetType();
                        for (int i = 0; i < dt.Rows[n].Table.Columns.Count; i++)
                        {
                            PropertyInfo proInfo = modeltType.GetProperty(dt.Rows[n].Table.Columns[i].ColumnName);
                            if (proInfo != null && dt.Rows[n][i] != DBNull.Value)
                            {
                                proInfo.SetValue(modelt, dt.Rows[n][i], null);
                            }
                        }
                        models.Add(modelt);
                    }
                    model.channel_fields = models;
                }
                #endregion
            }
            return model;
        }

        #endregion

        #region 私有方法================================
        /// <summary>
        /// 新增扩展字段及创建频道数据表
        /// </summary>
        private void FieldAdd(SqlConnection conn, SqlTransaction trans, Model.site_channel model)
        {
            string fieldIds = string.Empty;//存储已加截的扩展字段ID集合
            //新增扩展字段表及存储字段的ID
            if (model.channel_fields != null)
            {
                StringBuilder strSql1;
                foreach (Model.site_channel_field modelt in model.channel_fields)
                {
                    fieldIds += modelt.field_id + ",";
                    strSql1 = new StringBuilder();
                    strSql1.Append("insert into " + databaseprefix + "site_channel_field(");
                    strSql1.Append("channel_id,field_id)");
                    strSql1.Append(" values (");
                    strSql1.Append("@channel_id,@field_id)");
                    SqlParameter[] parameters1 = {
					                    new SqlParameter("@channel_id", SqlDbType.Int,4),
					                    new SqlParameter("@field_id", SqlDbType.Int,4)};
                    parameters1[0].Value = model.id;
                    parameters1[1].Value = modelt.field_id;
                    DbHelperSQL.ExecuteSql(conn, trans, strSql1.ToString(), parameters1);
                }
            }
            //创建频道数据表
            StringBuilder strSql2 = new StringBuilder();//存储创建频道表SQL语句
            strSql2.Append("CREATE TABLE " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + model.name + "(\r\n");
            strSql2.Append("[id] int IDENTITY(1,1) PRIMARY KEY,\r\n");
            strSql2.Append("[site_id] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[channel_id] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[category_id] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[call_index] nvarchar(50),\r\n");
            strSql2.Append("[title] nvarchar(100),\r\n");
            strSql2.Append("[link_url] nvarchar(255),\r\n");
            strSql2.Append("[img_url] nvarchar(255),\r\n");
            strSql2.Append("[seo_title] nvarchar(255),\r\n");
            strSql2.Append("[seo_keywords] nvarchar(255),\r\n");
            strSql2.Append("[seo_description] nvarchar(255),\r\n");
            strSql2.Append("[tags] nvarchar(500),\r\n");
            strSql2.Append("[zhaiyao] nvarchar(255),\r\n");
            strSql2.Append("[content] ntext,\r\n");
            strSql2.Append("[sort_id] int NOT NULL DEFAULT ((99)),\r\n");
            strSql2.Append("[click] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[status] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[is_msg] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[is_top] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[is_red] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[is_hot] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[is_slide] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[is_sys] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[user_name] nvarchar(100),\r\n");
            strSql2.Append("[like_count] int NOT NULL DEFAULT ((0)),\r\n");
            strSql2.Append("[add_time] datetime NOT NULL DEFAULT (getdate()),\r\n");
            strSql2.Append("[update_time] datetime,\r\n");
            if (fieldIds.Length > 0)
            {
                //查询扩展字段表
                DataTable dt = new DAL.article_attribute_field(databaseprefix).GetList(0, "id in(" + fieldIds.Trim(',') + ")", "sort_id asc,id desc").Tables[0];
                //判断及组合创表SQL语句
                foreach (DataRow dr in dt.Rows)
                {
                    strSql2.Append("[" + dr["name"].ToString() + "] " + dr["data_type"].ToString() + ",\r\n");
                }
            }
            //执行SQL创表语句
            DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString().TrimEnd(',') + ")");
        }

        /// <summary>
        /// 编辑扩展字段及频道数据表
        /// </summary>
        private void FieldUpdate(SqlConnection conn, SqlTransaction trans, Model.site_channel newModel, Model.site_channel oldModel)
        {
            if (newModel.channel_fields != null)
            {
                string newFieldIds = string.Empty; //用来存储新增的字段ID
                //添加扩展字段
                StringBuilder strSql1;
                foreach (Model.site_channel_field modelt in newModel.channel_fields)
                {
                    strSql1 = new StringBuilder();
                    Model.site_channel_field fieldModel = null;
                    if (oldModel.channel_fields != null)
                    {
                        fieldModel = oldModel.channel_fields.Find(p => p.field_id == modelt.field_id); //查找是否已经存在
                    }
                    if (fieldModel == null) //如果不存在则添加
                    {
                        newFieldIds += modelt.field_id + ","; //以逗号分隔开存储
                        strSql1.Append("insert into " + databaseprefix + "site_channel_field(");
                        strSql1.Append("channel_id,field_id)");
                        strSql1.Append(" values (");
                        strSql1.Append("@channel_id,@field_id)");
                        SqlParameter[] parameters1 = {
					                        new SqlParameter("@channel_id", SqlDbType.Int,4),
					                        new SqlParameter("@field_id", SqlDbType.Int,4)};
                        parameters1[0].Value = modelt.channel_id;
                        parameters1[1].Value = modelt.field_id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql1.ToString(), parameters1);
                    }
                }
                //添加频道数据表列
                if (newFieldIds.Length > 0)
                {
                    StringBuilder strSql2 = new StringBuilder();
                    strSql2.Append("select id,[name],data_type from " + databaseprefix + "article_attribute_field");
                    strSql2.Append(" where id in(" + newFieldIds.TrimEnd(',') + ")");
                    DataSet ds = DbHelperSQL.Query(conn, trans, strSql2.ToString());
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DbHelperSQL.ExecuteSql(conn, trans, "alter table " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + oldModel.name + " add " + dr["name"].ToString() + " " + dr["data_type"].ToString());
                    }
                }

            }
            //如果频道名称改变则需要更改数据表名
            if (newModel.name != oldModel.name)
            {
                DbHelperSQL.ExecuteSql(conn, trans, "exec sp_rename '" + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + oldModel.name + "', '" + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + newModel.name + "'");
            }
        }

        /// <summary>
        /// 删除已移除的扩展字段及频道数据表列
        /// </summary>
        private void FieldDelete(SqlConnection conn, SqlTransaction trans, Model.site_channel newModel, Model.site_channel oldModel)
        {
            if (oldModel.channel_fields == null)
            {
                return;
            }
            string fieldIds = string.Empty;
            foreach (Model.site_channel_field modelt in oldModel.channel_fields)
            {
                //查找对应的字段ID，不在旧实体则删除
                if (newModel.channel_fields.Find(p => p.field_id == modelt.field_id) == null)
                {
                    //记住要删除的字段ID
                    fieldIds += modelt.field_id + ",";
                    //删除该旧字段
                    DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "site_channel_field where channel_id=" + newModel.id + " and field_id=" + modelt.field_id);
                }
            }
            //删除频道数据表列
            if (fieldIds.Length > 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select id,[name] from " + databaseprefix + "article_attribute_field");
                strSql.Append(" where id in(" + fieldIds.TrimEnd(',') + ")");
                DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //删除频道数据表列
                    DbHelperSQL.ExecuteSql(conn, trans, "alter table " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + oldModel.name + " drop column " + dr["name"].ToString());
                }
            }
        }

        #endregion
    }
}