using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class recharge_edit : Web.UI.ManagePage
    {
        private string username = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.username = DTRequest.GetQueryString("username"); //获得用户名
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_recharge_log", DTEnums.ActionEnum.View.ToString()); //检查权限
                if (!string.IsNullOrEmpty(username))
                {
                    txtUserName.Text = username; //赋值用户名
                }
                TreeBind("type=1"); //绑定支付方式
                txtRechargeNo.Text = Utils.GetOrderNumber(); //随机生成订单号
            }
        }

        #region 绑定支付方式=============================
        private void TreeBind(string strWhere)
        {
            BLL.site_payment bll = new BLL.site_payment();
            DataTable dt = bll.GetList(0, strWhere).Tables[0];

            this.ddlPaymentId.Items.Clear();
            this.ddlPaymentId.Items.Add(new ListItem("请选择支付方式", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlPaymentId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.users userModel = new BLL.users().GetModel(txtUserName.Text.Trim());
            if (userModel == null)
            {
                return false;
            }

            bool result = false;
            Model.user_recharge model = new Model.user_recharge();
            BLL.user_recharge bll = new BLL.user_recharge();

            model.user_id = userModel.id;
            model.user_name = userModel.user_name;
            model.recharge_no = "R" + txtRechargeNo.Text.Trim(); //订单号R开头为充值订单
            model.payment_id = Utils.StrToInt(ddlPaymentId.SelectedValue, 0);
            model.amount = Utils.StrToDecimal(txtAmount.Text.Trim(), 0);
            model.status = 1;
            model.add_time = DateTime.Now;
            model.complete_time = DateTime.Now;

            if (bll.Recharge(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "给会员：" + model.user_name + "，充值:" + model.amount + "元"); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        // 提交保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_recharge_log", DTEnums.ActionEnum.Add.ToString()); //检查权限
            if (!DoAdd())
            {
                JscriptMsg("保存过程中发生错误！", "");
                return;
            }
            JscriptMsg("会员充值成功！", "recharge_list.aspx");
        }

    }
}