using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class userorder_show : Web.UI.UserPage
    {
        protected int id;
        protected Model.orders model;
        protected Model.payment payModel;

        /// <summary>
        /// 重写虚方法,此方法在Init事件执行
        /// </summary>
        protected override void InitPage()
        {
            id = DTRequest.GetQueryInt("id");
            BLL.orders bll = new BLL.orders();
            if (!bll.Exists(id))
            {
                HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错了，您要浏览的页面不存在或已删除！")));
                return;
            }
            model = bll.GetModel(id);
            if (model.user_id != userModel.id)
            {
                HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("出错了，您所查看的并非自己的订单信息！")));
                return;
            }
            payModel = new BLL.site_payment().GetPaymentModel(model.payment_id);
            if (payModel == null)
            {
                payModel = new Model.payment();
            }
        }

    }
}
