using System;

namespace DTcms.Model
{
    /// <summary>
    /// 手机短信模板
    /// </summary>
    [Serializable]
    public partial class sms_template
    {
        public sms_template()
        { }
        #region Model
        private int _id;
        private string _title = string.Empty;
        private string _call_index = string.Empty;
        private string _content = string.Empty;
        private int _is_sys = 0;
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
        /// 调用别名
        /// </summary>
        public string call_index
        {
            set { _call_index = value; }
            get { return _call_index; }
        }
        /// <summary>
        /// 短信内容
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// 系统默认
        /// </summary>
        public int is_sys
        {
            set { _is_sys = value; }
            get { return _is_sys; }
        }
        #endregion
    }
}