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
    /// 数据访问类:订单表
    /// </summary>
    public partial class orders
    {
        private string databaseprefix; //数据库表名前缀
        public orders(string _databaseprefix)
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
            strSql.Append("select count(1) from  " + databaseprefix + "orders");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.orders model)
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
                        strSql.Append("insert into " + databaseprefix + "orders(");
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键则追加sql字符串
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

                        #region 订单商品列表======================
                        if (model.order_goods != null)
                        {
                            StringBuilder strSql2; //SQL字符串
                            StringBuilder strSql3;
                            StringBuilder strSql4;
                            StringBuilder str21; //数据库字段
                            StringBuilder str22; //声明参数
                            foreach (Model.order_goods modelt in model.order_goods)
                            {
                                strSql2 = new StringBuilder();
                                str21 = new StringBuilder();
                                str22 = new StringBuilder();
                                PropertyInfo[] pros2 = modelt.GetType().GetProperties();
                                List<SqlParameter> paras2 = new List<SqlParameter>();
                                strSql2.Append("insert into " + databaseprefix + "order_goods(");
                                foreach (PropertyInfo pi in pros2)
                                {
                                    if (!pi.Name.Equals("id"))
                                    {
                                        if (pi.GetValue(modelt, null) != null)
                                        {
                                            str21.Append(pi.Name + ",");
                                            str22.Append("@" + pi.Name + ",");
                                            if (pi.Name.Equals("order_id"))
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

                                //扣减商品库存
                                string channelName = new DAL.site_channel(databaseprefix).GetChannelName(modelt.channel_id);//查询频道的名称
                                strSql4 = new StringBuilder();
                                strSql4.Append("update " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channelName + " set ");
                                strSql4.Append("stock_quantity=stock_quantity-@stock_quantity where id=@article_id");
                                SqlParameter[] parameters4 = {
                                        new SqlParameter("@stock_quantity", SqlDbType.Int,4),
                                        new SqlParameter("@article_id", SqlDbType.Int,4)};
                                parameters4[0].Value = modelt.quantity;
                                parameters4[1].Value = modelt.article_id;
                                DbHelperSQL.ExecuteSql(conn, trans, strSql4.ToString(), parameters4);

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
        public bool Update(Model.orders model)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            //利用反射获得属性的所有公共属性
            PropertyInfo[] pros = model.GetType().GetProperties();
            List<SqlParameter> paras = new List<SqlParameter>();
            strSql.Append("update " + databaseprefix + "orders set ");
            foreach (PropertyInfo pi in pros)
            {
                //如果不是主键则追加sql字符串
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
            strSql.Append(" where id=@id");
            paras.Add(new SqlParameter("@id", model.id));
            return DbHelperSQL.ExecuteSql(strSql.ToString(), paras.ToArray()) > 0;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            //删除订单商品
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "order_goods where order_id=@order_id");
            SqlParameter[] parameters2 = {
					new SqlParameter("@order_id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除订单主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "orders where id=@id");
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
        public Model.orders GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.orders model = new Model.orders();
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
            strSql.Append(" from " + databaseprefix + "orders");
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
            strSql.Append(" FROM  " + databaseprefix + "orders ");
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
            strSql.Append("select * FROM " + databaseprefix + "orders");
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
        public bool Exists(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "orders");
            strSql.Append(" where order_no=@order_no");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据订单号返回一个实体
        /// </summary>
        public Model.orders GetModel(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder str1 = new StringBuilder();
            Model.orders model = new Model.orders();
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
            strSql.Append("select top 1 " + str1.ToString().Trim(',') + " from " + databaseprefix + "orders");
            strSql.Append(" where order_no=@order_no");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据订单号获取支付方式ID
        /// </summary>
        public int GetPaymentId(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 payment_id from " + databaseprefix + "orders");
            strSql.Append(" where order_no=@order_no");
            SqlParameter[] parameters = {
                    new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + databaseprefix + "orders ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "orders set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string order_no, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "orders set " + strValue);
            strSql.Append(" where order_no='" + order_no + "'");
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }

        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.orders DataRowToModel(DataRow row)
        {
            Model.orders model = new Model.orders();
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
                strSql1.Append("select * from " + databaseprefix + "order_goods");
                strSql1.Append(" where order_id=@id");
                SqlParameter[] parameters1 = {
                        new SqlParameter("@id", SqlDbType.Int,4)};
                parameters1[0].Value = model.id;

                DataTable dt1 = DbHelperSQL.Query(strSql1.ToString(), parameters1).Tables[0];
                if (dt1.Rows.Count > 0)
                {
                    int rowsCount = dt1.Rows.Count;
                    List<Model.order_goods> models = new List<Model.order_goods>();
                    Model.order_goods modelt;
                    for (int n = 0; n < rowsCount; n++)
                    {
                        modelt = new Model.order_goods();
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
                    model.order_goods = models;
                }
                #endregion
            }
            return model;
        }
        #endregion
    }
}