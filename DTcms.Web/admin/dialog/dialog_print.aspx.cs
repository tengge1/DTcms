using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.dialog
{
    public partial class dialog_print : Web.UI.ManagePage
    {
        private string order_no = string.Empty;
        protected Model.orders model = new Model.orders();
        protected Model.manager adminModel = new Model.manager();

        protected void Page_Load(object sender, EventArgs e)
        {
            order_no = DTRequest.GetQueryString("order_no");
            if (order_no == "")
            {
                JscriptMsg("传输参数不正确！", "back");
                return;
            }
            if (!new BLL.orders().Exists(order_no))
            {
                JscriptMsg("订单不存在或已被删除！", "back");
                return;
            }
            if (!Page.IsPostBack)
            {
                ShowInfo(order_no);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(string _order_no)
        {
            BLL.orders bll = new BLL.orders();
            model = bll.GetModel(_order_no);
            adminModel = GetAdminInfo();
            this.rptList.DataSource = model.order_goods;
            this.rptList.DataBind();
        }
        #endregion

    }
}