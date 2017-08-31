<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.usercenter" ValidateRequest="false" %>
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

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>会员中心 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
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
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">会员中心</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<div class=\"section clearfix\">\r\n    <!--页面左边-->\r\n    ");

	templateBuilder.Append("    <div class=\"left-260\">\r\n        <div class=\"bg-wrap\">\r\n            <div class=\"avatar-box\">\r\n                <a class=\"img-box\" href=\"");
	templateBuilder.Append(linkurl("usercenter","avatar"));

	templateBuilder.Append("\">\r\n                ");
	if (userModel.avatar!="")
	{

	templateBuilder.Append("\r\n                    <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.avatar));
	templateBuilder.Append("\" />\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                    <i class=\"iconfont icon-user-full\"></i>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </a>\r\n                <h3>\r\n                ");
	if (userModel.nick_name!="")
	{

	templateBuilder.Append("\r\n                    ");
	templateBuilder.Append(Utils.ObjectToStr(userModel.nick_name));
	templateBuilder.Append("\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                    ");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </h3>\r\n                <p><b>");
	templateBuilder.Append(Utils.ObjectToStr(groupModel.title));
	templateBuilder.Append("</b></p>\r\n            </div>\r\n            \r\n            <div class=\"center-nav\">\r\n                <ul>\r\n                    <li>\r\n                        <h2>\r\n                            <i class=\"iconfont icon-order\"></i>\r\n                            <span>订单管理</span>\r\n                        </h2>\r\n                        <div class=\"list\">\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("userorder","list"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>交易订单</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("userorder","close"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>失效订单</a></p>\r\n                        </div>\r\n                    </li>\r\n                    <li>\r\n                        <h2>\r\n                            <i class=\"iconfont icon-amount\"></i>\r\n                            <span>余额管理</span>\r\n                        </h2>\r\n                        <div class=\"list\">\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("useramount","recharge"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>账户充值</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("useramount","log"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>充值记录</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("useramount","list"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>收支明细</a></p>\r\n                        </div>\r\n                    </li>\r\n                    <li>\r\n                        <h2>\r\n                            <i class=\"iconfont icon-point\"></i>\r\n                            <span>积分管理</span>\r\n                        </h2>\r\n                        <div class=\"list\">\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("userpoint","convert"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>积分兑换</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("userpoint","list"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>积分明细</a></p>\r\n                        </div>\r\n                    </li>\r\n                    <li>\r\n                        <h2>\r\n                            <i class=\"iconfont icon-comment\"></i>\r\n                            <span>站内消息</span>\r\n                        </h2>\r\n                        <div class=\"list\">\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("usermessage","system"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>系统消息</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("usermessage","accept"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>收件箱</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("usermessage","send"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>发件箱</a></p>\r\n                        </div>\r\n                    </li>\r\n                    <li>\r\n                        <h2>\r\n                            <i class=\"iconfont icon-user\"></i>\r\n                            <span>账户管理</span>\r\n                        </h2>\r\n                        <div class=\"list\">\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("usercenter","invite"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>邀请注册</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("usercenter","proinfo"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>账户资料</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("usercenter","avatar"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>头像设置</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("usercenter","password"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>修改密码</a></p>\r\n                            <p><a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\"><i class=\"iconfont icon-arrow-right\"></i>退出登录</a></p>\r\n                        </div>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n                \r\n        </div>\r\n    </div>");


	templateBuilder.Append("\r\n    <!--/页面左边-->\r\n    \r\n    <!--页面左边-->\r\n    <div class=\"right-auto\">\r\n        <div class=\"bg-wrap\" style=\"min-height:765px;\">\r\n            \r\n            ");
	if (action=="index")
	{

	templateBuilder.Append("\r\n            <!--会员中心-->\r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"javascript:history.go(-1);\"><i class=\"iconfont icon-reply\"></i>返回</a>\r\n                <ul>\r\n                    <li class=\"selected\">\r\n                        <a href=\"javascript:;\">个人中心</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            \r\n            <div class=\"center-head clearfix\">\r\n                <div class=\"img-box\">\r\n                ");
	if (userModel.avatar!="")
	{

	templateBuilder.Append("\r\n                    <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.avatar));
	templateBuilder.Append("\" />\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                    <i class=\"iconfont icon-user-full\"></i>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </div>\r\n                <div class=\"list-box\">\r\n                    <h3>欢迎您~ ");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("</h3>\r\n                    <ul>\r\n                        <li>组别：");
	templateBuilder.Append(Utils.ObjectToStr(groupModel.title));
	templateBuilder.Append("</li>\r\n                        <li>账户余额：￥");
	templateBuilder.Append(Utils.ObjectToStr(userModel.amount));
	templateBuilder.Append("</li>\r\n                        <li><p><a href=\"#\">消费记录</a></p></li>\r\n                        <li>账户成长值：");
	templateBuilder.Append(Utils.ObjectToStr(userModel.exp));
	templateBuilder.Append("</li>\r\n                        <li>账户积分：");
	templateBuilder.Append(Utils.ObjectToStr(userModel.point));
	templateBuilder.Append("</li>\r\n                        <li><p><a class=\"link-btn\" href=\"");
	templateBuilder.Append(linkurl("useramount","recharge"));

	templateBuilder.Append("\">立即充值</a></p></li>\r\n                    </ul>\r\n                </div>\r\n            </div>\r\n            <div class=\"center-info clearfix\">\r\n                <ul>\r\n                    <li>本次登录IP：");
	templateBuilder.Append(Utils.ObjectToStr(curr_login_ip));
	templateBuilder.Append("</li>\r\n                    <li>上次登录IP：");
	templateBuilder.Append(Utils.ObjectToStr(pre_login_ip));
	templateBuilder.Append("</li>\r\n                    <li>注册时间：");
	templateBuilder.Append(Utils.ObjectToStr(userModel.reg_time));
	templateBuilder.Append("</li>\r\n                    <li>上次登录时间：");
	templateBuilder.Append(Utils.ObjectToStr(pre_login_time));
	templateBuilder.Append("</li>\r\n                </ul>\r\n            </div>\r\n            \r\n            <div class=\"center-tit\">\r\n                <span><a href=\"");
	templateBuilder.Append(linkurl("userorder","list"));

	templateBuilder.Append("\">更多..</a></span>\r\n                <h3><i class=\"iconfont icon-order\"></i>我的订单</h3>\r\n            </div>\r\n            <div class=\"center-info clearfix\">\r\n                <ul>\r\n                    <li>已完成订单：");
	templateBuilder.Append(get_user_order_count("status=3 and user_id="+userModel.id).ToString());

	templateBuilder.Append("个</li>\r\n                    <li>待完成订单：");
	templateBuilder.Append(get_user_order_count("status<3 and user_id="+userModel.id).ToString());

	templateBuilder.Append("个</li>\r\n                </ul>\r\n            </div>\r\n            <div class=\"center-tit\">\r\n                <span><a href=\"");
	templateBuilder.Append(linkurl("usermessage","accept"));

	templateBuilder.Append("\">更多..</a></span>\r\n                <h3><i class=\"iconfont icon-comment\"></i>站内消息</h3>\r\n            </div>\r\n            \r\n            <div class=\"table-wrap\">\r\n                <table width=\"100%\" class=\"mtable\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n                <tbody>\r\n                ");
	DataTable messageList = get_user_message_list(10, "accept_user_name='"+userModel.user_name+"' and (type=1 or type=2)");

	foreach(DataRow dr in messageList.Rows)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td><a href=\"");
	templateBuilder.Append(linkurl("usermessage_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a> (发件人：" + Utils.ObjectToStr(dr["post_user_name"]) + ")</td>\r\n                        <td width=\"80\">\r\n                        ");
	if (Utils.ObjectToStr(dr["is_read"])=="1")
	{

	templateBuilder.Append("\r\n                            已读\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                            未读\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                        </td>\r\n                        <td width=\"150\">" + Utils.ObjectToStr(dr["post_time"]) + "</td>\r\n                    </tr>\r\n                ");
	}	//end for if

	if (messageList.Rows.Count==0)
	{

	templateBuilder.Append("\r\n                    <tr><td align=\"center\">暂无短消息...</td></tr>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </tbody>\r\n                </table>\r\n            </div>\r\n            <!--会员中心-->\r\n        \r\n            ");
	}
	else if (action=="password")
	{

	templateBuilder.Append("\r\n            <!--修改密码-->\r\n            <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\">\r\n            $(function(){\r\n                //初始化表单\r\n                AjaxInitForm('#pwdForm', '#btnSubmit', 1);\r\n            });\r\n            </");
	templateBuilder.Append("script>\r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\"><i class=\"iconfont icon-reply\"></i>返回</a>\r\n                <ul>\r\n                    <li class=\"selected\">\r\n                        <a href=\"javascript:;\">修改密码</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            \r\n            <form name=\"pwdForm\" id=\"pwdForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_password_edit\">\r\n                <div class=\"form-box\">\r\n                    <dl class=\"form-group\">\r\n                        <dt>用户名：</dt>\r\n                        <dd>");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("</dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>旧密码：</dt>\r\n                        <dd>\r\n                            <input name=\"txtOldPassword\" id=\"txtOldPassword\" type=\"password\" class=\"input\" datatype=\"*6-20\" nullmsg=\"请输入旧密码\" errormsg=\"密码范围在6-20位之间\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>新密码：</dt>\r\n                        <dd>\r\n                            <input name=\"txtPassword\" id=\"txtPassword\" type=\"password\" class=\"input\" datatype=\"*6-20\" nullmsg=\"请输入新密码\" errormsg=\"密码范围在6-20位之间\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>确认新密码：</dt>\r\n                        <dd>\r\n                            <input name=\"txtPassword1\" id=\"txtPassword1\" type=\"password\" class=\"input\" datatype=\"*\" recheck=\"txtPassword\" nullmsg=\"请再输入一次新密码\" errormsg=\"两次输入的密码不一致\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dd>\r\n                            <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认修改\" class=\"submit\" />\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n            </form>\r\n            <!--/修改密码-->\r\n            \r\n            ");
	}
	else if (action=="proinfo")
	{

	templateBuilder.Append("\r\n            <!--修改资料-->\r\n            <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n            <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/PCASClass.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/datepicker/WdatePicker.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\">\r\n            $(function(){\r\n                //初始化表单\r\n                AjaxInitForm('#infoForm', '#btnSubmit', 1);\r\n                //初始化地区\r\n                var mypcas = new PCAS(\"txtProvince,所属省份\", \"txtCity,所属城市\", \"txtArea,所属地区\");\r\n                var areaArr = (\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.area));
	templateBuilder.Append("\").split(\",\");\r\n                if (areaArr.length == 3) {\r\n                  mypcas.SetValue(areaArr[0], areaArr[1], areaArr[2]);\r\n                }\r\n            });\r\n            </");
	templateBuilder.Append("script>\r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\"><i class=\"iconfont icon-reply\"></i>返回</a>\r\n                <ul>\r\n                    <li class=\"selected\">\r\n                        <a href=\"javascript:;\">账户资料</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            <form name=\"infoForm\" id=\"infoForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_info_edit\">\r\n                <div class=\"form-box\">\r\n                    <dl class=\"form-group\">\r\n                        <dt>用户名：</dt>\r\n                        <dd>\r\n                            ");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>昵称：</dt>\r\n                        <dd>\r\n                            <input name=\"txtNickName\" id=\"txtNickName\" type=\"text\" class=\"input\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.nick_name));
	templateBuilder.Append("\" datatype=\"*\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>性别：</dt>\r\n                        <dd>\r\n                        ");
	if (userModel.sex=="男")
	{

	templateBuilder.Append("\r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"男\" checked=\"checked\" />男</label> \r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"女\" />女</label>\r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"保密\" datatype=\"*\" sucmsg=\" \" />保密</label>\r\n                        ");
	}
	else if (userModel.sex=="女")
	{

	templateBuilder.Append("\r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"男\" />男</label> \r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"女\" checked=\"checked\" />女</label>\r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"保密\" datatype=\"*\" sucmsg=\" \" />保密</label>\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"男\" />男</label> \r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"女\" />女</label>\r\n                            <label class=\"mart\"><input name=\"rblSex\" type=\"radio\" value=\"保密\" checked=\"checked\" datatype=\"*\" sucmsg=\" \" />保密</label>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>生日：</dt>\r\n                        <dd>\r\n                        ");
	if (userModel.birthday==null)
	{

	templateBuilder.Append("\r\n                            <input name=\"txtBirthday\" id=\"txtBirthday\" type=\"text\" class=\"input\" maxlength=\"30\" onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd'});\" />\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                            <input name=\"txtBirthday\" id=\"txtBirthday\" type=\"text\" class=\"input\" maxlength=\"30\" onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd'});\" value=\"");	templateBuilder.Append(Utils.ObjectToDateTime(userModel.birthday).ToString("yyyy-M-d"));

	templateBuilder.Append("\" />\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>邮箱：</dt>\r\n                        <dd>\r\n                            <input name=\"txtEmail\" id=\"txtEmail\" type=\"text\" class=\"input\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.email));
	templateBuilder.Append("\" datatype=\"e\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>手机：</dt>\r\n                        <dd>\r\n                            <input name=\"txtMobile\" id=\"txtMobile\" type=\"text\" class=\"input\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.mobile));
	templateBuilder.Append("\" datatype=\"m\" sucmsg=\" \" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>电话：</dt>\r\n                        <dd>\r\n                            <input name=\"txtTelphone\" id=\"txtTelphone\" type=\"text\" class=\"input\" maxlength=\"30\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.telphone));
	templateBuilder.Append("\" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>所属地区：</dt>\r\n                        <dd>\r\n                            <select id=\"txtProvince\" name=\"txtProvince\" class=\"select\"></select>\r\n                            <select id=\"txtCity\" name=\"txtCity\" class=\"select\"></select>\r\n                            <select id=\"txtArea\" name=\"txtArea\" class=\"select\" datatype=\"*\" sucmsg=\" \"></select>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>详细地址：</dt>\r\n                        <dd>\r\n                            <input name=\"txtAddress\" id=\"txtAddress\" type=\"text\" class=\"input\" maxlength=\"250\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.address));
	templateBuilder.Append("\" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>在线QQ：</dt>\r\n                        <dd>\r\n                            <input name=\"txtQQ\" id=\"txtQQ\" type=\"text\" class=\"input\" maxlength=\"20\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.qq));
	templateBuilder.Append("\" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>MSN账户：</dt>\r\n                        <dd>\r\n                            <input name=\"txtMsn\" id=\"txtMsn\" type=\"text\" class=\"input\" maxlength=\"20\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.msn));
	templateBuilder.Append("\" />\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dd>\r\n                            <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认修改\" class=\"submit\" />\r\n                        </dd>\r\n                    </dl>\r\n                 </div>\r\n            </form>\r\n            <!--/修改资料-->\r\n            \r\n            ");
	}
	else if (action=="avatar")
	{

	templateBuilder.Append("\r\n            <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/jquery.jcrop.css\" />\r\n            <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/webuploader/webuploader.min.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.jcrop.min.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/avatar.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\">\r\n            $(function(){\r\n                initWebUploader('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("');\r\n            });\r\n            </");
	templateBuilder.Append("script>\r\n            <!--设置头像-->\r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\"><i class=\"iconfont icon-reply\"></i>返回</a>\r\n                <ul>\r\n                    <li class=\"selected\">\r\n                        <a href=\"javascript:;\">设置头像</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            <div class=\"form-box\">\r\n                <div id=\"upload-box\" class=\"upload-box\"></div>\r\n                <div class=\"cropper-wrap\">\r\n                    <div class=\"cropper-box\">\r\n                        <img id=\"target\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/avatar-bg.png\" width=\"350\" height=\"350\" />\r\n                    </div>\r\n                    <div class=\"cropper-view\">\r\n                        <div class=\"img-box\">\r\n                            ");
	if (userModel.avatar!="")
	{

	templateBuilder.Append("\r\n                              <img id=\"preview\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.avatar));
	templateBuilder.Append("\" />\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                              <img id=\"preview\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/user-avatar.png\" />\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                            <span>头像预览</span>\r\n                        </div>\r\n                        <div class=\"btn-box\">\r\n                            <p><strong>头像预览区</strong></p>\r\n                            <p><input name=\"btnSubmit\" type=\"button\" class=\"btn\" value=\"确定保存\" onclick=\"CropSubmit(this);\" /></p>\r\n                            <p class=\"tip\">提示：生成头像大小180*180相素上传图片后，左侧选取图片合适大小，点击下面的保存按钮。</p>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <form id=\"uploadForm\" name=\"uploadForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_avatar_crop\">\r\n                <input id=\"hideFileName\" name=\"hideFileName\" type=\"hidden\" />\r\n                <input id=\"hideX1\" name=\"hideX1\" type=\"hidden\" value=\"0\" />\r\n                <input id=\"hideY1\" name=\"hideY1\" type=\"hidden\" value=\"0\" />\r\n                <input id=\"hideWidth\" name=\"hideWidth\" type=\"hidden\" value=\"0\" />\r\n                <input id=\"hideHeight\" name=\"hideHeight\" type=\"hidden\" value=\"0\" />\r\n            </form>\r\n            <!--/设置头像-->\r\n            \r\n            ");
	}
	else if (action=="invite")
	{

	templateBuilder.Append("\r\n            <!--邀请码-->\r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"javascript:;\" onclick=\"clickSubmit('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_invite_code');\">\r\n                    <i class=\"fa fa-reply\"></i>申请邀请码\r\n                </a>\r\n                <ul>\r\n                    <li class=\"selected\">\r\n                        <a href=\"javascript:;\">我的邀请码</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            <div class=\"table-wrap\">\r\n                <div class=\"msg-box\">\r\n                    <i class=\"iconfont icon-tip\"></i>\r\n                    <p>您购买的邀请码会在失效之后由系统定时清理，不会长期驻留在列表中。</p>\r\n                </div>\r\n                <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"ftable\">\r\n                    <tr>\r\n                        <th align=\"left\">邀请码</th>\r\n                        <th width=\"150\">申请时间</th>\r\n                        <th width=\"150\">过期时间</th>\r\n                        <th width=\"80\">已使用次数</th>\r\n                        <th width=\"80\">状态</th>\r\n                    </tr>\r\n                    ");
	DataTable inviteList = get_user_invite_list(0, "user_name='"+userModel.user_name+"'");

	foreach(DataRow dr in inviteList.Rows)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td>\r\n                            " + Utils.ObjectToStr(dr["str_code"]) + " &nbsp; \r\n                            <a href=\"javascript:;\" onclick=\"copyText('邀请码：" + Utils.ObjectToStr(dr["str_code"]) + "');\">[复制]</a>\r\n                        </td>\r\n                        <td align=\"center\">" + Utils.ObjectToStr(dr["add_time"]) + "</td>\r\n                        <td align=\"center\">\r\n                            " + Utils.ObjectToStr(dr["eff_time"]) + "\r\n                        </td>\r\n                        <td align=\"center\">" + Utils.ObjectToStr(dr["count"]) + "</td>\r\n                        <td align=\"center\">\r\n                            ");
	if (get_invite_status(Utils.ObjectToStr(dr["str_code"])))
	{

	templateBuilder.Append("\r\n                                有效\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                                已失效\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    ");
	}	//end for if

	if (inviteList.Rows.Count==0)
	{

	templateBuilder.Append("\r\n                        <tr><td colspan=\"8\" align=\"center\">暂无邀请码...</td></tr>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                </table>\r\n            </div>\r\n            <!--/邀请码-->\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            \r\n        </div>\r\n    </div>\r\n    <!--/页面左边-->\r\n</div>\r\n\r\n<!--页面底部-->\r\n");

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
