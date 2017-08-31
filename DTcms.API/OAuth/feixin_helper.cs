using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;

namespace DTcms.API.OAuth
{
    public class feixin_helper
    {
        public feixin_helper()
        { }

        /// <summary>
        /// 取得Access Token
        /// </summary>
        /// <param name="app_id">client_id</param>
        /// <param name="app_key">client_secret</param>
        /// <param name="return_uri">redirect_uri</param>
        /// <param name="code">临时Authorization Code</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(string app_id, string app_key, string return_uri, string code)
        {
            string send_url = "https://i.feixin.10086.cn/oauth2/access_token?grant_type=authorization_code&code=" + code + "&client_id=" + app_id + "&client_secret=" + app_key + "&redirect_uri=" + Utils.UrlEncode(return_uri);
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(result);
                return dic;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取登录用户自己的详细信息
        /// </summary>
        /// <param name="access_token">临时的Access Token</param>
        /// <returns>JsonData</returns>
        public static Dictionary<string, object> get_info(string access_token)
        {
            string send_url = "https://i.feixin.10086.cn/api/user.json?access_token=" + access_token;
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(result);
                return dic;
            }
            catch
            {
                return null;
            }
        }

    }
}
