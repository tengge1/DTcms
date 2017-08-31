using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.article
{
    public partial class comment_edit : Web.UI.ManagePage
    {
        private int id = 0;
        private string channel_name = string.Empty; //频道名称
        protected Model.article_comment model = new Model.article_comment();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = DTRequest.GetQueryInt("id");
            if (id == 0)
            {
                JscriptMsg("传输参数不正确！", "back");
                return;
            }
            if (!new BLL.article_comment().Exists(this.id))
            {
                JscriptMsg("记录不存在或已删除！", "back");
                return;
            }
            this.model = new BLL.article_comment().GetModel(this.id); //取得评论实体
            this.channel_name = new BLL.site_channel().GetChannelName(model.channel_id); //取得频道名称
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            txtReContent.Text = Utils.ToTxt(model.reply_content);
            rblIsLock.SelectedValue = model.is_lock.ToString();
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_comment", DTEnums.ActionEnum.Reply.ToString()); //检查权限
            BLL.article_comment bll = new BLL.article_comment();
            model.is_reply = 1;
            model.reply_content = Utils.ToHtml(txtReContent.Text);
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.reply_time = DateTime.Now;
            bll.Update(model);
            AddAdminLog(DTEnums.ActionEnum.Reply.ToString(), "回复" + this.channel_name + "频道评论ID:" + model.id); //记录日志
            JscriptMsg("评论回复成功！", "comment_list.aspx?channel_id=" + model.channel_id);
        }

    }
}