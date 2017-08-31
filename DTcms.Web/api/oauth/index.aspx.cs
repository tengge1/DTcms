using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获得配置信息
            int site_oauth_id = DTRequest.GetQueryInt("id"); //站点的OAuthID
            if (site_oauth_id == 0)
            {
                Response.Write("出错了，当前站点的OAuth参数有误！");
                return;
            }
            oauth_config config = oauth_helper.get_config(site_oauth_id); //获取OAuth应用信息
            if (config == null)
            {
                Response.Write("出错了，获取当前站点的OAuth参数有误！");
                return;
            }
            string state = Guid.NewGuid().ToString().Replace("-", "");
            Session["oauth_state"] = state; //防止CSRF攻击
            Session["site_oauth_id"] = site_oauth_id; //当前站点的OAuthID
            string send_url = string.Empty; //要跳转的网址
            switch (config.oauth_name)
            {
                case "qq":
                    send_url = "https://graph.qq.com/oauth2.0/authorize?response_type=code&client_id=" + config.oauth_app_id + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri) + "&scope=get_user_info,get_info";
                    break;
                case "sina":
                    send_url = "https://api.weibo.com/oauth2/authorize?response_type=code&client_id=" + config.oauth_app_id + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri);
                    break;
                case "renren":
                    send_url = "https://graph.renren.com/oauth/authorize?response_type=code&client_id=" + config.oauth_app_id + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri) + "&scope=read_user_share read_user_feed";
                    break;
                case "kaixin":
                    send_url = "http://api.kaixin001.com/oauth2/authorize?response_type=code&client_id=" + config.oauth_app_id + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri) + "&scope=basic user_birthday";
                    break;
                case "feixin":
                    send_url = "https://i.feixin.10086.cn/oauth2/authorize?response_type=code&client_id=" + config.oauth_app_id + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri) + "&scope=basic";
                    break;
            }
            if (!string.IsNullOrEmpty(send_url))
            {
                Response.Redirect(send_url); //跳转到该网址
            }
            else
            {
                Response.Write("出错了，无法找到该OAuth应用接口！");
                return;
            }

        }
    }
}