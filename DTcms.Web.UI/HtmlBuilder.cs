using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using DTcms.Common;
using System.Text.RegularExpressions;

namespace DTcms.Web.UI
{
    public class HtmlBuilder
    {

        BLL.article_category objarticle_category = new BLL.article_category();//文章分类
        BLL.site_channel objchannel = new BLL.site_channel();//频道
        BLL.sites objchannel_site = new BLL.sites();//系统站点
        BLL.article objarticle = new BLL.article();//文章
        Model.sysconfig config = new BLL.sysconfig().loadConfig();//站点配置
        Model.site_channel modelchanel = new Model.site_channel();//频道实体类
        private const string urlstr = "\"{0}tools/admin_ajax.ashx?action=get_builder_html&lang={1}&html_filename=&indexy=&aspx_filename={2}&catalogue={3}\"";

        public HtmlBuilder()
        {
            //构造函数
        }

        #region 获取生成静态地址
        /// <summary>
        /// 获取生成静态地址
        /// </summary>
        /// <param name="context"></param>
        public void getpublishsite(HttpContext context)
        {
            string lang = DTRequest.GetQueryString("lang");
            string name = DTRequest.GetQueryString("name");
            string type = DTRequest.GetQueryString("type");

            StringBuilder sbjson = new StringBuilder();

       
            //获得URL配置列表
            BLL.url_rewrite bll = new BLL.url_rewrite();
            List<Model.url_rewrite> ls = (!string.IsNullOrEmpty(type)) ? bll.GetList(name, type) : bll.GetList(name);
            string linkurl = string.Empty;
            sbjson.Append("[");
            if (type == "indexlist")
            {
                #region 针对特殊需求
                List<Model.url_rewrite> ls2 = (!string.IsNullOrEmpty("list")) ? bll.GetList(name, "list") : bll.GetList(name, "list");
                foreach (Model.url_rewrite modeltrewrite2 in ls2)
                {
                    if (modeltrewrite2.url_rewrite_items.Count > 0)
                    {
                        if (modeltrewrite2.channel == string.Empty || modeltrewrite2.channel == name)
                        {
                            //遍历URL字典的子节点
                            foreach (Model.url_rewrite_item item2 in modeltrewrite2.url_rewrite_items)
                            {
                                if (sbjson.ToString().Length > 1)
                                    sbjson.Append(",");
                                switch (modeltrewrite2.type.ToLower())
                                {
                                    case "list":
                                        sbjson.Append(GetArticleIndexUrlList(lang, name, modeltrewrite2.page, item2.pattern, item2.path, item2.querystring, Utils.StrToInt(modeltrewrite2.pagesize, 0)));
                                        break;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            else
            {

                foreach (Model.url_rewrite modeltrewrite in ls)
                {
                    if (modeltrewrite.url_rewrite_items.Count > 0)
                    {
                        if (modeltrewrite.channel == string.Empty || modeltrewrite.channel == name)
                        {
                            //遍历URL字典的子节点
                            foreach (Model.url_rewrite_item item in modeltrewrite.url_rewrite_items)
                            {
                                if (item.querystring == string.Empty)
                                {
                                    linkurl = string.Format("{0}/{1}/{2}", DTKeys.DIRECTORY_REWRITE_ASPX, lang, modeltrewrite.page);
                                    string HTMLPattern = string.Format("{0}/{1}/{2}", DTKeys.DIRECTORY_REWRITE_HTML, lang, Utils.GetUrlExtension(item.pattern, config.staticextension)); //替换扩展名
                                    if (sbjson.ToString().Length > 1)
                                        sbjson.Append(",");
                                    sbjson.AppendFormat(urlstr, config.webpath, lang, linkurl, HTMLPattern);



                                }
                                else
                                {
                                    if (sbjson.ToString().Length > 1)
                                        sbjson.Append(",");
                                    switch (modeltrewrite.type.ToLower())
                                    {
                                        case "list":
                                            sbjson.Append(GetArticleUrlList(lang, name, modeltrewrite.page, item.pattern, item.path, item.querystring, Utils.StrToInt(modeltrewrite.pagesize, 0)));
                                            break;

                                        case "detail":
                                            sbjson.Append(GetDetailUrlList(lang, name, modeltrewrite.page, item.pattern, item.path, item.querystring));
                                            break;
                                        case "category":
                                            sbjson.Append(GetCategoryUrlList(lang, name, modeltrewrite.page, item.pattern, item.path, item.querystring, Utils.StrToInt(modeltrewrite.pagesize, 0)));
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            sbjson.Append("]");
            context.Response.Write(sbjson.ToString());
        }
        #region 针对特殊需求
        /// <summary>
        /// 返回频道下首页所有的文章列表URL地址
        /// </summary>
        ///  <param name="lang">频道分类</param>
        /// <param name="channelname">频道Name</param>
        /// <returns>返回频道下所有的文章列表URL地址</returns>
        private string GetArticleIndexUrlList(string lang, string channelname, string page, string pattern, string path, string querystring, int pagesize)
        {
            StringBuilder sburl = new StringBuilder();
            int strLength = 0;
            if (!string.IsNullOrEmpty(querystring))
                strLength = querystring.Split('&').Length;

            int totalCount = objarticle.ArticleCount(channelname, 0, string.Empty);
            int pageindex = GetPageSize(totalCount, pagesize);
            if (strLength == 1)
                pageindex = 1;
            for (int q = 1; q <= pageindex; q++)
            {
                string querystr = Regex.Replace(string.Format(path, "0", q), pattern, querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                string linkurl = string.Format("{0}/{1}/{2}?{3}", DTKeys.DIRECTORY_REWRITE_ASPX, lang, page, querystr);
                string HTMLPattern = string.Format("{0}/{1}/{2}", DTKeys.DIRECTORY_REWRITE_HTML, lang, Utils.GetUrlExtension(string.Format(path, "0", q), config.staticextension)); //替换扩展名
                if (!string.IsNullOrEmpty(sburl.ToString()))
                    sburl.Append(",");
                sburl.AppendFormat(urlstr, config.webpath, lang, linkurl.Replace("&", "^"), HTMLPattern);
            }

            return sburl.ToString();

        }
        #endregion
        /// <summary>
        /// 返回频道下所有的文章列表URL地址
        /// </summary>
        ///  <param name="lang">频道分类</param>
        /// <param name="channelname">频道Name</param>
        /// <returns>返回频道下所有的文章列表URL地址</returns>
        private string GetArticleUrlList(string lang, string channelname, string page, string pattern, string path, string querystring, int pagesize)
        {
            StringBuilder sburl = new StringBuilder();
            DataTable dt = objarticle_category.GetList(0, channelname);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int strLength = 0;
                    if (!string.IsNullOrEmpty(querystring))
                        strLength = querystring.Split('&').Length;

                    int totalCount = objarticle.ArticleCount(channelname, Convert.ToInt32(dt.Rows[i]["id"].ToString()), string.Empty);
                    int pageindex = GetPageSize(totalCount, pagesize);
                    if (strLength == 1)
                        pageindex = 1;
                    for (int q = 1; q <= pageindex; q++)
                    {
                        string querystr = Regex.Replace(string.Format(path, dt.Rows[i]["id"].ToString(), q), pattern, querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                        string linkurl = string.Format("{0}/{1}/{2}?{3}", DTKeys.DIRECTORY_REWRITE_ASPX, lang, page, querystr);
                        string HTMLPattern = string.Format("{0}/{1}/{2}", DTKeys.DIRECTORY_REWRITE_HTML, lang, Utils.GetUrlExtension(string.Format(path, dt.Rows[i]["id"].ToString(), q), config.staticextension)); //替换扩展名
                        if (!string.IsNullOrEmpty(sburl.ToString()))
                            sburl.Append(",");
                        sburl.AppendFormat(urlstr, config.webpath, lang, linkurl.Replace("&","^"), HTMLPattern);
                    }
                }
            }

            return sburl.ToString();

        }

        /// <summary>
        /// 返回频道下所有分类列表URL地址
        /// </summary>
        ///  <param name="lang">频道分类</param>
        /// <param name="channelname">频道Name</param>
        /// <returns>返回频道下所有分类列表URL地址</returns>
        private string GetCategoryUrlList(string lang, string channelname, string page, string pattern, string path, string querystring, int pagesize)
        {
            StringBuilder sburl = new StringBuilder();
            DataTable dt = objarticle_category.GetList(0, channelname);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string querystr = Regex.Replace(string.Format(path, dt.Rows[i]["id"].ToString()), pattern, querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                    string linkurl = string.Format("{0}/{1}/{2}?{3}", DTKeys.DIRECTORY_REWRITE_ASPX, lang, page, querystr);
                    string HTMLPattern = string.Format("{0}/{1}/{2}", DTKeys.DIRECTORY_REWRITE_HTML, lang, Utils.GetUrlExtension(string.Format(path, dt.Rows[i]["id"].ToString()), config.staticextension)); //替换扩展名
                    if (!string.IsNullOrEmpty(sburl.ToString()))
                        sburl.Append(",");
                    sburl.AppendFormat(urlstr, config.webpath, lang, linkurl, HTMLPattern);

                }
            }

            return sburl.ToString();

        }
        /// <summary>
        /// 返回频道下所有的文章URL地址
        /// </summary>
        ///  <param name="lang">频道分类</param>
        /// <param name="channelname">频道Name</param>
        /// <returns>返回频道下所有的文章URL地址</returns>
        private string GetDetailUrlList(string lang, string channelname, string page, string pattern, string path, string querystring)
        {

            StringBuilder sburl = new StringBuilder();
            DataTable dt = objarticle.ArticleList(channelname, 0, string.Empty, "id desc").Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    string strvalue = string.IsNullOrEmpty(dt.Rows[i]["call_index"].ToString()) ? dt.Rows[i]["id"].ToString() : dt.Rows[i]["call_index"].ToString();
                    string querystr = Regex.Replace(string.Format(path, strvalue), pattern, querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                    string linkurl = string.Format("{0}/{1}/{2}?{3}", DTKeys.DIRECTORY_REWRITE_ASPX, lang, page, querystr);
                    string HTMLPattern = string.Format("{0}/{1}/{2}", DTKeys.DIRECTORY_REWRITE_HTML, lang, Utils.GetUrlExtension(string.Format(path, strvalue), config.staticextension)); //替换扩展名
                    if (!string.IsNullOrEmpty(sburl.ToString()))
                        sburl.Append(",");
                    sburl.AppendFormat(urlstr, config.webpath, lang, linkurl, HTMLPattern);
                }
            }

            return sburl.ToString();

        }

        /// <summary>
        /// 计算分页数量
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private int GetPageSize(int totalCount, int pageSize)
        {
            //计算页数
            if (totalCount < 1 || pageSize < 1)
            {
                return 1;
            }
            int pageCount = totalCount / pageSize;
            if (pageCount < 1)
            {
                return 1;
            }
            if (totalCount % pageSize > 0)
            {
                return (pageCount += 1);
            }
            else {
                if (totalCount % pageSize == 0)
                    return pageCount;
            }
            if (pageCount <= 1)
            {
                return 1;
            }
            return 1;
        }
        #endregion

        #region 生成静态方法
        /// <summary>
        /// 生成静态文件方法
        /// </summary>
        /// <param name="context"></param>
        public void handleHtml(HttpContext context)
        {
            string lang = DTRequest.GetQueryString("lang");
            string aspx_filename = DTRequest.GetQueryString("aspx_filename");
            string catalogue = DTRequest.GetQueryString("catalogue");
           
            CreateIndexHtml(lang, aspx_filename, catalogue);
        }
        private void CreateIndexHtml(string lang, string aspx_filename, string catalogue)
        {
            if (File.Exists(Utils.GetMapPath(config.webpath + aspx_filename.Substring(0, aspx_filename.IndexOf(".aspx") + 5))))
            {

                string urlPath = config.webpath + aspx_filename.Replace("^", "&"); //文件相对路径
                string htmlPath = config.webpath + catalogue; //保存相对路径
                if (htmlPath.IndexOf(".") < 0)
                    htmlPath = htmlPath + "index." + config.staticextension;
                //检查目录是否存在
                string directorystr = HttpContext.Current.Server.MapPath(htmlPath.Substring(0, htmlPath.LastIndexOf("/")));
                if (!Directory.Exists(directorystr))
                {
                    Directory.CreateDirectory(directorystr);
                }
                string linkwebsite = HttpContext.Current.Request.Url.Authority;
             
                Model.sites modelchannelsite =  objchannel_site.GetModel(lang);
                if (modelchannelsite != null&&!string.IsNullOrEmpty(modelchannelsite.domain))
                    linkwebsite = modelchannelsite.domain;
                System.Net.WebRequest request = System.Net.WebRequest.Create("http://" + linkwebsite + urlPath);
                System.Net.WebResponse response = request.GetResponse();
                System.IO.Stream stream = response.GetResponseStream();
                System.IO.StreamReader streamreader = new System.IO.StreamReader(stream, System.Text.Encoding.GetEncoding("utf-8"));
                string content = streamreader.ReadToEnd();
                using (StreamWriter sw = new StreamWriter(Utils.GetMapPath(htmlPath), false, Encoding.UTF8))
                {

                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                }
            }
            else
            {
                HttpContext.Current.Response.Write("1");//找不到生成的模版！
            }
        }
        #endregion

    }
}
