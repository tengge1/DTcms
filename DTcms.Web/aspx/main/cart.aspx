<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.cart" ValidateRequest="false" %>
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

	templateBuilder.Append("<!doctype html>\r\n<html>\r\n<head>\r\n<meta charset=\"utf-8\">\r\n<title>购物车 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
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
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/cart.js\"></");
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

	templateBuilder.Append("\">购物车</a>\r\n    </div>\r\n</div>\r\n<!--/当前位置-->\r\n\r\n<!--页面内容-->\r\n<div class=\"section\">\r\n    <div class=\"wrapper\">\r\n        <div class=\"bg-wrap\">\r\n            <!--购物车头部-->\r\n            <div class=\"cart-head clearfix\">\r\n                <h2><i class=\"iconfont icon-cart\"></i>我的购物车</h2>\r\n                <div class=\"cart-setp\">\r\n                    <ul>\r\n                        <li class=\"first active\">\r\n                            <div class=\"progress\">\r\n                                <span>1</span>\r\n                                放进购物车\r\n                            </div>\r\n                        </li>\r\n                        <li>\r\n                            <div class=\"progress\">\r\n                                <span>2</span>\r\n                                填写订单信息\r\n                            </div>\r\n                        </li>\r\n                        <li class=\"last\">\r\n                            <div class=\"progress\">\r\n                                <span>3</span>\r\n                                支付/确认订单\r\n                            </div>\r\n                        </li>\r\n                    </ul>\r\n                </div>\r\n            </div>\r\n            <!--购物车头部-->\r\n            \r\n            <!--商品列表-->\r\n            <div class=\"cart-box\">\r\n                <input id=\"jsondata\" name=\"jsondata\" type=\"hidden\" />\r\n                <table width=\"100%\" align=\"center\" class=\"cart-table\" border=\"0\" cellspacing=\"0\" cellpadding=\"8\">\r\n                    <tr>\r\n                        <th width=\"48\" align=\"center\">\r\n                            <a onclick=\"selectCart(this);\" href=\"javascript:;\">全选</a>\r\n                        </th>\r\n                        <th align=\"left\" colspan=\"2\">商品信息</th>\r\n                        <th width=\"84\" align=\"left\">单价</th>\r\n                        <th width=\"104\" align=\"center\">数量</th>\r\n                        <th width=\"104\" align=\"left\">金额(元)</th>\r\n                        <th width=\"84\" align=\"center\">积分</th>\r\n                        <th width=\"54\" align=\"center\">操作</th>\r\n                    </tr>\r\n                    ");
	foreach(DTcms.Model.cart_items modelt in goodsList)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td align=\"center\">\r\n                            <input type=\"checkbox\" name=\"checkall\" class=\"checkall\" onclick=\"selectCart();\" />\r\n                            <input name=\"hideChannelId\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.channel_id));
	templateBuilder.Append("\" />\r\n                            <input name=\"hideArticleId\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.article_id));
	templateBuilder.Append("\" />\r\n                            <input name=\"hideStockQuantity\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.stock_quantity));
	templateBuilder.Append("\" />\r\n                            <input name=\"hideGoodsPrice\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.user_price));
	templateBuilder.Append("\" />\r\n                            <input name=\"hidePoint\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.point));
	templateBuilder.Append("\" />\r\n                        </td>\r\n                        <td width=\"68\">\r\n                            <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.article_id));

	templateBuilder.Append("\">\r\n                                <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.img_url));
	templateBuilder.Append("\" class=\"img\" />\r\n                            </a>\r\n                        </td>\r\n                        <td>\r\n                            <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.article_id));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.title));
	templateBuilder.Append("</a>\r\n                        </td>\r\n                        <td>\r\n                            ￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.user_price));
	templateBuilder.Append("\r\n                        </td>\r\n                        <td align=\"center\">\r\n                            <div class=\"buy-box\">\r\n                                <a href=\"javascript:;\" class=\"reduce\" onclick=\"updateCart(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("', -1);\">-</a>\r\n                                <input type=\"text\" name=\"goodsQuantity\" class=\"input\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(modelt.quantity));
	templateBuilder.Append("\" onblur=\"updateCart(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("');\" onkeydown=\"return checkNumber(event);\" />\r\n                                <a href=\"javascript:;\" class=\"subjoin\" onclick=\"updateCart(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("', 1);\">+</a>\r\n                            </div>\r\n                        </td>\r\n                        <td>\r\n                            <span class=\"red\">\r\n                                ￥<label name=\"amountCount\">");
	templateBuilder.Append((modelt.user_price*modelt.quantity).ToString());

	templateBuilder.Append("</label>\r\n                            </span>\r\n                        </td>\r\n                        <td align=\"center\">\r\n                            <label name=\"pointCount\">\r\n                                ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n                                    +\r\n                                ");
	}	//end for if

	templateBuilder.Append((modelt.point*modelt.quantity).ToString());

	templateBuilder.Append("\r\n                            </label>\r\n                        </td>\r\n                        <td align=\"center\">\r\n                            <a onclick=\"deleteCart('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("', this);\" href=\"javascript:;\">删除</a>\r\n                        </td>\r\n                    </tr>\r\n                    ");
	}	//end for if

	if (goodsList.Count<1)
	{

	templateBuilder.Append("\r\n                    <tr>\r\n                        <td colspan=\"10\">\r\n                            <div class=\"msg-tips\">\r\n                                <div class=\"icon warning\"><i class=\"iconfont icon-tip\"></i></div>\r\n                                <div class=\"info\">\r\n                                    <strong>购物车没有商品！</strong>\r\n                                    <p>您的购物车为空，<a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">马上去购物</a>吧！</p>\r\n                                </div>\r\n                            </div>\r\n                        </td>\r\n                    </tr>\r\n                    ");
	}	//end for if

	templateBuilder.Append("\r\n                    <tr>\r\n                        <th align=\"right\" colspan=\"8\">\r\n                            已选择商品 <b class=\"red\" id=\"totalQuantity\">0</b> 件 &nbsp;&nbsp;&nbsp;\r\n                            商品总金额（不含运费）：<span class=\"red\">￥</span><b class=\"red\" id=\"totalAmount\">0</b>元\r\n                        </th>\r\n                    </tr>\r\n\r\n                </table>\r\n            </div>\r\n            <!--/商品列表-->\r\n            \r\n            <!--购物车底部-->\r\n            <div class=\"cart-foot clearfix\">\r\n                <div class=\"left-box\">\r\n                    <a onclick=\"selectCart(this);\" href=\"javascript:;\">全选</a>\r\n                    <a onclick=\"deleteCart('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("');\" href=\"javascript:;\">清空购物车</a>\r\n                </div>\r\n                <div class=\"right-box\">\r\n                    <button class=\"button\" onclick=\"javascript:location.href='");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("';\">继续购物</button>\r\n                    <button class=\"submit\" onclick=\"formSubmit(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("', '");
	templateBuilder.Append(linkurl("shopping"));

	templateBuilder.Append("');\">立即结算</button>\r\n                </div>\r\n            </div>\r\n            <!--购物车底部-->\r\n            \r\n        </div>\r\n    </div>\r\n</div>\r\n<!--/页面内容-->\r\n\r\n<!--页面底部-->\r\n");

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
