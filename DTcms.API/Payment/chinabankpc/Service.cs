using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Xml;

namespace DTcms.API.Payment.chinabankpc
{
    public class Service
    {
        #region 字段
        //商户编号
        private string _partner = string.Empty;
        //商户MD5密钥
        private string _key = string.Empty;
        //字符编码格式
        private string _input_charset = string.Empty;
        //页面跳转同步返回页面文件路径
        private string _return_url = string.Empty;
        //服务器通知的页面文件路径
        private string _notify_url = string.Empty;
        //支付网关地址
        private string _gateway = "https://pay3.chinabank.com.cn/PayGate?";
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        public Service(int site_payment_id)
        {
            Config xmlConfig = new Config(site_payment_id); //读取配置
            _partner = xmlConfig.Partner.Trim();
            _key = xmlConfig.Key.Trim();
            _input_charset = xmlConfig.Input_charset.Trim().ToLower();
            _return_url = xmlConfig.Return_url.Trim();
            _notify_url = xmlConfig.Notify_url.Trim();
        }



        /// <summary>
        /// 构造提交表单HTML数据
        /// </summary>
        /// <param name="dicPara">请求参数数组</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <param name="strButtonValue">确认按钮显示文字</param>
        /// <returns>提交表单HTML文本</returns>
        public string BuildFormHtml(SortedDictionary<string, string> dicPara, string strMethod, string strButtonValue)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<form id='chinabanksubmit' name='chinabanksubmit' action='" + _gateway + "encoding=" + _input_charset + "' method='" + strMethod.ToLower().Trim() + "'>");
            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }
            //submit按钮控件请不要含有name属性
            sbHtml.Append("<input type='submit' value='" + strButtonValue + "' style='display:none;'></form>");
            sbHtml.Append("<script>document.forms['chinabanksubmit'].submit();</script>");
            return sbHtml.ToString();
        }
    }
}
