using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.channel
{
    public partial class attribute_field_list : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string control_type = string.Empty;
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.control_type = DTRequest.GetQueryString("control_type");
            this.keywords = DTRequest.GetQueryString("keywords");

            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_channel_field", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(this.control_type, this.keywords), "is_sys desc,sort_id asc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = DTRequest.GetQueryInt("page", 1);
            this.ddlControlType.SelectedValue = this.control_type;
            this.txtKeywords.Text = this.keywords;
            BLL.article_attribute_field bll = new BLL.article_attribute_field();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}&page={2}", this.control_type, this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _control_type, string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            if (!string.IsNullOrEmpty(_control_type))
            {
                strTemp.Append(" and control_type='" + _control_type + "'");
            }
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (name like  '%" + _keywords + "%' or title like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("attribute_field_page_size", "DTcmsPage"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        #region 返回字段类型中文名称
        protected string GetTypeCn(string _control_type)
        {
            string type_name = "";
            switch (_control_type)
            {
                case "single-text":
                    type_name = "单行文本";
                    break;
                case "multi-text":
                    type_name = "多行文本";
                    break;
                case "editor":
                    type_name = "编辑器";
                    break;
                case "images":
                    type_name = "图片上传";
                    break;
                case "video":
                    type_name = "视频上传";
                    break;
                case "number":
                    type_name = "数字";
                    break;
                case "datetime":
                    type_name = "时间日期";
                    break;
                case "checkbox":
                    type_name = "复选框";
                    break;
                case "multi-radio":
                    type_name = "多项单选";
                    break;
                case "multi-checkbox":
                    type_name = "多项多选";
                    break;
                default:
                    type_name = "未知类型";
                    break;
            }
            return type_name;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.control_type, txtKeywords.Text));
        }

        //筛选类型
        protected void ddlControlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", ddlControlType.SelectedValue, this.keywords));
        }

        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("attribute_field_page_size", "DTcmsPage", _pagesize.ToString(), 43200);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.control_type, this.keywords));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_channel_field", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.article_attribute_field bll = new BLL.article_attribute_field();
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
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存扩展字段排序"); //记录日志
            JscriptMsg("保存排序成功！", Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.control_type, this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_channel_field", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            int sucCount = 0; //成功数量
            int errorCount = 0; //失败数量
            BLL.article_attribute_field bll = new BLL.article_attribute_field();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    if (bll.Delete(id))
                    {
                        sucCount++;
                    }
                    else
                    {
                        errorCount++;
                    }
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除扩展字段成功" + sucCount + "条，失败" + errorCount + "条"); //记录日志
            JscriptMsg("删除成功" + sucCount + "条，失败" + errorCount + "条！", Utils.CombUrlTxt("attribute_field_list.aspx", "control_type={0}&keywords={1}", this.control_type, this.keywords));
        }

    }
}