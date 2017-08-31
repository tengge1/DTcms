using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.users
{
    public partial class user_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int site_id;
        protected int group_id;
        protected string keywords = string.Empty;
        protected string start_time = string.Empty;
        protected string end_time = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            ChkAdminLevel("user_list", DTEnums.ActionEnum.View.ToString()); //检查权限
            this.site_id = DTRequest.GetQueryInt("site_id");
            this.group_id = DTRequest.GetQueryInt("group_id");
            this.keywords = DTRequest.GetQueryString("keywords");
            this.start_time = DTRequest.GetQueryString("start_time");
            this.end_time = DTRequest.GetQueryString("end_time");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                SiteBind(); //绑定站点
                TreeBind("is_lock=0"); //绑定类别
                RptBind("id>0" + CombSqlTxt(this.site_id, this.group_id, this.start_time, this.end_time, this.keywords), "reg_time desc,id desc");
            }
        }

        #region 绑定站点=================================
        private void SiteBind()
        {
            BLL.sites bll = new BLL.sites();
            DataTable dt = bll.GetList(0, "", "sort_id asc,id desc").Tables[0];

            this.ddlSiteId.Items.Clear();
            this.ddlSiteId.Items.Add(new ListItem("所有站点", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlSiteId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 绑定组别=================================
        private void TreeBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            DataTable dt = bll.GetList(0, strWhere, "id desc").Tables[0];

            this.ddlGroupId.Items.Clear();
            this.ddlGroupId.Items.Add(new ListItem("所有会员组", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlGroupId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.site_id > 0)
            {
                this.ddlSiteId.SelectedValue = this.site_id.ToString();
            }
            if (this.group_id > 0)
            {
                this.ddlGroupId.SelectedValue = this.group_id.ToString();
            }
            this.txtStartTime.Text = this.start_time;
            this.txtEndTime.Text = this.end_time;
            this.txtKeywords.Text = this.keywords;
            BLL.users bll = new BLL.users();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("user_list.aspx", "site_id={0}&group_id={1}&start_time={2}&end_time={3}&keywords={4}&page={5}",
                this.site_id.ToString(), this.group_id.ToString(), this.start_time, this.end_time, this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _site_id, int _group_id, string _start_time, string _end_time, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_site_id > 0)
            {
                strTemp.Append(" and site_id=" + _site_id);
            }
            if (_group_id > 0)
            {
                strTemp.Append(" and group_id=" + _group_id);
            }
            _start_time = _start_time.Replace("'", "");
            if (!string.IsNullOrEmpty(_start_time))
            {
                strTemp.Append(" and datediff(d,reg_time,'" + _start_time + "')<=0");
            }
            _end_time = _end_time.Replace("'", "");
            if (!string.IsNullOrEmpty(_end_time))
            {
                strTemp.Append(" and datediff(d,reg_time,'" + _end_time + "')>=0");
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like '%" + _keywords + "%' or mobile like '%" + _keywords + "%' or email like '%" + _keywords + "%' or nick_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("user_list_page_size", "DTcmsPage"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回用户状态=============================
        protected string GetUserStatus(int status)
        {
            string result = string.Empty;
            switch (status)
            {
                case 0:
                    result = "正常";
                    break;
                case 1:
                    result = "待验证";
                    break;
                case 2:
                    result = "待审核";
                    break;
                case 3:
                    result = "已禁用";
                    break;
            }
            return result;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "site_id={0}&group_id={1}&start_time={2}&end_time={3}&keywords={4}",
                this.site_id.ToString(), this.group_id.ToString(), txtStartTime.Text, txtEndTime.Text, txtKeywords.Text));
        }

        //筛选站点
        protected void ddlSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "site_id={0}&group_id={1}&start_time={2}&end_time={3}&keywords={4}",
                ddlSiteId.SelectedValue, this.group_id.ToString(), this.start_time, this.end_time, this.keywords));
        }

        //筛选组别
        protected void ddlGroupId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "site_id={0}&group_id={1}&start_time={2}&end_time={3}&keywords={4}",
                this.site_id.ToString(), ddlGroupId.SelectedValue, this.start_time, this.end_time, this.keywords));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("user_list_page_size", "DTcmsPage", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("user_list.aspx", "site_id={0}&group_id={1}&start_time={2}&end_time={3}&keywords={4}",
                this.site_id.ToString(), this.group_id.ToString(), this.start_time, this.end_time, this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("user_list", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.users bll = new BLL.users();
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
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除用户" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("user_list.aspx",
                "site_id={0}&group_id={1}&start_time={2}&end_time={3}&keywords={4}",
                this.site_id.ToString(), this.group_id.ToString(), this.start_time, this.end_time, this.keywords));
        }

    }
}