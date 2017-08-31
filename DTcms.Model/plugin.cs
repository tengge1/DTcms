using System;

namespace DTcms.Model
{
    /// <summary>
    /// 插件:实体类
    /// </summary>
    [Serializable]
    public partial class plugin
    {
        private string _directory;
        private string _name;
        private string _author;
        private string _version;
        private string _description;
        private int _isload;

        /// <summary>
        /// 插件目录
        /// </summary>
        public string directory
        {
            get { return _directory; }
            set { _directory = value; }
        }
        /// <summary>
        /// 插件名称
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
        /// 版本
        /// </summary>
        public string version
        {
            get { return _version; }
            set { _version = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
        /// <summary>
        /// 是否已加载
        /// </summary>
        public int isload
        {
            get { return _isload; }
            set { _isload = value; }
        }
    }
}