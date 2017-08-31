using System;

namespace DTcms.Model
{
    /// <summary>
    /// 物流快递
    /// </summary>
    [Serializable]
    public partial class express
    {
        public express()
        { }
        #region Model
        private int _id;
        private string _title = string.Empty;
        private string _express_code = string.Empty;
        private decimal _express_fee = 0M;
        private string _website = string.Empty;
        private string _remark = string.Empty;
        private int _sort_id = 99;
        private int _is_lock = 0;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 快递名称
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 物流编码
        /// </summary>
        public string express_code
        {
            set { _express_code = value; }
            get { return _express_code; }
        }
        /// <summary>
        /// 配送费用
        /// </summary>
        public decimal express_fee
        {
            set { _express_fee = value; }
            get { return _express_fee; }
        }
        /// <summary>
        /// 快递网址
        /// </summary>
        public string website
        {
            set { _website = value; }
            get { return _website; }
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
        /// 排序
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 是否不显示
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        #endregion
    }
}