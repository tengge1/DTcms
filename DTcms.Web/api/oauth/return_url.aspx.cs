using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["oauth_state"] == null || Session["site_oauth_id"] == null)
            {
                Response.Write("出错了，无法获取Session信息！");
                return;
            }
            //取得返回参数
            string state = DTRequest.GetQueryString("state");
            if (state != Session["oauth_state"].ToString())
            {
                Response.Write("出错了，state未初始化！");
                return;
            }

            int site_oauth_id = Utils.StrToInt(Session["site_oauth_id"].ToString(), 0); //获取站点OAuthID
            oauth_config config = oauth_helper.get_config(site_oauth_id); //获取配置信息
            switch (config.oauth_name)
            {
                case "qq":
                    qq(config);
                    break;
                case "sina":
                    sina(config);
                    break;
                case "renren":
                    renren(config);
                    break;
                case "kaixin":
                    kaixin(config);
                    break;
                case "feixin":
                    feixin(config);
                    break;
            }

        }

        #region QQ登录处理方法=============================
        private void qq(oauth_config config)
        {
            string access_token = string.Empty;
            string expires_in = string.Empty;
            string client_id = string.Empty;
            string openid = string.Empty;
            //取得返回参数
            string code = DTRequest.GetQueryString("code");
            string state = DTRequest.GetQueryString("state");

            //第一步：获取Access Token
            Dictionary<string, object> dic1 = qq_helper.get_access_token(config.oauth_app_id, config.oauth_app_key, config.return_uri, code, state);
            if (dic1 == null || !dic1.ContainsKey("access_token"))
            {
                Response.Write("错误代码：，无法获取Access Token，请检查App Key是否正确！");
                return;
            }
            access_token = dic1["access_token"].ToString();
            expires_in = dic1["expires_in"].ToString();

            //第二步：通过Access Token来获取用户的OpenID
            Dictionary<string, object> dic2 = qq_helper.get_open_id(access_token);
            if (dic2 == null || !dic2.ContainsKey("openid"))
            {
                if (dic2.ContainsKey("error"))
                {
                    Response.Write("error：" + dic2["error"] + ",error_description：" + dic2["error_description"]);
                }
                else
                {
                    Response.Write("出错啦，无法获取用户授权Openid！");
                }
                return;
            }
            client_id = dic2["client_id"].ToString();
            openid = dic2["openid"].ToString();
            //储存获取数据用到的信息
            Session["site_oauth_id"] = config.oauth_id;
            Session["oauth_name"] = config.oauth_name;
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //第三步：跳转到指定页面
            Response.Redirect(new Web.UI.BasePage().getlink(config.site_path, new Web.UI.BasePage().linkurl("oauth_login")));
            //Response.Write("\access_token:" + access_token + ",openid:" + openid);
            return;
        }
        #endregion

        #region 微博登录处理方法===========================
        private void sina(oauth_config config)
        {
            string access_token = string.Empty;
            string expires_in = string.Empty;
            string client_id = string.Empty;
            string openid = string.Empty;
            //取得返回参数
            string state = DTRequest.GetQueryString("state");
            string code = DTRequest.GetQueryString("code");

            //第一步：获取Access Token
            Dictionary<string, object> dic = sina_helper.get_access_token(config.oauth_app_id, config.oauth_app_key, config.return_uri, code);
            if (dic == null || !dic.ContainsKey("access_token"))
            {
                Response.Write("出错了，无法获取Access Token，请检查App Key是否正确！");
                return;
            }

            access_token = dic["access_token"].ToString();
            expires_in = dic["expires_in"].ToString();
            openid = dic["uid"].ToString();
            //储存获取数据用到的信息
            Session["site_oauth_id"] = config.oauth_id;
            Session["oauth_name"] = config.oauth_name;
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //第二步：跳转到指定页面
            Response.Redirect(new Web.UI.BasePage().getlink(config.site_path, new Web.UI.BasePage().linkurl("oauth_login")));
            return;
        }
        #endregion

        #region 人人网登录处理方法=========================
        private void renren(oauth_config config)
        {
            string access_token = string.Empty;
            string expires_in = string.Empty;
            string openid = string.Empty;
            //取得返回参数
            string state = DTRequest.GetQueryString("state");
            string code = DTRequest.GetQueryString("code");

            if (string.IsNullOrEmpty(code))
            {
                Response.Write("授权被取消，相关信息：" + DTRequest.GetQueryString("error"));
                return;
            }

            //获取Access Token
            Dictionary<string, object> dic = renren_helper.get_access_token(config.oauth_app_id, config.oauth_app_key, config.return_uri, code);
            if (dic == null)
            {
                Response.Write("错误代码：，无法获取Access Token，请检查App Key是否正确！");
            }

            access_token = dic["access_token"].ToString();
            expires_in = dic["expires_in"].ToString();
            Dictionary<string, object> dic1 = dic["user"] as Dictionary<string, object>;
            openid = dic1["id"].ToString();
            //储存获取数据用到的信息
            Session["site_oauth_id"] = config.oauth_id;
            Session["oauth_name"] = config.oauth_name;
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //跳转到指定页面
            Response.Redirect(new Web.UI.BasePage().getlink(config.site_path, new Web.UI.BasePage().linkurl("oauth_login")));
            return;
        }
        #endregion

        #region 开心网登录处理方法=========================
        private void kaixin(oauth_config config)
        {
            string access_token = string.Empty;
            string expires_in = string.Empty;
            string client_id = string.Empty;
            string openid = string.Empty;
            //取得返回参数
            string state = DTRequest.GetQueryString("state");
            string code = DTRequest.GetQueryString("code");

            //第一步：获取Access Token
            Dictionary<string, object> dic1 = kaixin_helper.get_access_token(config.oauth_app_id, config.oauth_app_key, config.return_uri, code, state);
            if (dic1 == null)
            {
                Response.Write("出错了，无法获取Access Token，请检查App Key是否正确！");
                return;
            }
            access_token = dic1["access_token"].ToString();
            expires_in = dic1["expires_in"].ToString();

            //第二步：通过Access Token来获取用户的ID
            Dictionary<string, object> dic2 = kaixin_helper.get_info(access_token, "uid");
            if (dic2 == null)
            {
                Response.Write("出错啦，无法获取用户授权uid！");
                return;
            }
            openid = dic2["uid"].ToString();
            //储存获取数据用到的信息
            Session["site_oauth_id"] = config.oauth_id;
            Session["oauth_name"] = config.oauth_name;
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //第三步：跳转到指定页面
            Response.Redirect(new Web.UI.BasePage().getlink(config.site_path, new Web.UI.BasePage().linkurl("oauth_login")));
            return;
        }
        #endregion

        #region 飞信登录处理方法===========================
        private void feixin(oauth_config config)
        {
            string access_token = string.Empty;
            string expires_in = string.Empty;
            string client_id = string.Empty;
            string openid = string.Empty;
            //取得返回参数
            string state = DTRequest.GetQueryString("state");
            string code = DTRequest.GetQueryString("code");

            //第一步：获取Access Token
            Dictionary<string, object> dic1 = feixin_helper.get_access_token(config.oauth_app_id, config.oauth_app_key, config.return_uri, code);
            if (dic1 == null || !dic1.ContainsKey("access_token"))
            {
                Response.Write("出错了，无法获取Access Token，请检查App Key是否正确！");
                return;
            }
            access_token = dic1["access_token"].ToString();
            expires_in = dic1["expires_in"].ToString();
            //第二步：通过Access Token来获取用户的ID
            Dictionary<string, object> dic2 = feixin_helper.get_info(access_token);
            if (dic2 == null || !dic2.ContainsKey("userId"))
            {
                Response.Write("出错啦，无法获取用户授权uid！");
                return;
            }
            openid = dic2["userId"].ToString();
            //储存获取数据用到的信息
            Session["site_oauth_id"] = config.oauth_id;
            Session["oauth_name"] = config.oauth_name;
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //第三步：跳转到指定页面
            Response.Redirect(new Web.UI.BasePage().getlink(config.site_path, new Web.UI.BasePage().linkurl("oauth_login")));
            return;
        }
        #endregion

    }
}