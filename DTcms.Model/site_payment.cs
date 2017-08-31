using System;

namespace DTcms.Model
{
    /// <summary>
    /// 站点支付方式表
    /// </summary>
    [Serializable]
    public partial class site_payment
    {
        public site_payment()
        { }
        #region Model
        private int _id;
        private int _site_id = 0;
        private int _payment_id = 0;
        private string _title = string.Empty;
        private string _key1 = string.Empty;
        private string _key2 = string.Empty;
        private string _key3 = string.Empty;
        private string _key4 = string.Empty;
        private int _sort_id = 99;
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
        /// 所属站点
        /// </summary>
        public int site_id
        {
            set { _site_id = value; }
            get { return _site_id; }
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
        /// 自定义标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 通讯密钥1
        /// </summary>
        public string key1
        {
            set { _key1 = value; }
            get { return _key1; }
        }
        /// <summary>
        /// 通讯密钥2
        /// </summary>
        public string key2
        {
            set { _key2 = value; }
            get { return _key2; }
        }
        /// <summary>
        /// 通讯密钥3
        /// </summary>
        public string key3
        {
            set { _key3 = value; }
            get { return _key3; }
        }
        /// <summary>
        /// 通讯密钥4
        /// </summary>
        public string key4
        {
            set { _key4 = value; }
            get { return _key4; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        #endregion
    }
}