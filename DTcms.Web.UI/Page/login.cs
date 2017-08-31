using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class login : Web.UI.BasePage
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
            turl = linkurl("usercenter", "index");
            if (HttpContext.Current.Request.Url != null && HttpContext.Current.Request.UrlReferrer != null)
            {
                string currUrl = HttpContext.Current.Request.Url.ToString().ToLower(); //当前页面
                string refUrl = HttpContext.Current.Request.UrlReferrer.ToString().ToLower(); //上一页面
                string regPath = linkurl("register").ToLower(); //注册页面
                if (currUrl != refUrl && refUrl.IndexOf(regPath) == -1)
                {
                    turl = HttpContext.Current.Request.UrlReferrer.ToString();
                }
            }
            Utils.WriteCookie(DTKeys.COOKIE_URL_REFERRER, turl); //记住上一页面
            
            Model.users model = GetUserInfo();
            if (model != null)
            {
                //写入登录日志
                //new BLL.user_login_log().Add(model.id, model.user_name, "自动登录");
                //自动登录,跳转URL
                HttpContext.Current.Response.Redirect(turl);
            }
        }

    }
}
