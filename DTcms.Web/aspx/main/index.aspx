<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.index" ValidateRequest="false" %>
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

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_title));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/jquery.flexslider-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/jqslider.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n$(function(){\r\n    $(\"#slide-box\").jqslider(); //初始化幻灯片\r\n    $(\"#focus-box\").flexslider({\r\n        directionNav: false,\r\n		pauseOnAction: false\r\n	});\r\n});\r\n</");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body id=\"index\">\r\n<!--页面头部-->\r\n");

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


	templateBuilder.Append("\r\n<!--/页面头部-->\r\n\r\n<!--Banner-->\r\n<div id=\"slide-box\" class=\"slide-box\">\r\n    <ul class=\"list-box\">\r\n        <li><a href=\"javascript:;\" target=\"_blank\"><img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/banner_1.png\" /></a></li>\r\n        <li><a href=\"javascript:;\" target=\"_blank\"><img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/banner_2.png\" /></a></li>\r\n        <li><a href=\"javascript:;\" target=\"_blank\"><img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/banner_3.png\" /></a></li>\r\n    </ul>\r\n</div>\r\n<!--/Banner-->\r\n\r\n<!--新闻资讯-->\r\n<div class=\"section\">\r\n    <div class=\"main-tit\">\r\n        <h2>新闻资讯</h2>\r\n        <p>\r\n            ");
	DataTable newsCList = get_category_child_list("news",0);

	foreach(DataRow dr in newsCList.Rows)
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("news_list",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">更多<i>+</i></a>\r\n        </p>\r\n    </div>\r\n    <div class=\"wrapper clearfix\">\r\n        <div class=\"wrap-box\">\r\n            <div class=\"left-455\" style=\"margin:0;height:341px;\">\r\n                <div id=\"focus-box\" class=\"focus-box\">\r\n                    <ul class=\"slides\">\r\n                    ");
	DataTable focusNews = get_article_list("news", 0, 8, "status=0 and is_slide=1 and img_url<>''");

	foreach(DataRow dr in focusNews.Rows)
	{

	templateBuilder.Append("\r\n                        <li>\r\n                            <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                                <span class=\"note-bg\"></span>\r\n                                <span class=\"note-txt\">" + Utils.ObjectToStr(dr["title"]) + "</span>\r\n                                <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n                            </a>\r\n                        </li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                    </ul>\r\n                </div>\r\n            </div>\r\n            <div class=\"left-455\">\r\n                <ul class=\"side-txt-list\">\r\n                ");
	DataTable newsList = get_article_list("news", 0, 10, "status=0");

	int newdr__loop__id=0;
	foreach(DataRow newdr in newsList.Rows)
	{
		newdr__loop__id++;


	if (newdr__loop__id==1||newdr__loop__id==6)
	{

	templateBuilder.Append("\r\n                    <li class=\"tit\"><a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(newdr["title"]) + "</a></li>\r\n                    ");
	}
	else
	{

	templateBuilder.Append("\r\n                    <li><span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(newdr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span><a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(newdr["title"]) + "</a></li>\r\n                    ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            <div class=\"left-220\">\r\n                <ul class=\"side-img-list\">\r\n                ");
	DataTable topNewsList = get_article_list("news", 0, 4, "status=0 and is_top=1 and img_url<>''");

	int topdr__loop__id=0;
	foreach(DataRow topdr in topNewsList.Rows)
	{
		topdr__loop__id++;


	templateBuilder.Append("\r\n                    <li>\r\n                        <div class=\"img-box\">\r\n                            <label>");
	templateBuilder.Append(Utils.ObjectToStr(topdr__loop__id));
	templateBuilder.Append("</label>\r\n                            <img src=\"" + Utils.ObjectToStr(topdr["img_url"]) + "\" />\r\n                        </div>\r\n                        <div class=\"txt-box\">\r\n                            <a href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(topdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(topdr["title"]) + "</a>\r\n                            <span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(topdr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span>\r\n                        </div>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/新闻资讯-->\r\n\r\n<!--/购物商城-->\r\n<div class=\"section\">\r\n    <div class=\"main-tit\">\r\n        <h2>购物商城</h2>\r\n        <p>\r\n            ");
	DataTable goodsCList = get_category_child_list("goods",0);

	foreach(DataRow dr in goodsCList.Rows)
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">更多<i>+</i></a>\r\n        </p>\r\n    </div>\r\n    <div class=\"wrapper clearfix\">\r\n        <div class=\"wrap-box\">\r\n            <ul class=\"img-list\">\r\n            ");
	DataTable redGoods = get_article_list("goods", 0, 10, "status=0 and is_red=1");

	foreach(DataRow dr in redGoods.Rows)
	{

	templateBuilder.Append("\r\n                <li>\r\n                    <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                        <div class=\"img-box\">\r\n                            <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n                        </div>\r\n                        <div class=\"info\">\r\n                            <h3>" + Utils.ObjectToStr(dr["title"]) + "</h3>\r\n                            <p>\r\n                                <strong>人气 " + Utils.ObjectToStr(dr["click"]) + "</strong>\r\n                                <span class=\"price\">¥" + Utils.ObjectToStr(dr["sell_price"]) + "</span>\r\n                            </p>\r\n                        </div>\r\n                    </a>\r\n                </li>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/购物商城-->\r\n\r\n<!--/视频专区-->\r\n<div class=\"section\">\r\n    <div class=\"main-tit\">\r\n        <h2>视频专区</h2>\r\n        <p>\r\n            ");
	DataTable videoCList = get_category_child_list("video",0);

	foreach(DataRow dr in videoCList.Rows)
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("video_list",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">更多<i>+</i></a>\r\n        </p>\r\n    </div>\r\n    <div class=\"wrapper clearfix\">\r\n        <div class=\"wrap-box\">\r\n            <div class=\"left-455\" style=\"margin:0;\">\r\n                <div class=\"side-img-box\">\r\n                    ");
	DataTable focusVideo = get_article_list("video", 0, 1, "status=0 and is_slide=1");

	foreach(DataRow dr in focusVideo.Rows)
	{

	templateBuilder.Append("\r\n                    <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("video_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                        <em><i class=\"iconfont icon-play\"></i></em>\r\n                        <div class=\"abs-bg\"></div>\r\n                        <div class=\"info\">\r\n                            <h3>" + Utils.ObjectToStr(dr["title"]) + "</h3>\r\n                            <p>" + Utils.ObjectToStr(dr["sub_title"]) + "</p>\r\n                        </div>\r\n                        <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n                    </a>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                </div>\r\n            </div>\r\n            <div class=\"left-690\">\r\n                <ul class=\"img-list\">\r\n                ");
	DataTable redVideo = get_article_list("video", 0, 6, "status=0 and is_red=1");

	foreach(DataRow dr in redVideo.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("video_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                            <div class=\"img-box\"><img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" /></div>\r\n                            <em><i class=\"iconfont icon-play\"></i></em>\r\n                            <div class=\"abs-bg\"></div>\r\n                            <div class=\"remark\">\r\n                                <h3>" + Utils.ObjectToStr(dr["title"]) + "</h3>\r\n                                <p>" + Utils.ObjectToStr(dr["sub_title"]) + "</p>\r\n                            </div>\r\n                        </a>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/视频专区-->\r\n\r\n<!--/图片分享-->\r\n<div class=\"section\">\r\n    <div class=\"main-tit\">\r\n        <h2>图片分享</h2>\r\n        <p>\r\n            ");
	DataTable photoCList = get_category_child_list("photo",0);

	foreach(DataRow dr in photoCList.Rows)
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("photo_list",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">更多<i>+</i></a>\r\n        </p>\r\n    </div>\r\n    <div class=\"wrapper clearfix\">\r\n        <div class=\"wrap-box\">\r\n            <div class=\"left-455\" style=\"margin:0;\">\r\n                <div class=\"side-img-box\">\r\n                ");
	DataTable focusPhoto = get_article_list("photo", 0, 1, "status=0 and is_slide=1");

	foreach(DataRow dr in focusPhoto.Rows)
	{

	templateBuilder.Append("\r\n                    <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("photo_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                        <div class=\"abs-bg\"></div>\r\n                        <div class=\"info\">\r\n                            <h3>" + Utils.ObjectToStr(dr["title"]) + "</h3>\r\n                            <p>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(dr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</p>\r\n                        </div>\r\n                        <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n                    </a>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </div>\r\n            </div>\r\n            <div class=\"left-690\">\r\n                <ul class=\"img-list\">\r\n                ");
	DataTable redPhoto = get_article_list("photo", 0, 6, "status=0 and is_red=1");

	foreach(DataRow dr in redPhoto.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("photo_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                            <div class=\"img-box\"><img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" /></div>\r\n                            <div class=\"abs-bg\"></div>\r\n                            <div class=\"remark\">\r\n                                <h3>" + Utils.ObjectToStr(dr["title"]) + "</h3>\r\n                                <p>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(dr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</p>\r\n                            </div>\r\n                        </a>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/图片分享-->\r\n\r\n<!--/资源下载-->\r\n<div class=\"section\">\r\n    <div class=\"main-tit\">\r\n        <h2>资源下载</h2>\r\n        <p>\r\n            ");
	DataTable downCList = get_category_child_list("down",0);

	foreach(DataRow dr in downCList.Rows)
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("down_list",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">更多<i>+</i></a>\r\n        </p>\r\n    </div>\r\n    <div class=\"wrapper clearfix\">\r\n        <div class=\"wrap-box\">\r\n            <ul class=\"img-list\">\r\n            ");
	DataTable redDown = get_article_list("down", 0, 5, "status=0 and is_red=1");

	foreach(DataRow dr in redDown.Rows)
	{

	templateBuilder.Append("\r\n                <li>\r\n                    <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                        <div class=\"img-box\"><img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" /></div>\r\n                        <div class=\"info\">\r\n                            <h3>" + Utils.ObjectToStr(dr["title"]) + "</h3>\r\n                            <p>\r\n                                <strong>下载 <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_attach_count&channel_id=" + Utils.ObjectToStr(dr["channel_id"]) + "&id=" + Utils.ObjectToStr(dr["id"]) + "&view=count\"></");
	templateBuilder.Append("script></strong>\r\n                                <span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(dr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span>\r\n                            </p>\r\n                        </div>\r\n                    </a>\r\n                </li>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/资源下载-->\r\n\r\n<!--留言链接-->\r\n<div class=\"section\">\r\n    <div class=\"wrapper clearfix\">\r\n        <div class=\"left-690 side-link-wrap\" style=\"margin:0;\">\r\n            <div class=\"main-tit\">\r\n                <h2>友情链接</h2>\r\n                <p>\r\n                    <a href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">更多<i>+</i></a>\r\n                </p>\r\n            </div>\r\n            <div class=\"side-link clearfix\">\r\n                <ul class=\"img\">\r\n                ");
	DataTable linkImg = get_plugin_method("DTcms.Web.Plugin.Link", "link", "get_link_list", 6, "site_id="+site.id+" and is_lock=0 and is_image=1 and is_red=1");

	foreach(DataRow dr in linkImg.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"" + Utils.ObjectToStr(dr["site_url"]) + "\" target=\"_blank\">\r\n                            <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\">\r\n                        </a>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n                <div class=\"txt\">\r\n                ");
	DataTable linkTxt = get_plugin_method("DTcms.Web.Plugin.Link", "link", "get_link_list", 0, "site_id="+site.id+" and is_lock=0 and is_image=0 and is_red=1");

	foreach(DataRow dr in linkTxt.Rows)
	{

	templateBuilder.Append("\r\n                    <a href=\"" + Utils.ObjectToStr(dr["site_url"]) + "\" target=\"_blank\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n                    <strong>|</strong>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </div>\r\n            </div>\r\n        </div>\r\n        <div class=\"left-455\">\r\n            <div class=\"main-tit\">\r\n                <h2>留言反馈</h2>\r\n                <p>\r\n                    <a href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">更多<i>+</i></a>\r\n                </p>\r\n            </div>\r\n            <div class=\"side-book\">\r\n                <ul>\r\n                ");
	DataTable backList = get_plugin_method("DTcms.Web.Plugin.Feedback", "feedback", "get_feedback_list", 4, "is_lock=0 and site_id="+site.id);

	foreach(DataRow dr in backList.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(dr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</span>\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n<!--留言链接-->\r\n\r\n<!--页面底部-->\r\n");

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
