using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.order
{
    public partial class site_payment_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private Model.site_payment model;
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.site_payment().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
                //赋值实体
                this.model = new BLL.site_payment().GetModel(this.id);
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("order_payment", DTEnums.ActionEnum.View.ToString()); //检查权限
                SiteBind(); //绑定站点
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 显示相关字段=============================
        private void ShowFields(int _payment_id)
        {
            Model.payment payModel = new BLL.payment().GetModel(_payment_id);
            if (payModel == null)
            {
                return;
            }
            switch (payModel.api_path.ToLower())
            {
                case "alipaypc":
                    div_key1_container.Visible = true;
                    div_key1_title.Text = "支付宝账号";
                    div_key1_tip.Text = "*签约支付宝账号或卖家支付宝帐户";
                    div_key2_container.Visible = true;
                    div_key2_title.Text = "合作者身份(partner ID)";
                    div_key2_tip.Text = "*合作身份者ID，以2088开头由16位纯数字组成的字符串";
                    div_key3_container.Visible = true;
                    div_key3_title.Text = "交易安全校验码(key)";
                    div_key3_tip.Text = "*交易安全检验码，由数字和字母组成的32位字符串";
                    div_key4_container.Visible = false;
                    txtKey4.Text = string.Empty;
                    break;
                case "tenpaypc":
                    div_key1_container.Visible = true;
                    div_key1_title.Text = "财付通商户号";
                    div_key1_tip.Text = "*财付通商家服务商户号";
                    div_key2_container.Visible = true;
                    div_key2_title.Text = "财付通密钥";
                    div_key2_tip.Text = "*财付通商家服务密钥";
                    div_key3_container.Visible = false;
                    txtKey3.Text = string.Empty;
                    div_key4_container.Visible = false;
                    txtKey4.Text = string.Empty;
                    break;
                case "chinabankpc":
                    div_key1_container.Visible = true;
                    div_key1_title.Text = "商户编号";
                    div_key1_tip.Text = "*网银在线商户编号";
                    div_key2_container.Visible = true;
                    div_key2_title.Text = "MD5校验码";
                    div_key2_tip.Text = "*网银在线MD5校验码";
                    div_key3_container.Visible = false;
                    txtKey3.Text = string.Empty;
                    div_key4_container.Visible = false;
                    txtKey4.Text = string.Empty;
                    break;
                default:
                    div_key1_container.Visible = false;
                    div_key2_container.Visible = false;
                    div_key3_container.Visible = false;
                    div_key4_container.Visible = false;
                    txtKey3.Text = string.Empty;
                    txtKey2.Text = string.Empty;
                    txtKey1.Text = string.Empty;
                    txtKey4.Text = string.Empty;
                    break;
            }
        }
        #endregion

        #region 绑定站点=================================
        private void SiteBind()
        {
            BLL.sites bll = new BLL.sites();
            DataTable dt = bll.GetList(0, "is_lock=0", "sort_id asc,id desc").Tables[0];

            this.ddlSiteId.Items.Clear();
            this.ddlSiteId.Items.Add(new ListItem("请选择站点...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlSiteId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 绑定平台=================================
        private void PaymentBind(int _site_id, int _payment_id)
        {
            if (_site_id > 0)
            {
                DataTable dt = new BLL.payment().GetList(_site_id, _payment_id).Tables[0];
                this.ddlPaymentId.Items.Clear();
                this.ddlPaymentId.Items.Add(new ListItem("请选择平台...", ""));
                foreach (DataRow dr in dt.Rows)
                {
                    this.ddlPaymentId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
                }
            }
            else
            {
                this.ddlPaymentId.Items.Clear();
                this.ddlPaymentId.Items.Add(new ListItem("请选择站点...", ""));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            PaymentBind(model.site_id, model.payment_id); //绑定平台
            ShowFields(model.payment_id); //显示字段
            ddlSiteId.SelectedValue = model.site_id.ToString();
            ddlPaymentId.SelectedValue = model.payment_id.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtTitle.Text = model.title;
            txtKey1.Text = model.key1;
            txtKey2.Text = model.key2;
            txtKey3.Text = model.key3;
            txtKey4.Text = model.key4;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            this.model = new Model.site_payment();
            BLL.site_payment bll = new BLL.site_payment();

            model.site_id = Utils.StrToInt(ddlSiteId.SelectedValue, 0);
            model.payment_id = Utils.StrToInt(ddlPaymentId.SelectedValue, 0);
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.title = txtTitle.Text.Trim();
            model.key1 = txtKey1.Text.Trim();
            model.key2 = txtKey2.Text.Trim();
            model.key3 = txtKey3.Text.Trim();
            model.key4 = txtKey4.Text.Trim();
            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加站点支付方式:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.site_payment bll = new BLL.site_payment();

            model.site_id = Utils.StrToInt(ddlSiteId.SelectedValue, 0);
            model.payment_id = Utils.StrToInt(ddlPaymentId.SelectedValue, 0);
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.title = txtTitle.Text.Trim();
            model.key1 = txtKey1.Text.Trim();
            model.key2 = txtKey2.Text.Trim();
            model.key3 = txtKey3.Text.Trim();
            model.key4 = txtKey4.Text.Trim();
            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改站点支付方式:" + model.title); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //根据站点显示未添加的平台
        protected void ddlSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currSiteId = Utils.StrToInt(ddlSiteId.SelectedValue, 0);
            if (this.model != null && currSiteId == this.model.site_id)
            {
                PaymentBind(currSiteId, model.payment_id);
            }
            else
            {
                PaymentBind(currSiteId, 0);
            }
        }

         //根据平台显示相关字段
        protected void ddlPaymentId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currPaymentId = Utils.StrToInt(ddlPaymentId.SelectedValue, 0);
            if (currPaymentId > 0)
            {
                ShowFields(currPaymentId);
            }
        }

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("order_payment", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("修改站点支付方式成功！", "site_payment_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("order_payment", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("添加站点支付方式成功！", "site_payment_list.aspx");
            }
        }

    }
}