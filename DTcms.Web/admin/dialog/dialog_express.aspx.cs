using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.dialog
{
    public partial class dialog_express : Web.UI.ManagePage
    {
        private string order_no = string.Empty;

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
            Model.orders model = bll.GetModel(_order_no);

            BLL.express bll2 = new BLL.express();
            DataTable dt = bll2.GetList(0, string.Empty, "sort_id asc,id desc").Tables[0];
            ddlExpressId.Items.Clear();
            ddlExpressId.Items.Add(new ListItem("请选择配送方式", ""));
            foreach (DataRow dr in dt.Rows)
            {
                ddlExpressId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
            txtExpressNo.Text = model.express_no;
            ddlExpressId.SelectedValue = model.express_id.ToString();

        }
        #endregion

    }
}