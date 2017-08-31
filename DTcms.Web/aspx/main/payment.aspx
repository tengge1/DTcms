<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.payment" ValidateRequest="false" %>
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

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>支付中心－");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\">\r\n<script type=\"text/javascript\" src=\"");
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

	templateBuilder.Append("\">首页</a> &gt;\r\n        <a href=\"javascript:;\">支付中心</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<!--导航推荐-->\r\n<div class=\"section\">\r\n    <div class=\"wrapper\">\r\n        <div class=\"bg-wrap\">\r\n            <div class=\"nav-tit\">\r\n                <a class=\"selected\" href=\"javascript:;\">支付中心</a>\r\n            </div>\r\n            \r\n            ");
	if (action=="confirm")
	{

	templateBuilder.Append("\r\n            <!--确认订单-->\r\n            <form id=\"payForm\" name=\"payForm\" method=\"post\" action=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("api/payment/");
	templateBuilder.Append(Utils.ObjectToStr(payModel.api_path));
	templateBuilder.Append("/index.aspx\" target=\"_blank\">\r\n                <input id=\"pay_order_no\" name=\"pay_order_no\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(order_no));
	templateBuilder.Append("\" />\r\n                <input id=\"pay_order_amount\" name=\"pay_order_amount\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(order_amount));
	templateBuilder.Append("\" />\r\n                <input id=\"pay_user_name\" name=\"pay_user_name\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("\" />\r\n                \r\n                ");
	if (order_type=="recharge")
	{

	templateBuilder.Append("\r\n                    <!--充值订单-->\r\n                    <input id=\"pay_subject\" name=\"pay_subject\" type=\"hidden\" value=\"账户充值\" />\r\n                    <div class=\"form-box full\">\r\n                        <dl class=\"form-group\">\r\n                            <dt>订 单 号：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(order_no));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>充值金额：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(order_amount));
	templateBuilder.Append(" 元</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>支付方式：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(payModel.title));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dd>\r\n                                <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认支付\" class=\"submit\" />\r\n                            </dd>\r\n                        </dl>\r\n                    </div>\r\n                    <!--/充值订单-->\r\n                ");
	}
	else if (order_type=="buygoods")
	{

	templateBuilder.Append("\r\n                    <!--商品订单-->\r\n                    <input id=\"pay_subject\" name=\"pay_subject\" type=\"hidden\" value=\"购买商品\" />\r\n                    <div class=\"form-box full\">\r\n                        <dl class=\"form-group\">\r\n                            <dt>订 单 号：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(order_no));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>收货人姓名：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(orderModel.accept_name));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>送货地址：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(orderModel.address));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>手机号码：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(orderModel.mobile));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>固定电话：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(orderModel.telphone));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>备注留言：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(orderModel.message));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>支付金额：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(order_amount));
	templateBuilder.Append(" 元</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dt>支付方式：</dt>\r\n                            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(payModel.title));
	templateBuilder.Append("</dd>\r\n                        </dl>\r\n                        <dl class=\"form-group\">\r\n                            <dd>\r\n                                <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认支付\" class=\"submit\" />\r\n                            </dd>\r\n                        </dl>\r\n                    </div>\r\n                    <!--/商品订单-->\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n            </form>\r\n            <!--/确认订单-->\r\n            ");
	}	//end for if

	if (action=="succeed")
	{

	templateBuilder.Append("\r\n            <!--支付成功-->\r\n            <div class=\"msg-tips\">\r\n                <div class=\"icon\">\r\n                    <i class=\"iconfont icon-check\"></i>\r\n                </div>\r\n                <div class=\"info\">\r\n                    <strong>订单已支付成功！</strong>\r\n                    <p>您可以点击这里进入<a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">会员中心</a>查看订单状态！</p>\r\n                    <p>如有其它问题，请立即与我们客服人员联系。</p>\r\n                </div>\r\n            </div>\r\n            <!--/支付成功-->\r\n            ");
	}	//end for if

	if (action=="error")
	{

	templateBuilder.Append("\r\n            <!--支付出错-->\r\n            <div class=\"msg-tips\">\r\n                <div class=\"icon error\">\r\n                    <i class=\"iconfont icon-error\"></i>\r\n                </div>\r\n                <div class=\"info\">\r\n                    <strong>订单支付失败！</strong>\r\n                    <p>支付错误，可点击这里进入<a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">会员中心</a>查看订单状态！</p>\r\n                    <p>如需要核实订单状态，请立即与我们客服人员联系。</p>\r\n                </div>\r\n            </div>\r\n            <!--/支付出错-->\r\n            ");
	}	//end for if

	if (action=="login")
	{

	templateBuilder.Append("\r\n            <!--用户未登录-->\r\n            <div class=\"msg-tips\">\r\n                <div class=\"icon warning\">\r\n                    <i class=\"iconfont icon-tip\"></i>\r\n                </div>\r\n                <div class=\"info\">\r\n                    <strong>对不起，请登录后再进行支付！</strong>\r\n                    <p>尚未登录或已经超时，请<a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">点击这里进行登录</a></p>\r\n                    <p>如果您尚未成为我们会员，请<a href=\"");
	templateBuilder.Append(linkurl("register"));

	templateBuilder.Append("\">点击这里注册</a></p>\r\n                </div>\r\n            </div>\r\n            <!--/用户未登录-->\r\n            ");
	}	//end for if

	if (action=="recharge")
	{

	templateBuilder.Append("\r\n            <!--用户余额不足-->\r\n            <div class=\"msg-tips\">\r\n                <div class=\"icon warning\">\r\n                    <i class=\"iconfont icon-tip\"></i>\r\n                </div>\r\n                <div class=\"info\">\r\n                    <strong>对不起，您的余额不足本次支付！</strong>\r\n                    <p>选择余额支付，必须确保账户余额充足才能支付订单金额</p>\r\n                    <p>请<a href=\"");
	templateBuilder.Append(linkurl("useramount","recharge"));

	templateBuilder.Append("\">点击这里充值</a>后再进行后续支付！</p>\r\n                </div>\r\n            </div>\r\n            <!--/用户余额不足-->\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            \r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<!--页面底部-->\r\n");

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
