using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.tenpaypc;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpaypc
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int site_payment_id = 0; //站点支付方式ID
            ResponseHandler resHandler = new ResponseHandler(Context); //创建ResponseHandler实例
            string order_no = resHandler.getParameter("out_trade_no").ToUpper(); //商户订单号
            string trade_no = resHandler.getParameter("transaction_id"); //财付通订单号
            string total_fee = resHandler.getParameter("total_fee"); //金额,以分为单位
            string discount = resHandler.getParameter("discount"); //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
            string trade_state = resHandler.getParameter("trade_state"); //支付结果
            if (order_no.StartsWith("R")) //充值订单
            {
                site_payment_id = new BLL.user_recharge().GetPaymentId(order_no);
            }
            else if (order_no.StartsWith("B")) //商品订单
            {
                site_payment_id = new BLL.orders().GetPaymentId(order_no);
            }
            if (site_payment_id == 0)
            {
                Response.Write("该订单号不存在");
                return;
            }

            //找到站点支付方式ID开始验证
            TenpayUtil config = new TenpayUtil(site_payment_id);
            resHandler.setKey(config.key);
            //判断签名
            if (resHandler.isTenpaySign())
            {
                string notify_id = resHandler.getParameter("notify_id"); //通知id
                RequestHandler queryReq = new RequestHandler(Context); //通过通知ID查询，确保通知来至财付通
                queryReq.init();
                queryReq.setKey(config.key);
                queryReq.setGateUrl("https://gw.tenpay.com/gateway/simpleverifynotifyid.xml");
                queryReq.setParameter("partner", config.partner);
                queryReq.setParameter("notify_id", notify_id);

                //通信对象
                TenpayHttpClient httpClient = new TenpayHttpClient();
                httpClient.setTimeOut(5);
                //设置请求内容
                httpClient.setReqContent(queryReq.getRequestURL());
                //后台调用
                if (httpClient.call())
                {
                    //设置结果参数
                    ClientResponseHandler queryRes = new ClientResponseHandler();
                    queryRes.setContent(httpClient.getResContent());
                    queryRes.setKey(config.key);
                    //判断签名及结果
                    //只有签名正确,retcode为0，trade_state为0才是支付成功
                    if (queryRes.isTenpaySign())
                    {
                        //判断签名及结果
                        if ("0".Equals(queryRes.getParameter("retcode")))
                        {
                            #region 即时到账处理方法====================================
                            if ("0".Equals(trade_state))
                            {
                                if (order_no.StartsWith("R")) //充值订单
                                {
                                    BLL.user_recharge bll = new BLL.user_recharge();
                                    Model.user_recharge model = bll.GetModel(order_no);
                                    if (model == null)
                                    {
                                        Response.Write("该订单号不存在");
                                        return;
                                    }
                                    if (model.status == 1) //已成功
                                    {
                                        Response.Write("success");
                                        return;
                                    }
                                    if (model.amount != (decimal.Parse(total_fee) / 100))
                                    {
                                        Response.Write("订单金额和支付金额不相符");
                                        return;
                                    }
                                    bool result = bll.Confirm(order_no);
                                    if (!result)
                                    {
                                        Response.Write("修改订单状态失败");
                                        return;
                                    }
                                }
                                else if (order_no.StartsWith("B")) //商品订单
                                {
                                    BLL.orders bll = new BLL.orders();
                                    Model.orders model = bll.GetModel(order_no);
                                    if (model == null)
                                    {
                                        Response.Write("该订单号不存在");
                                        return;
                                    }
                                    if (model.payment_status == 2) //已付款
                                    {
                                        Response.Write("success");
                                        return;
                                    }
                                    if (model.order_amount != (decimal.Parse(total_fee) / 100))
                                    {
                                        Response.Write("订单金额和支付金额不相符");
                                        return;
                                    }
                                    bool result = bll.UpdateField(order_no, "trade_no='" + trade_no + "',status=2,payment_status=2,payment_time='" + DateTime.Now + "'");
                                    if (!result)
                                    {
                                        Response.Write("修改订单状态失败");
                                        return;
                                    }
                                    //扣除积分
                                    if (model.point < 0)
                                    {
                                        new BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
                                    }
                                }
                                //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                                Response.Write("success");
                            }
                            else
                            {
                                Response.Write("即时到账支付失败");
                            }
                            #endregion
                        }
                        else
                        {
                            Response.Write("查询验证签名失败或id验证失败");
                        }
                    }
                    else
                    {
                        Response.Write("通知ID查询签名验证失败");
                    }
                }
                else
                {
                    Response.Write("后台调用通信失败");
                }
            }
            else
            {
                Response.Write("签名验证失败");
            }
            Response.End();
        }
    }
}