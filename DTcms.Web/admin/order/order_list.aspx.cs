using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.order
{
    public partial class order_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected int site_id;
        protected int status;
        protected int payment_status;
        protected int express_status;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.site_id = DTRequest.GetQueryInt("site_id");
            this.status = DTRequest.GetQueryInt("status");
            this.payment_status = DTRequest.GetQueryInt("payment_status");
            this.express_status = DTRequest.GetQueryInt("express_status");
            this.keywords = DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("order_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                SiteBind(); //绑定站点
                RptBind("id>0" + CombSqlTxt(this.site_id, this.status, this.payment_status, this.express_status, this.keywords), "status asc,add_time desc,id desc");
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

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            if (this.site_id > 0)
            {
                this.ddlSiteId.SelectedValue = this.site_id.ToString();
            }
            if (this.status > 0)
            {
                this.ddlStatus.SelectedValue = this.status.ToString();
            }
            if (this.payment_status > 0)
            {
                this.ddlPaymentStatus.SelectedValue = this.payment_status.ToString();
            }
            if (this.express_status > 0)
            {
                this.ddlExpressStatus.SelectedValue = this.express_status.ToString();
            }
            txtKeywords.Text = this.keywords;
            BLL.orders bll = new BLL.orders();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("order_list.aspx", "site_id={0}&status={1}&payment_status={2}&express_status={3}&keywords={4}&page={5}",
                this.site_id.ToString(), this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(int _site_id, int _status, int _payment_status, int _express_status, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (_site_id > 0)
            {
                strTemp.Append(" and site_id=" + _site_id);
            }
            if (_status > 0)
            {
                strTemp.Append(" and status=" + _status);
            }
            if (_payment_status > 0)
            {
                strTemp.Append(" and payment_status=" + _payment_status);
            }
            if (_express_status > 0)
            {
                strTemp.Append(" and express_status=" + _express_status);
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (order_no like '%" + _keywords + "%' or user_name like '%" + _keywords + "%' or accept_name like '%" + _keywords + "%')");
            }
            return strTemp.ToString();
        }
        #endregion

        #region 返回用户每页数量=========================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("order_list_page_size", "DTcmsPage"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回订单状态=============================
        protected string GetOrderStatus(int _id)
        {
            string _title = string.Empty;
            Model.orders model = new BLL.orders().GetModel(_id);
            switch (model.status)
            {
                case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                    if (model.payment_status > 0)
                    {
                        _title = "待付款";
                    }
                    else
                    {
                        _title = "待确认";
                    }
                    break;
                case 2: //如果订单为已确认状态，则进入发货状态
                    if (model.express_status > 1)
                    {
                        _title = "已发货";
                    }
                    else
                    {
                        _title = "待发货";
                    }
                    break;
                case 3:
                    _title = "交易完成";
                    break;
                case 4:
                    _title = "已取消";
                    break;
                case 5:
                    _title = "已作废";
                    break;
            }

            return _title;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "site_id={0}&status={1}&payment_status={2}&express_status={3}&keywords={4}",
                this.site_id.ToString(), this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), txtKeywords.Text));
        }

        //筛选站点
        protected void ddlSiteId_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "site_id={0}&status={1}&payment_status={2}&express_status={3}&keywords={4}",
                ddlSiteId.SelectedValue, this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
        }

        //订单状态
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "site_id={0}&status={1}&payment_status={2}&express_status={3}&keywords={4}",
                this.site_id.ToString(), ddlStatus.SelectedValue, this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
        }

        //支付状态
        protected void ddlPaymentStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "site_id={0}&status={1}&payment_status={2}&express_status={3}&keywords={4}",
                this.site_id.ToString(), this.status.ToString(), ddlPaymentStatus.SelectedValue, this.express_status.ToString(), this.keywords));
        }

        //发货状态
        protected void ddlExpressStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "site_id={0}&status={1}&payment_status={2}&express_status={3}&keywords={4}",
                this.site_id.ToString(), this.status.ToString(), this.payment_status.ToString(), ddlExpressStatus.SelectedValue, this.keywords));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("order_list_page_size", "DTcmsPage", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("order_list.aspx", "site_id={0}&status={1}&payment_status={2}&express_status={3}&keywords={4}",
                this.site_id.ToString(), this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("order_list", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0;
            int errorCount = 0;
            BLL.orders bll = new BLL.orders();
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
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除订单成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("order_list.aspx", 
                "site_id={0}&status={1}&payment_status={2}&express_status={3}&keywords={4}",
                this.site_id.ToString(), this.status.ToString(), this.payment_status.ToString(), this.express_status.ToString(), this.keywords));
        }

    }
}