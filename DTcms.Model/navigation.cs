using System;

namespace DTcms.Model
{
    /// <summary>
    /// 系统导航菜单
    /// </summary>
    [Serializable]
    public partial class navigation
    {
        public navigation()
        { }
        #region Model
        private int _id;
        private int _parent_id = 0;
        private int _channel_id = 0;
        private string _nav_type = string.Empty;
        private string _name = string.Empty;
        private string _title = string.Empty;
        private string _sub_title = string.Empty;
        private string _icon_url = string.Empty;
        private string _link_url = string.Empty;
        private int _sort_id = 99;
        private int _is_lock = 0;
        private string _remark = string.Empty;
        private string _action_type = string.Empty;
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
        /// 所属父导航ID
        /// </summary>
        public int parent_id
        {
            set { _parent_id = value; }
            get { return _parent_id; }
        }
        /// <summary>
        /// 所属频道ID
        /// </summary>
        public int channel_id
        {
            set { _channel_id = value; }
            get { return _channel_id; }
        }
        /// <summary>
        /// 导航类别
        /// </summary>
        public string nav_type
        {
            set { _nav_type = value; }
            get { return _nav_type; }
        }
        /// <summary>
        /// 导航ID
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
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
        /// 副标题
        /// </summary>
        public string sub_title
        {
            set { _sub_title = value; }
            get { return _sub_title; }
        }
        /// <summary>
        /// 图标地址
        /// </summary>
        public string icon_url
        {
            set { _icon_url = value; }
            get { return _icon_url; }
        }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string link_url
        {
            set { _link_url = value; }
            get { return _link_url; }
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
        /// 是否隐藏0显示1隐藏
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
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
        /// 权限资源
        /// </summary>
        public string action_type
        {
            set { _action_type = value; }
            get { return _action_type; }
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