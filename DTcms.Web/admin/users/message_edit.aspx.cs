using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class message_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.View.ToString())
            {
                this.action = DTEnums.ActionEnum.View.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.user_message().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_message", DTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == DTEnums.ActionEnum.View.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 返回短信类型=============================
        protected string GetMessageType(int _type)
        {
            string result = string.Empty;
            switch (_type)
            {
                case 1:
                    result = "系统消息";
                    break;
                case 2:
                    result = "收件箱";
                    break;
                case 3:
                    result = "发件箱";
                    break;
            }
            return result;
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.user_message bll = new BLL.user_message();
            Model.user_message model = bll.GetModel(_id);

            div_view.Visible = true;
            div_add.Visible = false;
            btnSubmit.Visible = false;
            labType.Text = GetMessageType(model.type);
            if (!string.IsNullOrEmpty(model.post_user_name))
            {
                labPostUserName.Text = model.post_user_name;
            }
            else
            {
                labPostUserName.Text = "-";
            }
            labAcceptUserName.Text = model.accept_user_name;
            labPostTime.Text = model.post_time.ToString();
            labIsRead.Text = model.is_read == 1 ? "已阅读" : "未阅读";
            if (model.read_time != null)
            {
                labReadTime.Text = model.read_time.ToString();
            }
            else
            {
                labReadTime.Text = "-";
            }
            labTitle.Text = model.title;
            litContent.Text = model.content;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.user_message model = new Model.user_message();
            BLL.user_message bll = new BLL.user_message();

            model.title = txtTitle.Text.Trim();
            model.content = txtContent.Value;

            string[] arrUserName = txtUserName.Text.Trim().Split(',');
            if (arrUserName.Length > 0)
            {
                foreach (string username in arrUserName)
                {
                    if (new BLL.users().Exists(username))
                    {
                        model.accept_user_name = username;
                        if (bll.Add(model) < 1)
                        {
                            result = false;
                        }
                    }
                }
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_message", DTEnums.ActionEnum.Add.ToString()); //检查权限
            if (!DoAdd())
            {
                JscriptMsg("发送过程中发生错误！", "");
                return;
            }
            AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "发送站内短消息"); //记录日志
            JscriptMsg("发送短消息成功", "message_list.aspx");
        }

    }
}