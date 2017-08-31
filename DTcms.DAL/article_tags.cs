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
    /// 数据访问类:TAG标签
    /// </summary>
    public partial class article_tags
    {
        private string databaseprefix;//数据库表名前缀
        public article_tags(string _databaseprefix)
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
            strSql.Append("select count(1) from  " + databaseprefix + "article_tags");
            strSql.Append(" where ");
            strSql.Append(" id = @id  ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_tags model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();//数据字段
            StringBuilder str2 = new StringBuilder();//数据参数
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();
            strSql.Append("insert into  " + databaseprefix + "article_tags(");
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
        public bool Update(Model.article_tags model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();
            strSql.Append("update  " + databaseprefix + "article_tags set ");
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
            return DbHelperSQL.ExecuteSql(strSql.ToString(), paras.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //删除Tag标签关系表
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "article_tags_relation");
            strSql1.Append(" where tag_id=@tag_id");
            SqlParameter[] parameters1 = {
					new SqlParameter("@tag_id", SqlDbType.Int,4)};
            parameters1[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_tags");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            return DbHelperSQL.ExecuteSqlTran(sqllist) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_tags GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.article_tags model = new Model.article_tags();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "article_tags");
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *,(select count(1) from " + databaseprefix + "article_tags_relation where tag_id=" + databaseprefix + "article_tags.id) as count");
            strSql.Append(" FROM " + databaseprefix + "article_tags");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" *,(select count(1) from " + databaseprefix + "article_tags_relation where tag_id=" + databaseprefix + "article_tags.id) as count");
            strSql.Append(" FROM  " + databaseprefix + "article_tags");
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
            strSql.Append("select *,(select count(0) from " + databaseprefix + "article_tags_relation where tag_id=" + databaseprefix + "article_tags.id) as count FROM " + databaseprefix + "article_tags");
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
        public bool Exists(string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_tags");
            strSql.Append(" where title=@title");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.VarChar,100)};
            parameters[0].Value = title;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_tags set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 检查更新Tags标签及关系，带事务
        /// </summary>
        public void Update(SqlConnection conn, SqlTransaction trans, string tags_title, int channel_id, int article_id)
        {
            int tagsId = 0;
            //检查该Tags标签是否已存在
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "article_tags");
            strSql.Append(" where title=@title");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100)};
            parameters[0].Value = tags_title;
            object obj1 = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters);
            if (obj1 != null)
            {
                //存在则将ID赋值
                tagsId = Convert.ToInt32(obj1);
            }
            //如果尚未创建该Tags标签则创建
            if (tagsId == 0)
            {
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("insert into " + databaseprefix + "article_tags(");
                strSql2.Append("title,is_red,sort_id,add_time)");
                strSql2.Append(" values (");
                strSql2.Append("@title,@is_red,@sort_id,@add_time)");
                strSql2.Append(";select @@IDENTITY");
                SqlParameter[] parameters2 = {
					    new SqlParameter("@title", SqlDbType.NVarChar,100),
					    new SqlParameter("@is_red", SqlDbType.TinyInt,1),
					    new SqlParameter("@sort_id", SqlDbType.Int,4),
					    new SqlParameter("@add_time", SqlDbType.DateTime)};
                parameters2[0].Value = tags_title;
                parameters2[1].Value = 0;
                parameters2[2].Value = 99;
                parameters2[3].Value = DateTime.Now;
                object obj2 = DbHelperSQL.GetSingle(conn, trans, strSql2.ToString(), parameters2);
                if (obj2 != null)
                {
                    //插入成功后返回ID
                    tagsId = Convert.ToInt32(obj2);
                }
            }
            //匹配Tags标签与文章之间的关系
            if (tagsId > 0)
            {
                StringBuilder strSql3 = new StringBuilder();
                strSql3.Append("insert into " + databaseprefix + "article_tags_relation(");
                strSql3.Append("channel_id,article_id,tag_id)");
                strSql3.Append(" values (");
                strSql3.Append("@channel_id,@article_id,@tag_id)");
                SqlParameter[] parameters3 = {
					    new SqlParameter("@channel_id", SqlDbType.Int,4),
                        new SqlParameter("@article_id", SqlDbType.Int,4),
					    new SqlParameter("@tag_id", SqlDbType.Int,4)};
                parameters3[0].Value = channel_id;
                parameters3[1].Value = article_id;
                parameters3[2].Value = tagsId;
                DbHelperSQL.GetSingle(conn, trans, strSql3.ToString(), parameters3);
            }
        }

        /// <summary>
        /// 删除文章对应的Tags标签关系
        /// </summary>
        public bool Delete(SqlConnection conn, SqlTransaction trans, int channel_id, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_tags_relation");
            strSql.Append(" where channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters = {
					new SqlParameter("@channel_id", SqlDbType.Int,4),
                    new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters[0].Value = channel_id;
            parameters[1].Value = article_id;
            return DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.article_tags DataRowToModel(DataRow row)
        {
            Model.article_tags model = new Model.article_tags();
            if (row != null)
            {
                //利用反射获得属性的所有公共属性
                Type modelType = model.GetType();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    //查找实体是否存在列表相同的公共属性
                    PropertyInfo proInfo = modelType.GetProperty(row.Table.Columns[i].ColumnName);
                    if (proInfo != null && row[i] != DBNull.Value)
                    {
                        proInfo.SetValue(model, row[i], null);//用索引值设置属性值
                    }
                }
            }
            return model;
        }
        #endregion
    }
}