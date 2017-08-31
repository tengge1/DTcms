using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class nav_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            this.id = DTRequest.GetQueryInt("id");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.navigation().Exists(this.id))
                {
                    JscriptMsg("导航不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(DTEnums.NavigationEnum.System.ToString()); //绑定导航菜单
                ActionTypeBind(); //绑定操作权限类型
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    if (this.id > 0)
                    {
                        this.ddlParentId.SelectedValue = this.id.ToString();
                    }
                    txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=navigation_validate");
                }
            }
        }

        #region 绑定导航菜单=============================
        private void TreeBind(string nav_type)
        {
            BLL.navigation bll = new BLL.navigation();
            DataTable dt = bll.GetList(0, nav_type);

            this.ddlParentId.Items.Clear();
            this.ddlParentId.Items.Add(new ListItem("无父级导航", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 绑定操作权限类型=========================
        private void ActionTypeBind()
        {
            cblActionType.Items.Clear();
            foreach (KeyValuePair<string, string> kvp in Utils.ActionType())
            {
                cblActionType.Items.Add(new ListItem(kvp.Value + "(" + kvp.Key + ")", kvp.Key));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.navigation bll = new BLL.navigation();
            Model.navigation model = bll.GetModel(_id);

            ddlParentId.SelectedValue = model.parent_id.ToString();
            txtSortId.Text = model.sort_id.ToString();
            if (model.is_lock == 1)
            {
                cbIsLock.Checked = true;
            }
            txtName.Text = model.name;
            txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=navigation_validate&old_name=" + Utils.UrlEncode(model.name));
            txtName.Focus(); //设置焦点，防止JS无法提交
            if (model.is_sys == 1)
            {
                ddlParentId.Enabled = false;
                txtName.ReadOnly = true;
            }
            txtTitle.Text = model.title;
            txtSubTitle.Text = model.sub_title;
            txtIconUrl.Text = model.icon_url;
            txtLinkUrl.Text = model.link_url;
            txtRemark.Text = model.remark;
            //赋值操作权限类型
            string[] actionTypeArr = model.action_type.Split(',');
            for (int i = 0; i < cblActionType.Items.Count; i++)
            {
                for (int n = 0; n < actionTypeArr.Length; n++)
                {
                    if (actionTypeArr[n].ToLower() == cblActionType.Items[i].Value.ToLower())
                    {
                        cblActionType.Items[i].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            try
            {
                Model.navigation model = new Model.navigation();
                BLL.navigation bll = new BLL.navigation();

                model.nav_type = DTEnums.NavigationEnum.System.ToString();
                model.name = txtName.Text.Trim();
                model.title = txtTitle.Text.Trim();
                model.sub_title = txtSubTitle.Text.Trim();
                model.icon_url = txtIconUrl.Text.Trim();
                model.link_url = txtLinkUrl.Text.Trim();
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.is_lock = 0;
                if (cbIsLock.Checked == true)
                {
                    model.is_lock = 1;
                }
                model.remark = txtRemark.Text.Trim();
                model.parent_id = int.Parse(ddlParentId.SelectedValue);

                //添加操作权限类型
                string action_type_str = string.Empty;
                for (int i = 0; i < cblActionType.Items.Count; i++)
                {
                    if (cblActionType.Items[i].Selected && Utils.ActionType().ContainsKey(cblActionType.Items[i].Value))
                    {
                        action_type_str += cblActionType.Items[i].Value + ",";
                    }
                }
                model.action_type = Utils.DelLastComma(action_type_str);

                if (bll.Add(model) > 0)
                {
                    AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加导航菜单:" + model.title); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            try
            {
                BLL.navigation bll = new BLL.navigation();
                Model.navigation model = bll.GetModel(_id);

                model.name = txtName.Text.Trim();
                model.title = txtTitle.Text.Trim();
                model.sub_title = txtSubTitle.Text.Trim();
                model.icon_url = txtIconUrl.Text.Trim();
                model.link_url = txtLinkUrl.Text.Trim();
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.is_lock = 0;
                if (cbIsLock.Checked == true)
                {
                    model.is_lock = 1;
                }
                model.remark = txtRemark.Text.Trim();
                if (model.is_sys == 0)
                {
                    int parentId = int.Parse(ddlParentId.SelectedValue);
                    //如果选择的父ID不是自己,则更改
                    if (parentId != model.id)
                    {
                        model.parent_id = parentId;
                    }
                }

                //添加操作权限类型
                string action_type_str = string.Empty;
                for (int i = 0; i < cblActionType.Items.Count; i++)
                {
                    if (cblActionType.Items[i].Selected && Utils.ActionType().ContainsKey(cblActionType.Items[i].Value))
                    {
                        action_type_str += cblActionType.Items[i].Value + ",";
                    }
                }
                model.action_type = Utils.DelLastComma(action_type_str);

                if (bll.Update(model))
                {
                    AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "修改导航菜单:" + model.title); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改导航菜单成功！", "nav_list.aspx", "parent.loadMenuTree");
            }
            else //添加
            {
                ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加导航菜单成功！", "nav_list.aspx", "parent.loadMenuTree");
            }
        }

    }
}