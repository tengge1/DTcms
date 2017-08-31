using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.channel
{
    public partial class site_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString();//操作类型
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
                if (!new BLL.sites().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    txtBuildPath.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=site_path_validate");
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.sites bll = new BLL.sites();
            Model.sites model = bll.GetModel(_id);

            txtTitle.Text = model.title;
            txtBuildPath.Text = model.build_path;
            txtBuildPath.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=site_path_validate&old_build_path=" + Utils.UrlEncode(model.build_path));
            txtBuildPath.Focus(); //设置焦点，防止JS无法提交
            txtDomain.Text = model.domain;
            txtSortId.Text = model.sort_id.ToString();
            if (model.is_default == 1)
            {
                cbIsDefault.Checked = true;
            }
            else
            {
                cbIsDefault.Checked = false;
            }
            if (model.is_lock == 0)
            {
                cbIsLock.Checked = true;
            }
            else
            {
                cbIsLock.Checked = false;
            }
            txtName.Text = model.name;
            txtLogo.Text = model.logo;
            txtCompany.Text = model.company;
            txtAddress.Text = model.address;
            txtTel.Text = model.tel;
            txtFax.Text = model.fax;
            txtEmail.Text = model.email;
            txtCrod.Text = model.crod;
            txtSeoTitle.Text = model.seo_title;
            txtSeoKeyword.Text = model.seo_keyword;
            txtSeoDescription.Text = model.seo_description;
            txtCopyright.Text = model.copyright;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.sites model = new Model.sites();
            BLL.sites bll = new BLL.sites();

            model.title = txtTitle.Text.Trim();
            model.build_path = txtBuildPath.Text.Trim();
            model.domain = txtDomain.Text.Trim();
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            if (cbIsDefault.Checked == true)
            {
                model.is_default = 1;
            }
            else
            {
                model.is_default = 0;
            }
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 0;
            }
            model.name = txtName.Text.Trim();
            model.logo = txtLogo.Text.Trim();
            model.company = txtCompany.Text.Trim();
            model.address = txtAddress.Text.Trim();
            model.tel = txtTel.Text.Trim();
            model.fax = txtFax.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.crod = txtCrod.Text.Trim();
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keyword = txtSeoKeyword.Text.Trim();
            model.seo_description = Utils.DropHTML(txtSeoDescription.Text);
            model.copyright = txtCopyright.Text.Trim();

            if (bll.Add(model) > 0)
            {
                //更新一下域名缓存
                CacheHelper.Remove(DTKeys.CACHE_SITE_HTTP_DOMAIN);
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加站点:" + model.title); //记录日志
                return true;
            }

            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.sites bll = new BLL.sites();
            Model.sites model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.build_path = txtBuildPath.Text.Trim();
            model.domain = txtDomain.Text.Trim();
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            if (cbIsDefault.Checked == true)
            {
                model.is_default = 1;
            }
            else
            {
                model.is_default = 0;
            }
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 0;
            }
            model.name = txtName.Text.Trim();
            model.logo = txtLogo.Text.Trim();
            model.company = txtCompany.Text.Trim();
            model.address = txtAddress.Text.Trim();
            model.tel = txtTel.Text.Trim();
            model.fax = txtFax.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.crod = txtCrod.Text.Trim();
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keyword = txtSeoKeyword.Text.Trim();
            model.seo_description = Utils.DropHTML(txtSeoDescription.Text);
            model.copyright = txtCopyright.Text.Trim();

            if (bll.Update(model))
            {
                CacheHelper.Remove(DTKeys.CACHE_SITE_HTTP_DOMAIN); //更新一下域名缓存
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改站点:" + model.title); //记录日志
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
                ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改站点信息成功！", "site_list.aspx", "parent.loadMenuTree");
            }
            else //添加
            {
                ChkAdminLevel("sys_site_manage", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加站点信息成功！", "site_list.aspx", "parent.loadMenuTree");
            }
        }

    }
}