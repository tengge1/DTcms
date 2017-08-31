using System.Web;
using System.Text;
using System.IO;
using System.Net;
using System;
using System.Xml;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.API.Payment.chinabankpc
{
    public class Config
    {
        #region 字段
        private string partner = string.Empty;
        private string key = string.Empty;
        private string return_url = string.Empty;
        private string notify_url = string.Empty;
        private string input_charset = string.Empty;
        #endregion

        public Config(int site_payment_id)
        {
            Model.site_payment model = new BLL.site_payment().GetModel(site_payment_id); //站点支付方式
            if (model != null)
            {
                Model.payment payModel = new BLL.payment().GetModel(model.payment_id); //支付平台
                Model.sites siteModel = new BLL.sites().GetModel(model.site_id); //站点配置
                Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig(); //系统配置

                partner = model.key1; //商户号（必须配置）
                key = model.key2; //商户支付密钥，参考开户邮件设置（必须配置）
                //回调处理地址
                if (!string.IsNullOrEmpty(siteModel.domain.Trim()) && siteModel.is_default == 0) //如果有自定义域名且不是默认站点
                {
                    return_url = "http://" + siteModel.domain + payModel.return_url;
                    notify_url = "http://" + siteModel.domain + payModel.notify_url;
                }
                else if (siteModel.is_default == 0) //不是默认站点也没有绑定域名
                {
                    return_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + siteModel.build_path.ToLower() + payModel.return_url;
                    notify_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + siteModel.build_path.ToLower() + payModel.notify_url;
                }
                else //否则使用当前域名
                {
                    return_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + payModel.return_url;
                    notify_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + payModel.notify_url;
                }
                input_charset = "utf-8";
            }
        }

        #region 属性
        /// <summary>
        /// 商户编号
        /// </summary>
        public string Partner
        {
            get { return partner; }
            set { partner = value; }
        }

        /// <summary>
        /// 获取或设置交易安全检验码
        /// </summary>
        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        /// <summary>
        /// 获取页面跳转同步通知页面路径
        /// </summary>
        public string Return_url
        {
            get { return return_url; }
        }

        /// <summary>
        /// 获取服务器异步通知页面路径
        /// </summary>
        public string Notify_url
        {
            get { return notify_url; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public string Input_charset
        {
            get { return input_charset; }
        }
        #endregion
    }
}
