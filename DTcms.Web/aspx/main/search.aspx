<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.search" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by DTcms Template Engine at 2017/6/12 21:12:28.
		本页面代码由DTcms模板引擎生成于 2017/6/12 21:12:28. 
	*/

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>站内搜索 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body id=\"news\">\r\n<!--页面头部-->\r\n");

	templateBuilder.Append("<div class=\"header\">\r\n    <div class=\"head-top\">\r\n        <div class=\"section\">\r\n            <div class=\"left-box\">\r\n                <span>网站链接：</span>\r\n                <a target=\"_blank\" href=\"http://www.dtcms.net\">动力启航官网</a>\r\n                <a target=\"_blank\" href=\"http://demo.dtcms.net\">DTcms演示站</a>\r\n            </div>\r\n            <script type=\"text/javascript\">\r\n                $.ajax({\r\n                    type: \"POST\",\r\n                    url: \"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_check_login\",\r\n                    dataType: \"json\",\r\n                    timeout: 20000,\r\n                    success: function (data, textStatus) {\r\n                        if (data.status == 1) {\r\n                            $(\"#menu\").prepend('<a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\">退出</a><strong>|</strong>');\r\n                            $(\"#menu\").prepend('<a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">会员中心</a>');\r\n                        } else {\r\n                            $(\"#menu\").prepend('<a href=\"");
	templateBuilder.Append(linkurl("register"));

	templateBuilder.Append("\">注册</a><strong>|</strong>');\r\n                            $(\"#menu\").prepend('<a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">登录</a>');\r\n                        }\r\n                    }\r\n                });\r\n            </");
	templateBuilder.Append("script>\r\n            <div id=\"menu\" class=\"right-box\">\r\n                <a href=\"");
	templateBuilder.Append(linkurl("content","contact"));

	templateBuilder.Append("\"><i class=\"iconfont icon-phone\"></i>联系我们</a>\r\n                <a href=\"");
	templateBuilder.Append(linkurl("cart"));

	templateBuilder.Append("\"><i class=\"iconfont icon-cart\"></i>购物车(<span id=\"shoppingCartCount\"><script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_cart_count\"></");
	templateBuilder.Append("script></span>)</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <div class=\"head-nav\">\r\n        <div class=\"section\">\r\n            <div class=\"logo\">\r\n                <a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\"><img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/logo.png\" /></a>\r\n            </div>\r\n            <div class=\"nav-box\">\r\n                <ul>\r\n                    <li class=\"index\"><a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a></li>\r\n                    <li class=\"news\"><a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">新闻资讯</a></li>\r\n                    <li class=\"goods\"><a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城</a></li>\r\n                    <li class=\"video\"><a href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频中心</a></li>\r\n                    <li class=\"photo\"><a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">图片分享</a></li>\r\n                    <li class=\"down\"><a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">资源下载</a></li>\r\n                </ul>\r\n            </div>\r\n            <div class=\"search-box\">\r\n                <div class=\"input-box\">\r\n                    <input id=\"keywords\" name=\"keywords\" type=\"text\" onkeydown=\"if(event.keyCode==13){SiteSearch('");
	templateBuilder.Append(linkurl("search"));

	templateBuilder.Append("', '#keywords');return false};\" placeholder=\"输入关健字\" x-webkit-speech=\"\" />\r\n                </div>\r\n                <a href=\"javascript:;\" onclick=\"SiteSearch('");
	templateBuilder.Append(linkurl("search"));

	templateBuilder.Append("', '#keywords');\">\r\n                    <i class=\"iconfont icon-search\"></i>\r\n                </a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/页面头部-->\r\n\r\n<!--当前位置-->\r\n<div class=\"section\">\r\n    <div class=\"location\">\r\n        <span>当前位置：</span>\r\n        <a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a> &gt;\r\n        <a href=\"javascript:;\">站内搜索</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<div class=\"section clearfix\">\r\n    <!--/页面右边-->\r\n    <div class=\"right-260\">\r\n        <div class=\"bg-wrap nobg\">\r\n            <div class=\"sidebar-box\">\r\n                <h4>热门标签</h4>\r\n                <div class=\"tags-box clearfix\">\r\n                ");
	DataTable tagsList = get_article_tags(0, "is_red=1");

	foreach(DataRow dr in tagsList.Rows)
	{

	templateBuilder.Append("\r\n                    <a href=\"");
	templateBuilder.Append(linkurl("search","?tags="+Server.UrlEncode(Utils.ObjectToStr(dr["title"]))));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "<i>(" + Utils.ObjectToStr(dr["count"]) + ")</i></a>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </div>\r\n            </div>\r\n            \r\n            <div class=\"sidebar-box\">\r\n                <h4>推荐新闻</h4>\r\n                <ul class=\"side-img-list\">\r\n                ");
	DataTable redNews = get_article_list("news", 0, 4, "status=0 and is_red=1 and img_url<>''");

	foreach(DataRow dr in redNews.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <div class=\"img-box\">\r\n                            <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n                        </div>\r\n                        <div class=\"txt-box\">\r\n                            <a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n                            <span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(dr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span>\r\n                        </div>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            \r\n        </div>\r\n    </div>\r\n    <!--/页面右边-->\r\n    \r\n    <!--页面左边-->\r\n    <div class=\"left-auto\">\r\n        <div class=\"bg-wrap\">\r\n            <!--取得一个DataTable-->\r\n            ");
	DataTable list = get_search_list(15, out totalcount);

	templateBuilder.Append("\r\n            <!--取得分页页码列表-->\r\n            ");
	if (tags!="")
	{

	 pagelist = get_page_link(15, page, totalcount, linkurl("search","?tags=" + Server.UrlEncode(tags) + "&page=__id__"));

	}
	else
	{

	 pagelist = get_page_link(15, page, totalcount, linkurl("search","?keyword=" + Server.UrlEncode(keyword) + "&page=__id__"));

	}	//end for if

	templateBuilder.Append("\r\n            <div class=\"search-tit\">\r\n                <h3>搜索结果</h3>\r\n                ");
	if (tags!="")
	{

	templateBuilder.Append("\r\n                <span>查询Tags标签：<strong>");
	templateBuilder.Append(Utils.ObjectToStr(tags));
	templateBuilder.Append("</strong>，共有<strong>");
	templateBuilder.Append(Utils.ObjectToStr(totalcount));
	templateBuilder.Append("</strong>条记录</span>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <span>搜索关健字：<strong>");
	templateBuilder.Append(Utils.ObjectToStr(keyword));
	templateBuilder.Append("</strong>，共有<strong>");
	templateBuilder.Append(Utils.ObjectToStr(totalcount));
	templateBuilder.Append("</strong>条记录</span>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n            </div>\r\n            \r\n            <ul class=\"n-list\">\r\n            ");
	foreach(DataRow dr in list.Rows)
	{

	templateBuilder.Append("\r\n                <li>\r\n                    <h2><a href=\"" + Utils.ObjectToStr(dr["link_url"]) + "\">" + Utils.ObjectToStr(dr["title"]) + "</a></h2>\r\n                    <div class=\"note\">\r\n                        ");
	if (Utils.ObjectToStr(dr["img_url"])!="")
	{

	templateBuilder.Append("\r\n                        <b><img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" /></b>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                        <p>" + Utils.ObjectToStr(dr["remark"]) + "</p>\r\n                        <div class=\"info\">\r\n                            <span><i class=\"iconfont icon-date\"></i>" + Utils.ObjectToStr(dr["add_time"]) + "</span>\r\n                            <span><i class=\"iconfont icon-comment\"></i><script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_comment_count&channel_id=" + Utils.ObjectToStr(dr["channel_id"]) + "&id=" + Utils.ObjectToStr(dr["id"]) + "\"></");
	templateBuilder.Append("script>人评论</span>\r\n                            <span><i class=\"iconfont icon-view\"></i><script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_article_click&channel_id=" + Utils.ObjectToStr(dr["channel_id"]) + "&id=" + Utils.ObjectToStr(dr["id"]) + "\"></");
	templateBuilder.Append("script>次浏览</span>\r\n                        </div>\r\n                    </div>\r\n                </li>\r\n            ");
	}	//end for if

	if (totalcount==0)
	{

	templateBuilder.Append("\r\n                <div class=\"nodata\">很抱歉，暂无查找到符合条件的信息！</div>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            </ul>\r\n            \r\n            <!--页码列表-->\r\n            <div class=\"page-box\">\r\n                <div class=\"digg\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n            </div>\r\n            <!--/页码列表-->\r\n            \r\n        </div>\r\n    </div>\r\n    <!--/页面左边-->\r\n</div>\r\n\r\n<!--页面底部-->\r\n");

	templateBuilder.Append("<div class=\"footer\">\r\n    <div class=\"section\">\r\n        <div class=\"foot-nav\">\r\n            <a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">新闻资讯</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频专区</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">图片分享</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">资源下载</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">在线留言</a>\r\n            <strong>|</strong>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">友情链接</a>\r\n        </div>\r\n        <div class=\"foot-box\">\r\n            <div class=\"copyright\">\r\n                <p>版权所有 ");
	templateBuilder.Append(Utils.ObjectToStr(site.company));
	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(site.crod));
	templateBuilder.Append("</p>\r\n                <p>公司地址：");
	templateBuilder.Append(Utils.ObjectToStr(site.address));
	templateBuilder.Append(" 联系电话：");
	templateBuilder.Append(Utils.ObjectToStr(site.tel));
	templateBuilder.Append("</p>\r\n                <p class=\"gray\">Copyright © 2009-2017 dtcms.net Corporation,All Rights Reserved.</p>\r\n            </div>\r\n            <div class=\"service\">\r\n                <p>周一至周日 9:00-24:00</p>\r\n                <a href=\"http://www.dtcms.net\" target=\"_blank\"><i class=\"iconfont icon-phone\"></i>在线客服</a>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/页面底部-->\r\n</body>\r\n</html>");
	Response.Write(templateBuilder.ToString());
}
</script>
