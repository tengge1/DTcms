using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class oauth_login : Web.UI.BasePage
    {
        protected string turl = string.Empty;
        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(UserPage_Init);
        }

        /// <summary>
        /// OnInit事件,检查用户是否已经登录
        /// </summary>
        void UserPage_Init(object sender, EventArgs e)
        {
            turl = Utils.GetCookie(DTKeys.COOKIE_URL_REFERRER);
            if (string.IsNullOrEmpty(turl) || turl == HttpContext.Current.Request.Url.ToString().ToLower())
            {
                turl = linkurl("usercenter", "index");
            }
            if (IsUserLogin())
            {
                //自动登录,跳转URL
                HttpContext.Current.Response.Redirect(turl);
                return;
            }
            //检查是否已授权
            if (HttpContext.Current.Session["oauth_name"] == null || HttpContext.Current.Session["oauth_access_token"] == null || HttpContext.Current.Session["oauth_openid"] == null)
            {
                HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("登录失败，用户授权已过期，请重新登录！")));
                return;
            }
            Model.user_oauth oauthModel = new BLL.user_oauth().GetModel(HttpContext.Current.Session["oauth_name"].ToString(), HttpContext.Current.Session["oauth_openid"].ToString());
            if (oauthModel != null)
            {
                //检查用户是否存在
                Model.users model = new BLL.users().GetModel(oauthModel.user_name);
                if (model == null)
                {
                    HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("登录失败，授权用户不存在或已被删除！")));
                    return;
                }
                
                //记住登录状态，防止Session提前过期
                HttpContext.Current.Session[DTKeys.SESSION_USER_INFO] = model;
                HttpContext.Current.Session.Timeout = 45;
                Utils.WriteCookie(DTKeys.COOKIE_USER_NAME_REMEMBER, "DTcms", model.user_name);
                Utils.WriteCookie(DTKeys.COOKIE_USER_PWD_REMEMBER, "DTcms", model.password);
                //更新最新的Access Token
                oauthModel.oauth_access_token = HttpContext.Current.Session["oauth_access_token"].ToString();
                new BLL.user_oauth().Update(oauthModel);
                //自动登录,跳转URL
                HttpContext.Current.Response.Redirect(turl);
                return;
            }
        }

    }
}
