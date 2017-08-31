using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using DTcms.Common;

namespace DTcms.API.OAuth
{
    public class renren_helper
    {
        public renren_helper()
        { }

        /// <summary>
        /// 取得临时的Access Token
        /// </summary>
        /// <param name="app_id">client_id</param>
        /// <param name="app_key">client_secret</param>
        /// <param name="return_uri">redirect_uri</param>
        /// <param name="code">Authorization Code</param>
        /// <returns>JsonData</returns>
        public static Dictionary<string, object> get_access_token(string app_id, string app_key, string return_uri, string code)
        {
            string send_url = "https://graph.renren.com/oauth/token?grant_type=authorization_code&client_id=" + app_id + "&client_secret=" + app_key + "&code=" + code + "&redirect_uri=" + Utils.UrlEncode(return_uri);
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(result);
                if (dic.Count > 0)
                {
                    return dic;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// 获取登录用户自己的详细信息
        /// </summary>
        /// <param name="app_key">client_secret</param>
        /// <param name="access_token">临时的Access Token</param>
        /// <param name="open_id">用户openid</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_info(string app_key, string access_token, string fields)
        {
            string send_url = "http://api.renren.com/restserver.do";
            StringBuilder param = new StringBuilder();
            param.Append("method=users.getInfo&");
            param.Append("access_token=" + access_token + "&");
            param.Append("fields=" + fields + "&");
            param.Append("format=json&");
            param.Append("v=1.0&");
            param.Append("sig=" + MD5Encrpt("access_token=" + access_token + "fields=" + fields + "format=jsonmethod=users.getInfov=1.0" + app_key));
            //发送并接受返回值
            string result = Utils.HttpPost(send_url, param.ToString());
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(result);
                if (dic.Count > 0)
                {
                    return dic;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="plainText">需要加密的字符串</param>
        public static string MD5Encrpt(string plainText)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            StringBuilder sbList = new StringBuilder();
            foreach (byte d in data)
            {
                sbList.Append(d.ToString("x2"));
            }
            return sbList.ToString();
        }

    }
}
