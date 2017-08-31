using System;
using System.Collections;
using System.Web;
using System.IO;
using System.Drawing;
using System.Net;
using System.Configuration;
using DTcms.Common;

namespace DTcms.Web.UI
{
    public class UpLoad
    {
        private Model.sysconfig sysConfig;

        public UpLoad()
        {
            sysConfig = new BLL.sysconfig().loadConfig();
        }

        /// <summary>
        /// 通过文件流上传文件方法
        /// </summary>
        /// <param name="byteData">文件字节数组</param>
        /// <param name="fileName">文件名</param>
        /// <param name="isThumbnail">是否生成缩略图</param>
        /// <param name="isWater">是否打水印</param>
        /// <returns>上传成功返回JSON字符串</returns>
        public string FileSaveAs(byte[] byteData, string fileName, bool isThumbnail, bool isWater)
        {
            try
            {
                string fileExt = Path.GetExtension(fileName).Trim('.'); //文件扩展名，不含“.”
                string newFileName = Utils.GetRamCode() + "." + fileExt; //随机生成新的文件名
                string newThumbnailFileName = "thumb_" + newFileName; //随机生成缩略图文件名

                string upLoadPath = GetUpLoadPath(); //本地上传目录相对路径
                string fullUpLoadPath = Utils.GetMapPath(upLoadPath); //本地上传目录的物理路径
                string newFilePath = upLoadPath + newFileName; //本地上传后的路径
                string newThumbnailPath = upLoadPath + newThumbnailFileName; //本地上传后的缩略图路径

                byte[] thumbData = null; //缩略图文件流

                //检查文件字节数组是否为NULL
                if (byteData == null)
                {
                    return "{\"status\": 0, \"msg\": \"请选择要上传的文件！\"}";
                }
                //检查文件扩展名是否合法
                if (!CheckFileExt(fileExt))
                {
                    return "{\"status\": 0, \"msg\": \"不允许上传" + fileExt + "类型的文件！\"}";
                }
                //检查文件大小是否合法
                if (!CheckFileSize(fileExt, byteData.Length))
                {
                    return "{\"status\": 0, \"msg\": \"文件超过限制的大小！\"}";
                }

                //如果是图片，检查图片是否超出最大尺寸，是则裁剪
                if (IsImage(fileExt) && (this.sysConfig.imgmaxheight > 0 || this.sysConfig.imgmaxwidth > 0))
                {
                    byteData = Thumbnail.MakeThumbnailImage(byteData, fileExt, this.sysConfig.imgmaxwidth, this.sysConfig.imgmaxheight);
                }
                //如果是图片，检查是否需要生成缩略图，是则生成
                if (IsImage(fileExt) && isThumbnail && this.sysConfig.thumbnailwidth > 0 && this.sysConfig.thumbnailheight > 0)
                {
                    thumbData = Thumbnail.MakeThumbnailImage(byteData, fileExt, this.sysConfig.thumbnailwidth, this.sysConfig.thumbnailheight, this.sysConfig.thumbnailmode);
                }
                else
                {
                    newThumbnailPath = newFilePath; //不生成缩略图则返回原图
                }
                //如果是图片，检查是否需要打水印
                if (IsWaterMark(fileExt) && isWater)
                {
                    switch (this.sysConfig.watermarktype)
                    {
                        case 1:
                            byteData = WaterMark.AddImageSignText(byteData, fileExt, this.sysConfig.watermarktext, this.sysConfig.watermarkposition,
                                this.sysConfig.watermarkimgquality, this.sysConfig.watermarkfont, this.sysConfig.watermarkfontsize);
                            break;
                        case 2:
                            byteData = WaterMark.AddImageSignPic(byteData, fileExt, this.sysConfig.watermarkpic, this.sysConfig.watermarkposition,
                                this.sysConfig.watermarkimgquality, this.sysConfig.watermarktransparency);
                            break;
                    }
                }

                //检查本地上传的物理路径是否存在，不存在则创建
                if (!Directory.Exists(fullUpLoadPath))
                {
                    Directory.CreateDirectory(fullUpLoadPath);
                }
                //保存主文件
                FileHelper.SaveFile(byteData, fullUpLoadPath + newFileName);
                //保存缩略图文件
                if (thumbData != null)
                {
                    FileHelper.SaveFile(thumbData, fullUpLoadPath + newThumbnailFileName);
                }

                //处理完毕，返回JOSN格式的文件信息
                return "{\"status\": 1, \"msg\": \"上传文件成功！\", \"name\": \""
                    + fileName + "\", \"path\": \"" + newFilePath + "\", \"thumb\": \""
                    + newThumbnailPath + "\", \"size\": " + byteData.Length + ", \"ext\": \"" + fileExt + "\"}";
            }
            catch
            {
                return "{\"status\": 0, \"msg\": \"上传过程中发生意外错误！\"}";
            }
        }

        /// <summary>
        /// 裁剪图片并保存
        /// </summary>
        public string CropSaveAs(string fileUri, int maxWidth, int maxHeight, int cropWidth, int cropHeight, int X, int Y)
        {
            string fileExt = Path.GetExtension(fileUri).Trim('.'); //文件扩展名，不含“.”
            if (string.IsNullOrEmpty(fileExt) || !IsImage(fileExt))
            {
                return "{\"status\": 0, \"msg\": \"该文件不是图片！\"}";
            }

            byte[] byteData = null;
            //判断是否远程文件
            if (fileUri.ToLower().StartsWith("http://") || fileUri.ToLower().StartsWith("https://"))
            {
                WebClient client = new WebClient();
                byteData = client.DownloadData(fileUri);
                client.Dispose();
            }
            else //本地源文件
            {
                string fullName = Utils.GetMapPath(fileUri);
                if (File.Exists(fullName))
                {
                    FileStream fs = File.OpenRead(fullName);
                    BinaryReader br = new BinaryReader(fs);
                    br.BaseStream.Seek(0, SeekOrigin.Begin);
                    byteData = br.ReadBytes((int)br.BaseStream.Length);
                    fs.Close();
                }
            }
            //裁剪后得到文件流
            byteData = Thumbnail.MakeThumbnailImage(byteData, fileExt, maxWidth, maxHeight, cropWidth, cropHeight, X, Y);
            //删除原图
            DeleteFile(fileUri);
            //保存制作好的缩略图
            return FileSaveAs(byteData, fileUri, false, false);
        }

        /// <summary>
        /// 保存远程文件到本地
        /// </summary>
        /// <param name="sourceUri">URI地址</param>
        /// <returns>上传后的路径</returns>
        public string RemoteSaveAs(string sourceUri)
        {
            if (!IsExternalIPAddress(sourceUri))
            {
                return "{\"status\": 0, \"msg\": \"INVALID_URL\"}";
            }
            var request = HttpWebRequest.Create(sourceUri) as HttpWebRequest;
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return "{\"status\": 0, \"msg\": \"Url returns " + response.StatusCode + ", " + response.StatusDescription + "\"}";
                }
                if (response.ContentType.IndexOf("image") == -1)
                {
                    return "{\"status\": 0, \"msg\": \"Url is not an image\"}";
                }
                try
                {
                    byte[] byteData = FileHelper.ConvertStreamToByteBuffer(response.GetResponseStream());
                    return FileSaveAs(byteData, sourceUri, false, false);
                }
                catch (Exception e)
                {
                    return "{\"status\": 0, \"msg\": \"抓取错误：" + e.Message + "\"}";
                }
            }
        }

        /// <summary>
        /// 删除上传文件
        /// </summary>
        /// <param name="fileUri">相对地址或网址</param>
        public void DeleteFile(string fileUri)
        {
            //文件不应是上传文件，防止跨目录删除
            if (fileUri.IndexOf("..") == -1 && fileUri.ToLower().StartsWith(sysConfig.webpath.ToLower() + sysConfig.filepath.ToLower()))
            {
                FileHelper.DeleteUpFile(fileUri);
            }
        }

        #region 私有方法
        /// <summary>
        /// 返回上传目录相对路径
        /// </summary>
        /// <param name="fileName">上传文件名</param>
        private string GetUpLoadPath()
        {
            string path = sysConfig.webpath + sysConfig.filepath + "/"; //站点目录+上传目录
            switch (this.sysConfig.filesave)
            {
                case 1: //按年月日每天一个文件夹
                    path += DateTime.Now.ToString("yyyyMMdd");
                    break;
                default: //按年月/日存入不同的文件夹
                    path += DateTime.Now.ToString("yyyyMM") + "/" + DateTime.Now.ToString("dd");
                    break;
            }
            return path + "/";
        }

        /// <summary>
        /// 是否需要打水印
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsWaterMark(string _fileExt)
        {
            //判断是否开启水印
            if (this.sysConfig.watermarktype > 0)
            {
                //判断是否可以打水印的图片类型
                ArrayList al = new ArrayList();
                al.Add("bmp");
                al.Add("jpeg");
                al.Add("jpg");
                al.Add("png");
                if (al.Contains(_fileExt.ToLower()))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 是否为图片文件
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        private bool IsImage(string _fileExt)
        {
            ArrayList al = new ArrayList();
            al.Add("bmp");
            al.Add("jpeg");
            al.Add("jpg");
            al.Add("gif");
            al.Add("png");
            if (al.Contains(_fileExt.ToLower()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查是否为合法的上传文件
        /// </summary>
        private bool CheckFileExt(string _fileExt)
        {
            //检查危险文件
            string[] excExt = { "asp", "aspx", "ashx", "asa", "asmx", "asax", "php", "jsp", "htm", "html" };
            for (int i = 0; i < excExt.Length; i++)
            {
                if (excExt[i].ToLower() == _fileExt.ToLower())
                {
                    return false;
                }
            }
            //检查合法文件
            string[] allowExt = (this.sysConfig.fileextension + "," + this.sysConfig.videoextension).Split(',');
            for (int i = 0; i < allowExt.Length; i++)
            {
                if (allowExt[i].ToLower() == _fileExt.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 检查文件大小是否合法
        /// </summary>
        /// <param name="_fileExt">文件扩展名，不含“.”</param>
        /// <param name="_fileSize">文件大小(B)</param>
        private bool CheckFileSize(string _fileExt, int _fileSize)
        {
            //将视频扩展名转换成ArrayList
            ArrayList lsVideoExt = new ArrayList(this.sysConfig.videoextension.ToLower().Split(','));
            //判断是否为图片文件
            if (IsImage(_fileExt))
            {
                if (this.sysConfig.imgsize > 0 && _fileSize > this.sysConfig.imgsize * 1024)
                {
                    return false;
                }
            }
            else if (lsVideoExt.Contains(_fileExt.ToLower()))
            {
                if (this.sysConfig.videosize > 0 && _fileSize > this.sysConfig.videosize * 1024)
                {
                    return false;
                }
            }
            else
            {
                if (this.sysConfig.attachsize > 0 && _fileSize > this.sysConfig.attachsize * 1024)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 检查文件地址是否文件服务器地址
        /// </summary>
        /// <param name="url">文件地址</param>
        private bool IsExternalIPAddress(string url)
        {
            var uri = new Uri(url);
            switch (uri.HostNameType)
            {
                case UriHostNameType.Dns:
                    var ipHostEntry = Dns.GetHostEntry(uri.DnsSafeHost);
                    foreach (IPAddress ipAddress in ipHostEntry.AddressList)
                    {
                        byte[] ipBytes = ipAddress.GetAddressBytes();
                        if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            if (!IsPrivateIP(ipAddress))
                            {
                                return true;
                            }
                        }
                    }
                    break;

                case UriHostNameType.IPv4:
                    return !IsPrivateIP(IPAddress.Parse(uri.DnsSafeHost));
            }
            return false;
        }

        /// <summary>
        /// 检查IP地址是否本地服务器地址
        /// </summary>
        /// <param name="myIPAddress">IP地址</param>
        private bool IsPrivateIP(IPAddress myIPAddress)
        {
            if (IPAddress.IsLoopback(myIPAddress)) return true;
            if (myIPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                byte[] ipBytes = myIPAddress.GetAddressBytes();
                // 10.0.0.0/24 
                if (ipBytes[0] == 10)
                {
                    return true;
                }
                // 172.16.0.0/16
                else if (ipBytes[0] == 172 && ipBytes[1] == 16)
                {
                    return true;
                }
                // 192.168.0.0/16
                else if (ipBytes[0] == 192 && ipBytes[1] == 168)
                {
                    return true;
                }
                // 169.254.0.0/16
                else if (ipBytes[0] == 169 && ipBytes[1] == 254)
                {
                    return true;
                }
            }
            return false;
        }
        #endregion

    }
}
