using System;
using System.Collections.Generic;

namespace DTcms.Model
{
    /// <summary>
    /// 订单表
    /// </summary>
    [Serializable]
    public partial class orders
    {
        public orders()
        { }
        #region Model
        private int _id;
        private int _site_id = 0;
        private string _order_no = string.Empty;
        private string _trade_no = string.Empty;
        private int _user_id = 0;
        private string _user_name = string.Empty;
        private int _payment_id = 0;
        private decimal _payment_fee = 0M;
        private int _payment_status = 0;
        private DateTime? _payment_time;
        private int _express_id = 0;
        private string _express_no = string.Empty;
        private decimal _express_fee = 0M;
        private int _express_status = 0;
        private DateTime? _express_time;
        private string _accept_name = string.Empty;
        private string _post_code = string.Empty;
        private string _telphone = string.Empty;
        private string _mobile = string.Empty;
        private string _email = string.Empty;
        private string _area = string.Empty;
        private string _address = string.Empty;
        private string _message = string.Empty;
        private string _remark = string.Empty;
        private int _is_invoice = 0;
        private string _invoice_title = string.Empty;
        private decimal _invoice_taxes = 0M;
        private decimal _payable_amount = 0M;
        private decimal _real_amount = 0M;
        private decimal _order_amount = 0M;
        private int _point = 0;
        private int _status = 1;
        private DateTime _add_time = DateTime.Now;
        private DateTime? _confirm_time;
        private DateTime? _complete_time;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 站点ID
        /// </summary>
        public int site_id
        {
            set { _site_id = value; }
            get { return _site_id; }
        }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no
        {
            set { _order_no = value; }
            get { return _order_no; }
        }
        /// <summary>
        /// 交易号担保支付用到
        /// </summary>
        public string trade_no
        {
            set { _trade_no = value; }
            get { return _trade_no; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int user_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name
        {
            set { _user_name = value; }
            get { return _user_name; }
        }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int payment_id
        {
            set { _payment_id = value; }
            get { return _payment_id; }
        }
        /// <summary>
        /// 支付手续费
        /// </summary>
        public decimal payment_fee
        {
            set { _payment_fee = value; }
            get { return _payment_fee; }
        }
        /// <summary>
        /// 支付状态0未支付1待支付2已支付
        /// </summary>
        public int payment_status
        {
            set { _payment_status = value; }
            get { return _payment_status; }
        }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime? payment_time
        {
            set { _payment_time = value; }
            get { return _payment_time; }
        }
        /// <summary>
        /// 快递ID
        /// </summary>
        public int express_id
        {
            set { _express_id = value; }
            get { return _express_id; }
        }
        /// <summary>
        /// 快递单号
        /// </summary>
        public string express_no
        {
            set { _express_no = value; }
            get { return _express_no; }
        }
        /// <summary>
        /// 物流费用
        /// </summary>
        public decimal express_fee
        {
            set { _express_fee = value; }
            get { return _express_fee; }
        }
        /// <summary>
        /// 发货状态1未发货2已发货
        /// </summary>
        public int express_status
        {
            set { _express_status = value; }
            get { return _express_status; }
        }
        /// <summary>
        /// 发货时间
        /// </summary>
        public DateTime? express_time
        {
            set { _express_time = value; }
            get { return _express_time; }
        }
        /// <summary>
        /// 收货人姓名
        /// </summary>
        public string accept_name
        {
            set { _accept_name = value; }
            get { return _accept_name; }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string post_code
        {
            set { _post_code = value; }
            get { return _post_code; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string telphone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 所属省市区
        /// </summary>
        public string area
        {
            set { _area = value; }
            get { return _area; }
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 订单留言
        /// </summary>
        public string message
        {
            set { _message = value; }
            get { return _message; }
        }
        /// <summary>
        /// 订单备注
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 是否索要发票
        /// </summary>
        public int is_invoice
        {
            set { _is_invoice = value; }
            get { return _is_invoice; }
        }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string invoice_title
        {
            set { _invoice_title = value; }
            get { return _invoice_title; }
        }
        /// <summary>
        /// 税金
        /// </summary>
        public decimal invoice_taxes
        {
            set { _invoice_taxes = value; }
            get { return _invoice_taxes; }
        }
        /// <summary>
        /// 应付商品总金额
        /// </summary>
        public decimal payable_amount
        {
            set { _payable_amount = value; }
            get { return _payable_amount; }
        }
        /// <summary>
        /// 实付商品总金额
        /// </summary>
        public decimal real_amount
        {
            set { _real_amount = value; }
            get { return _real_amount; }
        }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal order_amount
        {
            set { _order_amount = value; }
            get { return _order_amount; }
        }
        /// <summary>
        /// 积分,正数赠送|负数消费
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 订单状态1生成订单,2确认订单,3完成订单,4取消订单,5作废订单
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 下单时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 确认时间
        /// </summary>
        public DateTime? confirm_time
        {
            set { _confirm_time = value; }
            get { return _confirm_time; }
        }
        /// <summary>
        /// 订单完成时间
        /// </summary>
        public DateTime? complete_time
        {
            set { _complete_time = value; }
            get { return _complete_time; }
        }

        private List<order_goods> _order_goods;
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<order_goods> order_goods
        {
            set { _order_goods = value; }
            get { return _order_goods; }
        }
        #endregion
    }
}