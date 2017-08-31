using System;

namespace DTcms.Model
{
    /// <summary>
    /// 用户充值表
    /// </summary>
    [Serializable]
    public partial class user_recharge
    {
        public user_recharge()
        { }
        #region Model
        private int _id;
        private int _user_id = 0;
        private string _user_name = string.Empty;
        private string _recharge_no = string.Empty;
        private int _payment_id = 0;
        private decimal _amount = 0M;
        private int _status = 0;
        private DateTime _add_time = DateTime.Now;
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
        /// 充值单号
        /// </summary>
        public string recharge_no
        {
            set { _recharge_no = value; }
            get { return _recharge_no; }
        }
        /// <summary>
        /// 支付方式ID
        /// </summary>
        public int payment_id
        {
            set { _payment_id = value; }
            get { return _payment_id; }
        }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 状态0未完成1已完成
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? complete_time
        {
            set { _complete_time = value; }
            get { return _complete_time; }
        }
        #endregion
    }
}