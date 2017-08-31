using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;

namespace DTcms.API.OAuth
{
    public class sina_helper
    {
        public sina_helper()
        { }

        /// <summary>
        /// 取得Access Token
        /// </summary>
        /// <param name="app_id">client_id</param>
        /// <param name="app_key">client_secret</param>
        /// <param name="return_uri">redirect_uri</param>
        /// <param name="code">临时Authorization Code，官方提示10分钟过期</param>
        /// <param name="state">防止CSRF攻击，成功授权后回调时会原样带回</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(string app_id, string app_key, string return_uri, string code)
        {
            //获得配置信息
            //oauth_config config = oauth_helper.get_config("sina");
            string send_url = "https://api.weibo.com/oauth2/access_token";
            string param = "grant_type=authorization_code&code=" + code + "&client_id=" + app_id + "&client_secret=" + app_key + "&redirect_uri=" + Utils.UrlEncode(return_uri);
            //发送并接受返回值
            string result = Utils.HttpPost(send_url, param);
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
        /// 查询用户access_token的授权相关信息
        /// </summary>
        /// <param name="access_token">Access Token</param>
        /// <returns>Dictionary<T></returns>
        public static Dictionary<string, object> get_token_info(string access_token)
        {
            string send_url = "https://api.weibo.com/oauth2/get_token_info";
            string param = "access_token=" + access_token;
            //发送并接受返回值
            string result = Utils.HttpPost(send_url, param);
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
        /// <param name="open_id">用户属性，以英文逗号分隔</param>
        /// <returns>JsonData</returns>
        public static Dictionary<string, object> get_info(string access_token, string open_id)
        {
            string send_url = "https://api.weibo.com/2/users/show.json?access_token=" + access_token + "&uid=" + open_id;
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

    }
}
