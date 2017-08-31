using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.order
{
    public partial class order_edit : Web.UI.ManagePage
    {
        private int id = 0;
        protected Model.orders model = new Model.orders();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = DTRequest.GetQueryInt("id");
            if (this.id == 0)
            {
                JscriptMsg("传输参数不正确！", "back");
                return;
            }
            if (!new BLL.orders().Exists(this.id))
            {
                JscriptMsg("记录不存在或已被删除！", "back");
                return;
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("order_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo(this.id);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.orders bll = new BLL.orders();
            model = bll.GetModel(_id);
            //绑定商品列表
            this.rptList.DataSource = model.order_goods;
            this.rptList.DataBind();
            //获得会员信息
            if (model.user_id > 0)
            {
                Model.users user_info = new BLL.users().GetModel(model.user_id);
                if (user_info != null)
                {
                    Model.user_groups group_info = new BLL.user_groups().GetModel(user_info.group_id);
                    if (group_info != null)
                    {
                        dlUserInfo.Visible = true;
                        lbUserName.Text = user_info.user_name;
                        lbUserGroup.Text = group_info.title;
                        lbUserDiscount.Text = group_info.discount.ToString() + " %";
                        lbUserAmount.Text = user_info.amount.ToString();
                        lbUserPoint.Text = user_info.point.ToString();
                    }
                }
            }
            //根据订单状态，显示各类操作按钮
            switch (model.status)
            {
                case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                    if (model.payment_status > 0)
                    {
                        //确认付款、取消订单、修改收货按钮显示
                        btnPayment.Visible = btnCancel.Visible = btnEditAcceptInfo.Visible = true;
                    }
                    else
                    {
                        //确认订单、取消订单、修改收货按钮显示
                        btnConfirm.Visible = btnCancel.Visible = btnEditAcceptInfo.Visible = true;
                    }
                    //修改订单备注、修改商品总金额、修改配送费用、修改支付手续费、修改发票税金按钮显示
                    btnEditRemark.Visible = btnEditRealAmount.Visible = btnEditExpressFee.Visible = btnEditPaymentFee.Visible = btnEditInvoiceTaxes.Visible = true;
                    break;
                case 2: //如果订单为已确认状态，则进入发货状态
                    if (model.express_status == 1)
                    {
                        //确认发货、取消订单、修改收货信息按钮显示
                        btnExpress.Visible = btnCancel.Visible = btnEditAcceptInfo.Visible = true;
                    }
                    else if (model.express_status == 2)
                    {
                        //完成订单、取消订单按钮可见
                        btnComplete.Visible = btnCancel.Visible = true;
                    }
                    //修改订单备注按钮可见
                    btnEditRemark.Visible = true;
                    break;
                case 3:
                    //作废订单、修改订单备注按钮可见
                    btnInvalid.Visible = btnEditRemark.Visible = true;
                    break;
            }

        }
        #endregion

    }
}