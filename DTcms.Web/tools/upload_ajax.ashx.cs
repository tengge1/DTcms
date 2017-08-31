using System;
using System.Linq;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using DTcms.Common;
using DTcms.Web.UI;

namespace DTcms.Web.tools
{
    /// <summary>
    /// 文件上传处理页
    /// </summary>
    public class upload_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");

            switch (action)
            {
                case "config": //编辑器配置
                    EditorConfig(context);
                    break;
                case "uploadimage": //编辑器上传图片
                    EditorUploadImage(context);
                    break;
                case "uploadvideo": //编辑器上传视频
                    EditorUploadVideo(context);
                    break;
                case "uploadfile": //编辑器上传附件
                    EditorUploadFile(context);
                    break;
                case "uploadscrawl": //编辑器上传涂鸦
                    EditorUploadScrawl(context);
                    break;
                case "listimage": //编辑器浏览图片
                    EditorListImage(context);
                    break;
                case "listfile": //编辑器浏览文件
                    EditorListFile(context);
                    break;
                case "catchimage": //编辑器远程抓取图片
                    EditorCatchImage(context);
                    break;
                default: //普通上传
                    UpLoadFile(context);
                    break;
            }
        }

        #region 上传文件处理===================================
        private void UpLoadFile(HttpContext context)
        {
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();
            //检查是否允许匿名上传
            /*if (sysConfig.fileanonymous == 0 && !new ManagePage().IsAdminLogin() && !new BasePage().IsUserLogin())
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"禁止匿名非法上传！\"}");
                return;
            }*/

            string _delfile = DTRequest.GetString("DelFilePath"); //要删除的文件
            string fileName = DTRequest.GetString("name"); //文件名

            byte[] byteData = FileHelper.ConvertStreamToByteBuffer(context.Request.InputStream); //获取文件流
            bool _iswater = false; //默认不打水印
            bool _isthumbnail = false; //默认不生成缩略图

            if (DTRequest.GetQueryString("IsWater") == "1")
            {
                _iswater = true;
            }
            if (DTRequest.GetQueryString("IsThumbnail") == "1")
            {
                _isthumbnail = true;
            }
            if (byteData.Length == 0)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"请选择要上传文件！\"}");
                return;
            }
            UpLoad upLoad = new UpLoad();
            string msg = upLoad.FileSaveAs(byteData, fileName, _isthumbnail, _iswater);
            //删除已存在的旧文件
            if (!string.IsNullOrEmpty(_delfile))
            {
                upLoad.DeleteFile(_delfile);
            }
            //返回成功信息
            context.Response.Write(msg);
            context.Response.End();
        }
        #endregion

        #region 编辑器请求处理=================================
        /// <summary>
        /// 初始化参数
        /// </summary>
        private void EditorConfig(HttpContext context)
        {
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();
            StringBuilder jsonStr = new StringBuilder();
            jsonStr.Append("{");
            //上传图片配置项
            jsonStr.Append("\"imageActionName\": \"uploadimage\","); //执行上传图片的action名称
            jsonStr.Append("\"imageFieldName\": \"upfile\","); //提交的图片表单名称
            jsonStr.Append("\"imageMaxSize\": " + (sysConfig.imgsize * 1024) + ","); //上传大小限制，单位B
            jsonStr.Append("\"imageAllowFiles\": [\".png\", \".jpg\", \".jpeg\", \".gif\", \".bmp\"],"); //上传图片格式显示
            jsonStr.Append("\"imageCompressEnable\": false,"); //是否压缩图片,默认是true
            jsonStr.Append("\"imageCompressBorder\": 1600,"); //图片压缩最长边限制
            jsonStr.Append("\"imageInsertAlign\": \"none\","); //插入的图片浮动方式
            jsonStr.Append("\"imageUrlPrefix\": \"\","); //图片访问路径前缀
            jsonStr.Append("\"imagePathFormat\": \"\","); //上传保存路径
            //涂鸦图片上传配置项
            jsonStr.Append("\"scrawlActionName\": \"uploadscrawl\","); //执行上传涂鸦的action名称
            jsonStr.Append("\"scrawlFieldName\": \"upfile\","); //提交的图片表单名称
            jsonStr.Append("\"scrawlPathFormat\": \"\","); //上传保存路径
            jsonStr.Append("\"scrawlMaxSize\": " + (sysConfig.imgsize * 1024) + ","); //上传大小限制，单位B
            jsonStr.Append("\"scrawlUrlPrefix\": \"\","); //图片访问路径前缀
            jsonStr.Append("\"scrawlInsertAlign\": \"none\",");
            //截图工具上传
            jsonStr.Append("\"snapscreenActionName\": \"uploadimage\","); //执行上传截图的action名称
            jsonStr.Append("\"snapscreenPathFormat\": \"\","); //上传保存路径
            jsonStr.Append("\"snapscreenUrlPrefix\": \"\","); //图片访问路径前缀
            jsonStr.Append("\"snapscreenInsertAlign\": \"none\","); //插入的图片浮动方式
            //抓取远程图片配置
            jsonStr.Append("\"catcherLocalDomain\": [\"127.0.0.1\", \"localhost\", \"img.baidu.com\"],");
            jsonStr.Append("\"catcherActionName\": \"catchimage\","); //执行抓取远程图片的action名称
            jsonStr.Append("\"catcherFieldName\": \"source\","); //提交的图片列表表单名称
            jsonStr.Append("\"catcherPathFormat\": \"\","); //上传保存路径
            jsonStr.Append("\"catcherUrlPrefix\": \"\","); //图片访问路径前缀
            jsonStr.Append("\"catcherMaxSize\": " + (sysConfig.imgsize * 1024) + ","); //上传大小限制，单位B
            jsonStr.Append("\"catcherAllowFiles\": [\".png\", \".jpg\", \".jpeg\", \".gif\", \".bmp\"],"); //抓取图片格式显示
            //上传视频配置
            jsonStr.Append("\"videoActionName\": \"uploadvideo\","); //上传视频的action名称
            jsonStr.Append("\"videoFieldName\": \"upfile\","); //提交的视频表单名称
            jsonStr.Append("\"videoPathFormat\": \"\","); //上传保存路径
            jsonStr.Append("\"videoUrlPrefix\": \"\","); //视频访问路径前缀
            jsonStr.Append("\"videoMaxSize\": " + (sysConfig.videosize * 1024) + ","); //上传大小限制，单位B
            jsonStr.Append("\"videoAllowFiles\": " + GetExtension(sysConfig.videoextension) + ",");
            //上传附件配置
            jsonStr.Append("\"fileActionName\": \"uploadfile\","); //上传视频的action名称
            jsonStr.Append("\"fileFieldName\": \"upfile\","); //提交的文件表单名称
            jsonStr.Append("\"filePathFormat\": \"\","); //上传保存路径
            jsonStr.Append("\"fileUrlPrefix\": \"\","); //文件访问路径前缀
            jsonStr.Append("\"fileMaxSize\": " + (sysConfig.attachsize * 1024) + ","); //上传大小限制，单位B
            jsonStr.Append("\"fileAllowFiles\": " + GetExtension(sysConfig.fileextension) + ","); //上传文件格式
            //列出指定目录下的图片
            jsonStr.Append("\"imageManagerActionName\": \"listimage\","); //执行图片管理的action名称
            jsonStr.Append("\"imageManagerListPath\": \"\","); //指定要列出图片的目录
            jsonStr.Append("\"imageManagerListSize\": 20,"); //每次列出文件数量
            jsonStr.Append("\"imageManagerUrlPrefix\": \"\","); //图片访问路径前缀
            jsonStr.Append("\"imageManagerInsertAlign\": \"none\","); //插入的图片浮动方式
            jsonStr.Append("\"imageManagerAllowFiles\": [\".png\", \".jpg\", \".jpeg\", \".gif\", \".bmp\"],"); //列出的文件类型
            //列出指定目录下的文件
            jsonStr.Append("\"fileManagerActionName\": \"listfile\","); //执行文件管理的action名称
            jsonStr.Append("\"fileManagerListPath\": \"\","); //指定要列出文件的目录
            jsonStr.Append("\"fileManagerUrlPrefix\": \"\","); //文件访问路径前缀
            jsonStr.Append("\"fileManagerListSize\": 20,"); //每次列出文件数量
            jsonStr.Append("\"fileManagerAllowFiles\": [\".png\", \".jpg\", \".jpeg\", \".gif\", \".bmp\",");
            jsonStr.Append("\".flv\", \".swf\", \".mkv\", \".avi\", \".rm\", \".rmvb\", \".mpeg\", \".mpg\",");
            jsonStr.Append("\".ogg\", \".ogv\", \".mov\", \".wmv\", \".mp4\", \".webm\", \".mp3\", \".wav\", \".mid\",");
            jsonStr.Append("\".rar\", \".zip\", \".tar\", \".gz\", \".7z\", \".bz2\", \".cab\", \".iso\",");
            jsonStr.Append("\".doc\", \".docx\", \".xls\", \".xlsx\", \".ppt\", \".pptx\", \".pdf\", \".txt\", \".md\", \".xml\"]");
            jsonStr.Append("}");

            context.Response.AddHeader("Content-Type", "text/plain; charset=UTF-8");
            context.Response.Write(jsonStr.ToString());
            context.Response.End();
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        private void EditorUploadImage(HttpContext context)
        {
            bool _iswater = false; //默认不打水印
            if (DTRequest.GetQueryString("IsWater") == "1")
            {
                _iswater = true;
            }
            HttpPostedFile upFile = context.Request.Files["upfile"];
            FileSave(context, upFile, _iswater);
        }

        /// <summary>
        /// 上传视频
        /// </summary>
        private void EditorUploadVideo(HttpContext context)
        {
            HttpPostedFile upFile = context.Request.Files["upfile"];
            FileSave(context, upFile, false);
        }

        /// <summary>
        /// 上传附件
        /// </summary>
        private void EditorUploadFile(HttpContext context)
        {
            HttpPostedFile upFile = context.Request.Files["upfile"];
            FileSave(context, upFile, false);
        }

        /// <summary>
        /// 上传涂鸦
        /// </summary>
        private void EditorUploadScrawl(HttpContext context)
        {
            byte[] byteData = Convert.FromBase64String(context.Request["upfile"]);
            string fileName = "scrawl.png";
            //开始上传
            string remsg = new UpLoad().FileSaveAs(byteData, fileName, false, false);
            Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(remsg);
            string status = dic["status"].ToString();
            string msg = dic["msg"].ToString();
            if (status == "0")
            {
                showError(context, msg);
                return;
            }
            string filePath = dic["path"].ToString(); //取得上传后的路径
            showSuccess(context, fileName, filePath); //输出成功提示
        }

        /// <summary>
        /// 浏览图片
        /// </summary>
        private void EditorListImage(HttpContext context)
        {
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();
            string filePath = sysConfig.webpath + sysConfig.filepath + "/"; //站点目录+上传目录
            string fileTypes = ".gif,.jpg,.jpeg,.png,.bmp"; //允许浏览的文件扩展名
            ListFileManager(context, filePath, fileTypes);
        }

        /// <summary>
        /// 浏览文件
        /// </summary>
        private void EditorListFile(HttpContext context)
        {
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();
            string filePath = sysConfig.webpath + sysConfig.filepath + "/"; //站点目录+上传目录
            string fileTypes = ".png,.jpg,.jpeg,.gif,.bmp,.flv,.swf,.mkv,.avi,.rm,.rmvb,.mpeg,.mpg,.ogg,.ogv,.mov,.wmv,"
                +".mp4,.webm,.mp3,.wav,.mid,.rar,.zip,.tar,.gz,.7z,.bz2,.cab,.iso,.doc,.docx,.xls,.xlsx,.ppt,.pptx,.pdf,.txt,.md,.xml"; //允许浏览的文件扩展名
            ListFileManager(context, filePath, fileTypes);
        }

        /// <summary>
        /// 抓取远程图片
        /// </summary>
        private void EditorCatchImage(HttpContext context)
        {
            Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();
            if (sysConfig.fileremote == 0)
            {
                Hashtable hash = new Hashtable();
                hash["state"] = "未开启远程图片本地化";
                context.Response.AddHeader("Content-Type", "text/plain; charset=UTF-8");
                context.Response.Write(JsonHelper.ObjectToJSON(hash));
                context.Response.End();
            }
            string[] sourcesUriArr = context.Request.Form.GetValues("source[]");
            if (sourcesUriArr == null || sourcesUriArr.Length == 0)
            {
                Hashtable hash = new Hashtable();
                hash["state"] = "参数错误：没有指定抓取源";
                context.Response.AddHeader("Content-Type", "text/plain; charset=UTF-8");
                context.Response.Write(JsonHelper.ObjectToJSON(hash));
                context.Response.End();
            }
            UpLoad upLoad = new UpLoad(); //初始化上传类
            List<Hashtable> fileList = new List<Hashtable>(); //存储上传成功的文件列表
            foreach (string sourcesUri in sourcesUriArr)
            {
                string remsg = upLoad.RemoteSaveAs(sourcesUri);
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(remsg);
                //如果抓取成功则加入文件列表
                if (dic["status"].ToString() == "1")
                {
                    Hashtable hash = new Hashtable();
                    hash["state"] = "SUCCESS";
                    hash["source"] = sourcesUri;
                    hash["url"] = dic["path"].ToString();
                    fileList.Add(hash);
                }
            }
            Hashtable result = new Hashtable();
            result["state"] = "SUCCESS";
            result["list"] = fileList;
            context.Response.AddHeader("Content-Type", "text/plain; charset=UTF-8");
            context.Response.Write(JsonHelper.ObjectToJSON(result));
            context.Response.End();
        }
        #endregion

        #region 辅助工具方法===================================
        /// <summary>
        /// 统一保存文件
        /// </summary>
        private void FileSave(HttpContext context, HttpPostedFile upFiles, bool isWater)
        {
            if (upFiles == null)
            {
                showError(context, "请选择要上传文件！");
                return;
            }
            //检查是否允许匿名上传
            /*if (sysConfig.fileanonymous == 0 && !new ManagePage().IsAdminLogin() && !new BasePage().IsUserLogin())
            {
                showError(context, "禁止匿名非法上传！");
                return;
            }*/
            //获取文件信息
            string fileName = upFiles.FileName;
            byte[] byteData = FileHelper.ConvertStreamToByteBuffer(upFiles.InputStream); //获取文件流
            //开始上传
            string remsg = new UpLoad().FileSaveAs(byteData, fileName, false, isWater);
            Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(remsg);
            string status = dic["status"].ToString();
            string msg = dic["msg"].ToString();
            if (status == "0")
            {
                showError(context, msg);
                return;
            }
            string filePath = dic["path"].ToString(); //取得上传后的路径
            showSuccess(context, fileName, filePath); //输出成功提示
        }

        /// <summary>
        /// 显示错误提示
        /// </summary>
        private void showError(HttpContext context, string message)
        {
            Hashtable hash = new Hashtable();
            hash["state"] = message;
            hash["error"] = message;
            context.Response.AddHeader("Content-Type", "text/plain; charset=UTF-8");
            context.Response.Write(JsonHelper.ObjectToJSON(hash));
            context.Response.End();
        }

        /// <summary>
        /// 显示成功提示
        /// </summary>
        private void showSuccess(HttpContext context, string fileName, string filePath)
        {
            Hashtable hash = new Hashtable();
            hash["state"] = "SUCCESS";
            hash["url"] = filePath;
            hash["title"] = fileName;
            hash["original"] = fileName;
            context.Response.AddHeader("Content-Type", "text/plain; charset=UTF-8");
            context.Response.Write(JsonHelper.ObjectToJSON(hash));
            context.Response.End();
        }

        /// <summary>
        /// 重新组合扩展名
        /// </summary>
        private string GetExtension(string extStr)
        {
            if (string.IsNullOrEmpty(extStr))
            {
                return "[]";
            }
            string[] strArr = extStr.Split(',');
            StringBuilder sb = new StringBuilder();
            foreach (string str in strArr)
            {
                sb.Append("\"." + str + "\",");
            }
            return "[" + sb.ToString().TrimEnd(',') + "]";
        }

        /// <summary>
        /// 浏览目录文件
        /// </summary>
        private void ListFileManager(HttpContext context, string filePath, string fileTypes)
        {
            int Start = DTRequest.GetInt("start", 0); //开始索引
            int Size = DTRequest.GetInt("size", 20); //每页大小
            int Total = 0; //文件总数
            string State = "SUCCESS"; //状态，默认成功
            String[] FileList = null;

            var buildingList = new List<String>();
            try
            {
                var localPath = Utils.GetMapPath(filePath);
                buildingList.AddRange(Directory.GetFiles(localPath, "*", SearchOption.AllDirectories)
                    .Where(x => fileTypes.Contains(Path.GetExtension(x).ToLower()))
                    .Select(x => filePath + x.Substring(localPath.Length).Replace("\\", "/")));
                Total = buildingList.Count;
                FileList = buildingList.OrderBy(x => x).Skip(Start).Take(Size).ToArray();
            }
            catch (UnauthorizedAccessException)
            {
                State = "文件系统权限不足";
            }
            catch (DirectoryNotFoundException)
            {
                State = "路径不存在";
            }
            catch (IOException)
            {
                State = "文件系统读取错误";
            }
            finally
            {
                Hashtable hash = new Hashtable();
                hash["state"] = State;
                hash["list"] = FileList == null ? null : FileList.Select(x => new { url = x });
                hash["start"] = Start;
                hash["total"] = Total;
                context.Response.AddHeader("Content-Type", "text/plain; charset=UTF-8");
                context.Response.Write(JsonHelper.ObjectToJSON(hash));
                context.Response.End();
            }
        }

        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}