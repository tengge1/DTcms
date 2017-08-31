using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using DTcms.API.Payment.chinabankpc;

namespace DTcms.Web.api.payment.chinabankpc
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //读取站点配置信息
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig(); //系统配置
            int site_payment_id = 0; //订单支付方式

            //=============================获得订单信息================================
            string order_no = DTRequest.GetFormString("pay_order_no").ToUpper();
            decimal order_amount = DTRequest.GetFormDecimal("pay_order_amount", 0);
            string user_name = DTRequest.GetFormString("pay_user_name");
            string subject = DTRequest.GetFormString("pay_subject");
            //以下收货人信息
            string receive_name = string.Empty; //收货人姓名
            string receive_address = string.Empty; //收货人地址
            string receive_zip = string.Empty; //收货人邮编
            string receive_phone = string.Empty; //收货人电话
            string receive_mobile = string.Empty; //收货人手机
            //检查参数是否正确
            if (order_no == "" || order_amount == 0)
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

                //取得用户信息
                Model.users userModel = new BLL.users().GetModel(model.user_id);
                if (userModel == null)
                {
                    Response.Redirect(new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("对不起，用户账户不存在或已删除！")));
                    return;
                }
                receive_name = userModel.nick_name;
                receive_address = userModel.address;
                receive_phone = userModel.telphone;
                receive_mobile = userModel.mobile;
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
                receive_name = model.accept_name;
                receive_address = model.address;
                receive_zip = model.post_code;
                receive_phone = model.telphone;
                receive_mobile = model.mobile;
            }
            if (!string.IsNullOrEmpty(user_name))
            {
                user_name = "支付会员：" + user_name;
            }
            else
            {
                user_name = "匿名用户";
            }

            //===============================加密签名==================================
            Config config = new Config(site_payment_id);
            string moneytype = "CNY";
            // 拼凑加密串=订单金额+币种+订单号+商户号+返回地址+商户MD5密钥
            string signtext = order_amount + moneytype + order_no + config.Partner + config.Return_url + config.Key;
            string md5info = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(signtext, "md5").ToUpper();

            //===============================请求参数==================================
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("v_mid", config.Partner); //商户编号
            sParaTemp.Add("v_oid", order_no); //网站订单号
            sParaTemp.Add("v_amount", order_amount.ToString()); //订单总金额
            sParaTemp.Add("v_moneytype", moneytype); //币种
            sParaTemp.Add("v_url", config.Return_url); //返回地址
            sParaTemp.Add("remark2", "[url:=" + config.Notify_url + "]"); //回调地址
            sParaTemp.Add("v_md5info", md5info); //MD5校验码
            sParaTemp.Add("remark1", sysConfig.webname + "-" + subject + user_name); //订单描述

            sParaTemp.Add("v_rcvname", receive_name); //收货人姓名
            sParaTemp.Add("v_rcvaddr", receive_address); //收货人地址
            sParaTemp.Add("v_rcvtel", receive_phone); //收货人电话
            sParaTemp.Add("v_rcvpost", receive_zip); //收货人邮编
            sParaTemp.Add("v_rcvmobile", receive_mobile); //收货人手机号

            //构造即时到帐接口表单提交HTML数据，无需修改
            Service chinabank = new Service(site_payment_id);
            string sHtmlText = chinabank.BuildFormHtml(sParaTemp, "post", "确认");
            Response.Write(sHtmlText);
        }
    }
}