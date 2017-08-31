using System;

namespace DTcms.Model
{
    /// <summary>
    /// 支付方式
    /// </summary>
    [Serializable]
    public partial class payment
    {
        public payment()
        { }
        #region Model
        private int _id;
        private string _title = string.Empty;
        private string _img_url = string.Empty;
        private string _remark = string.Empty;
        private int _type = 1;
        private int _poundage_type = 1;
        private decimal _poundage_amount = 0M;
        private string _api_path = string.Empty;
        private string _redirect_url = string.Empty;
        private string _return_url = string.Empty;
        private string _notify_url = string.Empty;
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
        /// 支付名称
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 显示图片
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
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
        /// 支付类型1线上2线下
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 手续费类型1百分比2固定金额
        /// </summary>
        public int poundage_type
        {
            set { _poundage_type = value; }
            get { return _poundage_type; }
        }
        /// <summary>
        /// 手续费金额
        /// </summary>
        public decimal poundage_amount
        {
            set { _poundage_amount = value; }
            get { return _poundage_amount; }
        }
        /// <summary>
        /// API目录名
        /// </summary>
        public string api_path
        {
            set { _api_path = value; }
            get { return _api_path; }
        }
        /// <summary>
        /// 支付链接页面
        /// </summary>
        public string redirect_url
        {
            set { _redirect_url = value; }
            get { return _redirect_url; }
        }
        /// <summary>
        /// 跳转返回页面
        /// </summary>
        public string return_url
        {
            set { _return_url = value; }
            get { return _return_url; }
        }
        /// <summary>
        /// 支付通知页面
        /// </summary>
        public string notify_url
        {
            set { _notify_url = value; }
            get { return _notify_url; }
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
        /// 是否启用0正常1禁用
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        #endregion
    }
}