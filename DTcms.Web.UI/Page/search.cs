using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class search : Web.UI.BasePage
    {
        protected string tags = string.Empty; //TAG标签
        protected string keyword = string.Empty; //关健字
        protected string pagelist = string.Empty; //分页列表
        protected int page; //当前页码
        protected int totalcount; //OUT数据总数
        
        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            page = DTRequest.GetQueryInt("page", 1);
            tags = DTRequest.GetQueryString("tags").Replace("'", string.Empty);
            keyword = DTRequest.GetQueryString("keyword").Replace("'", string.Empty);
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        protected DataTable get_search_list(int _pagesize, out int _totalcount)
        {
            //创建一个DataTable
            DataTable dt = new DataTable();
            dt.Columns.Add("id", Type.GetType("System.Int32"));
            dt.Columns.Add("channel_id", Type.GetType("System.String"));
            dt.Columns.Add("title", Type.GetType("System.String"));
            dt.Columns.Add("remark", Type.GetType("System.String"));
            dt.Columns.Add("link_url", Type.GetType("System.String"));
            dt.Columns.Add("add_time", Type.GetType("System.String"));
            dt.Columns.Add("img_url", Type.GetType("System.String"));
            //创建一个DataSet,判断是使用Tags还是关健字查询
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(tags))
            {
                ds = new BLL.article().ArticleSearch(site.id, tags, _pagesize, page, string.Empty, "add_time desc,id desc", out _totalcount);
            }
            else
            {
                ds = new BLL.article().ArticleSearch(site.id, _pagesize, page, "(title like '%" + keyword + "%' or zhaiyao like '%" + keyword + "%')", "add_time desc,id desc", out _totalcount);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr1 = ds.Tables[0].Rows[i];
                    string link_url = get_url_rewrite(Utils.StrToInt(dr1["channel_id"].ToString(), 0), dr1["call_index"].ToString(), Utils.StrToInt(dr1["id"].ToString(), 0));
                    if (!string.IsNullOrEmpty(link_url))
                    {
                        DataRow dr = dt.NewRow();
                        dr["id"] = dr1["id"]; //自增ID
                        dr["channel_id"] = dr1["channel_id"]; //频道ID
                        dr["title"] = dr1["title"]; //标题
                        dr["remark"] = dr1["zhaiyao"]; //摘要
                        dr["link_url"] = link_url; //链接地址
                        dr["add_time"] = dr1["add_time"]; //发布时间
                        dr["img_url"] = dr1["img_url"]; //发布时间
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }

        //查找匹配的URL
        private string get_url_rewrite(int channel_id, string call_index, int id)
        {
            if (channel_id == 0)
            {
                return string.Empty;
            }
            string querystring = id.ToString();
            string channel_name = new BLL.site_channel().GetChannelName(channel_id);
            if (string.IsNullOrEmpty(channel_name))
            {
                return string.Empty;
            }
            if (!string.IsNullOrEmpty(call_index))
            {
                querystring = call_index;
            }
            BLL.url_rewrite bll = new BLL.url_rewrite();
            Model.url_rewrite model = bll.GetInfo(channel_name, "detail");
            if (model != null)
            {
                return linkurl(model.name, querystring);
            }
            return string.Empty;
        }

    }
}
