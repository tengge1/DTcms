using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;

namespace DTcms.DAL
{
    /// <summary>
    /// 数据访问类:系统配置
    /// </summary>
    public partial class sysconfig
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.sysconfig loadConfig(string configFilePath)
        {
            return (Model.sysconfig)SerializationHelper.Load(typeof(Model.sysconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.sysconfig saveConifg(Model.sysconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

    }
}
