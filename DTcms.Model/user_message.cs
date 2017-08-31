using System;

namespace DTcms.Model
{
    /// <summary>
    /// 会员短消息
    /// </summary>
    [Serializable]
    public partial class user_message
    {
        public user_message()
        { }
        #region Model
        private int _id;
        private int _type = 1;
        private string _post_user_name = string.Empty;
        private string _accept_user_name = string.Empty;
        private int _is_read = 0;
        private string _title = string.Empty;
        private string _content = string.Empty;
        private DateTime _post_time = DateTime.Now;
        private DateTime? _read_time;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 短信息类型0系统消息1收件箱2发件箱
        /// </summary>
        public int type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 发件人
        /// </summary>
        public string post_user_name
        {
            set { _post_user_name = value; }
            get { return _post_user_name; }
        }
        /// <summary>
        /// 收件人
        /// </summary>
        public string accept_user_name
        {
            set { _accept_user_name = value; }
            get { return _accept_user_name; }
        }
        /// <summary>
        /// 是否查看0未阅1已阅
        /// </summary>
        public int is_read
        {
            set { _is_read = value; }
            get { return _is_read; }
        }
        /// <summary>
        /// 短信息标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 短信息内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime post_time
        {
            set { _post_time = value; }
            get { return _post_time; }
        }
        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime? read_time
        {
            set { _read_time = value; }
            get { return _read_time; }
        }
        #endregion
    }
}