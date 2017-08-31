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
    public partial class comment_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int channel_id;
        protected string channel_name = string.Empty; //频道名称
        protected string property = string.Empty;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            this.channel_name = new BLL.site_channel().GetChannelName(this.channel_id); //取得频道名称
            this.property = DTRequest.GetQueryString("property");
            this.keywords = DTRequest.GetQueryString("keywords");

            if (channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back");
                return;
            }

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channel_name + "_comment", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("channel_id=" + this.channel_id.ToString() + CombSqlTxt(this.keywords, this.property), "add_time desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            this.ddlProperty.SelectedValue = this.property;
            this.txtKeywords.Text = this.keywords;
            BLL.article_comment bll = new BLL.article_comment();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}&page={3}",
                this.channel_id.ToString(), this.keywords, this.property, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords, string _property)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or content like '%" + _keywords + "%')");
            }
            if (!string.IsNullOrEmpty(_property))
            {
                switch (_property)
                {
                    case "isLock":
                        strTemp.Append(" and is_lock=1");
                        break;
                    case "unLock":
                        strTemp.Append(" and is_lock=0");
                        break;
                }
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回评论每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("channel_comment_page_size", "DTcmsPage"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}",
                this.channel_id.ToString(), txtKeywords.Text, this.property));
        }

        //筛选属性
        protected void ddlProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}",
               this.channel_id.ToString(), this.keywords, ddlProperty.SelectedValue));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("channel_comment_page_size", "DTcmsPage", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}",
            this.channel_id.ToString(), this.keywords, this.property));
        }

        //审核
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_comment", DTEnums.ActionEnum.Audit.ToString()); //检查权限
            BLL.article_comment bll = new BLL.article_comment();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.UpdateField(id, "is_lock=0");
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Audit.ToString(), "审核" + this.channel_name + "频道评论信息"); //记录日志
            JscriptMsg("审核通过成功！", Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}",
                this.channel_id.ToString(), this.keywords, this.property));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("channel_" + this.channel_name + "_comment", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.article_comment bll = new BLL.article_comment();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount += 1;
                    }
                    else
                    {
                        errorCount += 1;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除" + this.channel_name + "频道评论成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！",
                Utils.CombUrlTxt("comment_list.aspx", "channel_id={0}&keywords={1}&property={2}", this.channel_id.ToString(), this.keywords, this.property));
        }

    }
}