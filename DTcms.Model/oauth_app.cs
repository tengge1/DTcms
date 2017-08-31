using System;

namespace DTcms.Model
{
    /// <summary>
    /// OAuth应用
    /// </summary>
    [Serializable]
    public partial class oauth_app
    {
        public oauth_app()
        { }
        #region Model
        private int _id;
        private string _title = string.Empty;
        private string _img_url = string.Empty;
        private string _remark = string.Empty;
        private int _sort_id = 99;
        private int _is_lock = 0;
        private string _api_path = string.Empty;
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
        /// 显示图片
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
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
        /// 是否启用
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 接口目录
        /// </summary>
        public string api_path
        {
            set { _api_path = value; }
            get { return _api_path; }
        }
        #endregion
    }
}