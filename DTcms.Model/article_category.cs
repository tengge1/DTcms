using System;
using System.Collections.Generic;

namespace DTcms.Model
{
    /// <summary>
    /// 文章类别表
    /// </summary>
    [Serializable]
    public partial class article_category
    {
        public article_category()
        { }
        #region Model
        private int _id;
        private int _site_id = 0;
        private int _channel_id = 0;
        private string _title = string.Empty;
        private string _call_index = string.Empty;
        private int _parent_id = 0;
        private string _class_list = string.Empty;
        private int _class_layer = 0;
        private int _sort_id = 99;
        private string _link_url = string.Empty;
        private string _img_url = string.Empty;
        private string _content = string.Empty;
        private string _seo_title = string.Empty;
        private string _seo_keywords = string.Empty;
        private string _seo_description = string.Empty;
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
        /// 站点ID
        /// </summary>
        public int site_id
        {
            set { _site_id = value; }
            get { return _site_id; }
        }
        /// <summary>
        /// 频道ID
        /// </summary>
        public int channel_id
        {
            set { _channel_id = value; }
            get { return _channel_id; }
        }
        /// <summary>
        /// 类别标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 调用别名
        /// </summary>
        public string call_index
        {
            set { _call_index = value; }
            get { return _call_index; }
        }
        /// <summary>
        /// 父类别ID
        /// </summary>
        public int parent_id
        {
            set { _parent_id = value; }
            get { return _parent_id; }
        }
        /// <summary>
        /// 类别ID列表(逗号分隔开)
        /// </summary>
        public string class_list
        {
            set { _class_list = value; }
            get { return _class_list; }
        }
        /// <summary>
        /// 类别深度
        /// </summary>
        public int class_layer
        {
            set { _class_layer = value; }
            get { return _class_layer; }
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
        /// URL跳转地址
        /// </summary>
        public string link_url
        {
            set { _link_url = value; }
            get { return _link_url; }
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
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
        public string seo_keywords
        {
            set { _seo_keywords = value; }
            get { return _seo_keywords; }
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