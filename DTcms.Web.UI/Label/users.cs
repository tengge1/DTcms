using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 站点OAuth列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_site_oauth_list(int top, string strwhere)
        {
            string _where = "is_lock=0";
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " and " + strwhere;
            }
            return new BLL.site_oauth().GetList(top, _where).Tables[0];
        }

        /// <summary>
        /// 返回用户头像图片地址
        /// </summary>
        /// <param name="user_name">用户名</param>
        /// <returns>String</returns>
        protected string get_user_avatar(string user_name)
        {
            BLL.users bll = new BLL.users();
            if (!bll.Exists(user_name))
            {
                return "";
            }
            return bll.GetModel(user_name).avatar;
        }

        /// <summary>
        /// 统计短信息数量
        /// </summary>
        /// <param name="strwhere">查询条件</param>
        /// <returns>Int</returns>
        protected int get_user_message_count(string strwhere)
        {
            return new BLL.user_message().GetCount(strwhere);
        }

        /// <summary>
        /// 短信息列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_user_message_list(int top, string strwhere)
        {
            return new BLL.user_message().GetList(top, strwhere, "is_read asc,post_time desc").Tables[0];
        }

        /// <summary>
        /// 短信息分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_user_message_list(int page_size, int page_index, string strwhere, out int totalcount)
        {
            return new BLL.user_message().GetList(page_size, page_index, strwhere, "is_read asc,post_time desc", out totalcount).Tables[0];
        }

        /// <summary>
        /// 积分明细分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_user_point_list(int page_size, int page_index, string strwhere, out int totalcount)
        {
            return new BLL.user_point_log().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        /// <summary>
        /// 余额明细分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_user_amount_list(int page_size, int page_index, string strwhere, out int totalcount)
        {
            return new BLL.user_amount_log().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        /// <summary>
        /// 充值记录分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_user_recharge_list(int page_size, int page_index, string strwhere, out int totalcount)
        {
            return new BLL.user_recharge().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        /// <summary>
        /// 用户邀请码列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns></returns>
        protected DataTable get_user_invite_list(int top, string strwhere)
        {
            string _where = "type='" + DTEnums.CodeEnum.Register.ToString() + "'";
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " and " + strwhere;
            }
            return new BLL.user_code().GetList(top, _where, "add_time desc,id desc").Tables[0];
        }
        /// <summary>
        /// 返回邀请码状态
        /// </summary>
        /// <param name="str_code">邀请码</param>
        /// <returns>bool</returns>
        protected bool get_invite_status(string str_code)
        {
            Model.user_code model = new BLL.user_code().GetModel(str_code);
            if (model != null)
            {
                return true;
            }
            return false;
        }

    }
}
