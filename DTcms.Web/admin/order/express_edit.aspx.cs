using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.order
{
    public partial class express_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
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
                if (!new BLL.express().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("order_express", DTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.express bll = new BLL.express();
            Model.express model = bll.GetModel(_id);

            txtTitle.Text = model.title;
            txtExpressCode.Text = model.express_code;
            txtExpressFee.Text = model.express_fee.ToString();
            txtWebSite.Text = model.website;
            txtRemark.Text = Utils.ToTxt(model.remark);
            if (model.is_lock == 0)
            {
                cbIsLock.Checked = true;
            }
            else
            {
                cbIsLock.Checked = false;
            }
            txtSortId.Text = model.sort_id.ToString();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.express model = new Model.express();
            BLL.express bll = new BLL.express();

            model.title = txtTitle.Text.Trim();
            model.express_code = txtExpressCode.Text.Trim();
            model.express_fee = Utils.StrToDecimal(txtExpressFee.Text.Trim(), 0);
            model.website = txtWebSite.Text.Trim();
            model.remark = Utils.ToHtml(txtRemark.Text);
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 1;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);

            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加配送方式:" + model.title); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.express bll = new BLL.express();
            Model.express model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.express_code = txtExpressCode.Text.Trim();
            model.express_fee = Utils.StrToDecimal(txtExpressFee.Text.Trim(), 0);
            model.website = txtWebSite.Text.Trim();
            model.remark = Utils.ToHtml(txtRemark.Text);
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 1;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);

            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改配送方式:" + model.title); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("order_express", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("修改物流配送成功！", "express_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("order_express", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("添加物流配送成功！", "express_list.aspx");
            }
        }

    }
}