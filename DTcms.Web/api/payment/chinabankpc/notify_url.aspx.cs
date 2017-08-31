using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.API.Payment.chinabankpc;

namespace DTcms.Web.api.payment.chinabankpc
{
    public partial class notify_url : System.Web.UI.Page
    {
        protected string v_oid; //订单号
        protected string v_pstatus; //支付状态码
        //20（支付成功，对使用实时银行卡进行扣款的订单）；
        //30（支付失败，对使用实时银行卡进行扣款的订单）；
        protected string v_pstring; //支付状态描述
        protected string v_pmode; //支付银行
        protected string v_md5str; //MD5校验码
        protected string v_amount; //支付金额
        protected string v_moneytype; //币种		
        protected string remark1;//备注1
        protected string remark2;//备注1

        protected void Page_Load(object sender, EventArgs e)
        {
            //读取站点配置信息
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();
            int site_payment_id = 0; //站点支付方式ID

            v_oid = DTRequest.GetString("v_oid").ToUpper(); //订单号
            v_pstatus = DTRequest.GetString("v_pstatus");
            v_pstring = DTRequest.GetString("v_pstring");
            v_pmode = DTRequest.GetString("v_pmode");
            v_md5str = DTRequest.GetString("v_md5str");
            v_amount = DTRequest.GetString("v_amount");
            v_moneytype = DTRequest.GetString("v_moneytype");
            remark1 = DTRequest.GetString("remark1");
            remark2 = DTRequest.GetString("remark2");
            if (v_oid.StartsWith("R")) //充值订单
            {
                site_payment_id = new BLL.user_recharge().GetPaymentId(v_oid);
            }
            else if (v_oid.StartsWith("B")) //商品订单
            {
                site_payment_id = new BLL.orders().GetPaymentId(v_oid);
            }
            if (site_payment_id == 0)
            {
                Response.Write("error");
                return;
            }

            // 拼凑加密串
            Config config = new Config(site_payment_id);
            string signtext = v_oid + v_pstatus + v_amount + v_moneytype + config.Key;
            signtext = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(signtext, "md5").ToUpper();

            if (signtext == v_md5str && v_pstatus.Equals("20"))
            {
                //成功状态
                if (v_oid.StartsWith("R")) //充值订单
                {
                    BLL.user_recharge bll = new BLL.user_recharge();
                    Model.user_recharge model = bll.GetModel(v_oid);
                    if (model == null)
                    {
                        Response.Write("error");
                        return;
                    }
                    if (model.status == 1) //已成功
                    {
                        Response.Write("ok");
                        return;
                    }
                    if (model.amount != decimal.Parse(v_amount))
                    {
                        Response.Write("error");
                        return;
                    }
                    bool result = bll.Confirm(v_oid);
                    if (!result)
                    {
                        Response.Write("error");
                        return;
                    }
                }
                else if (v_oid.StartsWith("B")) //商品订单
                {
                    BLL.orders bll = new BLL.orders();
                    Model.orders model = bll.GetModel(v_oid);
                    if (model == null)
                    {
                        Response.Write("error");
                        return;
                    }
                    if (model.payment_status == 2) //已付款
                    {
                        Response.Write("ok");
                        return;
                    }
                    if (model.order_amount != decimal.Parse(v_amount))
                    {
                        Response.Write("error");
                        return;
                    }
                    bool result = bll.UpdateField(v_oid, "status=2,payment_status=2,payment_time='" + DateTime.Now + "'");
                    if (!result)
                    {
                        Response.Write("error");
                        return;
                    }

                    //扣除积分
                    if (model.point < 0)
                    {
                        new BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
                    }
                }

                //成功状态
                Response.Write("ok");
                return;
            }

            //失败状态
            Response.Write("error");
            return;
        }
    }
}