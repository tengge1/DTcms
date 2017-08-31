using System;

namespace DTcms.Model
{
    /// <summary>
    /// 会员金额日志表
    /// </summary>
    [Serializable]
    public partial class user_amount_log
    {
        public user_amount_log()
        { }
        #region Model
        private int _id;
        private int _user_id = 0;
        private string _user_name = string.Empty;
        private decimal _value = 0M;
        private string _remark = string.Empty;
        private DateTime _add_time = DateTime.Now;
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
        /// 增减值
        /// </summary>
        public decimal value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        #endregion
    }
}