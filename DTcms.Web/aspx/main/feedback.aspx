<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.Plugin.Feedback.feedback" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by DTcms Template Engine at 2017/6/9 21:43:31.
		本页面代码由DTcms模板引擎生成于 2017/6/9 21:43:31. 
	*/

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>留言反馈 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" name=\"keywords\" />\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" name=\"description\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n$(function(){\r\n	//初始化发表评论表单\r\n	AjaxInitForm('#feedback_form', '#btnSubmit', 1);\r\n});\r\n</");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body>\r\n<!--页面头部-->\r\n");

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

	templateBuilder.Append("\">首页</a> &gt;\r\n        <a href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">留言反馈</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<div class=\"section clearfix\">\r\n    <!--/页面右边-->\r\n    <div class=\"right-260\">\r\n        <div class=\"bg-wrap nobg\">\r\n            <div class=\"sidebar-box\">\r\n                <h4>栏目导航</h4>\r\n                <ul class=\"navbar\">\r\n                    ");
	DataTable contentlist = get_article_list("content", 0, 0, "status=0");

	foreach(DataRow dr in contentlist.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <h5><a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("content",Utils.ObjectToStr(dr["call_index"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a></h5>\r\n                    </li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                    <li>\r\n                        <h5><a href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">留言反馈</a></h5>\r\n                    </li>\r\n                    <li>\r\n                        <h5><a href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">友情链接</a></h5>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            \r\n            <div class=\"sidebar-box\">\r\n                <h4>联系我们</h4>\r\n                <ul class=\"txt-list\">\r\n                    <li>");
	templateBuilder.Append(Utils.ObjectToStr(site.company));
	templateBuilder.Append("</li>\r\n                    <li>地址：");
	templateBuilder.Append(Utils.ObjectToStr(site.address));
	templateBuilder.Append("</li>\r\n                    <li>电话：");
	templateBuilder.Append(Utils.ObjectToStr(site.tel));
	templateBuilder.Append("</li>\r\n                    <li>E-mail：");
	templateBuilder.Append(Utils.ObjectToStr(site.email));
	templateBuilder.Append("</li>\r\n                    <li>微信公众号：动力启航</li>\r\n                </ul>\r\n            </div>\r\n            \r\n        </div>\r\n    </div>\r\n    <!--/页面右边-->\r\n    \r\n    <!--页面左边-->\r\n    <div class=\"left-auto\">\r\n        <div class=\"bg-wrap\">\r\n            <div class=\"meta\">\r\n                <h2>留言反馈</h2>\r\n            </div>\r\n            \r\n            <!--内容列表-->\r\n            <div class=\"comment-box\">\r\n                <ul class=\"list-box\">\r\n                    ");
	DataTable feedbackList = new DTcms.Web.Plugin.Feedback.feedback().get_feedback_list(10, page, "", out totalcount);

	string pagelist = get_page_link(10, page, totalcount, "feedback", "__id__");

	foreach(DataRow dr in feedbackList.Rows)
	{

	templateBuilder.Append("\r\n                    <li>\r\n                        <div class=\"avatar-box\">\r\n                            <i class=\"iconfont icon-user-full\"></i>\r\n                        </div>\r\n                        <div class=\"inner-box\">\r\n                            <div class=\"info\">\r\n                                <span>" + Utils.ObjectToStr(dr["user_name"]) + "</span>\r\n                                <span>" + Utils.ObjectToStr(dr["add_time"]) + "</span>\r\n                            </div>\r\n                            <p>" + Utils.ObjectToStr(dr["content"]) + "</p>\r\n                        </div>\r\n                        \r\n                        ");
	if (Utils.ObjectToStr(dr["reply_content"])!="")
	{

	templateBuilder.Append("\r\n                        <div class=\"answer-box\">\r\n                            <div class=\"info\">\r\n                                <span class=\"right\">" + Utils.ObjectToStr(dr["reply_time"]) + "</span>\r\n                                <span>管理员回复：</span>\r\n                            </div>\r\n                            <p>" + Utils.ObjectToStr(dr["reply_content"]) + "</p>\r\n                        </div>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                    </li>\r\n                    ");
	}	//end for if

	if (totalcount==0)
	{

	templateBuilder.Append("\r\n                        <p style=\"margin:5px 0 15px;line-height:80px;text-align:center;border:1px solid #f7f7f7;\">暂无留言，快来发表留言吧！</p>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n                \r\n                <!--放置页码-->\r\n                <div class=\"page-box\" style=\"margin:5px 0 0 62px;\">\r\n                    <div id=\"pagination\" class=\"digg\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n                </div>\r\n                <!--/放置页码-->\r\n                \r\n                <h2 class=\"slide-tit\">\r\n                    <span>发表留言</span>\r\n                </h2>\r\n                <form id=\"feedback_form\" name=\"feedback_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/feedback/ajax.ashx?action=add&site=");
	templateBuilder.Append(Utils.ObjectToStr(site.id));
	templateBuilder.Append("\">\r\n                <div class=\"form-box\" style=\"margin:0 20px;\">\r\n                    <dl class=\"form-group\">\r\n                        <dt>用户昵称：</dt>\r\n                        <dd>\r\n                            <input id=\"txtUserName\" name=\"txtUserName\" type=\"text\" class=\"input\" datatype=\"*\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>联系电话：</dt>\r\n                        <dd>\r\n                            <input id=\"txtUserTel\" name=\"txtUserTel\" type=\"text\" class=\"input\" datatype=\"*0-20\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>在线QQ：</dt>\r\n                        <dd>\r\n                            <input id=\"txtUserQQ\" name=\"txtUserQQ\" type=\"text\" class=\"input\" datatype=\"*0-20\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>电子邮箱：</dt>\r\n                        <dd>\r\n                            <input id=\"txtUserEmail\" name=\"txtUserEmail\" type=\"text\" class=\"input\" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>留言标题：</dt>\r\n                        <dd>\r\n                            <input id=\"txtTitle\" name=\"txtTitle\" type=\"text\" class=\"input txt\" datatype=\"*2-100\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>留言内容：</dt>\r\n                        <dd>\r\n                            <textarea id=\"txtContent\" name=\"txtContent\" class=\"textarea\" datatype=\"*\" sucmsg=\" \" style=\"height:80px;\"></textarea>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>验 证 码：</dt>\r\n                        <dd>\r\n                            <input id=\"txtCode\" name=\"txtCode\" type=\"text\" class=\"input code\" placeholder=\"输入验证码\" datatype=\"s4-20\" nullmsg=\"请输入右边显示的验证码\" sucmsg=\" \" />\r\n                            <a href=\"javascript:;\" onclick=\"ToggleCode(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx');return false;\">\r\n                                <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx\" width=\"80\" height=\"22\" /> 看不清楚？\r\n                            </a>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                      <dd>\r\n                          <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"发表留言\" class=\"submit\" />\r\n                      </dd>\r\n                    </dl>\r\n                </div>\r\n                </form>\r\n            </div>\r\n            <!--/内容列表-->\r\n            \r\n        </div>\r\n    </div>\r\n    <!--/页面左边-->\r\n</div>\r\n\r\n<!--页面底部-->\r\n");

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
