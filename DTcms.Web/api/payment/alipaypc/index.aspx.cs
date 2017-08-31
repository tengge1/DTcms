using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.API.Payment.alipaypc;

namespace DTcms.Web.api.payment.alipaypc
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //读取站点配置信息
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();
            int site_payment_id = 0; //订单支付方式

            //=============================获得订单信息================================
            string order_no = DTRequest.GetFormString("pay_order_no").ToUpper();
            decimal order_amount = DTRequest.GetFormDecimal("pay_order_amount", 0);
            string user_name = DTRequest.GetFormString("pay_user_name");
            string subject = DTRequest.GetFormString("pay_subject");
            
            //检查参数是否正确
            if (string.IsNullOrEmpty(order_no) || order_amount == 0)
            {
                Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，您提交的参数有误！")));
                return;
            }
            if (order_no.StartsWith("R")) //R开头为在线充值订单
            {
                Model.user_recharge model = new BLL.user_recharge().GetModel(order_no);
                if (model == null)
                {
                    Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，您充值的订单号不存在或已删除！")));
                    return;
                }
                if (model.amount != order_amount)
                {
                    Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，您充值的订单金额与实际金额不一致！")));
                    return;
                }
                site_payment_id = model.payment_id; //站点支付方式ID
            }
            else //B开头为商品订单
            {
                Model.orders model = new BLL.orders().GetModel(order_no);
                if (model == null)
                {
                    Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，您支付的订单号不存在或已删除！")));
                    return;
                }
                if (model.order_amount != order_amount)
                {
                    Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，您支付的订单金额与实际金额不一致！")));
                    return;
                }
                site_payment_id = model.payment_id; //站点支付方式ID
            }
            if (user_name != "")
            {
                user_name = "支付会员：" + user_name;
            }
            else
            {
                user_name = "匿名用户";
            }

            //===============================请求参数==================================

            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("payment_type", "1"); //收款类型1商品购买
            sParaTemp.Add("show_url", sysConfig.weburl); //商品展示地址
            sParaTemp.Add("out_trade_no", order_no); //网站订单号
            sParaTemp.Add("subject", sysConfig.webname + "-" + subject); //订单名称
            sParaTemp.Add("body", user_name); //订单描述
            sParaTemp.Add("total_fee", order_amount.ToString()); //订单总金额
            sParaTemp.Add("paymethod", ""); //默认支付方式
            sParaTemp.Add("defaultbank", ""); //默认网银代号
            sParaTemp.Add("anti_phishing_key", ""); //防钓鱼时间戳
            sParaTemp.Add("exter_invoke_ip", DTRequest.GetIP()); ////获取客户端的IP地址
            sParaTemp.Add("buyer_email", ""); //默认买家支付宝账号
            sParaTemp.Add("royalty_type", "");
            sParaTemp.Add("royalty_parameters", "");

            //构造即时到帐接口表单提交HTML数据，无需修改
            Service ali = new Service(site_payment_id);
            string sHtmlText = ali.Create_direct_pay_by_user(sParaTemp);
            Response.Write(sHtmlText);

        }
    }
}