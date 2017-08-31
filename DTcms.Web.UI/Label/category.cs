using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 返回当前类别名称
        /// </summary>
        /// <param name="category_id">类别ID</param>
        /// <returns>String</returns>
        protected string get_category_title(int category_id, string default_value)
        {
            BLL.article_category bll = new BLL.article_category();
            if (bll.Exists(category_id))
            {
                return bll.GetTitle(category_id);
            }
            return default_value;
        }

        /// <summary>
        /// 返回类别一个实体类
        /// </summary>
        /// <param name="category_id">类别ID</param>
        /// <returns>Model.category</returns>
        protected Model.article_category get_category_model(int category_id)
        {
            return new BLL.article_category().GetModel(category_id);
        }

        /// <summary>
        /// 返回类别导航面包屑
        /// </summary>
        /// <param name="urlKey">URL重写Name</param>
        /// <param name="category_id">类别ID</param>
        /// <returns>String</returns>
        protected string get_category_menu(string urlKey, int category_id)
        {
            StringBuilder strTxt = new StringBuilder();
            if (category_id > 0)
            {
                LoopChannelMenu(strTxt, urlKey, category_id);
            }
            return strTxt.ToString();
        }

        /// <summary>
        /// 返回类别列表
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="parent_id">父类别ID</param>
        /// <returns>DataTable</returns>
        protected DataTable get_category_list(string channel_name, int parent_id)
        {
            return new BLL.article_category().GetList(parent_id, channel_name);
        }

        /// <summary>
        /// 返回指定类别下列表(一层)
        /// </summary>
        /// <param name="channel_name">频道名称</param>
        /// <param name="parent_id">父类别ID</param>
        /// <returns>DataTable</returns>
        protected DataTable get_category_child_list(string channel_name, int parent_id)
        {
            return new BLL.article_category().GetChildList(0, parent_id, channel_name);
        }

        #region 私有方法===========================================
        /// <summary>
        /// 递归找到父节点
        /// </summary>
        private void LoopChannelMenu(StringBuilder strTxt, string urlKey, int category_id)
        {
            BLL.article_category bll = new BLL.article_category();
            int parentId = bll.GetParentId(category_id);
            if (parentId > 0)
            {
                this.LoopChannelMenu(strTxt, urlKey, parentId);
            }
            strTxt.Append("&nbsp;&gt;&nbsp;<a href=\"" + linkurl(urlKey, category_id, 1) + "\">" + bll.GetTitle(category_id) + "</a>");
        }
        #endregion
    }
}
