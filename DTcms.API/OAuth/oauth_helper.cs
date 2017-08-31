using System;
using System.Xml;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.API.OAuth
{
    public class oauth_helper
    {
        public oauth_helper()
        { }

        /// <summary>
        /// 获取OAuth配置信息
        /// </summary>
        public static oauth_config get_config(int site_oauth_id)
        {
            //读取接口配置信息
            Model.site_oauth model = new BLL.site_oauth().GetModel(site_oauth_id);
            if (model != null)
            {
                Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig(); //系统配置
                Model.sites siteModel = new BLL.sites().GetModel(model.site_id); //站点配置
                Model.oauth_app appModel = new BLL.oauth_app().GetModel(model.oauth_id); //OAuth应用

                //赋值
                oauth_config config = new oauth_config();
                config.oauth_id = model.id;
                config.oauth_name = appModel.api_path.Trim();
                config.oauth_app_id = model.app_id.Trim();
                config.oauth_app_key = model.app_key.Trim();
                config.site_path = siteModel.build_path;
                if (!string.IsNullOrEmpty(siteModel.domain.Trim()) && siteModel.is_default == 0) //如果有自定义域名且不是默认站点
                {
                    config.return_uri = "http://" + siteModel.domain + "/api/oauth/return_url.aspx";
                }
                else if (siteModel.is_default == 0) //不是默认站点也没有绑定域名
                {
                    config.return_uri = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + siteModel.build_path.ToLower() + "/api/oauth/return_url.aspx";
                }
                else //否则使用当前域名
                {
                    config.return_uri = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + sysConfig.webpath + "api/oauth/return_url.aspx";
                }
                return config;
            }
            return null;
        }
    }
}
