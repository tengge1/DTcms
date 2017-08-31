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
    /// 数据访问类:站点管理
    /// </summary>
    public partial class sites
    {
        private string databaseprefix;//数据库表名前缀
        public sites(string _databaseprefix)
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
            strSql.Append("select count(1) from  " + databaseprefix + "sites");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.sites model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();//数据字段
                        StringBuilder str2 = new StringBuilder();//数据参数
                        
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("insert into " + databaseprefix + "sites(");
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
                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), paras.ToArray());//带事务
                        model.id = Convert.ToInt32(obj);
                        //添加站点导航菜单
                        new DAL.navigation(databaseprefix).Add(conn, trans, "sys_contents", "channel_" + model.build_path, model.title, "", model.sort_id, 0, "Show");
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
        public bool Update(Model.sites model, string old_build_path)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("update  " + databaseprefix + "sites set ");
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
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(),paras.ToArray());

                        //检查旧导航是否存在
                        if (new DAL.navigation(databaseprefix).GetModel(conn, trans, "channel_" + old_build_path) != null)
                        {
                            //修改导航菜单
                            new DAL.navigation(databaseprefix).Update(conn, trans, "channel_" + old_build_path, "channel_" + model.build_path, model.title, model.sort_id);
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
            string build_path = GetBuildPath(id);
            if (string.IsNullOrEmpty(build_path))
            {
                return false;
            }
            //取得要删除的所有导航ID
            string navIds = new navigation(databaseprefix).GetIds("channel_" + build_path);
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //删除导航菜单
                        if (!string.IsNullOrEmpty(navIds))
                        {
                            DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "navigation where id in(" + navIds + ")");
                        }
                        //删除站点
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("delete from  " + databaseprefix + "sites ");
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
        public Model.sites GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.sites model = new Model.sites();
            //利用反射获得属性的所有公共属性
            Type modelType = model.GetType();
            PropertyInfo[] pros = modelType.GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "sites");
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
            strSql.Append(" FROM " + databaseprefix + "sites ");
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
            strSql.Append("select * FROM " + databaseprefix + "sites");
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
        /// 查询生成目录名是否存在
        /// </summary>
        public bool Exists(string build_path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "sites");
            strSql.Append(" where build_path=@build_path ");
            SqlParameter[] parameters = {
					new SqlParameter("@build_path", SqlDbType.NVarChar,100)};
            parameters[0].Value = build_path;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回站点名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "sites");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            return title;
        }

        /// <summary>
        /// 返回站点名称
        /// </summary>
        public string GetTitle(string build_path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "sites");
            strSql.Append(" where build_path=@build_path");
            SqlParameter[] parameters = {
                    new SqlParameter("@build_path", SqlDbType.NVarChar,100)};
            parameters[0].Value = build_path;
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            return title;
        }

        /// <summary>
        /// 返回站点的生成目录名
        /// </summary>
        public string GetBuildPath(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 build_path from " + databaseprefix + "sites");
            strSql.Append(" where id=" + id);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return Convert.ToString(obj);
            }
            return string.Empty;
        }

        /// <summary>
        /// 返回站点对应的导航ID
        /// </summary>
        public int GetSiteNavId(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select N.id from " + databaseprefix + "navigation as N," + databaseprefix + "sites as S");
            strSql.Append(" where 'channel_'+S.build_path=N.name and S.id=" + id);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "sites set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string build_path, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "sites set " + strValue);
            strSql.Append(" where build_path=@build_path");
            SqlParameter[] parameters = {
                    new SqlParameter("@build_path", SqlDbType.NVarChar,100)};
            parameters[0].Value = build_path;
            return DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.sites GetModel(string build_path)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.sites model = new Model.sites();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                str1.Append(p.Name + ",");//拼接字段
            }
            strSql.Append("select " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "sites");
            strSql.Append(" where build_path=@build_path");
            SqlParameter[] parameters = {
					new SqlParameter("@build_path", SqlDbType.NVarChar,50)};
            parameters[0].Value = build_path;
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
        #endregion

        #region 扩展方法=============================
        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.sites DataRowToModel(DataRow row)
        {
            Model.sites model = new Model.sites();
            if (row != null)
            {
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
            }
            return model;
        }
        #endregion
    }
}