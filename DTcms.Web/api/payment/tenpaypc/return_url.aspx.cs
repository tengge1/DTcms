using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.tenpaypc;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpaypc
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int site_payment_id = 0; //站点支付方式ID
            ResponseHandler resHandler = new ResponseHandler(Context); //创建ResponseHandler实例
            string notify_id = resHandler.getParameter("notify_id"); //通知id
            string out_trade_no = resHandler.getParameter("out_trade_no").ToUpper(); //商户订单号
            string transaction_id = resHandler.getParameter("transaction_id"); //财付通订单号
            string total_fee = resHandler.getParameter("total_fee"); //金额,以分为单位
            string discount = resHandler.getParameter("discount"); //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
            string trade_state = resHandler.getParameter("trade_state"); //支付结果

            if (out_trade_no.StartsWith("R")) //充值订单
            {
                site_payment_id = new BLL.user_recharge().GetPaymentId(out_trade_no);
            }
            else if (out_trade_no.StartsWith("B")) //商品订单
            {
                site_payment_id = new BLL.orders().GetPaymentId(out_trade_no);
            }
            //找到站点支付方式ID开始验证
            if (site_payment_id > 0)
            {
                TenpayUtil config = new TenpayUtil(site_payment_id);
                resHandler.setKey(config.key);
                //判断签名
                if (resHandler.isTenpaySign())
                {
                    if ("0".Equals(trade_state))
                    {
                        //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                        Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=succeed&order_no=" + out_trade_no));
                        return;
                    }
                }
            }
            //认证签名失败
            Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=error"));
            return;
        }
    }
}