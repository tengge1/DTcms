using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class repassword: Web.UI.BasePage
    {
        protected string action;
        protected string username = string.Empty;
        protected string code = string.Empty;

        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            action = DTRequest.GetQueryString("action");
            if (action == "mobile")
            {
                username = DTRequest.GetQueryString("username");
            }
            else if (action == "email")
            {
                code = DTRequest.GetQueryString("code");
                Model.user_code model = new BLL.user_code().GetModel(code);
                if (model == null)
                {
                    HttpContext.Current.Response.Redirect(linkurl("repassword", "?action=error"));
                    return;
                }
                username = model.user_name;
            }
        }

    }
}
