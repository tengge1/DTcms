using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        #region 列表标签======================================
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int top, string strwhere)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().ArticleList(channel_name, top, strwhere, "sort_id asc,add_time desc").Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int top, string strwhere)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().ArticleList(channel_name, category_id, top, strwhere, "sort_id asc,add_time desc").Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int top, string strwhere, string orderby)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().ArticleList(channel_name, category_id, top, strwhere, orderby).Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 文章分页列表(自定义页面大小)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int page_size, int page_index, string strwhere, string orderby, out int totalcount)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().ArticleList(channel_name, category_id, page_size, page_index, strwhere, orderby, out totalcount).Tables[0];
            }
            else
            {
                totalcount = 0;
            }
            return dt;
        }

        /// <summary>
        /// 文章分页列表(自动页面大小)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <param name="_key">URL配置名称</param>
        /// <param name="_params">传输参数</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int page_size, int page_index, string strwhere, out int totalcount, out string pagelist, string _key, params object[] _params)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().ArticleList(channel_name, category_id, page_size, page_index, strwhere, "sort_id asc,add_time desc", out totalcount).Tables[0];
                pagelist = Utils.OutPageList(page_size, page_index, totalcount, linkurl(_key, _params), 8);
            }
            else
            {
                totalcount = 0;
                pagelist = "";
            }
            return dt;
        }

        /// <summary>
        /// 文章分页列表(可排序)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <param name="_key">URL配置名称</param>
        /// <param name="_params">传输参数</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, int page_size, int page_index, string strwhere, string orderby, out int totalcount, out string pagelist, string _key, params object[] _params)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().ArticleList(channel_name, category_id, page_size, page_index, strwhere, orderby, out totalcount).Tables[0];
                pagelist = Utils.OutPageList(page_size, page_index, totalcount, linkurl(_key, _params), 8);
            }
            else
            {
                totalcount = 0;
                pagelist = "";
            }
            return dt;
        }

        /// <summary>
        /// 根据频道及规格获得分页列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="category_id">分类ID</param>
        /// <param name="spec_ids">规格列表</param>
        /// <param name="page_size">分页大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="orderby">排序</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_list(string channel_name, int category_id, Dictionary<string, string> spec_ids, int page_size, int page_index, string strwhere, string orderby, out int totalcount)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(channel_name))
            {
                dt = new BLL.article().ArticleList(channel_name, category_id, spec_ids, page_size, page_index, strwhere, orderby, out totalcount).Tables[0];
            }
            else
            {
                totalcount = 0;
            }
            return dt;
        }

        /// <summary>
        /// 文章Tags标签列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_article_tags(int top, string strwhere)
        {
            return new BLL.article_tags().GetList(top, strwhere, "count desc").Tables[0];
        }
        #endregion

        #region 内容标签======================================
        /// <summary>
        /// 根据调用标识取得内容
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="call_index">调用别名</param>
        /// <returns>String</returns>
        protected string get_article_content(string channel_name, string call_index)
        {
            if (!string.IsNullOrEmpty(channel_name) && !string.IsNullOrEmpty(call_index))
            {
                return new BLL.article().GetContent(channel_name, call_index);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取对应的图片路径
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="article_id">信息ID</param>
        /// <returns>String</returns>
        protected string get_article_img_url(int channel_id, int article_id)
        {
            if (channel_id > 0)
            {
                return new BLL.article().GetImgUrl(channel_id, article_id);
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取扩展字段的值
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="article_id">内容ID</param>
        /// <param name="field_name">扩展字段名</param>
        /// <returns>String</returns>
        protected string get_article_field(int channel_id, int article_id, string field_name)
        {
            Model.article model = new BLL.article().GetModel(channel_id, article_id);
            if (model != null && model.fields.ContainsKey(field_name))
            {
                return model.fields[field_name];
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取扩展字段的值
        /// </summary>
        /// <param name="channel_id">频道ID</param>
        /// <param name="call_index">调用别名</param>
        /// <param name="field_name">扩展字段名</param>
        /// <returns>String</returns>
        protected string get_article_field(int channel_id, string call_index, string field_name)
        {
            if (string.IsNullOrEmpty(call_index))
                return string.Empty;
            BLL.article bll = new BLL.article();
            if (!bll.Exists(channel_id, call_index))
            {
                return string.Empty;
            }
            Model.article model = bll.GetModel(channel_id, call_index);
            if (model != null && model.fields.ContainsKey(field_name))
            {
                return model.fields[field_name];
            }
            return string.Empty;
        }
        #endregion

    }
}
