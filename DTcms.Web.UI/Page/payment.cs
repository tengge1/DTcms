using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class payment : Web.UI.BasePage
    {
        protected string action = string.Empty;
        protected string order_no = string.Empty;
        protected string order_type = string.Empty;
        protected decimal order_amount = 0;

        protected Model.orderconfig orderConfig = new BLL.orderconfig().loadConfig(); //订单配置
        protected Model.users userModel; //用户
        protected Model.orders orderModel; //购物
        protected Model.user_recharge rechargeModel; //充值
        protected Model.payment payModel; //支付

        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(payment_Init); //加入Init事件
        }

        /// <summary>
        /// 将在Init事件执行
        /// </summary>
        protected void payment_Init(object sender, EventArgs e)
        {
            //取得处事类型
            action = DTRequest.GetString("action");
            order_no = DTRequest.GetString("order_no");
            if (order_no.ToUpper().StartsWith("R")) //充值订单
            {
                order_type = DTEnums.AmountTypeEnum.Recharge.ToString().ToLower();
            }
            else if (order_no.ToUpper().StartsWith("B")) //商品订单
            {
                order_type = DTEnums.AmountTypeEnum.BuyGoods.ToString().ToLower();
            }
            
            switch (action)
            {
                case "confirm":
                    if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(order_no))
                    {
                        HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，URL传输参数有误！")));
                        return;
                    }
                    //是否需要支持匿名购物
                    userModel = new Web.UI.BasePage().GetUserInfo(); //取得用户登录信息
                    if (orderConfig.anonymous == 0 || order_no.ToUpper().StartsWith("R"))
                    {
                        if (userModel == null)
                        {
                            //用户未登录
                            HttpContext.Current.Response.Redirect(linkurl("payment", "?action=login"));
                            return;
                        }
                    }
                    else if (userModel == null)
                    {
                        userModel = new Model.users();
                    }
                    //检查订单的类型(充值或购物)
                    if (order_no.ToUpper().StartsWith("R")) //充值订单
                    {
                        rechargeModel = new BLL.user_recharge().GetModel(order_no);
                        if (rechargeModel == null)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，订单号不存在或已删除！")));
                            return;
                        }
                        //检查订单号是否已支付
                        if (rechargeModel.status == 1)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("payment", "?action=succeed&order_no=" + rechargeModel.recharge_no));
                            return;
                        }
                        //检查支付方式
                        payModel = new BLL.site_payment().GetPaymentModel(rechargeModel.payment_id);
                        if (payModel == null)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，支付方式不存在或已删除！")));
                            return;
                        }
                        //检查是否线上支付
                        if (payModel.type == 2)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，账户充值不允许线下支付！")));
                            return;
                        }
                        order_amount = rechargeModel.amount; //订单金额
                    }
                    else if (order_no.ToUpper().StartsWith("B")) //商品订单
                    {
                        //检查订单是否存在
                        orderModel = new BLL.orders().GetModel(order_no);
                        if (orderModel == null)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，订单号不存在或已删除！")));
                            return;
                        }
                        //检查是否已支付过
                        if (orderModel.payment_status == 2)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("payment", "?action=succeed&order_no=" + orderModel.order_no));
                            return;
                        }
                        //检查支付方式
                        payModel = new BLL.site_payment().GetPaymentModel(orderModel.payment_id);
                        if (payModel == null)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，支付方式不存在或已删除！")));
                            return;
                        }
                        //检查是否线下付款
                        if (orderModel.payment_status == 0)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("payment", "?action=succeed&order_no=" + orderModel.order_no));
                            return;
                        }
                        //检查是否积分换购，直接跳转成功页面
                        if (orderModel.order_amount == 0)
                        {
                            //修改订单状态
                            bool result = new BLL.orders().UpdateField(orderModel.order_no, "status=2,payment_status=2,payment_time='" + DateTime.Now + "'");
                            if (!result)
                            {
                                HttpContext.Current.Response.Redirect(linkurl("payment", "?action=error"));
                                return;
                            }
                            HttpContext.Current.Response.Redirect(linkurl("payment", "?action=succeed&order_no=" + orderModel.order_no));
                            return;
                        }
                        order_amount = orderModel.order_amount; //订单金额
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，找不到您要提交的订单类型！")));
                        return;
                    }
                    break;
                case "succeed":
                    //检查订单的类型(充值或购物)
                    if (order_no.ToUpper().StartsWith("R")) //充值订单
                    {
                        rechargeModel = new BLL.user_recharge().GetModel(order_no);
                        if (rechargeModel == null)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，订单号不存在或已删除！")));
                            return;
                        }

                    }
                    else if (order_no.ToUpper().StartsWith("B")) //商品订单
                    {
                        orderModel = new BLL.orders().GetModel(order_no);
                        if (orderModel == null)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，订单号不存在或已删除！")));
                            return;
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，找不到您要提交的订单类型！")));
                        return;
                    }
                    break;
            }
        }
    }
}
