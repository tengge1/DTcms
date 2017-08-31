using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.article
{
    public partial class category_list : Web.UI.ManagePage
    {
        protected int channel_id;
        protected string channel_name = string.Empty; //频道名称

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            this.channel_name = new BLL.site_channel().GetChannelName(this.channel_id); //取得频道名称
            if (this.channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back");
                return;
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        //数据绑定
        private void RptBind()
        {
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(0, this.channel_id);
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.article_category bll = new BLL.article_category();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存" + this.channel_name + "频道栏目分类排序"); //记录日志
            JscriptMsg("保存排序成功！", Utils.CombUrlTxt("category_list.aspx", "channel_id={0}", this.channel_id.ToString()));
        }

        //删除类别
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_category", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.article_category bll = new BLL.article_category();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "删除" + this.channel_name + "频道栏目分类数据"); //记录日志
            JscriptMsg("删除数据成功！", Utils.CombUrlTxt("category_list.aspx", "channel_id={0}", this.channel_id.ToString()));
        }

    }
}