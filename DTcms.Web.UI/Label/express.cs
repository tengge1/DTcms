using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 返回配送列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_express_list(int top, string strwhere)
        {
            DataTable dt = new DataTable();
            string _where = "is_lock=0";
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " and " + strwhere;
            }
            dt = new BLL.express().GetList(top, _where, "sort_id asc,id desc").Tables[0];
            return dt;
        }

        /// <summary>
        /// 返回配送方式的标题
        /// </summary>
        /// <param name="payment_id">ID</param>
        /// <returns>String</returns>
        protected string get_express_title(int express_id)
        {
            return new BLL.express().GetTitle(express_id);
        }

    }
}
