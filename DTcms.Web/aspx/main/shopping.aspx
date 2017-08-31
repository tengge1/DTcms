<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.shopping" ValidateRequest="false" %>
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

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>确认订单 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/PCASClass.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/cart.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n$(function(){\r\n	//初始化收货地址\r\n	initUserAddress(\"#userAddress\");\r\n	//初始化表单\r\n	AjaxInitForm('#orderForm', '#btnSubmit', 0);\r\n});\r\n</");
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
	templateBuilder.Append(linkurl("cart"));

	templateBuilder.Append("\">购物车</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<!--页面内容-->\r\n<div class=\"section\">\r\n    <div class=\"wrapper\">\r\n        <div class=\"bg-wrap\">\r\n            <!--购物车头部-->\r\n            <div class=\"cart-head clearfix\">\r\n                <h2><i class=\"iconfont icon-cart\"></i>我的购物车</h2>\r\n                <div class=\"cart-setp\">\r\n                    <ul>\r\n                        <li class=\"first active\">\r\n                            <div class=\"progress\">\r\n                                <span>1</span>\r\n                                放进购物车\r\n                            </div>\r\n                        </li>\r\n                        <li class=\"active\">\r\n                            <div class=\"progress\">\r\n                                <span>2</span>\r\n                                填写订单信息\r\n                            </div>\r\n                        </li>\r\n                        <li class=\"last\">\r\n                            <div class=\"progress\">\r\n                                <span>3</span>\r\n                                支付/确认订单\r\n                            </div>\r\n                        </li>\r\n                    </ul>\r\n                </div>\r\n            </div>\r\n            <!--购物车头部-->\r\n            \r\n            <div class=\"cart-box\">\r\n                <h2 class=\"slide-tit\">\r\n                    <span>1、收货地址</span>\r\n                </h2>\r\n                \r\n                <form id=\"orderForm\" name=\"orderForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=order_save&site_id=");
	templateBuilder.Append(Utils.ObjectToStr(site.id));
	templateBuilder.Append("\">\r\n                <div class=\"form-box address-info\">\r\n                    <dl class=\"form-group\">\r\n                        <dt>收货人姓名：</dt>\r\n                        <dd>\r\n                            <input name=\"book_id\" id=\"book_id\" type=\"hidden\" value=\"0\" />\r\n                            <input name=\"accept_name\" id=\"accept_name\" type=\"text\" class=\"input\" value=\"\" datatype=\"s2-20\" sucmsg=\" \" />\r\n                            <span class=\"Validform_checktip\">*收货人姓名</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>所属地区：</dt>\r\n                        <dd>\r\n                            <select id=\"province\" name=\"province\" class=\"select\"></select>\r\n                            <select id=\"city\" name=\"city\" class=\"select\"></select>\r\n                            <select id=\"area\" name=\"area\" class=\"select\" datatype=\"*\" sucmsg=\" \"></select>\r\n                            <span class=\"Validform_checktip\">*请选择您所在的地区</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>详细地址：</dt>\r\n                        <dd>\r\n                            <input name=\"address\" id=\"address\" type=\"text\" class=\"input\" value=\"\" datatype=\"*2-100\" sucmsg=\" \" />\r\n                            <span class=\"Validform_checktip\">*除上面所属地区外的详细地址</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>手机号码：</dt>\r\n                        <dd>\r\n                            <input name=\"mobile\" id=\"mobile\" type=\"text\" class=\"input\" value=\"\" datatype=\"m\" sucmsg=\" \" />\r\n                            <span class=\"Validform_checktip\">*收货人的手机号码</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>联系电话：</dt>\r\n                        <dd>\r\n                            <input name=\"telphone\" id=\"telphone\" type=\"text\" class=\"input\" value=\"\" />\r\n                            <span class=\"Validform_checktip\">收货人的联系电话，非必填</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>电子邮箱：</dt>\r\n                        <dd>\r\n                            <input name=\"email\" id=\"email\" type=\"text\" class=\"input\" value=\"\" />\r\n                            <span class=\"Validform_checktip\">方便通知订单状态，非必填</span>\r\n                        </dd>\r\n                    </dl>\r\n                    <dl class=\"form-group\">\r\n                        <dt>邮政编码：</dt>\r\n                        <dd>\r\n                            <input name=\"post_code\" id=\"post_code\" type=\"txt\" class=\"input code\" />\r\n                            <span class=\"Validform_checktip\">所在地区的邮政编码，非必填</span>\r\n                        </dd>\r\n                    </dl>\r\n                </div>\r\n                \r\n                <h2 class=\"slide-tit\">\r\n                    <span>2、支付方式</span>\r\n                </h2>\r\n                <ul class=\"item-box clearfix\">\r\n                    ");
	DataTable paymentList = get_payment_list(0, "site_id="+site.id);

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n                    ");
	int dr1__loop__id=0;
	foreach(DataRow dr1 in paymentList.Rows)
	{
		dr1__loop__id++;


	decimal poundage_amount = get_payment_poundage_amount(Utils.StrToInt(Utils.ObjectToStr(dr1["id"]), 0),goodsTotal.real_amount);

	templateBuilder.Append("\r\n                        <li>\r\n                            <label>\r\n                            ");
	if (dr1__loop__id==(paymentList.Rows.Count))
	{

	templateBuilder.Append("\r\n                                <input name=\"payment_id\" type=\"radio\" onclick=\"paymentAmountTotal(this);\" value=\"" + Utils.ObjectToStr(dr1["id"]) + "\" datatype=\"*\" sucmsg=\" \" />\r\n                                <input name=\"payment_price\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(poundage_amount));
	templateBuilder.Append("\" />" + Utils.ObjectToStr(dr1["title"]) + "\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                                <input name=\"payment_id\" type=\"radio\" onclick=\"paymentAmountTotal(this);\" value=\"" + Utils.ObjectToStr(dr1["id"]) + "\" />\r\n                                <input name=\"payment_price\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(poundage_amount));
	templateBuilder.Append("\" />" + Utils.ObjectToStr(dr1["title"]) + "\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                                <em>手续费：");
	templateBuilder.Append(Utils.ObjectToStr(poundage_amount));
	templateBuilder.Append("元</em>\r\n                            </label>\r\n                        </li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n                \r\n                <h2 class=\"slide-tit\">\r\n                    <span>3、配送方式</span>\r\n                </h2>\r\n                <ul class=\"item-box clearfix\">\r\n                    ");
	DataTable expressList = get_express_list(0, "");

	templateBuilder.Append(" <!--取得一个DataTable-->\r\n                    ");
	int dr2__loop__id=0;
	foreach(DataRow dr2 in expressList.Rows)
	{
		dr2__loop__id++;


	templateBuilder.Append("\r\n                        <li>\r\n                            <label>\r\n                            ");
	if (dr2__loop__id==(expressList.Rows.Count))
	{

	templateBuilder.Append("\r\n                                <input name=\"express_id\" type=\"radio\" onclick=\"freightAmountTotal(this);\" value=\"" + Utils.ObjectToStr(dr2["id"]) + "\" datatype=\"*\" sucmsg=\" \" />\r\n                                <input name=\"express_price\" type=\"hidden\" value=\"" + Utils.ObjectToStr(dr2["express_fee"]) + "\" />" + Utils.ObjectToStr(dr2["title"]) + "\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                                <input name=\"express_id\" type=\"radio\" onclick=\"freightAmountTotal(this);\" value=\"" + Utils.ObjectToStr(dr2["id"]) + "\" />\r\n                                <input name=\"express_price\" type=\"hidden\" value=\"" + Utils.ObjectToStr(dr2["express_fee"]) + "\" />" + Utils.ObjectToStr(dr2["title"]) + "\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                                <em>费用：" + Utils.ObjectToStr(dr2["express_fee"]) + "元</em>\r\n                            </label>\r\n                        </li>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                </ul>\r\n                \r\n                <h2 class=\"slide-tit\">\r\n                    <span>4、商品清单</span>\r\n                </h2>\r\n                <div class=\"line15\"></div>\r\n                <table width=\"100%\" border=\"0\" align=\"center\" cellpadding=\"8\" cellspacing=\"0\" class=\"cart-table\">\r\n                    <tr>\r\n                        <th colspan=\"2\" align=\"left\">商品信息</th>\r\n                        <th width=\"84\" align=\"left\">单价</th>\r\n                        <th width=\"84\" align=\"left\">优惠(元)</th>\r\n                        <th width=\"84\" align=\"center\">数量</th>\r\n                        <th width=\"104\" align=\"left\">金额(元)</th>\r\n                        <th width=\"84\" align=\"left\">积分</th>\r\n                        <th width=\"84\" align=\"center\">库存(件)</th>\r\n                    </tr>\r\n                    ");
	foreach(DTcms.Model.cart_items modelt in goodsList)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td width=\"68\">\r\n                            <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.article_id));

	templateBuilder.Append("\">\r\n                                <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.img_url));
	templateBuilder.Append("\" class=\"img\" />\r\n                            </a>\r\n                        </td>\r\n                        <td>\r\n                            <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.article_id));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.title));
	templateBuilder.Append("</a>\r\n                        </td>\r\n                        <td>\r\n                            <span class=\"red\">\r\n                                ￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.user_price));
	templateBuilder.Append("\r\n                            </span>\r\n                        </td>\r\n                        <td>\r\n                            <span class=\"red\">\r\n                                ￥");
	templateBuilder.Append((modelt.sell_price-modelt.user_price).ToString());

	templateBuilder.Append("\r\n                            </span>\r\n                        </td>\r\n                        <td align=\"center\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.quantity));
	templateBuilder.Append("</td>\r\n                        <td>\r\n                            <span class=\"red\">\r\n                                ￥");
	templateBuilder.Append((modelt.user_price*modelt.quantity).ToString());

	templateBuilder.Append("\r\n                            </span>\r\n                        </td>\r\n                        <td>\r\n                            ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n                                +\r\n                            ");
	}	//end for if

	templateBuilder.Append((modelt.point*modelt.quantity).ToString());

	templateBuilder.Append("\r\n                        </td>\r\n                        <td align=\"center\">\r\n                            ");
	templateBuilder.Append(Utils.ObjectToStr(modelt.stock_quantity));
	templateBuilder.Append("\r\n                        </td>\r\n                    </tr>\r\n                    ");
	}	//end for if

	if (goodsList.Count<1)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td colspan=\"8\">\r\n                            <div class=\"msg-tips\">\r\n                                <div class=\"icon warning\"><i class=\"iconfont icon-tip\"></i></div>\r\n                                <div class=\"info\">\r\n                                    <strong>购物车没有商品！</strong>\r\n                                    <p>您的购物车为空，<a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">马上去购物</a>吧！</p>\r\n                                </div>\r\n                            </div>\r\n                        </td>\r\n                    </tr>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                </table>\r\n                <div class=\"line15\"></div>\r\n                \r\n                <h2 class=\"slide-tit\">\r\n                    <span>5、结算信息</span>\r\n                </h2>\r\n                <div class=\"buy-foot clearfix\">\r\n                    <div class=\"left-box\">\r\n                        <dl>\r\n                            <dd>\r\n                                <label><input name=\"is_invoice\" id=\"is_invoice\" type=\"checkbox\" value=\"1\" onclick=\"taxAmoutTotal(this);\" /> 是否开具发票</label>\r\n                                <input name=\"taxAmout\" id=\"taxAmout\" type=\"hidden\" value=\"");
	templateBuilder.Append(get_order_taxamount(goodsTotal.real_amount).ToString());

	templateBuilder.Append("\" />\r\n                            </dd>\r\n                        </dl>\r\n                        <dl id=\"invoiceBox\" style=\"display:none;\">\r\n                            <dt>发票抬头(100字符以内)</dt>\r\n                            <dd>\r\n                                <input name=\"invoice_title\" id=\"invoice_title\" type=\"text\" class=\"input\" />\r\n                            </dd>\r\n                        </dl>\r\n                        <dl>\r\n                            <dt>订单备注(100字符以内)</dt>\r\n                            <dd>\r\n                                <textarea name=\"message\" class=\"input\" style=\"height:35px;\"></textarea>\r\n                            </dd>\r\n                        </dl>\r\n                    </div>\r\n                    <div class=\"right-box\">\r\n                        <p>\r\n                            商品 <label class=\"price\">");
	templateBuilder.Append(Utils.ObjectToStr(goodsTotal.total_quantity));
	templateBuilder.Append("</label> 件&nbsp;&nbsp;&nbsp;&nbsp;\r\n                            商品金额：￥<label id=\"goodsAmount\" class=\"price\">");
	templateBuilder.Append(Utils.ObjectToStr(goodsTotal.real_amount));
	templateBuilder.Append("</label> 元&nbsp;&nbsp;&nbsp;&nbsp;\r\n                            总积分：<label class=\"price\">");
	templateBuilder.Append(Utils.ObjectToStr(goodsTotal.total_point));
	templateBuilder.Append("</label> 分\r\n                        </p>\r\n                        <p>\r\n                            运费：￥<label id=\"expressFee\" class=\"price\">0.00</label> 元 +\r\n                            支付手续费：￥<label id=\"paymentFee\" class=\"price\">0.00</label> 元 +\r\n                            税费：￥<label id=\"taxesFee\" class=\"price\">0.00</label> 元\r\n                        </p>\r\n                        <p class=\"txt-box\">\r\n                            应付总金额：￥<label id=\"totalAmount\" class=\"price\">");
	templateBuilder.Append(Utils.ObjectToStr(goodsTotal.real_amount));
	templateBuilder.Append("</label>\r\n                        </p>\r\n                        <p class=\"btn-box\">\r\n                            <a class=\"btn button\" href=\"");
	templateBuilder.Append(linkurl("cart"));

	templateBuilder.Append("\">返回购物车</a>\r\n                            ");
	if (goodsTotal.total_quantity>0)
	{

	templateBuilder.Append("\r\n                                <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"确认提交\" class=\"btn submit\" />\r\n                            ");
	}
	else
	{

	templateBuilder.Append("\r\n                                <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"button\" value=\"确认提交\" class=\"btn gray\" disabled=\"disabled\" />\r\n                            ");
	}	//end for if

	templateBuilder.Append("\r\n                        </p>\r\n                    </div>\r\n                </div>\r\n                </form>\r\n                \r\n            </div>\r\n            \r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/页面内容-->\r\n\r\n<!--页面底部-->\r\n");

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
