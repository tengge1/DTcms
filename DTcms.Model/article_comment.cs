using System;

namespace DTcms.Model
{
    /// <summary>
    /// 类别与规格关系表
    /// </summary>
    [Serializable]
    public partial class article_comment
    {
        public article_comment()
        { }
        #region Model
        private int _id;
        private int _site_id = 0;
        private int _channel_id = 0;
        private int _article_id = 0;
        private int _parent_id = 0;
        private int _user_id = 0;
        private string _user_name = string.Empty;
        private string _user_ip;
        private string _content;
        private int _is_lock = 0;
        private DateTime _add_time = DateTime.Now;
        private int _is_reply = 0;
        private string _reply_content = string.Empty;
        private DateTime? _reply_time;
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
        /// 主表ID
        /// </summary>
        public int article_id
        {
            set { _article_id = value; }
            get { return _article_id; }
        }
        /// <summary>
        /// 父评论ID
        /// </summary>
        public int parent_id
        {
            set { _parent_id = value; }
            get { return _parent_id; }
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
        /// 用户IP
        /// </summary>
        public string user_ip
        {
            set { _user_ip = value; }
            get { return _user_ip; }
        }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        /// <summary>
        /// 是否已答复
        /// </summary>
        public int is_reply
        {
            set { _is_reply = value; }
            get { return _is_reply; }
        }
        /// <summary>
        /// 答复内容
        /// </summary>
        public string reply_content
        {
            set { _reply_content = value; }
            get { return _reply_content; }
        }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? reply_time
        {
            set { _reply_time = value; }
            get { return _reply_time; }
        }
        #endregion Model
    }
}