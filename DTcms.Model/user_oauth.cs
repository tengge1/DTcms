using System;

namespace DTcms.Model
{
    /// <summary>
    /// OAuth授权用户信息
    /// </summary>
    [Serializable]
    public partial class user_oauth
    {
        public user_oauth()
        { }
        #region Model
        private int _id;
        private int _user_id = 0;
        private string _user_name = string.Empty;
        private string _oauth_name  = string.Empty;
        private string _oauth_access_token = string.Empty;
        private string _oauth_openid = string.Empty;
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
        /// 开放平台名称
        /// </summary>
        public string oauth_name
        {
            set { _oauth_name = value; }
            get { return _oauth_name; }
        }
        /// <summary>
        /// access_token
        /// </summary>
        public string oauth_access_token
        {
            set { _oauth_access_token = value; }
            get { return _oauth_access_token; }
        }
        /// <summary>
        /// 授权key
        /// </summary>
        public string oauth_openid
        {
            set { _oauth_openid = value; }
            get { return _oauth_openid; }
        }
        /// <summary>
        /// 授权时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        #endregion
    }
}