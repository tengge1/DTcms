using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DTcms.Web.UI
{
    public partial class BasePage : System.Web.UI.Page
    {
        /// <summary>
        /// 返回支付列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_payment_list(int top, string strwhere)
        {
            DataTable dt = new DataTable();
            string _where = "is_lock=0";
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " and " + strwhere;
            }
            dt = new BLL.site_payment().GetList(top, _where).Tables[0];
            return dt;
        }

        /// <summary>
        /// 返回支付类型的标题
        /// </summary>
        /// <param name="payment_id">ID</param>
        /// <returns>String</returns>
        protected string get_payment_title(int payment_id)
        {
            return new BLL.site_payment().GetTitle(payment_id);
        }

        /// <summary>
        /// 返回支付费用金额
        /// </summary>
        /// <param name="payment_id">支付ID</param>
        /// <param name="total_amount">总金额</param>
        /// <returns>decimal</returns>
        protected decimal get_payment_poundage_amount(int payment_id, decimal total_amount)
        {
            Model.payment payModel = new BLL.site_payment().GetPaymentModel(payment_id);
            if (payModel == null)
            {
                return 0;
            }
            decimal poundage_amount = payModel.poundage_amount;
            if (payModel.poundage_type == 1)
            {
                poundage_amount = (poundage_amount * total_amount) / 100;
            }
            return poundage_amount;
        }

    }
}
