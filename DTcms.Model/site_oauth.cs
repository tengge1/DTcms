using System;

namespace DTcms.Model
{
    /// <summary>
    /// 所属站点的OAuth登录表
    /// </summary>
    [Serializable]
    public partial class site_oauth
    {
        public site_oauth()
        { }
        #region Model
        private int _id;
        private int _site_id = 0;
        private int _oauth_id = 0;
        private string _title = string.Empty;
        private string _app_id = string.Empty;
        private string _app_key = string.Empty;
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
        /// 站点ID
        /// </summary>
        public int site_id
        {
            set { _site_id = value; }
            get { return _site_id; }
        }
        /// <summary>
        /// OAuth应用ID
        /// </summary>
        public int oauth_id
        {
            set { _oauth_id = value; }
            get { return _oauth_id; }
        }
        /// <summary>
        /// 自定义标签
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// AppId
        /// </summary>
        public string app_id
        {
            set { _app_id = value; }
            get { return _app_id; }
        }
        /// <summary>
        /// AppKey
        /// </summary>
        public string app_key
        {
            set { _app_key = value; }
            get { return _app_key; }
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