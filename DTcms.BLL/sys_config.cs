using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using DTcms.Common;

namespace DTcms.BLL
{
    public partial class sysconfig
    {
        private readonly DAL.sysconfig dal = new DAL.sysconfig();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.sysconfig loadConfig()
        {
            Model.sysconfig model = CacheHelper.Get<Model.sysconfig>(DTKeys.CACHE_SYS_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(DTKeys.CACHE_SYS_CONFIG, dal.loadConfig(Utils.GetXmlMapPath(DTKeys.FILE_SYS_XML_CONFING)),
                    Utils.GetXmlMapPath(DTKeys.FILE_SYS_XML_CONFING));
                model = CacheHelper.Get<Model.sysconfig>(DTKeys.CACHE_SYS_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.sysconfig saveConifg(Model.sysconfig model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath(DTKeys.FILE_SYS_XML_CONFING));
        }

    }
}
