<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.repassword" ValidateRequest="false" %>
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

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>取回密码 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\">\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
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

	templateBuilder.Append("\">首页</a> &gt;\r\n        <a href=\"javascript:;\">取回密码</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<!--导航推荐-->\r\n<div class=\"section\">\r\n    <div class=\"wrapper\">\r\n        <div class=\"bg-wrap\">\r\n            <div class=\"nav-tit\">\r\n                <a class=\"selected\" href=\"javascript:;\">取回密码</a>\r\n                <i>|</i>\r\n                <a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">账户登录</a>\r\n            </div>\r\n            \r\n            <!--取回密码-->\r\n            ");
	if (action=="")
	{

	templateBuilder.Append("\r\n            <script type=\"text/javascript\">\r\n                $(function(){\r\n                    //初始化表单\r\n                    AjaxInitForm('#pwdForm', '#btnSubmit', 1);\r\n                });\r\n            </");
	templateBuilder.Append("script>\r\n            <form id=\"pwdForm\" name=\"pwdForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_getpassword&site_id=");
	templateBuilder.Append(Utils.ObjectToStr(site.id));
	templateBuilder.Append("\">\r\n                <div class=\"form-box full\">\r\n                    <dl class=\"form-group\">\r\n                        <dt>取回方式：</dt>\r\n                        <dd>\r\n                            <label class=\"mart\"><input name=\"txtType\" type=\"radio\" value=\"mobile\" checked=\"checked\" /> 手机短信</label>\r\n                            <label class=\"mart\"><input name=\"txtType\" type=\"radio\" value=\"email\" datatype=\"*\" sucmsg=\" \" /> 电子邮箱</label>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>用 户 名：</dt>\r\n                        <dd>\r\n                            <input id=\"txtUserName\" name=\"txtUserName\" type=\"text\" class=\"input\" datatype=\"*1-50\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>验 证 码：</dt>\r\n                        <dd>\r\n                            <input id=\"txtCode\" name=\"txtCode\" type=\"text\" class=\"input code\" placeholder=\"输入验证码\" datatype=\"s4-20\" nullmsg=\"请输入右边显示的验证码\" sucmsg=\" \" />\r\n                            <a class=\"send\" href=\"javascript:;\" onclick=\"ToggleCode(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx');return false;\">\r\n                              <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx\" width=\"80\" height=\"22\" />\r\n                            </a>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dd>\r\n                            <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认提交\" class=\"submit\" />\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n            </form>\r\n            <!--取回密码-->\r\n            \r\n            <!--手机取回密码-->\r\n            ");
	}
	else if (action=="mobile")
	{

	templateBuilder.Append("\r\n            <script type=\"text/javascript\">\r\n                $(function(){\r\n                    //初始化表单\r\n                    AjaxInitForm('#pwdForm', '#btnSubmit', 1, '#turl');\r\n                });\r\n            </");
	templateBuilder.Append("script>\r\n            <form id=\"pwdForm\" name=\"pwdForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_repassword&site_id=");
	templateBuilder.Append(Utils.ObjectToStr(site.id));
	templateBuilder.Append("\">\r\n                <div class=\"form-box full\">\r\n                    <dl class=\"form-group\">\r\n                        <dt>短信验证码：</dt>\r\n                        <dd>\r\n                            <input name=\"hideCode\" id=\"txtTelphone\" type=\"text\" class=\"input\" datatype=\"*\" />\r\n                            <span class=\"Validform_checktip\">输入您手机收到的验证码</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>新 密 码：</dt>\r\n                        <dd>\r\n                            <input name=\"txtPassword\" id=\"txtPassword\" type=\"password\" class=\"input\" datatype=\"*6-20\" nullmsg=\"请输入新密码\" errormsg=\"密码范围在6-20位之间\" sucmsg=\" \" />\r\n                            <span class=\"Validform_checktip\">重新设置新的密码</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>确认新密码：</dt>\r\n                        <dd>\r\n                            <input name=\"txtPassword1\" id=\"txtPassword1\" type=\"password\" class=\"input\" datatype=\"*\" recheck=\"txtPassword\" nullmsg=\"请再输入一次新密码\" errormsg=\"两次输入的密码不一致\" sucmsg=\" \" />\r\n                            <span class=\"Validform_checktip\">再次输入新的密码</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dd>\r\n                            <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认提交\" class=\"submit\" />\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n            </form>\r\n            <input id=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\" />\r\n            <!--/手机取回密码-->\r\n            \r\n            <!--邮箱取回密码-->\r\n            ");
	}
	else if (action=="email")
	{

	templateBuilder.Append("\r\n            <script type=\"text/javascript\">\r\n                $(function(){\r\n                    //初始化表单\r\n                    AjaxInitForm('#pwdForm', '#btnSubmit', 1, '#turl');\r\n                });\r\n            </");
	templateBuilder.Append("script>\r\n            <form id=\"pwdForm\" name=\"pwdForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_repassword&site_id=");
	templateBuilder.Append(Utils.ObjectToStr(site.id));
	templateBuilder.Append("\">\r\n                <div class=\"form-box full\">\r\n                    <dl class=\"form-group\">\r\n                        <dt>用户名：</dt>\r\n                        <dd>\r\n                            ");
	templateBuilder.Append(Utils.ObjectToStr(username));
	templateBuilder.Append("\r\n                            <input name=\"hideCode\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(code));
	templateBuilder.Append("\" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>新密码：</dt>\r\n                        <dd>\r\n                            <input name=\"txtPassword\" id=\"txtPassword\" type=\"password\" class=\"input\" datatype=\"*6-20\" nullmsg=\"请输入新密码\" errormsg=\"密码范围在6-20位之间\" sucmsg=\" \" />\r\n                            <span class=\"Validform_checktip\">重新设置新的密码</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>确认新密码：</dt>\r\n                        <dd>\r\n                            <input name=\"txtPassword1\" id=\"txtPassword1\" type=\"password\" class=\"input\" datatype=\"*\" recheck=\"txtPassword\" nullmsg=\"请再输入一次新密码\" errormsg=\"两次输入的密码不一致\" sucmsg=\" \" />\r\n                            <span class=\"Validform_checktip\">再次输入新的密码</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dd>\r\n                            <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认提交\" class=\"submit\" />\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n            </form>\r\n            <input id=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\" />\r\n            <!--/邮箱取回密码-->\r\n            \r\n            <!--错误界面-->\r\n            ");
	}
	else if (action=="error")
	{

	templateBuilder.Append("\r\n            <div class=\"msg-tips\">\r\n                <div class=\"icon error\">\r\n                    <i class=\"iconfont icon-error\"></i>\r\n                </div>\r\n                <div class=\"info\">\r\n                    <strong>出错了，该用户不存在或验证已过期！</strong>\r\n                    <p>无法验证你的账户，不知神马原因，可能是你的用户名不存在或者验证码已经过期！</p>\r\n                    <p>不过别担心，如果您还记得你的会员名称的话，点击这里<a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">进入会员中心</a>吧。</p>\r\n                </div>\r\n            </div>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <!--/错误界面-->\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<!--页面底部-->\r\n");

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
