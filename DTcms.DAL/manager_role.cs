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
    /// 数据访问类:管理角色表
    /// </summary>
    public partial class manager_role
    {
        private string databaseprefix; //数据库表名前缀
        public manager_role(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "manager_role");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager_role model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();//打开数据连接
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 主表信息==========================
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();//数据字段
                        StringBuilder str2 = new StringBuilder();//数据参数
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("insert into " + databaseprefix + "manager_role(");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键和LIST<T>则追加sql字符串
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

                        #region 角色权限表信息====================
                        if (model.manager_role_values != null)
                        {
                            StringBuilder strSql2; //SQL字符串
                            StringBuilder str21; //数据库字段
                            StringBuilder str22; //声明参数
                            foreach (Model.manager_role_value modelt in model.manager_role_values)
                            {
                                strSql2 = new StringBuilder();
                                str21 = new StringBuilder();
                                str22 = new StringBuilder();
                                PropertyInfo[] pros2 = modelt.GetType().GetProperties();
                                List<SqlParameter> paras2 = new List<SqlParameter>();
                                strSql2.Append("insert into " + databaseprefix + "manager_role_value(");
                                foreach (PropertyInfo pi in pros2)
                                {
                                    if (!pi.Name.Equals("id"))
                                    {
                                        if (pi.GetValue(modelt, null) != null)
                                        {
                                            str21.Append(pi.Name + ",");
                                            str22.Append("@" + pi.Name + ",");
                                            if (pi.Name.Equals("role_id"))
                                            {
                                                paras2.Add(new SqlParameter("@" + pi.Name, model.id));//将刚插入的父ID赋值
                                            }
                                            else
                                            {
                                                paras2.Add(new SqlParameter("@" + pi.Name, pi.GetValue(modelt, null)));
                                            }
                                        }
                                    }
                                }
                                strSql2.Append(str21.ToString().Trim(','));
                                strSql2.Append(") values (");
                                strSql2.Append(str22.ToString().Trim(','));
                                strSql2.Append(") ");
                                DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), paras2.ToArray());
                            }
                        }
                        #endregion

                        trans.Commit(); //提交事务
                    }
                    catch
                    {
                        trans.Rollback(); //回滚事务
                        return 0;
                    }
                }
            }
            return model.id;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.manager_role model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 主表信息==========================
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("update " + databaseprefix + "manager_role set ");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键和LIST<T>则追加sql字符串
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

                        #region 角色权限表信息====================
                        //删除角色所有的权限
                        StringBuilder strSql1 = new StringBuilder();
                        strSql1.Append("delete from " + databaseprefix + "manager_role_value where role_id=@role_id");
                        SqlParameter[] parameters1 = {
                                new SqlParameter("@role_id", SqlDbType.Int,4)};
                        parameters1[0].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql1.ToString(), parameters1);
                        //重新添加角色权限
                        if (model.manager_role_values != null)
                        {
                            StringBuilder strSql2; //SQL字符串
                            StringBuilder str21; //数据库字段
                            StringBuilder str22; //声明参数
                            foreach (Model.manager_role_value modelt in model.manager_role_values)
                            {
                                strSql2 = new StringBuilder();
                                str21 = new StringBuilder();
                                str22 = new StringBuilder();
                                PropertyInfo[] pros2 = modelt.GetType().GetProperties();
                                List<SqlParameter> paras2 = new List<SqlParameter>();
                                strSql2.Append("insert into " + databaseprefix + "manager_role_value(");
                                foreach (PropertyInfo pi in pros2)
                                {
                                    if (!pi.Name.Equals("id"))
                                    {
                                        if (pi.GetValue(modelt, null) != null)
                                        {
                                            str21.Append(pi.Name + ",");
                                            str22.Append("@" + pi.Name + ",");
                                            if (pi.Name.Equals("role_id"))
                                            {
                                                paras2.Add(new SqlParameter("@" + pi.Name, model.id));//将刚角色的ID赋值
                                            }
                                            else
                                            {
                                                paras2.Add(new SqlParameter("@" + pi.Name, pi.GetValue(modelt, null)));
                                            }
                                        }
                                    }
                                }
                                strSql2.Append(str21.ToString().Trim(','));
                                strSql2.Append(") values (");
                                strSql2.Append(str22.ToString().Trim(','));
                                strSql2.Append(") ");
                                DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), paras2.ToArray());
                            }
                        }
                        #endregion

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            //删除管理员
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "manager where role_id=@role_id");
            SqlParameter[] parameters2 = {
					new SqlParameter("@role_id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除管理角色权限
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "manager_role_value where role_id=@role_id");
            SqlParameter[] parameters1 = {
					new SqlParameter("@role_id", SqlDbType.Int,4)};
            parameters1[0].Value = id;
            cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);

            //删除管理角色
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "manager_role where id=@id");
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
        public Model.manager_role GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.manager_role model = new Model.manager_role();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            foreach (PropertyInfo p in pros)
            {
                //拼接字段，忽略List<T>
                if (!typeof(System.Collections.IList).IsAssignableFrom(p.PropertyType))
                {
                    str1.Append(p.Name + ",");//拼接字段
                }
            }
            strSql.Append("select top 1 " + str1.ToString().Trim(','));
            strSql.Append(" from " + databaseprefix + "manager_role");
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
            strSql.Append("select * FROM  " + databaseprefix + "manager_role");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 返回角色名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 role_name from " + databaseprefix + "manager_role");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            return title;
        }

        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.manager_role DataRowToModel(DataRow row)
        {
            Model.manager_role model = new Model.manager_role();
            if (row != null)
            {
                #region 主表信息======================
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
                #endregion

                #region 子表信息======================
                StringBuilder strSql1 = new StringBuilder();
                strSql1.Append("select * from " + databaseprefix + "manager_role_value");
                strSql1.Append(" where role_id=@role_id");
                SqlParameter[] parameters1 = {
                        new SqlParameter("@role_id", SqlDbType.Int,4)};
                parameters1[0].Value = model.id;

                DataTable dt1 = DbHelperSQL.Query(strSql1.ToString(), parameters1).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    int rowsCount = dt1.Rows.Count;
                    List<Model.manager_role_value> models = new List<Model.manager_role_value>();
                    Model.manager_role_value modelt;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        modelt = new Model.manager_role_value();
                        Type modeltType = modelt.GetType();
                        for (int i = 0; i < dt1.Rows[n].Table.Columns.Count; i++)
                        {
                            PropertyInfo proInfo = modeltType.GetProperty(dt1.Rows[n].Table.Columns[i].ColumnName);
                            if (proInfo != null && dt1.Rows[n][i] != DBNull.Value)
                            {
                                proInfo.SetValue(modelt, dt1.Rows[n][i], null);
                            }
                        }
                        models.Add(modelt);
                    }
                    model.manager_role_values = models;
                }
                #endregion
            }
            return model;
        }
        #endregion
    }
}