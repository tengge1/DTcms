using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class error : System.Web.UI.Page
    {
        protected internal Model.sysconfig config = new BLL.sysconfig().loadConfig();
        protected string msg = string.Empty;

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        public error()
        {
            msg = Utils.ToHtml(DTRequest.GetQueryString("msg"));
        }

    }
}
