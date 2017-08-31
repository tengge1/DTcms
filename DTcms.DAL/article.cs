using System;
using System.Data;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL
{
	/// <summary>
	/// 数据访问类:文章内容
	/// </summary>
	public partial class article
	{
        private string databaseprefix; //数据库表名前缀
        public article(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }
		#region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string channel_name, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where id=@article_id");
            SqlParameter[] parameters = {
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters[0].Value = article_id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string channel_name, string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where call_index=@call_index");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.article model)
        {
            //查询频道名称
            string channelName = new DAL.site_channel(databaseprefix).GetChannelName(model.channel_id);
            if (channelName.Length == 0)
            {
                return 0;
            }
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 添加主表数据====================
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();//数据字段
                        StringBuilder str2 = new StringBuilder();//数据参数
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("insert into " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channelName + "(");
                        //主表字段信息
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键或List<T>则追加sql字符串
                            if (!pi.Name.Equals("id") && !pi.Name.Equals("fields") && !typeof(System.Collections.IList).IsAssignableFrom(pi.PropertyType))
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
                        //扩展字段信息
                        foreach (KeyValuePair<string, string> kvp in model.fields)
                        {
                            str1.Append(kvp.Key + ",");//拼接字段
                            str2.Append("@" + kvp.Key + ",");//声明参数
                            paras.Add(new SqlParameter("@" + kvp.Key, kvp.Value));//对参数赋值
                        }
                        strSql.Append(str1.ToString().Trim(','));
                        strSql.Append(") values (");
                        strSql.Append(str2.ToString().Trim(','));
                        strSql.Append(") ");
                        strSql.Append(";select @@IDENTITY;");
                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), paras.ToArray());//带事务
                        model.id = Convert.ToInt32(obj);//插入后赋值
                        #endregion

                        #region 添加图片相册====================
                        if (model.albums != null)
                        {
                            new DAL.article_albums(databaseprefix).Add(conn, trans, model.albums, model.channel_id, model.id);
                        }
                        #endregion

                        #region 添加内容附件====================
                        if (model.attach != null)
                        {
                            new DAL.article_attach(databaseprefix).Add(conn, trans, model.attach, model.channel_id, model.id);
                        }
                        #endregion

                        #region 添加用户组价格==================
                        if (model.group_price != null)
                        {
                            foreach (Model.user_group_price modelt in model.group_price)
                            {
                                new DAL.user_group_price(databaseprefix).Add(conn, trans, modelt, model.channel_id, model.id);
                            }
                        }
                        #endregion

                        #region 添加Tags标签====================
                        if (model.tags != null && model.tags.Trim().Length > 0)
                        {
                            string[] tagsArr = model.tags.Trim().Split(',');
                            if (tagsArr.Length > 0)
                            {
                                foreach (string tagsStr in tagsArr)
                                {
                                    new DAL.article_tags(databaseprefix).Update(conn, trans, tagsStr, model.channel_id, model.id);
                                }
                            }
                        }
                        #endregion

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            return model.id;
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.article model)
		{
            //查询频道名称
            string channelName = new DAL.site_channel(databaseprefix).GetChannelName(model.channel_id);
            if (channelName.Length == 0)
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
                        #region 修改主表数据==========================
                        StringBuilder strSql = new StringBuilder();
                        StringBuilder str1 = new StringBuilder();
                        //利用反射获得属性的所有公共属性
                        PropertyInfo[] pros = model.GetType().GetProperties();
                        List<SqlParameter> paras = new List<SqlParameter>();
                        strSql.Append("update  " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channelName + " set ");
                        //主表字段信息
                        foreach (PropertyInfo pi in pros)
                        {
                            //如果不是主键或List<T>则追加sql字符串
                            if (!pi.Name.Equals("id") && !pi.Name.Equals("fields") && !typeof(System.Collections.IList).IsAssignableFrom(pi.PropertyType))
                            {
                                //判断属性值是否为空
                                if (pi.GetValue(model, null) != null)
                                {
                                    str1.Append(pi.Name + "=@" + pi.Name + ",");//声明参数
                                    paras.Add(new SqlParameter("@" + pi.Name, pi.GetValue(model, null)));//对参数赋值
                                }
                            }
                        }
                        //扩展字段信息
                        foreach (KeyValuePair<string, string> kvp in model.fields)
                        {
                            str1.Append(kvp.Key + "=@" + kvp.Key + ",");//声明参数
                            paras.Add(new SqlParameter("@" + kvp.Key, kvp.Value));//对参数赋值
                        }
                        strSql.Append(str1.ToString().Trim(','));
                        strSql.Append(" where id=@id");
                        paras.Add(new SqlParameter("@id", model.id));
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), paras.ToArray());
                        #endregion

                        #region 修改图片相册==========================
                        //删除/添加/修改相册图片
                        new DAL.article_albums(databaseprefix).Update(conn, trans, model.albums, model.channel_id, model.id);
                        #endregion

                        #region 修改内容附件==========================
                        //删除/添加/修改附件
                        new DAL.article_attach(databaseprefix).Update(conn, trans, model.attach, model.channel_id, model.id);
                        #endregion

                        #region 修改用户组价格========================
                        //删除旧用户组价格
                        new DAL.user_group_price(databaseprefix).Delete(conn, trans, model.channel_id, model.id);
                        //添加用户组价格
                        if (model.group_price != null)
                        {
                            foreach (Model.user_group_price modelt in model.group_price)
                            {
                                new DAL.user_group_price(databaseprefix).Add(conn, trans, modelt, model.channel_id, model.id);
                            }
                        }
                        #endregion

                        #region 修改Tags标签==========================
                        //删除已有的Tags标签关系
                        new DAL.article_tags(databaseprefix).Delete(conn, trans, model.channel_id, model.id);
                        //添加添加标签
                        if (model.tags != null && model.tags.Trim().Length > 0)
                        {
                            string[] tagsArr = model.tags.Trim().Split(',');
                            if (tagsArr.Length > 0)
                            {
                                foreach (string tagsStr in tagsArr)
                                {
                                    new DAL.article_tags(databaseprefix).Update(conn, trans, tagsStr, model.channel_id, model.id);
                                }
                            }
                        }
                        #endregion

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string channel_name, int channel_id, int article_id)
		{
            //取得相册MODEL
            List<Model.article_albums> albumsList = new DAL.article_albums(databaseprefix).GetList(channel_id, article_id);
            //取得附件MODEL
            List<Model.article_attach> attachList = new DAL.article_attach(databaseprefix).GetList(channel_id, article_id);

            //删除图片相册
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "article_albums");
            strSql2.Append(" where channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters2 = {
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters2[0].Value = channel_id;
            parameters2[1].Value = article_id;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除附件
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from " + databaseprefix + "article_attach");
            strSql3.Append(" where channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters3 = {
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters3[0].Value = channel_id;
            parameters3[1].Value = article_id;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //删除用户组价格
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from " + databaseprefix + "user_group_price");
            strSql4.Append(" where channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters4 = {
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
                    new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters4[0].Value = channel_id;
            parameters4[1].Value = article_id;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);

            //删除Tags标签关系
            StringBuilder strSql7 = new StringBuilder();
            strSql7.Append("delete from " + databaseprefix + "article_tags_relation");
            strSql7.Append(" where channel_id=@channel_id and article_id=@article_id");
            SqlParameter[] parameters7 = {
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters7[0].Value = channel_id;
            parameters7[1].Value = article_id;
            cmd = new CommandInfo(strSql7.ToString(), parameters7);
            sqllist.Add(cmd);

            //删除评论
            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append("delete from " + databaseprefix + "article_comment");
            strSql8.Append(" where channel_id=@channel_id and article_id=@article_id ");
            SqlParameter[] parameters8 = {
                    new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters8[0].Value = channel_id;
            parameters8[1].Value = article_id;
            cmd = new CommandInfo(strSql8.ToString(), parameters8);
            sqllist.Add(cmd);

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = article_id;
            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                new DAL.article_albums(databaseprefix).DeleteFile(albumsList); //删除图片
                new DAL.article_attach(databaseprefix).DeleteFile(attachList); //删除附件
                return true;
            }
            else
            {
                return false;
            }
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public Model.article GetModel(string channel_name, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = article_id;

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
        /// 得到一个对象实体
        /// </summary>
        public Model.article GetModel(string channel_name, string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 * from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where call_index=@call_index");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;

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
		/// 获得前几行数据
		/// </summary>
        public DataSet GetList(string channel_name, int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
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
        public DataSet GetList(string channel_name, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            if (category_id > 0)
            {
                strSql.Append(" where category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                if (category_id > 0)
                {
                    strSql.Append(" and " + strWhere);
                }
                else
                {
                    strSql.Append(" where " + strWhere);
                }
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
		#endregion

        #region 扩展方法================================
        /// <summary>
        /// 是否存在标题
        /// </summary>
        public bool ExistsTitle(string channel_name, string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where title=@title");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.VarChar,200)};
            parameters[0].Value = title;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在标题
        /// </summary>
        public bool ExistsTitle(string channel_name, int category_id, string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where category_id=@category_id and title=@title");
            SqlParameter[] parameters = {
                    new SqlParameter("@category_id", SqlDbType.Int,4),
                    new SqlParameter("@title", SqlDbType.VarChar,200)}
                                        ;
            parameters[0].Value = category_id;
            parameters[1].Value = title;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回信息标题
        /// </summary>
        public string GetTitle(string channel_name, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where id=" + article_id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            return title;
        }

        /// <summary>
        /// 返回信息内容
        /// </summary>
        public string GetContent(string channel_name, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 content from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where id=" + article_id);
            string content = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            return content;
        }

        /// <summary>
        /// 返回信息内容
        /// </summary>
        public string GetContent(string channel_name, string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 content from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where call_index=@call_index");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;
            string content = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            if (string.IsNullOrEmpty(content))
            {
                return string.Empty;
            }
            return content;
        }

        /// <summary>
        /// 返回信息封面图
        /// </summary>
        public string GetImgUrl(string channel_name, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 img_url from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where id=" + article_id);
            string imgsrc = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(imgsrc))
            {
                return string.Empty;
            }
            return imgsrc;
        }

        /// <summary>
        /// 获取阅读次数
        /// </summary>
        public int GetClick(string channel_name, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 click from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where id=" + article_id);
            string str = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string channel_name, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 返回商品库存数量
        /// </summary>
        public int GetStockQuantity(string channel_name, int article_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 stock_quantity ");
            strSql.Append(" from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where id=" + article_id);
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string channel_name, int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name + " set " + strValue);
            strSql.Append(" where id=" + id);
            return DbHelperSQL.ExecuteSql(strSql.ToString()) > 0;
        }
        #endregion

        #region 私有方法================================
        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        private Model.article DataRowToModel(DataRow row)
        {
            Model.article model = new Model.article();//主表字段
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
                        //用索引值设置属性值
                        proInfo.SetValue(model, row[i], null);
                    }
                }
                #endregion

                #region 扩展字段信息===================
                Dictionary<string, string> fieldDic = new DAL.article_attribute_field(databaseprefix).GetFields(model.channel_id);//扩展字段字典
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    if (fieldDic.ContainsKey(row.Table.Columns[i].ColumnName) && row[i] != null)
                    {
                        fieldDic[row.Table.Columns[i].ColumnName] = row[i].ToString();
                    }
                }
                model.fields = fieldDic;
                #endregion

                //相册信息
                model.albums = new DAL.article_albums(databaseprefix).GetList(model.channel_id, model.id);
                //附件信息
                model.attach = new DAL.article_attach(databaseprefix).GetList(model.channel_id, model.id);
                //用户组价格
                model.group_price = new DAL.user_group_price(databaseprefix).GetList(model.channel_id, model.id);
            }
            return model;
        }
        #endregion

        #region 前台模板调用方法========================
        /// <summary>
        /// 根据频道名称获取总记录数
        /// </summary>
        public int ArticleCount(string channel_name, int category_id, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (category_id > 0)
            {
                strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 根据频道名称显示前几条数据
        /// </summary>
        public DataSet ArticleList(string channel_name, int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * FROM " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据频道名称显示前几条数据
        /// </summary>
        public DataSet ArticleList(string channel_name, int category_id, int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * FROM " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (category_id > 0)
            {
                strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据频道名称获得查询分页数据
        /// </summary>
        public DataSet ArticleList(string channel_name, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (category_id > 0)
            {
                strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 根据频道名称及规格查询分页数据
        /// </summary>
        public DataSet ArticleList(string channel_name, int category_id, Dictionary<string, string> dicSpecIds, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            string specWhere = string.Empty;
            foreach (KeyValuePair<string, string> kv in dicSpecIds)
            {
                if (Utils.StrToInt(kv.Value, 0) > 0)
                {
                    if (!string.IsNullOrEmpty(specWhere))
                    {
                        specWhere += "and ";
                    }
                    specWhere += "B.spec_ids like '%," + kv.Value + ",%'";
                }
            }
            if (!string.IsNullOrEmpty(specWhere))
            {
                specWhere = " and (" + specWhere + ")";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (category_id > 0)
            {
                strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            if (!string.IsNullOrEmpty(specWhere))
            {
                strSql.Append(" and id in(");
                strSql.Append("select A.id from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + channel_name + " as A," + databaseprefix + "article_goods as B");
                strSql.Append(" where A.channel_id=B.channel_id and A.id=B.article_id " + specWhere);
                strSql.Append(" group by A.id)");
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获得关健字查询分页数据(搜索用到)
        /// </summary>
        public DataSet ArticleSearch(int site_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            //查询站点频道列表
            DataTable dt = new DAL.site_channel(databaseprefix).GetList("site_id=" + site_id).Tables[0];
            if (dt.Rows.Count > 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from (");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strSql.Append("select id,site_id,channel_id,call_index,title,zhaiyao,add_time,img_url");
                    strSql.Append(" from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + dt.Rows[i]["name"].ToString());
                    strSql.Append(" where datediff(d,add_time,getdate())>=0");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" and " + strWhere);
                    }
                    if (i < (dt.Rows.Count - 1))
                    {
                        strSql.Append(" UNION ALL ");//合并频道数据表
                    }
                }
                strSql.Append(") as temp_article");
                recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
                return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
            }
            recordCount = 0;
            return new DataSet();
        }

        /// <summary>
        /// 获得Tags查询分页数据(搜索用到)
        /// </summary>
        public DataSet ArticleSearch(int site_id, string tags, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            //查询站点频道列表
            DataTable dt = new DAL.site_channel(databaseprefix).GetList("site_id=" + site_id).Tables[0];
            if (dt.Rows.Count > 0)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from (");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    strSql.Append("select id,site_id,A.channel_id,call_index,title,zhaiyao,add_time,img_url");
                    strSql.Append(" from " + databaseprefix + DTKeys.TABLE_CHANNEL_ARTICLE + dt.Rows[i]["name"].ToString() + " as A INNER JOIN (");
                    strSql.Append("select R.channel_id,R.article_id");
                    strSql.Append(" from dt_article_tags_relation as R INNER JOIN dt_article_tags as S ON R.tag_id=S.id and S.title='" + tags + "'");
                    strSql.Append(") as T ON A.channel_id=T.channel_id and A.id=T.article_id");
                    strSql.Append(" where datediff(d,add_time,getdate())>=0");
                    if (strWhere.Trim() != "")
                    {
                        strSql.Append(" and " + strWhere);
                    }
                    if (i < (dt.Rows.Count - 1))
                    {
                        strSql.Append(" UNION ALL ");//合并频道数据表
                    }
                }
                strSql.Append(") as temp_article");
                recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
                return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
            }
            recordCount = 0;
            return new DataSet();
        }
        #endregion
    }
}

