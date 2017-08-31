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
        /// 统计订单数量
        /// </summary>
        /// <param name="strwhere">查询条件</param>
        /// <returns>Int</returns>
        protected int get_user_order_count(string strwhere)
        {
            return new BLL.orders().GetCount(strwhere);
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        protected DataTable get_order_list(int top, string strwhere)
        {
            return new BLL.orders().GetList(top, strwhere, "add_time desc,id desc").Tables[0];
        }

        /// <summary>
        /// 订单分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        protected DataTable get_order_list(int page_size, int page_index, string strwhere, out int totalcount)
        {
            return new BLL.orders().GetList(page_size, page_index, strwhere, "add_time desc,id desc", out totalcount).Tables[0];
        }

        /// <summary>
        /// 返回订单商品列表
        /// </summary>
        /// <param name="order_id">订单</param>
        /// <returns>List</returns>
        protected List<Model.article> get_order_goods_list(int order_id)
        {
            Model.orders model = new BLL.orders().GetModel(order_id);
            if (model == null)
            {
                return null;
            }
            List<Model.article> ls = new List<Model.article>();
            if (model.order_goods != null)
            {
                foreach (Model.order_goods modelt in model.order_goods)
                {
                    Model.article goodsModel = new BLL.article().GetModel(modelt.channel_id, modelt.article_id);
                    if (goodsModel != null)
                    {
                        ls.Add(goodsModel);
                    }
                }
            }
            return ls;
        }

        /// <summary>
        /// 返回订单状态
        /// </summary>
        /// <param name="_id">订单ID</param>
        /// <returns>String</returns>
        protected string get_order_status(int _id)
        {
            string _title = "";
            Model.orders model = new BLL.orders().GetModel(_id);
            switch (model.status)
            {
                case 1: //如果是线下支付，支付状态为0，如果是线上支付，支付成功后会自动改变订单状态为已确认
                    if (model.payment_status > 0)
                    {
                        _title = "待付款";
                    }
                    else
                    {
                        _title = "待确认";
                    }
                    break;
                case 2: //如果订单为已确认状态，则进入发货状态
                    if (model.express_status > 1)
                    {
                        _title = "已发货";
                    }
                    else
                    {
                        _title = "待发货";
                    }
                    break;
                case 3:
                    _title = "交易完成";
                    break;
                case 4:
                    _title = "已取消";
                    break;
                case 5:
                    _title = "已作废";
                    break;
            }

            return _title;
        }

        /// <summary>
        /// 返回订单是否需要在线支付
        /// </summary>
        /// <param name="order_id">订单ID</param>
        /// <returns>bool</returns>
        protected bool get_order_payment_status(int order_id)
        {
            Model.orders model = new BLL.orders().GetModel(order_id);
            if (model == null)
            {
                return false;
            }
            if (model.status != 1)
            {
                return false;
            }
            Model.payment payModel = new BLL.site_payment().GetPaymentModel(model.payment_id);
            if (payModel == null)
            {
                return false;
            }
            if (payModel.type == 1 && model.payment_status == 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回税金费用金额
        /// </summary>
        /// <param name="total_amount">总金额</param>
        /// <returns>decimal</returns>
        protected decimal get_order_taxamount(decimal total_amount)
        {
            Model.orderconfig model = new BLL.orderconfig().loadConfig();
            decimal taxamount = model.taxamount;
            if (model.taxtype == 1)
            {
                taxamount = (taxamount * total_amount) / 100;
            }
            return taxamount;
        }

    }
}
