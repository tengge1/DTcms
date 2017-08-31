using System;

namespace DTcms.Model
{
    /// <summary>
    /// 会员组别
    /// </summary>
    [Serializable]
    public partial class user_groups
    {
        public user_groups()
        { }
        #region Model
        private int _id;
        private string _title = string.Empty;
        private int _grade = 0;
        private int _upgrade_exp = 0;
        private decimal _amount = 0M;
        private int _point = 0;
        private int _discount;
        private int _is_default = 0;
        private int _is_upgrade = 1;
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
        /// 组别名称
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 会员等级值
        /// </summary>
        public int grade
        {
            set { _grade = value; }
            get { return _grade; }
        }
        /// <summary>
        /// 升级经验值
        /// </summary>
        public int upgrade_exp
        {
            set { _upgrade_exp = value; }
            get { return _upgrade_exp; }
        }
        /// <summary>
        /// 默认预存款
        /// </summary>
        public decimal amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 默认积分
        /// </summary>
        public int point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 购物折扣
        /// </summary>
        public int discount
        {
            set { _discount = value; }
            get { return _discount; }
        }
        /// <summary>
        /// 是否注册用户组
        /// </summary>
        public int is_default
        {
            set { _is_default = value; }
            get { return _is_default; }
        }
        /// <summary>
        /// 是否自动升级
        /// </summary>
        public int is_upgrade
        {
            set { _is_upgrade = value; }
            get { return _is_upgrade; }
        }
        /// <summary>
        /// 是否禁用
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }
        #endregion
    }
}