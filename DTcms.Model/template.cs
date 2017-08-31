using System;
using System.Collections.Generic;

namespace DTcms.Model
{
    /// <summary>
    /// 模板实体类
    /// </summary>
    [Serializable]
    public class template
    {
        private string _name = string.Empty;
        private string _author = string.Empty;
        private string _createdate = string.Empty;
        private string _version = string.Empty;
        private string _fordntver = string.Empty;

        /// <summary>
        /// 模板名称
        /// </summary>
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 作者
        /// </summary>
        public string author
        {
            get { return _author; }
            set { _author = value; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public string createdate
        {
            get { return _createdate; }
            set { _createdate = value; }
        }
        /// <summary>
        /// 模板版本
        /// </summary>
        public string version
        {
            get { return _version; }
            set { _version = value; }
        }
        /// <summary>
        /// 模板适用的版本
        /// </summary>
        public string fordntver
        {
            get { return _fordntver; }
            set { _fordntver = value; }
        }
    }
}
