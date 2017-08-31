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
    /// 数据访问类:会员组价格
    /// </summary>
    public partial class user_group_price
    {
        private string databaseprefix;//数据库表名前缀
        public user_group_price(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
        /// 增加一条数据，带事务
        /// </summary>
        public bool Add(SqlConnection conn, SqlTransaction trans, Model.user_group_price model, int channel_id, int article_id)
        {
            StringBuilder strSql = new StringBuilder(); //SQL字符串
            StringBuilder str1 = new StringBuilder(); //数据库字段
            StringBuilder str2 = new StringBuilder(); //声明参数
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();

            strSql.Append("insert into " + databaseprefix + "user_group_price(");
            foreach (PropertyInfo pi in pros)
            {
                if (!pi.Name.Equals("id"))
                {
                    if (pi.GetValue(model, null) != null)
                    {
                        str1.Append(pi.Name + ",");
                        str2.Append("@" + pi.Name + ",");
                        switch (pi.Name)
                        {
                            case "channel_id":
                                paras.Add(new SqlParameter("@" + pi.Name, channel_id));
                                break;
                            case "article_id":
                                paras.Add(new SqlParameter("@" + pi.Name, article_id));
                                break;
                            default:
                                paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(model, null)));
                                break;
                        }
                    }
                }
            }
            strSql.Append(str1.ToString().Trim(','));
            strSql.Append(") values (");
            strSql.Append(str2.ToString().Trim(','));
            strSql.Append(")");
            return DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), paras.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据，带事务
        /// </summary>
        public void Delete(SqlConnection conn, SqlTransaction trans, int channel_id, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_group_price");
            strSql.Append(" where channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters = {
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters[0].Value = channel_id;
            parameters[1].Value = article_id;
            DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_group_price GetModel(int group_id, int channel_id, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.user_group_price model = new Model.user_group_price();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "user_group_price");
            strSql.Append(" where group_id=@group_id and channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters = {
					new SqlParameter("@group_id", SqlDbType.Int,4),
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
                    new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters[0].Value = group_id;
            parameters[1].Value = channel_id;
            parameters[2].Value = article_id;
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
        public List<Model.user_group_price> GetList(int channel_id, int article_id)
        {
            List<Model.user_group_price> modelList = new List<Model.user_group_price>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "user_group_price ");
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
        /// 将对象转换实体
        /// </summary>
        public Model.user_group_price DataRowToModel(DataRow row)
        {
            Model.user_group_price model = new Model.user_group_price();
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