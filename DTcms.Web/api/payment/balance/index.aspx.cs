using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.api.payment.balance
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //读取站点配置信息
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();
            string order_no = DTRequest.GetFormString("pay_order_no").ToUpper();
            BLL.orders objorders = new BLL.orders();
            Model.orders modelorders = objorders.GetModel(order_no);
            if (modelorders == null)
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，订单详情获取出错,请重试！")));
                return;
            }
            decimal order_amount = modelorders.order_amount;
            string subject = DTRequest.GetFormString("pay_subject");
            if (order_no == "" || order_amount == 0 )
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，您提交的参数有误！")));
                return;
            }
            //检查是否已登录
            Model.users userModel = new Web.UI.BasePage().GetUserInfo();
            if (userModel == null)
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=login")); //尚未登录
                return;
            }
            if (userModel.amount < order_amount)
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=recharge")); //账户的余额不足
                return;
            }

            if (order_no.StartsWith("B")) //B开头为商品订单
            {
                BLL.orders bll = new BLL.orders();
                Model.orders model = bll.GetModel(order_no);
                if (model == null)
                {
                    Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，商品订单号不存在！")));
                    return;
                }
                if (model.payment_status == 1)
                {
                    //执行扣取账户金额
                    int result = new BLL.user_amount_log().Add(userModel.id, userModel.user_name, -1 * order_amount, subject);
                    if (result > 0)
                    {
                        //更改订单状态
                        bool result1 = bll.UpdateField(order_no, "status=2,payment_status=2,payment_time='" + DateTime.Now + "'");
                        if (!result1)
                        {
                            Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=error"));
                            return;
                        }
                        //扣除积分
                        if (model.point < 0)
                        {
                            new BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
                        }
                    }
                    else
                    {
                        Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=error"));
                        return;
                    }
                }
                //支付成功
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=succeed&order_no=" + order_no));
                return;
            }
            Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，找不到需要支付的订单类型！")));
            return;
        }
    }
}