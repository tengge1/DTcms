using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;
namespace DTcms.Web.api.oauth
{
    public partial class result_json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int site_oauth_id = 0;
            string oauth_access_token = string.Empty;
            string oauth_openid = string.Empty;
            string oauth_name = string.Empty;

            if (Session["oauth_id"] == null || Session["oauth_name"] == null || Session["oauth_access_token"] == null || Session["oauth_openid"] == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，Access Token已过期或不存在！\"}");
                return;
            }
            site_oauth_id = Utils.StrToInt(Session["oauth_id"].ToString(), 0);
            oauth_name = Session["oauth_name"].ToString();
            oauth_access_token = Session["oauth_access_token"].ToString();
            oauth_openid = Session["oauth_openid"].ToString();

            switch (oauth_name)
            {
                case "qq":
                    qq(site_oauth_id, oauth_name, oauth_access_token, oauth_openid);
                    break;
                case "sina":
                    sina(oauth_name, oauth_access_token, oauth_openid);
                    break;
                case "renren":
                    renren(site_oauth_id, oauth_name, oauth_access_token, oauth_openid);
                    break;
                case "kaixin":
                    kaixin(oauth_name, oauth_access_token, oauth_openid);
                    break;
                case "feixin":
                    feixin(oauth_name, oauth_access_token, oauth_openid);
                    break;
            }
        }

        #region QQ登录处理方法=============================
        private void qq(int site_oauth_id, string oauth_name, string oauth_access_token, string oauth_openid)
        {
            oauth_config config = oauth_helper.get_config(site_oauth_id); //获取参数信息
            Dictionary<string, object> dic = qq_helper.get_user_info(config.oauth_app_id, oauth_access_token, oauth_openid);
            if (dic == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }
            try
            {
                if (dic["ret"].ToString() != "0")
                {
                    Response.Write("{\"ret\":\"" + dic["ret"].ToString() + "\", \"msg\":\"出错信息:" + dic["msg"].ToString() + "！\"}");
                    return;
                }
                StringBuilder str = new StringBuilder();
                str.Append("{");
                str.Append("\"ret\": \"" + dic["ret"].ToString() + "\", ");
                str.Append("\"msg\": \"" + dic["msg"].ToString() + "\", ");
                str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
                str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
                str.Append("\"oauth_openid\": \"" + oauth_openid + "\", ");
                str.Append("\"nick\": \"" + dic["nickname"].ToString() + "\", ");
                str.Append("\"avatar\": \"" + dic["figureurl_qq_2"].ToString() + "\", ");
                str.Append("\"sex\": \"" + dic["gender"].ToString() + "\", ");
                str.Append("\"birthday\": \"\"");
                str.Append("}");
                Response.Write(str.ToString());
            }
            catch
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
            }
            return;
        }
        #endregion

        #region 微博登录处理方法===========================
        private void sina(string oauth_name, string oauth_access_token, string oauth_openid)
        {
            Dictionary<string, object> dic = sina_helper.get_info(oauth_access_token, oauth_openid);
            if (dic == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append("\"ret\": \"0\", ");
            str.Append("\"msg\": \"获得用户信息成功！\", ");
            str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
            str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
            str.Append("\"oauth_openid\": \"" + dic["id"].ToString() + "\", ");
            str.Append("\"nick\": \"" + dic["screen_name"].ToString() + "\", ");
            str.Append("\"avatar\": \"" + dic["profile_image_url"].ToString() + "\", ");
            if (dic["gender"].ToString() == "m")
            {
                str.Append("\"sex\": \"男\", ");
            }
            else if (dic["gender"].ToString() == "f")
            {
                str.Append("\"sex\": \"女\", ");
            }
            else
            {
                str.Append("\"sex\": \"保密\", ");
            }
            str.Append("\"birthday\": \"\"");
            str.Append("}");

            Response.Write(str.ToString());
            return;
        }
        #endregion

        #region 人人网登录处理方法=========================
        private void renren(int site_oauth_id, string oauth_name, string oauth_access_token, string oauth_openid)
        {
            oauth_config config = oauth_helper.get_config(site_oauth_id); //获取参数信息
            Dictionary<string, object> dic = renren_helper.get_info(config.oauth_app_key, oauth_access_token, "uid,name,sex,birthday,mainurl");
            if (dic == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }

            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append("\"ret\": \"0\", ");
            str.Append("\"msg\": \"获得用户信息成功！\", ");
            str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
            str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
            str.Append("\"oauth_openid\": \"" + dic["uid"].ToString() + "\", ");
            str.Append("\"nick\": \"" + dic["name"].ToString() + "\", ");
            str.Append("\"avatar\": \"" + dic["mainurl"].ToString() + "\", ");
            if (dic["sex"].ToString() == "1")
            {
                str.Append("\"sex\": \"男\", ");
            }
            else if (dic["sex"].ToString() == "0")
            {
                str.Append("\"sex\": \"女\", ");
            }
            else
            {
                str.Append("\"sex\": \"保密\", ");
            }
            str.Append("\"birthday\": \"" + dic["birthday"].ToString() + "\"");
            str.Append("}");

            Response.Write(str.ToString());
            return;
        }
        #endregion

        #region 开心网登录处理方法=========================
        private void kaixin(string oauth_name, string oauth_access_token, string oauth_openid)
        {
            Dictionary<string, object> dic = kaixin_helper.get_info(oauth_access_token, "uid,name,logo120,gender,birthday");
            if (dic == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append("\"ret\": \"0\", ");
            str.Append("\"msg\": \"获得用户信息成功！\", ");
            str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
            str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
            str.Append("\"oauth_openid\": \"" + dic["uid"].ToString() + "\", ");
            str.Append("\"nick\": \"" + dic["name"].ToString() + "\", ");
            str.Append("\"avatar\": \"" + dic["logo120"].ToString() + "\", ");
            if (dic["gender"].ToString() == "0")
            {
                str.Append("\"sex\": \"男\", ");
            }
            else if (dic["gender"].ToString() == "1")
            {
                str.Append("\"sex\": \"女\", ");
            }
            else
            {
                str.Append("\"sex\": \"保密\", ");
            }
            str.Append("\"birthday\": \"" + dic["birthday"].ToString() + "\"");
            str.Append("}");

            Response.Write(str.ToString());
            return;
        }
        #endregion

        #region 飞信登录处理方法===========================
        private void feixin(string oauth_name, string oauth_access_token, string oauth_openid)
        {
            Dictionary<string, object> dic = feixin_helper.get_info(oauth_access_token);
            if (dic == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }
            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append("\"ret\": \"0\", ");
            str.Append("\"msg\": \"获得用户信息成功！\", ");
            str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
            str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
            str.Append("\"oauth_openid\": \"" + dic["userId"].ToString() + "\", ");
            str.Append("\"nick\": \"" + dic["nickname"].ToString() + "\", ");
            str.Append("\"avatar\": \"" + dic["portraitLarge"].ToString() + "\", ");
            if (dic["gender"].ToString() == "1")
            {
                str.Append("\"sex\": \"男\", ");
            }
            else if (dic["gender"].ToString() == "2")
            {
                str.Append("\"sex\": \"女\", ");
            }
            else
            {
                str.Append("\"sex\": \"保密\", ");
            }
            str.Append("\"birthday\": \"" + dic["birthday"].ToString() + "\"");
            str.Append("}");

            Response.Write(str.ToString());
            return;
        }
        #endregion

    }
}