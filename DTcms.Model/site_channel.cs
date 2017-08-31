using System;
using System.Collections.Generic;

namespace DTcms.Model
{
    /// <summary>
    /// 系统频道表
    /// </summary>
    [Serializable]
    public partial class site_channel
    {
        public site_channel()
        { }
        #region Model
        private int _id;
        private int _site_id = 0;
        private string _name = string.Empty;
        private string _title = string.Empty;
        private int _is_comment = 0;
        private int _is_albums = 0;
        private int _is_attach = 0;
        private int _is_spec = 0;
        private int _is_contribute = 0;
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
        /// 站点ID
        /// </summary>
        public int site_id
        {
            set { _site_id = value; }
            get { return _site_id; }
        }
        /// <summary>
        /// 频道名称
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 频道标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 是否开启评论
        /// </summary>
        public int is_comment
        {
            set { _is_comment = value; }
            get { return _is_comment; }
        }
        /// <summary>
        /// 是否开启相册功能
        /// </summary>
        public int is_albums
        {
            set { _is_albums = value; }
            get { return _is_albums; }
        }
        /// <summary>
        /// 是否开启附件功能
        /// </summary>
        public int is_attach
        {
            set { _is_attach = value; }
            get { return _is_attach; }
        }
        /// <summary>
        /// 是否开启用户组价格
        /// </summary>
        public int is_spec
        {
            set { _is_spec = value; }
            get { return _is_spec; }
        }
        /// <summary>
        /// 是否允许投稿
        /// </summary>
        public int is_contribute
        {
            set { _is_contribute = value; }
            get { return _is_contribute; }
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
        /// 状态0启用1禁用
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }

        private List<site_channel_field> _channel_fields;
        /// <summary>
        /// 扩展字段 
        /// </summary>
        public List<site_channel_field> channel_fields
        {
            set { _channel_fields = value; }
            get { return _channel_fields; }
        }
        #endregion
    }
}