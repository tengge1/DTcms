using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DTcms.Common;

namespace DTcms.BLL
{
    public class url_rewrite
    {
        private readonly DAL.url_rewrite dal = new DAL.url_rewrite();

        #region 基本方法=================================
        /// <summary>
        /// 增加节点
        /// </summary>
        public bool Add(Model.url_rewrite model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        public bool Edit(Model.url_rewrite model)
        {
            return dal.Edit(model);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public bool Remove(string attrName, string attrValue)
        {
            return dal.Remove(attrName, attrValue);
        }

        /// <summary>
        /// 批量删除节点
        /// </summary>
        public bool Remove(XmlNodeList xnList)
        {
            return dal.Remove(xnList);
        }
        #endregion

        #region 扩展方法=================================

        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            foreach (Model.url_rewrite modelt in GetListAll())
            {
                if (modelt.name == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public Model.url_rewrite GetInfo(string attrValue)
        {
            return dal.GetInfo(attrValue);
        }

        /// <summary>
        /// 根据频道名称和类别返回一条URL映射
        /// </summary>
        public Model.url_rewrite GetInfo(string channel, string attrType)
        {
            foreach (Model.url_rewrite modelt in GetListAll())
            {
                if (channel != "" && channel != modelt.channel)
                {
                    continue;
                }
                if (attrType != "" && attrType != modelt.type)
                {
                    continue;
                }
                return modelt;
            }
            return null;
        }

        /// <summary>
        /// 返回URL映射列表
        /// </summary>
        public Hashtable GetList()
        {
            Hashtable ht = CacheHelper.Get<Hashtable>(DTKeys.CACHE_SITE_URLS);
            if (ht == null)
            {
                CacheHelper.Insert(DTKeys.CACHE_SITE_URLS, dal.GetList(), Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING));
                ht = CacheHelper.Get<Hashtable>(DTKeys.CACHE_SITE_URLS);
            }
            return ht;
        }

        /// <summary>
        /// 返回URL映射List列表
        /// </summary>
        public List<Model.url_rewrite> GetListAll()
        {
            List<Model.url_rewrite> ls = CacheHelper.Get<List<Model.url_rewrite>>(DTKeys.CACHE_SITE_URLS_LIST);
            if (ls == null)
            {
                CacheHelper.Insert(DTKeys.CACHE_SITE_URLS_LIST, dal.GetList(""), Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING));
                ls = CacheHelper.Get<List<Model.url_rewrite>>(DTKeys.CACHE_SITE_URLS_LIST);
            }
            return ls;
        }

        /// <summary>
        /// 根据频道名称返回URL映射列表
        /// </summary>
        public List<Model.url_rewrite> GetList(string channel)
        {
            List<Model.url_rewrite> ls = GetListAll();
            if (channel == "")
            {
                return ls;
            }
            List<Model.url_rewrite> nls = new List<Model.url_rewrite>();
            foreach (Model.url_rewrite modelt in ls)
            {
                if (modelt.channel == channel)
                {
                    nls.Add(modelt);
                }
            }
            return nls;
        }

        /// <summary>
        /// 根据频道名称和类别返回URL映射列表
        /// </summary>
        public List<Model.url_rewrite> GetList(string channel, string attrType)
        {
            List<Model.url_rewrite> nls = new List<Model.url_rewrite>();
            foreach (Model.url_rewrite modelt in GetListAll())
            {
                if (channel != "" && channel != modelt.channel)
                {
                    continue;
                }
                if (attrType != "" && attrType != modelt.type)
                {
                    continue;
                }
                nls.Add(modelt);
            }
            return nls;
        }

        #endregion
    }
}
