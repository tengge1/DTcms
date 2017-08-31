using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.alipaypc;
using DTcms.Common;

namespace DTcms.Web.api.payment.alipaypc
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int site_payment_id = 0; //站点支付方式ID
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                string trade_no = DTRequest.GetString("trade_no"); //支付宝交易号
                string order_no = DTRequest.GetString("out_trade_no").ToUpper(); //获取订单号
                string total_fee = DTRequest.GetString("total_fee"); //获取总金额
                string trade_status = DTRequest.GetString("trade_status"); //交易状态
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
                Notify aliNotify = new Notify(site_payment_id);
                bool verifyResult = aliNotify.Verify(sPara, DTRequest.GetString("notify_id"), DTRequest.GetString("sign"));

                if (verifyResult)//验证成功
                {
                    if (trade_status == "TRADE_FINISHED" || trade_status == "TRADE_SUCCESS")
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
                            if (model.amount != decimal.Parse(total_fee))
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
                            if (model.order_amount != decimal.Parse(total_fee))
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
                    }

                    Response.Write("success");  //请不要修改或删除
                }
                else//验证失败
                {
                    Response.Write("fail");
                }
            }
            else
            {
                Response.Write("无通知参数");
            }
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            coll = Request.Form;

            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }

    }
}