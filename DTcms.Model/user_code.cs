using System;

namespace DTcms.Model
{
    /// <summary>
    /// 用户随机码表
    /// </summary>
    [Serializable]
    public partial class user_code
    {
        public user_code()
        { }
        #region Model
        private int _id;
        private int _user_id = 0;
        private string _user_name = string.Empty;
        private string _type = string.Empty;
        private string _str_code = string.Empty;
        private int _count = 0;
        private int _status = 0;
        private string _user_ip = string.Empty;
        private DateTime _eff_time;
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
        /// 生成码类别 password取回密码,register邀请注册
        /// </summary>
        public string type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 生成的字符串
        /// </summary>
        public string str_code
        {
            set { _str_code = value; }
            get { return _str_code; }
        }
        /// <summary>
        /// 使用次数
        /// </summary>
        public int count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 状态0未使用1已使用
        /// </summary>
        public int status
        {
            set { _status = value; }
            get { return _status; }
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
        /// 有效时间
        /// </summary>
        public DateTime eff_time
        {
            set { _eff_time = value; }
            get { return _eff_time; }
        }
        /// <summary>
        /// 生成时间
        /// </summary>
        public DateTime add_time
        {
            set { _add_time = value; }
            get { return _add_time; }
        }
        #endregion
    }
}