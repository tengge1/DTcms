<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.useramount" ValidateRequest="false" %>
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

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>账户余额 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
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

	templateBuilder.Append("\">会员中心</a>\r\n        <a href=\"");
	templateBuilder.Append(linkurl("useramount","list"));

	templateBuilder.Append("\">账户余额</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<div class=\"section clearfix\">\r\n    <!--页面左边-->\r\n    ");

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
	if (action=="recharge")
	{

	templateBuilder.Append("\r\n            <!--账户充值-->\r\n            <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n            <script type=\"text/javascript\">\r\n            $(function(){\r\n                //初始化表单\r\n                AjaxInitForm('#rechargeForm', '#btnSubmit', 0);\r\n            });\r\n            </");
	templateBuilder.Append("script>\r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"javascript:history.go(-1);\"><i class=\"iconfont icon-reply\"></i>返回</a>\r\n                <ul>\r\n                    <li class=\"selected\">\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","recharge"));

	templateBuilder.Append("\">账户充值</a>\r\n                    </li>\r\n                    <li>\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","log"));

	templateBuilder.Append("\">充值记录</a>\r\n                    </li>\r\n                    <li>\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","list"));

	templateBuilder.Append("\">收支明细</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            \r\n            <form id=\"rechargeForm\" name=\"rechargeForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_amount_recharge&site_id=");
	templateBuilder.Append(Utils.ObjectToStr(site.id));
	templateBuilder.Append("\">\r\n                <div class=\"form-box\">\r\n                    <dl class=\"form-group\">\r\n                        <dt>账户余额：</dt>\r\n                        <dd>\r\n                            当前账户金额为：<b>￥");
	templateBuilder.Append(Utils.ObjectToStr(userModel.amount));
	templateBuilder.Append("</b> 元\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>充值金额：</dt>\r\n                        <dd>\r\n                            <input name=\"order_amount\" id=\"order_amount\" type=\"text\" class=\"input code\" onkeydown=\"return checkNumber(event);\" datatype=\"n\"  nullmsg=\"请输入金额\" sucmsg=\" \" /> 元\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>支付方式：</dt>\r\n                        <dd>\r\n                            ");
	DataTable payList = get_payment_list(0, "type=1 and payment_id>2 and site_id="+site.id);

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n                            ");
	int dr__loop__id=0;
	foreach(DataRow dr in payList.Rows)
	{
		dr__loop__id++;


	if (dr__loop__id==(payList.Rows.Count))
	{

	templateBuilder.Append("\r\n                                    <label class=\"mart\"><input name=\"payment_id\" type=\"radio\" value=\"" + Utils.ObjectToStr(dr["id"]) + "\" datatype=\"*\" sucmsg=\" \" /> " + Utils.ObjectToStr(dr["title"]) + "</label>\r\n                                ");
	}
	else
	{

	templateBuilder.Append("\r\n                                    <label class=\"mart\"><input name=\"payment_id\" type=\"radio\" value=\"" + Utils.ObjectToStr(dr["id"]) + "\" /> " + Utils.ObjectToStr(dr["title"]) + "</label>\r\n                                ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dd>\r\n                            <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认充值\" class=\"submit\" />\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n            </form>\r\n            <!--账户充值-->\r\n            \r\n            ");
	}
	else if (action=="log")
	{

	templateBuilder.Append("\r\n            <!--充值记录-->\r\n            <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n            <script type=\"text/javascript\">\r\n            function ExecPostBack(checkValue) {\r\n                if (arguments.length == 1) {\r\n                    ExecDelete('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_recharge_delete', checkValue, '#turl');\r\n                }else{\r\n                    var valueArr = '';\r\n                    $(\"input[name='checkId']:checked\").each(function(i){\r\n                        valueArr += $(this).val();\r\n                        if(i < $(\"input[name='checkId']:checked\").length - 1){\r\n                            valueArr += \",\"\r\n                        }\r\n                    });\r\n                    ExecDelete('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_recharge_delete', valueArr, '#turl');\r\n                }\r\n            }\r\n            </");
	templateBuilder.Append("script>\r\n            \r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"javascript:history.go(-1);\"><i class=\"iconfont icon-reply\"></i>返回</a>\r\n                <ul>\r\n                    <li>\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","recharge"));

	templateBuilder.Append("\">账户充值</a>\r\n                    </li>\r\n                    <li class=\"selected\">\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","log"));

	templateBuilder.Append("\">充值记录</a>\r\n                    </li>\r\n                    <li>\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","list"));

	templateBuilder.Append("\">收支明细</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            \r\n            <div class=\"table-wrap\">\r\n                <table width=\"100%\" class=\"mtable\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n                <tbody>\r\n                ");
	DataTable rechargeList = get_user_recharge_list(10, page, "user_id="+userModel.id, out totalcount);

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n                ");
	string pagelist = get_page_link(10, page, totalcount, "useramount", action, "__id__");

	templateBuilder.Append(" <!--取得分页页码列表-->\r\n                ");
	foreach(DataRow dr in rechargeList.Rows)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td width=\"20\" align=\"center\">\r\n                            <input name=\"checkId\" class=\"checkall\" type=\"checkbox\" value=\"" + Utils.ObjectToStr(dr["id"]) + "\" >\r\n                        </td>\r\n                        <td>" + Utils.ObjectToStr(dr["add_time"]) + "</td>\r\n                        <td width=\"140\">" + Utils.ObjectToStr(dr["recharge_no"]) + "</td>\r\n                        <td width=\"80\">");
	templateBuilder.Append(get_payment_title(Utils.StrToInt(Utils.ObjectToStr(dr["payment_id"]), 0)).ToString());

	templateBuilder.Append("</td>\r\n                        <td width=\"80\">￥" + Utils.ObjectToStr(dr["amount"]) + "</td>\r\n                        <td width=\"50\">\r\n                            ");
	if (Utils.StrToInt(Utils.ObjectToStr(dr["status"]), 0)>0)
	{

	templateBuilder.Append("\r\n                                已完成\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                                未完成\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                        </td>\r\n                        <td width=\"30\">\r\n                            <a onclick=\"ExecPostBack('" + Utils.ObjectToStr(dr["id"]) + "');\" href=\"javascript:;\">删除</a>\r\n                        </td>\r\n                    </tr>\r\n                ");
	}	//end for if

	if (rechargeList.Rows.Count==0)
	{

	templateBuilder.Append("\r\n                    <tr><td align=\"center\">暂无充值记录...</td></tr>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </tbody>\r\n                </table>\r\n                \r\n                <div class=\"page-foot\">\r\n                    <div class=\"flickr right\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div><!--放置页码列表-->\r\n                    <div class=\"btn-box\">\r\n                        <a onclick=\"checkAll(this);\" href=\"javascript:;\">全选</a>\r\n                        <a onclick=\"ExecPostBack();\" href=\"javascript:;\">删除</a>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <input id=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(linkurl("useramount","log"));

	templateBuilder.Append("\" /><!--存在跳转的URL值-->\r\n            <!--/充值记录-->\r\n            \r\n            ");
	}
	else if (action=="list")
	{

	templateBuilder.Append("\r\n            <!--收支明细-->\r\n            <link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n            <script type=\"text/javascript\">\r\n            function ExecPostBack(checkValue) {\r\n                if (arguments.length == 1) {\r\n                    ExecDelete('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_amount_delete', checkValue, '#turl');\r\n                }else{\r\n                    var valueArr = '';\r\n                    $(\"input[name='checkId']:checked\").each(function(i){\r\n                        valueArr += $(this).val();\r\n                        if(i < $(\"input[name='checkId']:checked\").length - 1){\r\n                            valueArr += \",\"\r\n                        }\r\n                    });\r\n                    ExecDelete('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_amount_delete', valueArr, '#turl');\r\n                }\r\n            }\r\n            </");
	templateBuilder.Append("script>\r\n            \r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"javascript:history.go(-1);\"><i class=\"iconfont icon-reply\"></i>返回</a>\r\n                <ul>\r\n                    <li>\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","recharge"));

	templateBuilder.Append("\">账户充值</a>\r\n                    </li>\r\n                    <li>\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","log"));

	templateBuilder.Append("\">充值记录</a>\r\n                    </li>\r\n                    <li class=\"selected\">\r\n                        <a href=\"");
	templateBuilder.Append(linkurl("useramount","list"));

	templateBuilder.Append("\">收支明细</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            \r\n            <div class=\"table-wrap\">\r\n                <table width=\"100%\" class=\"mtable\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n                <tbody>\r\n                ");
	DataTable amountList = get_user_amount_list(10, page, "user_id="+userModel.id, out totalcount);

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n                ");
	string pagelist = get_page_link(10, page, totalcount, "useramount", action, "__id__");

	templateBuilder.Append(" <!--取得分页页码列表-->\r\n                ");
	foreach(DataRow dr in amountList.Rows)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td width=\"20\" align=\"center\">\r\n                            <input name=\"checkId\" class=\"checkall\" type=\"checkbox\" value=\"" + Utils.ObjectToStr(dr["id"]) + "\" >\r\n                        </td>\r\n                        <td width=\"150\">" + Utils.ObjectToStr(dr["add_time"]) + "</td>\r\n                        <td>" + Utils.ObjectToStr(dr["remark"]) + "</td>\r\n                        <td width=\"50\">\r\n                            ");
	if (Utils.StrToInt(Utils.ObjectToStr(dr["value"]), 0)>0)
	{

	templateBuilder.Append("\r\n                                +" + Utils.ObjectToStr(dr["value"]) + "\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                                " + Utils.ObjectToStr(dr["value"]) + "\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                        </td>\r\n                        <td width=\"30\">\r\n                            <a onclick=\"ExecPostBack('" + Utils.ObjectToStr(dr["id"]) + "');\" href=\"javascript:;\">删除</a>\r\n                        </td>\r\n                    </tr>\r\n                ");
	}	//end for if

	if (amountList.Rows.Count==0)
	{

	templateBuilder.Append("\r\n                    <tr><td align=\"center\">暂无消费明细...</td></tr>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                </tbody>\r\n                </table>\r\n                \r\n                <div class=\"page-foot\">\r\n                    <div class=\"flickr right\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div><!--放置页码列表-->\r\n                    <div class=\"btn-box\">\r\n                        <a onclick=\"checkAll(this);\" href=\"javascript:;\">全选</a>\r\n                        <a onclick=\"ExecPostBack();\" href=\"javascript:;\">删除</a>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n            <input id=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(linkurl("useramount","list"));

	templateBuilder.Append("\" /><!--存在跳转的URL值-->\r\n            <!--收支明细-->\r\n            ");
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
