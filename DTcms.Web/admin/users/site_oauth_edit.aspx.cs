using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class site_oauth_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private Model.site_oauth model;
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
                if (!new BLL.site_oauth().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
                //赋值实体
                this.model = new BLL.site_oauth().GetModel(this.id);
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_oauth", DTEnums.ActionEnum.View.ToString()); //检查权限
                SiteBind(); //绑定站点
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

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

        #region 绑定应用=================================
        private void OauthBind(int _site_id, int _oauth_id)
        {
            if (_site_id > 0)
            {
                DataTable dt = new BLL.oauth_app().GetList(_site_id, _oauth_id).Tables[0];
                this.ddlOauthId.Items.Clear();
                this.ddlOauthId.Items.Add(new ListItem("请选择应用...", ""));
                foreach (DataRow dr in dt.Rows)
                {
                    this.ddlOauthId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
                }
            }
            else
            {
                this.ddlOauthId.Items.Clear();
                this.ddlOauthId.Items.Add(new ListItem("请选择站点...", ""));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            OauthBind(model.site_id, model.oauth_id); //绑定应用
            ddlSiteId.SelectedValue = model.site_id.ToString();
            ddlOauthId.SelectedValue = model.oauth_id.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtTitle.Text = model.title;
            txtAppId.Text = model.app_id;
            txtAppKey.Text = model.app_key;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            this.model = new Model.site_oauth();
            BLL.site_oauth bll = new BLL.site_oauth();

            model.site_id = Utils.StrToInt(ddlSiteId.SelectedValue, 0);
            model.oauth_id = Utils.StrToInt(ddlOauthId.SelectedValue, 0);
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.title = txtTitle.Text.Trim();
            model.app_id = txtAppId.Text.Trim();
            model.app_key = txtAppKey.Text.Trim();
            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加站点OAuth列表:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.site_oauth bll = new BLL.site_oauth();

            model.site_id = Utils.StrToInt(ddlSiteId.SelectedValue, 0);
            model.oauth_id = Utils.StrToInt(ddlOauthId.SelectedValue, 0);
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.title = txtTitle.Text.Trim();
            model.app_id = txtAppId.Text.Trim();
            model.app_key = txtAppKey.Text.Trim();
            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改站点OAuth列表:" + model.title); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //根据站点显示未安装的应用
        protected void ddlSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            int currSiteId = Utils.StrToInt(ddlSiteId.SelectedValue, 0);
            if (this.model != null && currSiteId == this.model.site_id)
            {
                OauthBind(currSiteId, model.oauth_id);
            }
            else
            {
                OauthBind(currSiteId, 0);
            }
        }

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("user_oauth", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("修改站点OAuth列表成功！", "site_oauth_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("user_oauth", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("添加站点OAuth列表成功！", "site_oauth_list.aspx");
            }
        }

    }
}