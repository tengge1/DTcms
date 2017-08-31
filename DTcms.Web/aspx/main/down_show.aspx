<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.article_show" ValidateRequest="false" %>
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

	base.channel = "down";
	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n");
	string category_title = get_category_title(model.category_id,"资源下载");

	templateBuilder.Append("\r\n<title>");
	templateBuilder.Append(Utils.ObjectToStr(model.title));
	templateBuilder.Append(" - ");
	templateBuilder.Append(Utils.ObjectToStr(category_title));
	templateBuilder.Append(" - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(model.seo_keywords));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(model.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\">\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body id=\"down\">\r\n<!--页面头部-->\r\n");

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


	templateBuilder.Append("\r\n<!--/页面头部-->\r\n\r\n<!--当前位置-->\r\n");
	string category_nav = get_category_menu("down_list", model.category_id);

	templateBuilder.Append("\r\n<div class=\"section\">\r\n    <div class=\"location\">\r\n        <span>当前位置：</span>\r\n        <a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a> &gt;\r\n        <a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">资源下载</a>\r\n        ");
	templateBuilder.Append(Utils.ObjectToStr(category_nav));
	templateBuilder.Append("\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<div class=\"section\">\r\n    <!--/页面右边-->\r\n    <div class=\"right-260\">\r\n        <div class=\"bg-wrap nobg\">\r\n            <div class=\"sidebar-box\">\r\n                <h4>人气排行</h4>\r\n                <ul class=\"txt-list\">\r\n                ");
	DataTable hotDown = get_article_list(channel, 0, 10, "status=0", "click desc,id desc");

	int hotdr__loop__id=0;
	foreach(DataRow hotdr in hotDown.Rows)
	{
		hotdr__loop__id++;


	templateBuilder.Append("\r\n                    <li>\r\n                        ");
	if (hotdr__loop__id==1)
	{

	templateBuilder.Append("\r\n                        <label class=\"hot\">");
	templateBuilder.Append(Utils.ObjectToStr(hotdr__loop__id));
	templateBuilder.Append("</label>\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                        <label>");
	templateBuilder.Append(Utils.ObjectToStr(hotdr__loop__id));
	templateBuilder.Append("</label>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(hotdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(hotdr["title"]) + "</a>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            \r\n            <div class=\"sidebar-box\">\r\n                <h4>推荐资源</h4>\r\n                <ul class=\"side-img-list\">\r\n                ");
	DataTable redDown = get_article_list(channel, 0, 5, "status=0 and is_red=1");

	foreach(DataRow dr in redDown.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <div class=\"img-box\">\r\n                            <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                                <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n                            </a>\r\n                        </div>\r\n                        <div class=\"txt-box\">\r\n                            <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a>\r\n                            <span>" + Utils.ObjectToStr(dr["add_time"]) + "</span>\r\n                        </div>\r\n                    </li>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            \r\n        </div>\r\n    </div>\r\n    <!--/页面右边-->\r\n    \r\n    <!--页面左边-->\r\n    <div class=\"left-auto\">\r\n        <div class=\"bg-wrap\">\r\n            <div class=\"meta\">\r\n                <h2>");
	templateBuilder.Append(Utils.ObjectToStr(model.title));
	templateBuilder.Append("</h2>\r\n                <div class=\"info\">\r\n                    <span><i class=\"iconfont icon-date\"></i>");
	templateBuilder.Append(Utils.ObjectToStr(model.add_time));
	templateBuilder.Append("</span>\r\n                    <span><i class=\"iconfont icon-comment\"></i><script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_comment_count&channel_id=");
	templateBuilder.Append(Utils.ObjectToStr(model.channel_id));
	templateBuilder.Append("&id=");
	templateBuilder.Append(Utils.ObjectToStr(model.id));
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>人评论</span>\r\n                    <span><i class=\"iconfont icon-view\"></i><script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_article_click&channel_id=");
	templateBuilder.Append(Utils.ObjectToStr(model.channel_id));
	templateBuilder.Append("&id=");
	templateBuilder.Append(Utils.ObjectToStr(model.id));
	templateBuilder.Append("&click=1\"></");
	templateBuilder.Append("script>次</span>\r\n                </div>\r\n                <div class=\"note\">\r\n                    <p>");
	templateBuilder.Append(Utils.ObjectToStr(model.zhaiyao));
	templateBuilder.Append("</p>\r\n                </div>\r\n            </div>\r\n            \r\n            <div class=\"entry\">\r\n                ");
	templateBuilder.Append(Utils.ObjectToStr(model.content));
	templateBuilder.Append("\r\n            </div>\r\n            \r\n            <div class=\"attach-list clearfix\">\r\n                <h2 class=\"slide-tit\">\r\n                    <span>附件下载</span>\r\n                </h2>\r\n                <ul>\r\n                ");
	if (model.attach!=null)
	{

	foreach(DTcms.Model.article_attach modelt in model.attach)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <a class=\"link-btn\" href=\"javascript:;\" onclick=\"downLink(");
	templateBuilder.Append(Utils.ObjectToStr(modelt.point));
	templateBuilder.Append(",'");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/download.ashx?site=");
	templateBuilder.Append(Utils.ObjectToStr(site.build_path));
	templateBuilder.Append("&id=");
	templateBuilder.Append(Utils.ObjectToStr(modelt.id));
	templateBuilder.Append("');\">\r\n                            <i class=\"iconfont icon-down\"></i>下载\r\n                        </a>\r\n                        <div class=\"icon-box\">\r\n                            <i class=\"iconfont icon-attach\"></i>\r\n                        </div>\r\n                        <div class=\"info\">\r\n                            <h3>");
	templateBuilder.Append(Utils.ObjectToStr(modelt.file_name));
	templateBuilder.Append("</h3>\r\n                            <p>\r\n                                <span>文件类型：");
	templateBuilder.Append(Utils.ObjectToStr(modelt.file_ext));
	templateBuilder.Append("</span>\r\n                                <span>\r\n                                    大小：\r\n                                    ");
	if (modelt.file_size>1024)
	{

	                                            string tempSize = (modelt.file_size/1024f).ToString("#.##");
	                                        

	templateBuilder.Append("\r\n                                        ");
	templateBuilder.Append(Utils.ObjectToStr(tempSize));
	templateBuilder.Append("MB\r\n                                    ");
	}
	else
	{

	templateBuilder.Append("\r\n                                        ");
	templateBuilder.Append(Utils.ObjectToStr(modelt.file_size));
	templateBuilder.Append("KB\r\n                                    ");
	}	//end for if

	templateBuilder.Append("\r\n                                </span>\r\n                                <span>下载：<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_attach_count&id=");
	templateBuilder.Append(Utils.ObjectToStr(modelt.id));
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>次</span>\r\n                                <span>所需积分：");
	templateBuilder.Append(Utils.ObjectToStr(modelt.point));
	templateBuilder.Append("</span>\r\n                            </p>\r\n                        </div>\r\n                    </li>\r\n                ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            \r\n            <!--上下一条-->\r\n            <div class=\"next-box clearfix\">\r\n                <p class=\"prev\">上一篇：");
	templateBuilder.Append(get_prevandnext_article("down_show", -1, "没有了", 0).ToString());

	templateBuilder.Append("</p>\r\n                <p class=\"next\">下一篇：");
	templateBuilder.Append(get_prevandnext_article("down_show", 1, "没有了", 0).ToString());

	templateBuilder.Append("</p>\r\n            </div>\r\n            <!--/上下一条-->\r\n            \r\n            <!--相关资讯-->\r\n            <div class=\"rel-box\">\r\n                <h2 class=\"slide-tit\">\r\n                    <span>相关资源</span>\r\n                </h2>\r\n                <ul class=\"rel-list\">\r\n                ");
	DataTable relList = get_article_list(channel, model.category_id, 4, "is_red=1 and id<>"+model.id);

	foreach(DataRow dr in relList.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <div class=\"img-box\">\r\n                            <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                                <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n                            </a>\r\n                      </div>\r\n                      <div class=\"info\">\r\n                            <h3><a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a></h3>\r\n                            <p>" + Utils.ObjectToStr(dr["zhaiyao"]) + "</p>\r\n                            <span>" + Utils.ObjectToStr(dr["add_time"]) + "</span>\r\n                        </div>\r\n                    </li>\r\n                ");
	}	//end for if

	if (relList.Rows.Count<1)
	{

	templateBuilder.Append("\r\n                    <div class=\"nodata\">暂无相关数据...</div>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n            </div>\r\n            <!--/相关资讯-->\r\n            \r\n            <!--网友评论-->\r\n            ");
	if (model.is_msg==1)
	{

	templateBuilder.Append("\r\n            <div class=\"comment-box\">\r\n                <h2 class=\"slide-tit\">\r\n                    <strong>共有<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_comment_count&channel_id=");
	templateBuilder.Append(Utils.ObjectToStr(model.channel_id));
	templateBuilder.Append("&id=");
	templateBuilder.Append(Utils.ObjectToStr(model.id));
	templateBuilder.Append("\"></");
	templateBuilder.Append("script>条评论</strong>\r\n                    <span>网友评论</span>\r\n                </h2>\r\n                ");

	int comment_count = get_comment_count(model.channel_id, model.id, "is_lock=0");

	templateBuilder.Append("<!--取得评论总数-->\r\n                <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n                <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n                <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n                <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n                <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n                <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n                <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.pagination.js\"></");
	templateBuilder.Append("script>\r\n                <script type=\"text/javascript\">\r\n                    $(function(){\r\n                        //初始化评论列表\r\n                        pageInitComment();\r\n                        //初始化发表评论表单\r\n                        AjaxInitForm('#commentForm', '#btnSubmit', 1, '', pageInitComment);\r\n                    });\r\n                    //初始化评论列表\r\n                    function pageInitComment(){\r\n                        AjaxPageList('#commentList', '#pagination', 10, ");
	templateBuilder.Append(Utils.ObjectToStr(comment_count));
	templateBuilder.Append(", '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=comment_list&channel_id=");
	templateBuilder.Append(Utils.ObjectToStr(model.channel_id));
	templateBuilder.Append("&article_id=");
	templateBuilder.Append(Utils.ObjectToStr(model.id));
	templateBuilder.Append("');\r\n                    }\r\n                </");
	templateBuilder.Append("script>\r\n                <form id=\"commentForm\" name=\"commentForm\" class=\"form-box\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=comment_add&channel_id=");
	templateBuilder.Append(Utils.ObjectToStr(model.channel_id));
	templateBuilder.Append("&article_id=");
	templateBuilder.Append(Utils.ObjectToStr(model.id));
	templateBuilder.Append("\">\r\n                    <div class=\"avatar-box\">\r\n                        ");
	bool isLogin = IsUserLogin();

	if (isLogin==true)
	{

	string userAvatar = GetUserInfo().avatar;

	if (userAvatar!="")
	{

	templateBuilder.Append("\r\n                            <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userAvatar));
	templateBuilder.Append("\" />\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                            <i class=\"iconfont icon-user-full\"></i>\r\n                            ");
	}	//end for if

	}
	else
	{

	templateBuilder.Append("\r\n                            <i class=\"iconfont icon-user-full\"></i>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                    </div>\r\n                    <div class=\"conn-box\">\r\n                        <div class=\"editor\">\r\n                            <textarea id=\"txtContent\" name=\"txtContent\" sucmsg=\" \" datatype=\"*10-1000\" nullmsg=\"请填写评论内容！\"></textarea>\r\n                        </div>\r\n                        <div class=\"subcon\">\r\n                            <input id=\"btnSubmit\" name=\"submit\" type=\"submit\" value=\"提交评论\"class=\"submit\" />\r\n                            <strong>验证码：</strong>\r\n                            <input id=\"txtCode\" name=\"txtCode\" class=\"code\" onkeydown=\"if(event.ctrlKey&amp;&amp;event.keyCode==13){document.getElementById('btnSubmit').click();return false};\" type=\"text\" sucmsg=\" \" datatype=\"s4-4\" errormsg=\"请填写4位验证码\" nullmsg=\"请填写验证码！\" />\r\n                            <a href=\"javascript:;\" onclick=\"ToggleCode(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx');return false;\">\r\n                                <img width=\"80\" height=\"22\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx\"> 看不清楚？\r\n                            </a>\r\n                        </div>\r\n                    </div>\r\n                </form>\r\n                \r\n                <ul id=\"commentList\" class=\"list-box\"></ul>\r\n                \r\n                <!--放置页码-->\r\n                <div class=\"page-box\" style=\"margin:5px 0 0 62px\">\r\n                    <div id=\"pagination\" class=\"digg\"></div>\r\n                </div>\r\n                <!--/放置页码-->");


	templateBuilder.Append("\r\n            </div>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <!--/网友评论-->\r\n            \r\n        </div>\r\n    </div>\r\n    <!--/页面左边-->\r\n</div>\r\n\r\n<!--页面底部-->\r\n");

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
