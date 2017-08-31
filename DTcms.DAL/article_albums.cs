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
    /// 数据访问类:图片相册
    /// </summary>
    public partial class article_albums
    {
        private string databaseprefix;//数据库表名前缀
        public article_albums(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(SqlConnection conn, SqlTransaction trans, List<Model.article_albums> models, int channel_id, int article_id)
        {
            if (models != null)
            {
                StringBuilder strSql;
                StringBuilder str1; ;//数据字段
                StringBuilder str2;//数据参数
                foreach (Model.article_albums modelt in models)
                {
                    strSql = new StringBuilder();
                    str1 = new StringBuilder();
                    str2 = new StringBuilder();
                    //利用反射获得属性的所有公共属性
                    PropertyInfo[] pros = modelt.GetType().GetProperties();
                    List<SqlParameter> paras = new List<SqlParameter>();
                    strSql.Append("insert into " + databaseprefix + "article_albums(");
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
        public void Update(SqlConnection conn, SqlTransaction trans, List<Model.article_albums> models, int channel_id, int article_id)
        {
            //删除已移除的图片
            DeleteList(conn, trans, models, channel_id, article_id);
            //添加/修改相册
            if (models != null)
            {
                StringBuilder strSql;
                StringBuilder str1;//数据字段
                StringBuilder str2;//数据参数
                foreach (Model.article_albums modelt in models)
                {
                    strSql = new StringBuilder();
                    str1 = new StringBuilder();
                    str2 = new StringBuilder();
                    //利用反射获得属性的所有公共属性
                    PropertyInfo[] pros = modelt.GetType().GetProperties();
                    List<SqlParameter> paras = new List<SqlParameter>();
                    if (modelt.id > 0)
                    {
                        strSql.Append("update " + databaseprefix + "article_albums set ");
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
                        strSql.Append("insert into " + databaseprefix + "article_albums(");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键则追加sql字符串
                            if (!pi.Name.Equals("id"))
                            {
                                //判断属性值是否为空
                                if (pi.GetValue(modelt, null) != null && !pi.GetValue(modelt, null).ToString().Equals(""))
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
        /// 获得数据列表
        /// </summary>
        public List<Model.article_albums> GetList(int channel_id, int article_id)
        {
            List<Model.article_albums> modelList = new List<Model.article_albums>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "article_albums ");
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

        /// <summary>
        /// 查找不存在的图片并删除已移除的图片及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.article_albums> models, int channel_id, int article_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string delIds = idList.ToString().TrimEnd(',');
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,thumb_path,original_path from " + databaseprefix + "article_albums where channel_id=" + channel_id + " and article_id=" + article_id);
            if (!string.IsNullOrEmpty(delIds))
            {
                strSql.Append(" and id not in(" + delIds + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int rows = DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "article_albums where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    FileHelper.DeleteFile(dr["thumb_path"].ToString()); //删除缩略图
                    FileHelper.DeleteFile(dr["original_path"].ToString()); //删除原图
                }
            }
        }

        /// <summary>
        /// 删除相册图片
        /// </summary>
        public void DeleteFile(List<Model.article_albums> models)
        {
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    FileHelper.DeleteFile(modelt.thumb_path);
                    FileHelper.DeleteFile(modelt.original_path);
                }
            }
        }

        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.article_albums DataRowToModel(DataRow row)
        {
            Model.article_albums model = new Model.article_albums();
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