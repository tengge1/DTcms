using System;

namespace DTcms.Model
{
    /// <summary>
    /// 站点管理
    /// </summary>
    [Serializable]
    public partial class sites
    {
        public sites()
        { }
        #region Model
        private int _id;
        private string _title = string.Empty;
        private string _build_path = string.Empty;
        private string _templet_path = string.Empty;
        private string _domain = string.Empty;
        private int _site_type = 0;
        private int _is_default = 0;
        private string _name = string.Empty;
        private string _logo = string.Empty;
        private string _company = string.Empty;
        private string _address = string.Empty;
        private string _tel = string.Empty;
        private string _fax = string.Empty;
        private string _email = string.Empty;
        private string _crod = string.Empty;
        private string _copyright = string.Empty;
        private string _seo_title = string.Empty;
        private string _seo_keyword = string.Empty;
        private string _seo_description = string.Empty;
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
        /// 标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 站点目录名
        /// </summary>
        public string build_path
        {
            set { _build_path = value; }
            get { return _build_path; }
        }
        /// <summary>
        /// 模板目录名
        /// </summary>
        public string templet_path
        {
            set { _templet_path = value; }
            get { return _templet_path; }
        }
        /// <summary>
        /// 绑定域名
        /// </summary>
        public string domain
        {
            set { _domain = value; }
            get { return _domain; }
        }
        /// <summary>
        /// 是否默认站
        /// </summary>
        public int is_default
        {
            set { _is_default = value; }
            get { return _is_default; }
        }
        /// <summary>
        /// 网站名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 网站LOGO
        /// </summary>
        public string logo
        {
            set { _logo = value; }
            get { return _logo; }
        }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 通讯地址
        /// </summary>
        public string address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 传真号码
        /// </summary>
        public string fax
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 备案号
        /// </summary>
        public string crod
        {
            set { _crod = value; }
            get { return _crod; }
        }
        /// <summary>
        /// 版权信息
        /// </summary>
        public string copyright
        {
            set { _copyright = value; }
            get { return _copyright; }
        }
        /// <summary>
        /// SEO标题
        /// </summary>
        public string seo_title
        {
            set { _seo_title = value; }
            get { return _seo_title; }
        }
        /// <summary>
        /// SEO关健字
        /// </summary>
        public string seo_keyword
        {
            set { _seo_keyword = value; }
            get { return _seo_keyword; }
        }
        /// <summary>
        /// SEO描述
        /// </summary>
        public string seo_description
        {
            set { _seo_description = value; }
            get { return _seo_description; }
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
        /// 状态0正常1禁用
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        #endregion
    }
}