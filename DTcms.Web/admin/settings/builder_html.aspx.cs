using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class builder_html : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_builder_html", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        #region 数据绑定=================================
        private void RptBind()
        {
            BLL.sites bll = new BLL.sites();
            DataSet ds = bll.GetList(0, "is_lock=0", "sort_id asc,id desc");
            this.rptList.DataSource = ds;
            this.rptList.DataBind();
        }
        #endregion

        //嵌套绑定
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            BLL.site_channel bll = new BLL.site_channel();
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Repeater rptChannel = (Repeater)e.Item.FindControl("rptChannel");
                //找到分类Repeater关联的数据项 
                DataRowView drv = (DataRowView)e.Item.DataItem;
                //提取站点ID 
                int siteId = Convert.ToInt32(drv["id"]);
                //根据分类ID查询并绑定所属频道 
                rptChannel.DataSource = bll.GetList(0, "site_id=" + siteId, "sort_id asc,id desc");
                rptChannel.DataBind();
            }
        }

    }
}