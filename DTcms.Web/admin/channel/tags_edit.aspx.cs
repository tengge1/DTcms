using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.channel
{
    public partial class tags_edit : Web.UI.ManagePage
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
                if (!new BLL.article_tags().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_article_tags", DTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.article_tags bll = new BLL.article_tags();
            Model.article_tags model = bll.GetModel(_id);

            txtTitle.Text = model.title;
            if (model.is_red == 1)
            {
                cbIsRed.Checked = true;
            }
            else
            {
                cbIsRed.Checked = false;
            }
            txtSortId.Text = model.sort_id.ToString();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.article_tags model = new Model.article_tags();
            BLL.article_tags bll = new BLL.article_tags();

            model.title = txtTitle.Text.Trim();
            if (cbIsRed.Checked == true)
            {
                model.is_red = 1;
            }
            else
            {
                model.is_red = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);

            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加Tags标签:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.article_tags bll = new BLL.article_tags();
            Model.article_tags model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            if (cbIsRed.Checked == true)
            {
                model.is_red = 1;
            }
            else
            {
                model.is_red = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);

            if (bll.Update(model))
            {
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改Tags标签:" + model.title); //记录日志
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
                ChkAdminLevel("sys_article_tags", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("修改Tags标签成功！", "tags_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("sys_article_tags", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("添加Tags标签成功！", "tags_list.aspx");
            }
        }

    }
}