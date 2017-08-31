using System;
using System.Xml;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.API.Payment.tenpaypc
{
	/// <summary>
	/// TenpayUtil 的摘要说明。
	/// </summary>
	public class TenpayUtil
	{
        public string tenpay = "1";
        public string partner = ""; //财付通商户号
        public string key  = ""; //财付通密钥;
        public string return_url = ""; //显示支付通知页面;
        public string notify_url = ""; //支付完成后的回调处理页面;

        public TenpayUtil(int site_payment_id)
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
            }
        }

		/** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
		public UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** 取随机数 */
		public string BuildRandomStr(int length) 
		{
			Random rand = new Random();

			int num = rand.Next();

			string str = num.ToString();

			if(str.Length > length)
			{
				str = str.Substring(0,length);
			}
			else if(str.Length < length)
			{
				int n = length - str.Length;
				while(n > 0)
				{
					str.Insert(0, "0");
					n--;
				}
			}
			
			return str;
		}
	}
}