using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class sys_config : Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_config", DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo();
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            BLL.sysconfig bll = new BLL.sysconfig();
            Model.sysconfig model = bll.loadConfig();

            webname.Text = model.webname;
            weburl.Text = model.weburl;
            webcompany.Text = model.webcompany;
            webaddress.Text = model.webaddress;
            webtel.Text = model.webtel;
            webfax.Text = model.webfax;
            webmail.Text = model.webmail;
            webcrod.Text = model.webcrod;

            webpath.Text = model.webpath;
            webmanagepath.Text = model.webmanagepath;
            staticstatus.SelectedValue = model.staticstatus.ToString();
            staticextension.Text = model.staticextension;
            if (model.memberstatus == 1)
            {
                memberstatus.Checked = true;
            }
            else
            {
                memberstatus.Checked = false;
            }
            if (model.commentstatus == 1)
            {
                commentstatus.Checked = true;
            }
            else
            {
                commentstatus.Checked = false;
            }
            if (model.logstatus == 1)
            {
                logstatus.Checked = true;
            }
            else
            {
                logstatus.Checked = false;
            }
            if (model.webstatus == 1)
            {
                webstatus.Checked = true;
            }
            else
            {
                webstatus.Checked = false;
            }
            webclosereason.Text = model.webclosereason;
            webcountcode.Text = model.webcountcode;

            smsapiurl.Text = model.smsapiurl;
            smsusername.Text = model.smsusername;
            if (!string.IsNullOrEmpty(model.smspassword))
            {
                smspassword.Attributes["value"] = defaultpassword;
            }
            labSmsCount.Text = GetSmsCount(); //取得短信数量

            emailsmtp.Text = model.emailsmtp;
            if (model.emailssl == 1)
            {
                emailssl.Checked = true;
            }
            else
            {
                emailssl.Checked = false;
            }
            emailport.Text = model.emailport.ToString();
            emailfrom.Text = model.emailfrom;
            emailusername.Text = model.emailusername;
            if (!string.IsNullOrEmpty(model.emailpassword))
            {
                emailpassword.Attributes["value"] = defaultpassword;
            }
            emailnickname.Text = model.emailnickname;

            filepath.Text = model.filepath;
            filesave.SelectedValue = model.filesave.ToString();
            fileremote.SelectedValue = model.fileremote.ToString();
            fileextension.Text = model.fileextension;
            videoextension.Text = model.videoextension;
            attachsize.Text = model.attachsize.ToString();
            videosize.Text = model.videosize.ToString();
            imgsize.Text = model.imgsize.ToString();
            imgmaxheight.Text = model.imgmaxheight.ToString();
            imgmaxwidth.Text = model.imgmaxwidth.ToString();
            thumbnailheight.Text = model.thumbnailheight.ToString();
            thumbnailwidth.Text = model.thumbnailwidth.ToString();
            thumbnailmode.SelectedValue = model.thumbnailmode;
            watermarktype.SelectedValue = model.watermarktype.ToString();
            watermarkposition.Text = model.watermarkposition.ToString();
            watermarkimgquality.Text = model.watermarkimgquality.ToString();
            watermarkpic.Text = model.watermarkpic;
            watermarktransparency.Text = model.watermarktransparency.ToString();
            watermarktext.Text = model.watermarktext;
            watermarkfont.SelectedValue = model.watermarkfont;
            watermarkfontsize.Text = model.watermarkfontsize.ToString();
        }
        #endregion

        #region 获取短信数量=================================
        private string GetSmsCount()
        {
            string code = string.Empty;
            int count = new BLL.sms_message().GetAccountQuantity(out code);
            if (code == "115")
            {
                return "查询出错：请完善账户信息";
            }
            else if (code != "100")
            {
                return "错误代码：" + code;
            }
            return count + " 条";
        }
        #endregion

        /// <summary>
        /// 保存配置信息
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_config", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.sysconfig bll = new BLL.sysconfig();
            Model.sysconfig model = bll.loadConfig();
            try
            {
                model.webname = webname.Text;
                model.weburl = weburl.Text;
                model.webcompany = webcompany.Text;
                model.webaddress = webaddress.Text;
                model.webtel = webtel.Text;
                model.webfax = webfax.Text;
                model.webmail = webmail.Text;
                model.webcrod = webcrod.Text;

                model.webpath = webpath.Text;
                model.webmanagepath = webmanagepath.Text;
                model.staticstatus = Utils.StrToInt(staticstatus.SelectedValue, 0);
                model.staticextension = staticextension.Text;
                if (memberstatus.Checked == true)
                {
                    model.memberstatus = 1;
                }
                else
                {
                    model.memberstatus = 0;
                }
                if (commentstatus.Checked == true)
                {
                    model.commentstatus = 1;
                }
                else
                {
                    model.commentstatus = 0;
                }
                if (logstatus.Checked == true)
                {
                    model.logstatus = 1;
                }
                else
                {
                    model.logstatus = 0;
                }
                if (webstatus.Checked == true)
                {
                    model.webstatus = 1;
                }
                else
                {
                    model.webstatus = 0;
                }
                model.webclosereason = webclosereason.Text;
                model.webcountcode = webcountcode.Text;

                model.smsapiurl = smsapiurl.Text;
                model.smsusername = smsusername.Text;
                //判断密码是否更改
                if (smspassword.Text.Trim() != "" && smspassword.Text.Trim() != defaultpassword)
                {
                    model.smspassword = Utils.MD5(smspassword.Text.Trim());
                }

                model.emailsmtp = emailsmtp.Text;
                if (emailssl.Checked == true)
                {
                    model.emailssl = 1;
                }
                else
                {
                    model.emailssl = 0;
                }
                model.emailport = Utils.StrToInt(emailport.Text.Trim(), 25);
                model.emailfrom = emailfrom.Text;
                model.emailusername = emailusername.Text;
                //判断密码是否更改
                if (emailpassword.Text.Trim() != defaultpassword)
                {
                    model.emailpassword = DESEncrypt.Encrypt(emailpassword.Text, model.sysencryptstring);
                }
                model.emailnickname = emailnickname.Text;

                model.filepath = filepath.Text;
                model.filesave = Utils.StrToInt(filesave.SelectedValue, 2);
                model.fileremote = Utils.StrToInt(fileremote.SelectedValue, 0);
                model.fileextension = fileextension.Text;
                model.videoextension = videoextension.Text;
                model.attachsize = Utils.StrToInt(attachsize.Text.Trim(), 0);
                model.videosize = Utils.StrToInt(videosize.Text.Trim(), 0);
                model.imgsize = Utils.StrToInt(imgsize.Text.Trim(), 0);
                model.imgmaxheight = Utils.StrToInt(imgmaxheight.Text.Trim(), 0);
                model.imgmaxwidth = Utils.StrToInt(imgmaxwidth.Text.Trim(), 0);
                model.thumbnailheight = Utils.StrToInt(thumbnailheight.Text.Trim(), 0);
                model.thumbnailwidth = Utils.StrToInt(thumbnailwidth.Text.Trim(), 0);
                model.thumbnailmode = thumbnailmode.SelectedValue;
                model.watermarktype = Utils.StrToInt(watermarktype.SelectedValue, 0);
                model.watermarkposition = Utils.StrToInt(watermarkposition.Text.Trim(), 9);
                model.watermarkimgquality = Utils.StrToInt(watermarkimgquality.Text.Trim(), 80);
                model.watermarkpic = watermarkpic.Text;
                model.watermarktransparency = Utils.StrToInt(watermarktransparency.Text.Trim(), 5);
                model.watermarktext = watermarktext.Text;
                model.watermarkfont = watermarkfont.Text;
                model.watermarkfontsize = Utils.StrToInt(watermarkfontsize.Text.Trim(), 12);

                bll.saveConifg(model);
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改系统配置信息"); //记录日志
                JscriptMsg("修改系统配置成功！", "sys_config.aspx");
            }
            catch
            {
                JscriptMsg("文件写入失败，请检查文件夹权限！", "");
            }
        }

    }
}