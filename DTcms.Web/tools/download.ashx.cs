using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;

namespace DTcms.Web.tools
{
    /// <summary>
    /// download 的摘要说明
    /// </summary>
    public class download : IHttpHandler, IRequiresSessionState
    {
        Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig(); //系统配置
        public void ProcessRequest(HttpContext context)
        {
            string sitepath = DTRequest.GetQueryString("site");
            int id = DTRequest.GetQueryInt("id");
            if (string.IsNullOrEmpty(sitepath))
            {
                context.Response.Write("出错了，站点传输参数不正确！");
                return;
            }
            //获得下载ID
            if (id == 0)
            {
                context.Response.Redirect(new Web.UI.BasePage().getlink(sitepath,
                    new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("出错了，文件参数传值不正确！"))));
                return;
            }
            //检查下载记录是否存在
            BLL.article_attach bll = new BLL.article_attach();
            if (!bll.Exists(id))
            {
                context.Response.Redirect(new Web.UI.BasePage().getlink(sitepath, 
                    new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("出错了，您要下载的文件不存在或已经被删除！"))));
                return;
            }
            Model.article_attach model = bll.GetModel(id);
            //检查积分是否足够
            if (model.point > 0)
            {
                //检查用户是否登录
                Model.users userModel = new Web.UI.BasePage().GetUserInfo();
                if (userModel == null)
                {
                    //自动跳转URL
                    HttpContext.Current.Response.Redirect(new Web.UI.BasePage().getlink(sitepath, new Web.UI.BasePage().linkurl("login")));
                }
                //如果该用户未曾下载过该附件
                if (!bll.ExistsLog(model.id, userModel.id))
                {
                    //检查积分
                    if (model.point > userModel.point)
                    {
                        context.Response.Redirect(new Web.UI.BasePage().getlink(sitepath, 
                            new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("出错啦，您的积分不足支付本次下载！"))));
                        return;
                    }
                    //扣取积分
                    int result = new BLL.user_point_log().Add(userModel.id, userModel.user_name, model.point * -1, "下载附件：“" + model.file_name + "”，扣减积分", false);
                    //添加下载记录
                    if (result > 0)
                    {
                        bll.AddLog(userModel.id, userModel.user_name, model.id, model.file_name);
                    }
                }
            }
            //下载次数+1
            bll.UpdateField(id, "down_num=down_num+1");
            //检查文件本地还是远程
            if (model.file_path.ToLower().StartsWith("http://") || model.file_path.ToLower().StartsWith("https://"))
            {
                var request = System.Net.HttpWebRequest.Create(model.file_path) as System.Net.HttpWebRequest;
                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        context.Response.Redirect(new Web.UI.BasePage().getlink(sitepath,
                        new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("出错了，您要下载的文件不存在或已经被删除！"))));
                        return;
                    }
                    byte[] byteData = FileHelper.ConvertStreamToByteBuffer(response.GetResponseStream());
                    context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8"); //解决中文乱码
                    context.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(model.file_name)); //解决中文文件名乱码    
                    context.Response.AddHeader("Content-length", byteData.Length.ToString());
                    context.Response.ContentType = "application/octet-stream";
                    context.Response.BinaryWrite(byteData);
                    context.Response.Flush();
                    context.Response.End();
                }
                return;
            }
            else
            {
                //取得文件物理路径
                string fullFileName = Utils.GetMapPath(model.file_path);
                if (!File.Exists(fullFileName))
                {
                    context.Response.Redirect(new Web.UI.BasePage().getlink(sitepath, 
                        new Web.UI.BasePage().linkurl("error", "?msg=" + Utils.UrlEncode("出错了，您要下载的文件不存在或已经被删除！"))));
                    return;
                }
                FileInfo file = new FileInfo(fullFileName);//路径
                context.Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8"); //解决中文乱码
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(model.file_name)); //解决中文文件名乱码    
                context.Response.AddHeader("Content-length", file.Length.ToString());
                context.Response.ContentType = "application/octet-stream";
                context.Response.WriteFile(file.FullName);
                context.Response.End();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}