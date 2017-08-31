using System;
using System.Collections.Generic;
using System.Text;

namespace DTcms.Model
{
    /// <summary>
    /// 会员配置信息
    /// </summary>
    [Serializable]
    public class userconfig
    {
        public userconfig()
        { }
        private int _regstatus = 0;
        private int _regverify = 0;
        private int _regmsgstatus = 0;
        private string _regmsgtxt = "";
        private string _regkeywords = "";
        private int _regctrl = 0;
        private int _regsmsexpired = 0;
        private int _regemailexpired = 0;
        private int _mobilelogin = 0;
        private int _emaillogin = 0;
        private int _regrules = 0;
        private string _regrulestxt = "";
        
        private int _invitecodeexpired = 0;
        private int _invitecodecount = 0;
        private int _invitecodenum = 10;
        private decimal _pointcashrate = 0;
        private int _pointinvitenum = 0;
        private int _pointloginnum = 0;

        /// <summary>
        /// 新用户注册设置0关闭注册,1开放注册,2手机注册,3邮件注册,4邀请注册
        /// </summary>
        public int regstatus
        {
            get { return _regstatus; }
            set { _regstatus = value; }
        }
        /// <summary>
        /// 新用户注册审核0不需要,1人工审核
        /// </summary>
        public int regverify
        {
            get { return _regverify; }
            set { _regverify = value; }
        }
        /// <summary>
        /// 注册欢迎短信息0不发送1站内短消息2发送邮件3手机短信
        /// </summary>
        public int regmsgstatus
        {
            get { return _regmsgstatus; }
            set { _regmsgstatus = value; }
        }
        /// <summary>
        /// 欢迎短信息内容
        /// </summary>
        public string regmsgtxt
        {
            get { return _regmsgtxt; }
            set { _regmsgtxt = value; }
        }
        /// <summary>
        /// 用户名保留关健字
        /// </summary>
        public string regkeywords
        {
            get { return _regkeywords; }
            set { _regkeywords = value; }
        }
        /// <summary>
        /// IP注册间隔限制0不限制(小时)
        /// </summary>
        public int regctrl
        {
            get { return _regctrl; }
            set { _regctrl = value; }
        }
        /// <summary>
        /// 手机验证码有效期(分钟)须大于0
        /// </summary>
        public int regsmsexpired
        {
            get { return _regsmsexpired; }
            set { _regsmsexpired = value; }
        }
        /// <summary>
        /// 邮件链接有效期(天)须大于0
        /// </summary>
        public int regemailexpired
        {
            get { return _regemailexpired; }
            set { _regemailexpired = value; }
        }
        /// <summary>
        /// 允许手机号登录0不允许1允许
        /// </summary>
        public int mobilelogin
        {
            get { return _mobilelogin; }
            set { _mobilelogin = value; }
        }
        /// <summary>
        /// 允许邮箱登录0不允许1允许
        /// </summary>
        public int emaillogin
        {
            get { return _emaillogin; }
            set { _emaillogin = value; }
        }
        /// <summary>
        /// 注册许可协议0否1是
        /// </summary>
        public int regrules
        {
            get { return _regrules; }
            set { _regrules = value; }
        }
        /// <summary>
        /// 许可协议内容
        /// </summary>
        public string regrulestxt
        {
            get { return _regrulestxt; }
            set { _regrulestxt = value; }
        }

        /// <summary>
        /// 邀请码使用期限(天)
        /// </summary>
        public int invitecodeexpired
        {
            get { return _invitecodeexpired; }
            set { _invitecodeexpired = value; }
        }
        /// <summary>
        /// 邀请码可使用次数0无限制
        /// </summary>
        public int invitecodecount
        {
            get { return _invitecodecount; }
            set { _invitecodecount = value; }
        }
        /// <summary>
        /// 每天可申请的邀请码数量0不限制
        /// </summary>
        public int invitecodenum
        {
            get { return _invitecodenum; }
            set { _invitecodenum = value; }
        }
        /// <summary>
        /// 现金/积分兑换比例0禁用
        /// </summary>
        public decimal pointcashrate
        {
            get { return _pointcashrate; }
            set { _pointcashrate = value; }
        }
        /// <summary>
        /// 邀请注册获得积分
        /// </summary>
        public int pointinvitenum
        {
            get { return _pointinvitenum; }
            set { _pointinvitenum = value; }
        }
        /// <summary>
        /// 每天登录获得积分
        /// </summary>
        public int pointloginnum
        {
            get { return _pointloginnum; }
            set { _pointloginnum = value; }
        }
        
    }
}
