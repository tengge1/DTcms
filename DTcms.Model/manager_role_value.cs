using System;

namespace DTcms.Model
{
    /// <summary>
    /// 管理角色权限表
    /// </summary>
    [Serializable]
    public partial class manager_role_value
    {
        public manager_role_value()
        { }
        #region Model
        private int _id;
        private int _role_id = 0;
        private string _nav_name = string.Empty;
        private string _action_type = string.Empty;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int role_id
        {
            set { _role_id = value; }
            get { return _role_id; }
        }
        /// <summary>
        /// 导航名称
        /// </summary>
        public string nav_name
        {
            set { _nav_name = value; }
            get { return _nav_name; }
        }
        /// <summary>
        /// 权限类型
        /// </summary>
        public string action_type
        {
            set { _action_type = value; }
            get { return _action_type; }
        }
        #endregion
    }
}