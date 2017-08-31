<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.userorder_show" ValidateRequest="false" %>
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

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>查看订单 - ");
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
	templateBuilder.Append(linkurl("userorder","list"));

	templateBuilder.Append("\">订单管理</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<div class=\"section clearfix\">\r\n    <!--页面左边-->\r\n    ");

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


	templateBuilder.Append("\r\n    <!--/页面左边-->\r\n    \r\n    <!--页面左边-->\r\n    <div class=\"right-auto\">\r\n        <div class=\"bg-wrap\" style=\"min-height:765px;\">\r\n            <div class=\"sub-tit\">\r\n                <a class=\"add\" href=\"javascript:history.go(-1);\"><i class=\"iconfont icon-reply\"></i>返回</a>\r\n                <ul>\r\n                    <li class=\"selected\">\r\n                        <a href=\"javascript:;\">查看订单</a>\r\n                    </li>\r\n                </ul>\r\n            </div>\r\n            \r\n        ");
	if (model.status<4)
	{

	templateBuilder.Append("\r\n            <!--订单进度-->\r\n            ");
	if (model.payment_status>0)
	{

	templateBuilder.Append("\r\n            <div class=\"order-progress\">\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <div class=\"order-progress mini\">\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n                <ul>\r\n                    <!--下单-->\r\n                    <li class=\"first active\">\r\n                        <div class=\"progress\">下单</div>\r\n                        <div class=\"info\">");
	templateBuilder.Append(Utils.ObjectToStr(model.add_time));
	templateBuilder.Append("</div>\r\n                    </li>\r\n                    <!--/下单-->\r\n                    \r\n                    ");
	if (model.payment_status>0)
	{

	templateBuilder.Append("\r\n                        <!--付款-->\r\n                        ");
	if (model.payment_status==2)
	{

	templateBuilder.Append("\r\n                        <li class=\"active\">\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                        <li>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                            <div class=\"progress\">付款</div>\r\n                            ");
	if (model.payment_status==2)
	{

	templateBuilder.Append("\r\n                            <div class=\"info\">");
	templateBuilder.Append(Utils.ObjectToStr(model.payment_time));
	templateBuilder.Append("</div>\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                        </li>\r\n                        <!--/付款-->\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                    \r\n                    <!--确认-->\r\n                    ");
	if (model.status>=2)
	{

	templateBuilder.Append("\r\n                    <li class=\"active\">\r\n                    ");
	}
	else
	{

	templateBuilder.Append("\r\n                    <li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                        <div class=\"progress\">确认</div>\r\n                        ");
	if (model.status>=2)
	{

	templateBuilder.Append("\r\n                        <div class=\"info\">");
	templateBuilder.Append(Utils.ObjectToStr(model.confirm_time));
	templateBuilder.Append("</div>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                    </li>\r\n                    <!--/确认-->\r\n                    \r\n                    <!--发货-->\r\n                    ");
	if (model.express_status==2)
	{

	templateBuilder.Append("\r\n                    <li class=\"active\">\r\n                    ");
	}
	else
	{

	templateBuilder.Append("\r\n                    <li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                        <div class=\"progress\">发货</div>\r\n                        ");
	if (model.express_status==2)
	{

	templateBuilder.Append("\r\n                        <div class=\"info\">");
	templateBuilder.Append(Utils.ObjectToStr(model.express_time));
	templateBuilder.Append("</div>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                    </li>\r\n                    <!--/发货-->\r\n                    \r\n                    <!--完成-->\r\n                    ");
	if (model.status>2)
	{

	templateBuilder.Append("\r\n                    <li class=\"last active\">\r\n                    ");
	}
	else
	{

	templateBuilder.Append("\r\n                    <li class=\"last\">\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                        <div class=\"progress\">完成</div>\r\n                        ");
	if (model.status>2)
	{

	templateBuilder.Append("\r\n                        <div class=\"info\">");
	templateBuilder.Append(Utils.ObjectToStr(model.complete_time));
	templateBuilder.Append("</div>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                    </li>\r\n                    <!--/完成-->\r\n                </ul>\r\n            </div>\r\n            <!--/订单进度-->\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n            \r\n            <!--订单概述-->\r\n            <div class=\"form-box accept-box\">\r\n                <dl class=\"head form-group\">\r\n                    <dd>\r\n                        订单号：");
	templateBuilder.Append(Utils.ObjectToStr(model.order_no));
	templateBuilder.Append("\r\n                        ");
	if (get_order_payment_status(model.id))
	{

	templateBuilder.Append("\r\n                            <a class=\"btn-pay\" href=\"");
	templateBuilder.Append(linkurl("payment","?action=confirm&order_no="+model.order_no));

	templateBuilder.Append("\">去付款</a>\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                    </dd>\r\n                </dl>\r\n                <dl class=\"form-group\">\r\n                    <dt>订单状态：</dt>\r\n                    <dd>\r\n                        ");
	templateBuilder.Append(get_order_status(model.id).ToString());

	templateBuilder.Append("\r\n                    </dd>\r\n                </dl>\r\n                ");
	if (model.payment_status>0)
	{

	templateBuilder.Append("\r\n                <dl class=\"form-group\">\r\n                    <dt>支付方式：</dt>\r\n                    <dd>");
	templateBuilder.Append(get_payment_title(model.payment_id).ToString());

	templateBuilder.Append("</dd>\r\n                </dl>\r\n                ");
	}	//end for if

	if (model.express_status==2)
	{

	templateBuilder.Append("\r\n                <dl class=\"form-group\">\r\n                    <dt>发货单号：</dt>\r\n                    <dd>");
	templateBuilder.Append(get_express_title(model.express_id).ToString());

	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(model.express_no));
	templateBuilder.Append("</dd>\r\n                </dl>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n            </div>\r\n            <!--/订单概述-->\r\n            \r\n            <!--商品列表-->\r\n            <div class=\"table-wrap\">\r\n                <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" class=\"ftable\">\r\n                    <tr>\r\n                        <th align=\"left\" colspan=\"2\">商品信息</th>\r\n                        <th width=\"10%\">单价</td>\r\n                        <th width=\"10%\">积分</th>\r\n                        <th width=\"10%\">数量</th>\r\n                        <th width=\"10%\">金额</th>\r\n                        <th width=\"10%\">积分</th>\r\n                    </tr>\r\n                    ");
	if (model.order_goods!=null)
	{

	foreach(DTcms.Model.order_goods modelt in model.order_goods)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td width=\"60\">\r\n                            <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.article_id));

	templateBuilder.Append("\">\r\n                                <img src=\"");
	templateBuilder.Append(get_article_img_url(modelt.channel_id, modelt.article_id).ToString());

	templateBuilder.Append("\" class=\"img\" />\r\n                            </a>\r\n                        </td>\r\n                        <td align=\"left\">\r\n                            <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.article_id));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.goods_title));
	templateBuilder.Append("</a>\r\n                        </td>\r\n                        <td align=\"center\">\r\n                            <s>￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.goods_price));
	templateBuilder.Append("</s>\r\n                            <p>￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.real_price));
	templateBuilder.Append("</p>\r\n                        </td>\r\n                        <td align=\"center\">\r\n                            ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n                                +\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                            ");
	templateBuilder.Append(Utils.ObjectToStr(modelt.point));
	templateBuilder.Append("\r\n                        </td>\r\n                        <td align=\"center\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.quantity));
	templateBuilder.Append("</td>\r\n                        <td align=\"center\">￥");
	templateBuilder.Append((modelt.real_price*modelt.quantity).ToString());

	templateBuilder.Append("</td>\r\n                        <td align=\"center\">");
	templateBuilder.Append((modelt.point*modelt.quantity).ToString());

	templateBuilder.Append("</td>\r\n                    </tr>\r\n                    ");
	}	//end for if

	}
	else
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td colspan=\"7\" align=\"center\">暂无记录</td>\r\n                    </tr>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td colspan=\"7\" align=\"right\">\r\n                            <p>商品金额：<b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.real_amount));
	templateBuilder.Append("</b>&nbsp;&nbsp;+&nbsp;&nbsp;运费：<b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.express_fee));
	templateBuilder.Append("</b>&nbsp;&nbsp;+ &nbsp;&nbsp;支付手续费：<b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.payment_fee));
	templateBuilder.Append("</b>&nbsp;&nbsp;税费：<b class=\"red\">");
	templateBuilder.Append(Utils.ObjectToStr(model.invoice_taxes));
	templateBuilder.Append("</b></p>\r\n                            <p style=\"font-size:22px;\">应付总金额：<b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.order_amount));
	templateBuilder.Append("</b></p>\r\n                        </td>\r\n                    </tr>\r\n                </table>\r\n            </div>\r\n            <!--/商品列表-->\r\n            \r\n            <!--收货信息-->\r\n            <div class=\"form-box accept-box\">\r\n                <dl class=\"head form-group\">\r\n                    <dd>收货信息</dd>\r\n                </dl>\r\n                <dl class=\"form-group\">\r\n                    <dt>顾客姓名：</dt>\r\n                    <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.accept_name));
	templateBuilder.Append("</dd>\r\n                </dl>\r\n                <dl class=\"form-group\">\r\n                    <dt>送货地址：</dt>\r\n                    <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.area));
	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(model.address));
	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(model.post_code));
	templateBuilder.Append("</dd>\r\n                </dl>\r\n                <dl class=\"form-group\">\r\n                    <dt>联系电话：</dt>\r\n                    <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.mobile));
	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(model.telphone));
	templateBuilder.Append("</dd>\r\n                </dl>\r\n                <dl class=\"form-group\">\r\n                    <dt>电子邮箱：</dt>\r\n                    <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.email));
	templateBuilder.Append("</dd>\r\n                </dl>\r\n                <dl class=\"form-group\">\r\n                    <dt>备注留言：</dt>\r\n                    <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.message));
	templateBuilder.Append("</dd>\r\n                </dl>\r\n                <dl class=\"form-group\">\r\n                    <dt>开具发票：</dt>\r\n                    <dd>\r\n                        ");
	if (model.is_invoice==1)
	{

	templateBuilder.Append("\r\n                            是\r\n                        ");
	}
	else
	{

	templateBuilder.Append("\r\n                            否\r\n                        ");
	}	//end for if

	templateBuilder.Append("\r\n                    </dd>\r\n                </dl>\r\n                ");
	if (model.is_invoice==1)
	{

	templateBuilder.Append("\r\n                <dl class=\"form-group\">\r\n                    <dt>发票抬头：</dt>\r\n                    <dd>\r\n                        ");
	templateBuilder.Append(Utils.ObjectToStr(model.invoice_title));
	templateBuilder.Append("\r\n                    </dd>\r\n                </dl>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n            </div>\r\n            <!--/收货信息-->\r\n            \r\n        </div>\r\n    </div>\r\n    <!--/页面左边-->\r\n</div>\r\n\r\n<!--页面底部-->\r\n");

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
