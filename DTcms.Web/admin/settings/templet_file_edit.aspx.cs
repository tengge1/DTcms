using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class templet_file_edit : Web.UI.ManagePage
    {
        protected string filePath; //文件路径
        protected string pathName; //模板目录
        protected string fileName; //文件名称

        protected void Page_Load(object sender, EventArgs e)
        {
            pathName = DTRequest.GetQueryString("path");
            fileName = DTRequest.GetQueryString("filename");
            if (string.IsNullOrEmpty(pathName) || string.IsNullOrEmpty(fileName))
            {
                JscriptMsg("传输参数不正确！", "back");
                return;
            }
            filePath = Utils.GetMapPath(@"../../templates/" + pathName.Replace("..", "") + "/" + fileName.Replace("/", ""));
            if (!File.Exists(filePath))
            {
                JscriptMsg("模板文件不存在！", "back");
                return;
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo(filePath);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(string _path)
        {
            using (StreamReader objReader = new StreamReader(_path, Encoding.UTF8))
            {
                txtContent.Text = objReader.ReadToEnd();
                objReader.Close();
            }
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_site_templet", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            if (!File.Exists(filePath))
            {
                JscriptMsg("模板文件不存在！", "back");
                return;
            }
            using (FileStream fs = new FileStream(this.filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                Byte[] info = Encoding.UTF8.GetBytes(txtContent.Text);
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改模板文件:" + this.fileName);//记录日志
            JscriptMsg("模板保存成功！", Utils.CombUrlTxt("templet_file_list.aspx", "skin={0}", this.pathName));
        }

    }
}