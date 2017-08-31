using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class register : Web.UI.BasePage
    {
        protected string action = string.Empty;
        protected string username = string.Empty;

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            action = DTRequest.GetQueryString("action");
            username = DTRequest.GetQueryString("username");
            username = Utils.DropHTML(username);
            //检查是否关闭会员注册服务
            if (action == "" && uconfig.regstatus == 0)
            {
                HttpContext.Current.Response.Redirect(linkurl("register", "?action=close"));
                return;
            }
            //Email验证
            if (action == "checkmail")
            {
                string code = DTRequest.GetQueryString("code");
                BLL.user_code bll = new BLL.user_code();
                Model.user_code model = bll.GetModel(code);
                if (model == null) //返回出错
                {
                    HttpContext.Current.Response.Redirect(linkurl("register", "?action=checkerror"));
                    return;
                }
                //修改申请码状态
                model.status = 1;
                bll.Update(model);
                //修改用户状态
                new BLL.users().UpdateField(model.user_id, "status=0");
            }
        }
    }
}
