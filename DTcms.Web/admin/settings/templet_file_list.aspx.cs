using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class templet_file_list : Web.UI.ManagePage
    {
        protected string skinName = string.Empty; //模板目录

        protected void Page_Load(object sender, EventArgs e)
        {
            skinName = DTRequest.GetQueryString("skin");
            if (string.IsNullOrEmpty(skinName))
            {
                JscriptMsg("传输参数不正确！", "back");
                return;
            }
            if (skinName.IndexOf("..") != -1)
            {
                JscriptMsg("模板目录名有误！", "back");
                return;
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind(skinName);
            }
        }

        #region 数据绑定=================================
        private void RptBind(string skin_name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Columns.Add("skinname", Type.GetType("System.String"));
            dt.Columns.Add("creationtime", Type.GetType("System.String"));
            dt.Columns.Add("updatetime", Type.GetType("System.String"));

            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(@"../../templates/" + skin_name));
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                if (file.Name != "about.xml" && file.Name != "about.png")
                {
                    DataRow dr = dt.NewRow();
                    dr["name"] = file.Name;
                    dr["skinname"] = skin_name;
                    dr["creationtime"] = file.CreationTime;
                    dr["updatetime"] = file.LastWriteTime;
                    dt.Rows.Add(dr);
                }
            }

            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        //删除文件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string fileName = ((HiddenField)rptList.Items[i].FindControl("hideName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    FileHelper.DeleteFile("../../templates/" + this.skinName + "/" + fileName);
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除模板文件，模板:" + this.skinName);//记录日志
            JscriptMsg("文件删除成功！", Utils.CombUrlTxt("templet_file_list.aspx", "skin={0}", this.skinName));
        }

    }
}