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
    /// 数据访问类:附件表
    /// </summary>
    public partial class article_attach
    {
        private string databaseprefix;//数据库表名前缀
        public article_attach(string _databaseprefix)
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
            strSql.Append("select count(1) from  " + databaseprefix + "article_attach");
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
        public void Add(SqlConnection conn, SqlTransaction trans, List<Model.article_attach> models, int channel_id, int article_id)
        {
            if (models != null)
            {
                StringBuilder strSql;
                StringBuilder str1; ;//数据字段
                StringBuilder str2;//数据参数
                foreach (Model.article_attach modelt in models)
                {
                    strSql = new StringBuilder();
                    str1 = new StringBuilder();
                    str2 = new StringBuilder();
                    //利用反射获得属性的所有公共属性
                    PropertyInfo[] pros = modelt.GetType().GetProperties();
                    List<SqlParameter> paras = new List<SqlParameter>();
                    strSql.Append("insert into " + databaseprefix + "article_attach(");
                    foreach (PropertyInfo pi in pros)
                    {
                        //如果不是主键则追加sql字符串
                        if (!pi.Name.Equals("id"))
                        {
                            //判断属性值是否为空
                            if (pi.GetValue(modelt, null) != null)
                            {
                                str1.Append(pi.Name + ",");//拼接字段
                                str2.Append("@" + pi.Name + ",");//声明参数
                                switch (pi.Name)
                                {
                                    case "channel_id":
                                        paras.Add(new SqlParameter("@" + pi.Name, channel_id));
                                        break;
                                    case "article_id":
                                        paras.Add(new SqlParameter("@" + pi.Name, article_id));//刚插入的文章ID
                                        break;
                                    default:
                                        paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(modelt, null)));//对参数赋值
                                        break;
                                }
                            }
                        }
                    }
                    strSql.Append(str1.ToString().Trim(','));
                    strSql.Append(") values (");
                    strSql.Append(str2.ToString().Trim(','));
                    strSql.Append(") ");
                    DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), paras.ToArray());//带事务
                }
            }
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(SqlConnection conn, SqlTransaction trans, List<Model.article_attach> models, int channel_id, int article_id)
        {
            //删除已移除的附件
            DeleteList(conn, trans, models, channel_id, article_id);
            //添加/修改附件
            if (models != null)
            {
                StringBuilder strSql;
                StringBuilder str1;//数据字段
                StringBuilder str2;//数据参数
                foreach (Model.article_attach modelt in models)
                {
                    strSql = new StringBuilder();
                    str1 = new StringBuilder();
                    str2 = new StringBuilder();
                    //利用反射获得属性的所有公共属性
                    PropertyInfo[] pros = modelt.GetType().GetProperties();
                    List<SqlParameter> paras = new List<SqlParameter>();
                    if (modelt.id > 0)
                    {
                        strSql.Append("update " + databaseprefix + "article_attach set ");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键则追加sql字符串
                            if (!pi.Name.Equals("id"))
                            {
                                //判断属性值是否为空
                                if (pi.GetValue(modelt, null) != null)
                                {
                                    str1.Append(pi.Name + "=@" + pi.Name + ",");//声明参数
                                    paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(modelt, null)));//对参数赋值
                                }
                            }
                        }
                        strSql.Append(str1.ToString().Trim(','));
                        strSql.Append(" where id=@id");
                        paras.Add(new SqlParameter("@id", modelt.id));
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), paras.ToArray());
                    }
                    else
                    {
                        strSql.Append("insert into " + databaseprefix + "article_attach(");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键则追加sql字符串
                            if (!pi.Name.Equals("id"))
                            {
                                //判断属性值是否为空
                                if (pi.GetValue(modelt, null) != null)
                                {
                                    str1.Append(pi.Name + ",");//拼接字段
                                    str2.Append("@" + pi.Name + ",");//声明参数
                                    paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(modelt, null)));//对参数赋值
                                }
                            }
                        }
                        strSql.Append(str1.ToString().Trim(','));
                        strSql.Append(") values (");
                        strSql.Append(str2.ToString().Trim(','));
                        strSql.Append(") ");
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), paras.ToArray());//带事务
                    }
                }
            }
        }

        /// <summary>
        /// 查找不存在的文件并删除已移除的附件及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.article_attach> models, int channel_id, int article_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.article_attach modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string delIds = idList.ToString().TrimEnd(',');
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,file_path from " + databaseprefix + "article_attach where channel_id=" + channel_id + " and article_id=" + article_id);
            if (!string.IsNullOrEmpty(delIds))
            {
                strSql.Append(" and id not in(" + delIds + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int rows = DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "article_attach where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    FileHelper.DeleteFile(dr["file_path"].ToString()); //删除文件
                }
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_attach GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.article_attach model = new Model.article_attach();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "article_attach");
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
        public List<Model.article_attach> GetList(int channel_id, int article_id)
        {
            List<Model.article_attach> modelList = new List<Model.article_attach>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "article_attach ");
            strSql.Append(" where channel_id=" + channel_id + " and article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            if (dt.Rows.Count > 0)
            {
                for (int n = 0; n < dt.Rows.Count; n++)
                {
                    modelList.Add(DataRowToModel(dt.Rows[n]));
                }
            }
            return modelList;
        }
        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 检查用户是否下载过该附件
        /// </summary>
        public bool ExistsLog(int attach_id, int user_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_attach_log");
            strSql.Append(" where attach_id=@attach_id and user_id=@user_id");
            SqlParameter[] parameters = {
					new SqlParameter("@attach_id", SqlDbType.Int,4),
                    new SqlParameter("@user_id", SqlDbType.Int,4)};
            parameters[0].Value = attach_id;
            parameters[1].Value = user_id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 插入一条下载附件记录
        /// </summary>
        public int AddLog(Model.user_attach_log model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();//数据字段
            StringBuilder str2 = new StringBuilder();//数据参数
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();
            strSql.Append("insert into  " + databaseprefix + "user_attach_log(");
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
        /// 获取单个附件下载次数
        /// </summary>
        public int GetDownNum(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 down_num from " + databaseprefix + "article_attach");
            strSql.Append(" where id=" + id);
            string str = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 获取总下载次数
        /// </summary>
        public int GetCountNum(int channel_id, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sum(down_num) from " + databaseprefix + "article_attach");
            strSql.Append(" where channel_id=" + channel_id + " and article_id=" + article_id);
            string str = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_attach set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 删除附件文件
        /// </summary>
        public void DeleteFile(List<Model.article_attach> models)
        {
            if (models != null)
            {
                foreach (Model.article_attach modelt in models)
                {
                    FileHelper.DeleteFile(modelt.file_path);
                }
            }
        }

        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.article_attach DataRowToModel(DataRow row)
        {
            Model.article_attach model = new Model.article_attach();
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