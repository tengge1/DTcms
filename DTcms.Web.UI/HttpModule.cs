using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Configuration;
using System.Xml;
using DTcms.Common;

namespace DTcms.Web.UI
{
    /// <summary>
    /// DTcms的HttpModule类
    /// </summary>
    public class HttpModule : System.Web.IHttpModule
    {
        /// <summary>
        /// 实现接口的Init方法
        /// </summary>
        /// <param name="context"></param>
        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(ReUrl_BeginRequest);
        }

        /// <summary>
        /// 实现接口的Dispose方法
        /// </summary>
        public void Dispose()
        { }

        #region 页面请求事件处理===================================
        /// <summary>
        /// 页面请求事件处理
        /// </summary>
        /// <param name="sender">事件的源</param>
        /// <param name="e">包含事件数据的 EventArgs</param>
        private void ReUrl_BeginRequest(object sender, EventArgs e)
        {
            HttpContext context = ((HttpApplication)sender).Context;
            Model.sysconfig siteConfig = new BLL.sysconfig().loadConfig();//获得系统配置信息
            string requestPath = context.Request.Path.ToLower();//获得当前页面(含目录)

            //如果虚拟目录(不含安装目录)与站点根目录名相同则不需要重写
            if (IsDirExist(DTKeys.CACHE_SITE_DIRECTORY, siteConfig.webpath, siteConfig.webpath, requestPath))
            {
                return;
            }

            string requestDomain = context.Request.Url.Authority.ToLower();//获得当前域名(含端口号)
            string sitePath = GetSitePath(siteConfig.webpath, requestPath, requestDomain);//获取当前站点目录
            string requestPage = CutStringPath(siteConfig.webpath, sitePath, requestPath);//截取除安装、站点目录部分

            //检查网站重写状态0表示不开启重写、1开启重写、2生成静态
            if (siteConfig.staticstatus == 0)
            {
                #region 站点不开启重写处理方法===========================
                //遍历URL字典，匹配URL页面部分
                foreach (Model.url_rewrite model in SiteUrls.GetUrls().Urls)
                {
                    //查找到与页面部分匹配的节点
                    if (model.page == requestPath.Substring(requestPath.LastIndexOf("/") + 1))
                    {
                        //映射到站点目录下
                        context.RewritePath(string.Format("{0}{1}/{2}{3}",
                            siteConfig.webpath, DTKeys.DIRECTORY_REWRITE_ASPX, sitePath, requestPage));
                    }
                }
                #endregion
            }
            else
            {
                #region 站点开启重写或静态处理方法=======================
                //遍历URL字典
                foreach (Model.url_rewrite model in SiteUrls.GetUrls().Urls)
                {
                    //如果没有重写表达式则不需要重写
                    if (model.url_rewrite_items.Count == 0 &&
                        Utils.GetUrlExtension(model.page, siteConfig.staticextension) == requestPath.Substring(requestPath.LastIndexOf("/") + 1))
                    {
                        //映射到站点目录
                        context.RewritePath(string.Format("{0}{1}/{2}/{3}",
                            siteConfig.webpath, DTKeys.DIRECTORY_REWRITE_ASPX, sitePath, model.page));
                        return;
                    }
                    //遍历URL字典的子节点
                    foreach (Model.url_rewrite_item item in model.url_rewrite_items)
                    {
                        string newPattern = Utils.GetUrlExtension(item.pattern, siteConfig.staticextension);//替换扩展名

                        //如果与URL节点匹配则重写
                        if (Regex.IsMatch(requestPage, string.Format("^/{0}$", newPattern), RegexOptions.None | RegexOptions.IgnoreCase)
                            || (model.page == "index.aspx" && Regex.IsMatch(requestPage, string.Format("^/{0}$", item.pattern), RegexOptions.None | RegexOptions.IgnoreCase)))
                        {
                            //如果开启生成静态、是频道页或首页,则映射重写到HTML目录
                            if (siteConfig.staticstatus == 2 && (model.channel.Length > 0 || model.page.ToLower() == "index.aspx")) //频道页
                            {
                                context.RewritePath(siteConfig.webpath + DTKeys.DIRECTORY_REWRITE_HTML + "/" + sitePath +
                                        Utils.GetUrlExtension(requestPage, siteConfig.staticextension, true));
                                return;
                            }
                            else //其它
                            {
                                string queryString = Regex.Replace(requestPage, string.Format("/{0}", newPattern), item.querystring, RegexOptions.None | RegexOptions.IgnoreCase);
                                context.RewritePath(string.Format("{0}{1}/{2}/{3}",
                                    siteConfig.webpath, DTKeys.DIRECTORY_REWRITE_ASPX, sitePath, model.page), string.Empty, queryString);
                                return;
                            }

                        }
                    }
                }
                #endregion
            }
        }
        #endregion

        #region 辅助方法(私有)=====================================
        /// <summary>
        /// 获取URL的虚拟目录(除安装目录)
        /// </summary>
        /// <param name="webPath">网站安装目录</param>
        /// <param name="requestPath">当前页面，包含目录</param>
        /// <returns>String</returns>
        private string GetFirstPath(string webPath, string requestPath)
        {
            if (requestPath.StartsWith(webPath))
            {
                string tempStr = requestPath.Substring(webPath.Length);
                if (tempStr.IndexOf("/") > 0)
                {
                    return tempStr.Substring(0, tempStr.IndexOf("/")).ToLower();
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取当前域名包含的站点目录
        /// </summary>
        /// <param name="requestDomain">获取的域名(含端口号)</param>
        /// <returns>String</returns>
        private string GetCurrDomainPath(string requestDomain)
        {
            //当前域名是否存在于站点目录列表
            if (SiteDomains.GetSiteDomains().Paths.ContainsValue(requestDomain))
            {
                string sitePath = SiteDomains.GetSiteDomains().Domains[requestDomain];
                //如果存在，检查是否默认站点，是则还需检查虚拟目录部分
                if (sitePath != SiteDomains.GetSiteDomains().DefaultPath)
                {
                    return sitePath;
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取当前页面包含的站点目录
        /// </summary>
        /// <param name="webPath">网站安装目录</param>
        /// <param name="requestPath">获取的页面，包含目录</param>
        /// <returns>String</returns>
        private string GetCurrPagePath(string webPath, string requestPath)
        {
            //获取URL的虚拟目录(除安装目录)
            string requestFirstPath = GetFirstPath(webPath, requestPath);
            if (requestFirstPath != string.Empty && SiteDomains.GetSiteDomains().Paths.ContainsKey(requestFirstPath))
            {
                return requestFirstPath;
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取站点的目录
        /// </summary>
        /// <param name="webPath">网站安装目录</param>
        /// <param name="requestPath">获取的页面，包含目录</param>
        /// <param name="requestDomain">获取的域名(含端口号)</param>
        /// <returns>String</returns>
        private string GetSitePath(string webPath, string requestPath, string requestDomain)
        {
            //获取当前域名包含的站点目录
            string domainPath = GetCurrDomainPath(requestDomain);
            if (domainPath != string.Empty)
            {
                return domainPath;
            }
            // 获取当前页面包含的站点目录
            string pagePath = GetCurrPagePath(webPath, requestPath);
            if (pagePath != string.Empty)
            {
                return pagePath;
            }
            return SiteDomains.GetSiteDomains().DefaultPath;
        }

        /// <summary>
        /// 遍历指定路径目录，如果缓存存在则直接返回
        /// </summary>
        /// <param name="cacheKey">缓存KEY</param>
        /// <param name="dirPath">指定路径</param>
        /// <returns>ArrayList</returns>
        private ArrayList GetSiteDirs(string cacheKey, string dirPath)
        {
            ArrayList _cache = CacheHelper.Get<ArrayList>(cacheKey); //从续存中取
            if (_cache == null)
            {
                _cache = new ArrayList();
                DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(dirPath));
                foreach (DirectoryInfo dir in dirInfo.GetDirectories())
                {
                    _cache.Add(dir.Name.ToLower());
                }
                CacheHelper.Insert(cacheKey, _cache, 2); //存入续存，弹性2分钟
            }
            return _cache;
        }

        /// <summary>
        /// 遍历指定路径的子目录，检查是否匹配
        /// </summary>
        /// <param name="cacheKey">缓存KEY</param>
        /// <param name="webPath">网站安装目录，以“/”结尾</param>
        /// <param name="dirPath">指定的路径，以“/”结尾</param>
        /// <param name="requestPath">获取的URL全路径</param>
        /// <returns>布尔值</returns>
        private bool IsDirExist(string cacheKey, string webPath, string dirPath, string requestPath)
        {
            ArrayList list = GetSiteDirs(cacheKey, dirPath);//取得所有目录
            string requestFirstPath = string.Empty;//获得当前页面除站点安装目录的虚拟目录名称
            string tempStr = string.Empty;//临时变量
            if (requestPath.StartsWith(webPath))
            {
                tempStr = requestPath.Substring(webPath.Length);
                if (tempStr.IndexOf("/") > 0)
                {
                    requestFirstPath = tempStr.Substring(0, tempStr.IndexOf("/"));
                }
            }
            if (requestFirstPath.Length > 0 && list.Contains(requestFirstPath.ToLower()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 截取安装目录和站点目录部分
        /// </summary>
        /// <param name="webPath">站点安装目录</param>
        /// <param name="sitePath">站点目录</param>
        /// <param name="requestPath">当前页面路径</param>
        /// <returns>String</returns>
        private string CutStringPath(string webPath, string sitePath, string requestPath)
        {
            if (requestPath.StartsWith(webPath))
            {
                requestPath = requestPath.Substring(webPath.Length);
            }
            sitePath += "/";
            if (requestPath.StartsWith(sitePath))
            {
                requestPath = requestPath.Substring(sitePath.Length);
            }
            return "/" + requestPath;
        }

        #endregion

    }

    #region 站点URL字典信息类===================================
    /// <summary>
    /// 站点伪Url信息类
    /// </summary>
    public class SiteUrls
    {
        //属性声明
        private static object lockHelper = new object();
        private static volatile SiteUrls instance = null;
        private ArrayList _urls;
        public ArrayList Urls
        {
            get { return _urls; }
            set { _urls = value; }
        }
        //构造函数
        private SiteUrls()
        {
            Urls = new ArrayList();
            BLL.url_rewrite bll = new BLL.url_rewrite();
            List<Model.url_rewrite> ls = bll.GetList("");
            foreach(Model.url_rewrite model in ls)
            {
                foreach (Model.url_rewrite_item item in model.url_rewrite_items)
                {
                    item.querystring = item.querystring.Replace("^", "&");
                }
                Urls.Add(model);
            }
        }
        //返回URL字典
        public static SiteUrls GetUrls()
        {
            SiteUrls _cache = CacheHelper.Get<SiteUrls>(DTKeys.CACHE_SITE_HTTP_MODULE);
            lock (lockHelper)
            {
                if (_cache == null)
                {
                    CacheHelper.Insert(DTKeys.CACHE_SITE_HTTP_MODULE, new SiteUrls(), Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING));
                    instance = CacheHelper.Get<SiteUrls>(DTKeys.CACHE_SITE_HTTP_MODULE);
                }
            }
            return instance;
        }

    }
    #endregion

    #region 站点绑定域名信息类==================================
    /// <summary>
    /// 域名字典
    /// </summary>
    public class SiteDomains
    {
        private static object lockHelper = new object();
        private static volatile SiteDomains instance = null;
        //默认站点目录
        private string _default_path = string.Empty;
        public string DefaultPath
        {
            get { return _default_path; }
            set { _default_path = value; }
        }
        //站点目录列表
        private Dictionary<string, string> _paths;
        public Dictionary<string, string> Paths
        {
            get { return _paths; }
            set { _paths = value; }
        }
        //站点域名列表
        private Dictionary<string, string> _domains;
        public Dictionary<string, string> Domains
        {
            get { return _domains; }
            set { _domains = value; }
        }
        //所有站点实体
        private List<Model.sites> _sitelist;
        public List<Model.sites> SiteList
        {
            get { return _sitelist; }
            set { _sitelist = value; }
        }
        //构造函数
        public SiteDomains()
        {
            SiteList = new BLL.sites().GetModelList();//所有站点信息
            Paths = new Dictionary<string, string>();//所有站点目录
            Domains = new Dictionary<string, string>();//所有独立域名列表
            if (SiteList != null)
            {
                foreach (Model.sites modelt in SiteList)
                {
                    //所有站点目录赋值
                    Paths.Add(modelt.build_path, modelt.domain);
                    //所有独立域名列表赋值
                    if (modelt.domain.Length > 0 && !Domains.ContainsKey(modelt.domain))
                    {
                        Domains.Add(modelt.domain, modelt.build_path);
                    }
                    //默认站点赋值
                    if (modelt.is_default == 1 && DefaultPath == string.Empty)
                    {
                        DefaultPath = modelt.build_path;
                    }
                }
            }
        }
        /// <summary>
        /// 返回域名字典
        /// </summary>
        public static SiteDomains GetSiteDomains()
        {
            SiteDomains _cache = CacheHelper.Get<SiteDomains>(DTKeys.CACHE_SITE_HTTP_DOMAIN);
            lock (lockHelper)
            {
                if (_cache == null)
                {
                    CacheHelper.Insert(DTKeys.CACHE_SITE_HTTP_DOMAIN, new SiteDomains(), 10);
                    instance = CacheHelper.Get<SiteDomains>(DTKeys.CACHE_SITE_HTTP_DOMAIN);
                }
            }
            return instance;
        }
    }
    #endregion
}