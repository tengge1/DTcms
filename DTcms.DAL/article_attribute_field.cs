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
    /// 数据访问类:扩展字段表
    /// </summary>
    public partial class article_attribute_field
    {
        private string databaseprefix;//数据库表名前缀
        public article_attribute_field(string _databaseprefix)
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
            strSql.Append("select count(1) from  " + databaseprefix + "article_attribute_field");
            strSql.Append(" where id = @id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_attribute_field model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();//数据字段
            StringBuilder str2 = new StringBuilder();//数据参数
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();
            strSql.Append("insert into  " + databaseprefix + "article_attribute_field(");
            foreach (PropertyInfo pi in pros)
            {
                //如果不是主键则追加sql字符串
                if (!pi.Name.Equals("id"))
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
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), paras.ToArray());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);

            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_attribute_field model)
        {
            Model.article_attribute_field oldModel = GetModel(model.id);//取到旧的数据
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //修改主表信息
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("update  " + databaseprefix + "article_attribute_field set ");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键则追加sql字符串
                            if (!pi.Name.Equals("id"))
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

                        //检查字段名和类型有无变化
                        if (oldModel.name.ToLower() != model.name.ToLower() || oldModel.data_type.ToLower() != model.data_type.ToLower())
                        {
                            DataTable dt = new DAL.site_channel(databaseprefix).GetFieldList(conn, trans, model.id).Tables[0];
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    //检查有无该频道数据表和列

                                    int rowsCount = Convert.ToInt32(DbHelperSQL.GetSingle(conn, trans, "select count(1) from syscolumns where id=object_id('" + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + dr["name"].ToString() + "') and name='" + oldModel.name + "'"));
                                    if (rowsCount > 0)
                                    {
                                        //修改列数据类型
                                        if (oldModel.data_type.ToLower() != model.data_type.ToLower())
                                        {
                                            DbHelperSQL.ExecuteSql(conn, trans, "alter table " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + dr["name"].ToString() + " alter column " + oldModel.name + " " + model.data_type);
                                        }
                                        //修改列名
                                        if (oldModel.name.ToLower() != model.name.ToLower())
                                        {
                                            DbHelperSQL.ExecuteSql(conn, trans, "exec sp_rename '" + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + dr["name"].ToString() + "." + oldModel.name + "','" + model.name + "','column'");
                                        }
                                    }
                                }
                            }
                        }

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
            Model.article_attribute_field model = GetModel(id);//取得扩展字段实体
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //删除所关联的频道数据表相关列
                        DataTable dt = new DAL.site_channel(databaseprefix).GetFieldList(conn, trans, id).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                //检查有无该频道数据表和列
                                int rowsCount = Convert.ToInt32(DbHelperSQL.GetSingle(conn, trans, "select count(1) from syscolumns where id=object_id('" + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + dr["name"].ToString() + "') and name='" + model.name + "'"));
                                if (rowsCount > 0)
                                {
                                    //删除频道数据表一列
                                    DbHelperSQL.ExecuteSql(conn, trans, "alter table " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + dr["name"].ToString() + " drop column " + model.name);
                                }
                            }
                        }

                        //删除频道关联字段表
                        StringBuilder strSql1 = new StringBuilder();
                        strSql1.Append("delete from " + databaseprefix + "site_channel_field");
                        strSql1.Append(" where field_id=@field_id");
                        SqlParameter[] parameters1 = {
					            new SqlParameter("@field_id", SqlDbType.Int,4)};
                        parameters1[0].Value = id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql1.ToString(), parameters1);

                        //删除扩展字段主表
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("delete from " + databaseprefix + "article_attribute_field");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

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
        /// 得到一个对象实体
        /// </summary>
        public Model.article_attribute_field GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.article_attribute_field model = new Model.article_attribute_field();
            //利用反射获得属性的所有公共属性
            Type modelType = model.GetType();
            PropertyInfo[] pros = modelType.GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "article_attribute_field");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            DataTable dt = DbHelperSQL.Query(strSql.ToString(), parameters).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows[0].Table.Columns.Count; i++)
                {
                    //查找实体是否存在列表相同的公共属性
                    PropertyInfo proInfo = modelType.GetProperty(dt.Rows[0].Table.Columns[i].ColumnName);
                    if (proInfo != null && dt.Rows[0][i] != DBNull.Value)
                    {
                        proInfo.SetValue(model, dt.Rows[0][i], null);//用索引值设置属性值
                    }
                }
                return model;
            }
            else
            {
                return null;
            }
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
            strSql.Append(" FROM  " + databaseprefix + "article_attribute_field");
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
            strSql.Append("select * FROM " + databaseprefix + "article_attribute_field");
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string column_name)
        {
            //检查是否与文章字段相同
            Model.article artModel = new Model.article();
            //利用反射获得属性的所有公共属性
            Type modelType = artModel.GetType();
            PropertyInfo[] proInfo = modelType.GetProperties();
            foreach (PropertyInfo pi in proInfo)
            {
                if (pi.Name.ToLower() == column_name.ToLower())
                {
                    return true;
                }
            }
            //检查是否与扩展字段表列相同
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from  " + databaseprefix + "article_attribute_field");
            strSql.Append(" where name=@name ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,100)};
            parameters[0].Value = column_name;
            if (DbHelperSQL.Exists(strSql.ToString(), parameters))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_attribute_field set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 获得频道对应的数据
        /// </summary>
        public DataSet GetList(int channel_id, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.* ");
            strSql.Append(" FROM " + databaseprefix + "article_attribute_field as A INNER JOIN " + databaseprefix + "site_channel_field as S ON A.id=S.field_id");
            strSql.Append(" where S.channel_id=" + channel_id);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by A.is_sys desc,A.sort_id asc,A.id desc");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取频道的扩展字段
        /// </summary>
        public Dictionary<string, string> GetFields(int channel_id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            DataTable dt = GetList(channel_id, string.Empty).Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    dic.Add(dr["name"].ToString(), string.Empty);
                }
            }
            return dic;
        }
        #endregion
    }
}