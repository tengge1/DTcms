using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using DTcms.Common;

namespace DTcms.DAL
{
    public class url_rewrite
    {
        #region 基本方法==============================
        /// <summary>
        /// 增加节点
        /// </summary>
        public bool Add(Model.url_rewrite model)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                XmlElement xe = doc.CreateElement("rewrite");
                if (!string.IsNullOrEmpty(model.name))
                    xe.SetAttribute("name", model.name);
                if (!string.IsNullOrEmpty(model.type))
                    xe.SetAttribute("type", model.type);
                if (!string.IsNullOrEmpty(model.page))
                    xe.SetAttribute("page", model.page);
                if (!string.IsNullOrEmpty(model.inherit))
                    xe.SetAttribute("inherit", model.inherit);
                if (!string.IsNullOrEmpty(model.templet))
                    xe.SetAttribute("templet", model.templet);
                if (!string.IsNullOrEmpty(model.channel))
                    xe.SetAttribute("channel", model.channel);
                if (!string.IsNullOrEmpty(model.pagesize))
                    xe.SetAttribute("pagesize", model.pagesize);
                XmlNode newXn = xn.AppendChild(xe);

                //创建子节点
                foreach (Model.url_rewrite_item modelt in model.url_rewrite_items)
                {
                    XmlElement xeItem = doc.CreateElement("item");
                    if (!string.IsNullOrEmpty(modelt.path))
                        xeItem.SetAttribute("path", modelt.path);
                    if (!string.IsNullOrEmpty(modelt.pattern))
                        xeItem.SetAttribute("pattern", modelt.pattern);
                    if (!string.IsNullOrEmpty(modelt.querystring))
                        xeItem.SetAttribute("querystring", modelt.querystring);
                    newXn.AppendChild(xeItem);
                }

                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        public bool Edit(Model.url_rewrite model)
        {
            string filePath = Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.Attributes["name"].Value.ToLower() == model.name.ToLower())
                    {
                        if (!string.IsNullOrEmpty(model.type))
                            xe.SetAttribute("type", model.type);
                        else if (xe.Attributes["type"] != null)
                            xe.Attributes["type"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.page))
                            xe.SetAttribute("page", model.page);
                        else if (xe.Attributes["page"] != null)
                            xe.Attributes["page"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.inherit))
                            xe.SetAttribute("inherit", model.inherit);
                        else if (xe.Attributes["inherit"] != null)
                            xe.Attributes["inherit"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.templet))
                            xe.SetAttribute("templet", model.templet);
                        else if (xe.Attributes["templet"] != null)
                            xe.Attributes["templet"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.channel))
                            xe.SetAttribute("channel", model.channel);
                        else if (xe.Attributes["channel"] != null)
                            xe.Attributes["channel"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.pagesize))
                            xe.SetAttribute("pagesize", model.pagesize);
                        else if (xe.Attributes["pagesize"] != null)
                            xe.Attributes["pagesize"].RemoveAll();

                        //移除所有的子节点重新添加
                        XmlNodeList itemXnList = xe.ChildNodes;
                        foreach (XmlElement itemXe in itemXnList)
                        {
                            for (int i = itemXnList.Count - 1; i >= 0; i--)
                            {
                                XmlElement xe2 = (XmlElement)itemXnList.Item(i);
                                xe.RemoveChild(xe2);
                            }
                        }
                        //创建子节点
                        foreach (Model.url_rewrite_item modelt in model.url_rewrite_items)
                        {
                            XmlElement xeItem = doc.CreateElement("item");
                            if (!string.IsNullOrEmpty(modelt.path))
                                xeItem.SetAttribute("path", modelt.path);
                            if (!string.IsNullOrEmpty(modelt.pattern))
                                xeItem.SetAttribute("pattern", modelt.pattern);
                            if (!string.IsNullOrEmpty(modelt.querystring))
                                xeItem.SetAttribute("querystring", modelt.querystring);
                            xe.AppendChild(xeItem);
                        }

                        doc.Save(filePath);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public bool Remove(string attrName, string attrValue)
        {
            string filePath = Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                for (int i = xnList.Count - 1; i >= 0; i--)
                {
                    XmlElement xe = (XmlElement)xnList.Item(i);
                    if (xe.Attributes[attrName] != null && xe.Attributes[attrName].Value.ToLower() == attrValue.ToLower())
                    {
                        xn.RemoveChild(xe);
                    }
                }
                doc.Save(filePath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量删除节点
        /// </summary>
        public bool Remove(XmlNodeList xnList)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                foreach (XmlElement xe in xnList)
                {
                    for (int i = xn.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        XmlElement xe2 = (XmlElement)xn.ChildNodes.Item(i);
                        if (xe2.Attributes["name"].Value.ToLower() == xe.Attributes["name"].Value.ToLower())
                        {
                            xn.RemoveChild(xe2);
                        }
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 导入节点
        /// </summary>
        public bool Import(XmlNodeList xnList)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                foreach (XmlElement xe in xnList)
                {
                    if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                    {
                        if (xe.Attributes["name"] != null)
                        {
                            XmlNode n = doc.ImportNode(xe, true);
                            xn.AppendChild(n);
                        }
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 扩展方法==============================
        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public Model.url_rewrite GetInfo(string attrValue)
        {
            Model.url_rewrite model = new Model.url_rewrite();
            string filePath = Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.Attributes["name"].Value.ToLower() == attrValue.ToLower())
                    {
                        if (xe.Attributes["name"] != null)
                            model.name = xe.Attributes["name"].Value;
                        if (xe.Attributes["type"] != null)
                            model.type = xe.Attributes["type"].Value;
                        if (xe.Attributes["page"] != null)
                            model.page = xe.Attributes["page"].Value;
                        if (xe.Attributes["inherit"] != null)
                            model.inherit = xe.Attributes["inherit"].Value;
                        if (xe.Attributes["templet"] != null)
                            model.templet = xe.Attributes["templet"].Value;
                        if (xe.Attributes["channel"] != null)
                            model.channel = xe.Attributes["channel"].Value;
                        if (xe.Attributes["pagesize"] != null)
                            model.pagesize = xe.Attributes["pagesize"].Value;
                        //再次遍历子节点
                        List<Model.url_rewrite_item> lsItems = new List<Model.url_rewrite_item>();
                        foreach (XmlElement xe1 in xe.ChildNodes)
                        {
                            if (xe1.NodeType != XmlNodeType.Comment && xe1.Name.ToLower() == "item")
                            {
                                Model.url_rewrite_item item = new Model.url_rewrite_item();
                                if (xe1.Attributes["path"] != null)
                                    item.path = xe1.Attributes["path"].Value;
                                if (xe1.Attributes["pattern"] != null)
                                    item.pattern = xe1.Attributes["pattern"].Value;
                                if (xe1.Attributes["querystring"] != null)
                                    item.querystring = xe1.Attributes["querystring"].Value;
                                lsItems.Add(item);
                            }
                        }
                        model.url_rewrite_items = lsItems;
                        return model;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 取得URL配制列表
        /// </summary>
        public Hashtable GetList()
        {
            Hashtable ht = new Hashtable();
            List<Model.url_rewrite> ls = GetList("");
            foreach (Model.url_rewrite model in ls)
            {
                if (!ht.Contains(model.name))
                {
                    foreach (Model.url_rewrite_item item in model.url_rewrite_items)
                    {
                        item.querystring = item.querystring.Replace("^", "&");
                    }
                    ht.Add(model.name, model);
                }
            }
            return ht;
        }

        /// <summary>
        /// 取得URL配制列表
        /// </summary>
        public List<Model.url_rewrite> GetList(string channel)
        {
            List<Model.url_rewrite> ls = new List<Model.url_rewrite>();
            string filePath = Utils.GetXmlMapPath(DTKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            foreach (XmlElement xe in xn.ChildNodes)
            {
                if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                {
                    if (xe.Attributes["name"] != null)
                    {
                        if (!string.IsNullOrEmpty(channel))
                        {
                            if (xe.Attributes["channel"] != null && channel.ToLower() == xe.Attributes["channel"].Value.ToLower())
                            {
                                Model.url_rewrite model = new Model.url_rewrite();
                                if (xe.Attributes["name"] != null)
                                    model.name = xe.Attributes["name"].Value;
                                if (xe.Attributes["type"] != null)
                                    model.type = xe.Attributes["type"].Value;
                                if (xe.Attributes["page"] != null)
                                    model.page = xe.Attributes["page"].Value;
                                if (xe.Attributes["inherit"] != null)
                                    model.inherit = xe.Attributes["inherit"].Value;
                                if (xe.Attributes["templet"] != null)
                                    model.templet = xe.Attributes["templet"].Value;
                                if (xe.Attributes["channel"] != null)
                                    model.channel = xe.Attributes["channel"].Value;
                                if (xe.Attributes["pagesize"] != null)
                                    model.pagesize = xe.Attributes["pagesize"].Value;
                                //再次遍历子节点
                                StringBuilder urlRewriteString = new StringBuilder();
                                List<Model.url_rewrite_item> lsItems = new List<Model.url_rewrite_item>();
                                foreach (XmlElement xe1 in xe.ChildNodes)
                                {
                                    if (xe1.NodeType != XmlNodeType.Comment && xe1.Name.ToLower() == "item")
                                    {
                                        Model.url_rewrite_item item = new Model.url_rewrite_item();
                                        if (xe1.Attributes["path"] != null)
                                            item.path = xe1.Attributes["path"].Value;
                                        if (xe1.Attributes["pattern"] != null)
                                            item.pattern = xe1.Attributes["pattern"].Value;
                                        if (xe1.Attributes["querystring"] != null)
                                            item.querystring = xe1.Attributes["querystring"].Value;
                                        urlRewriteString.Append(item.path + "," + item.pattern + "," + item.querystring + "&"); //组合成字符串
                                        lsItems.Add(item);
                                    }
                                }
                                model.url_rewrite_str = Utils.DelLastChar(urlRewriteString.ToString(), "&");
                                model.url_rewrite_items = lsItems;
                                ls.Add(model);
                            }
                        }
                        else
                        {
                            Model.url_rewrite model = new Model.url_rewrite();
                            if (xe.Attributes["name"] != null)
                                model.name = xe.Attributes["name"].Value;
                            if (xe.Attributes["type"] != null)
                                model.type = xe.Attributes["type"].Value;
                            if (xe.Attributes["page"] != null)
                                model.page = xe.Attributes["page"].Value;
                            if (xe.Attributes["inherit"] != null)
                                model.inherit = xe.Attributes["inherit"].Value;
                            if (xe.Attributes["templet"] != null)
                                model.templet = xe.Attributes["templet"].Value;
                            if (xe.Attributes["channel"] != null)
                                model.channel = xe.Attributes["channel"].Value;
                            if (xe.Attributes["pagesize"] != null)
                                model.pagesize = xe.Attributes["pagesize"].Value;
                            //再次遍历子节点
                            StringBuilder urlRewriteString = new StringBuilder();
                            List<Model.url_rewrite_item> lsItems = new List<Model.url_rewrite_item>();
                            foreach (XmlElement xe1 in xe.ChildNodes)
                            {
                                if (xe1.NodeType != XmlNodeType.Comment && xe1.Name.ToLower() == "item")
                                {
                                    Model.url_rewrite_item item = new Model.url_rewrite_item();
                                    if (xe1.Attributes["path"] != null)
                                        item.path = xe1.Attributes["path"].Value;
                                    if (xe1.Attributes["pattern"] != null)
                                        item.pattern = xe1.Attributes["pattern"].Value;
                                    if (xe1.Attributes["querystring"] != null)
                                        item.querystring = xe1.Attributes["querystring"].Value;
                                    urlRewriteString.Append(item.path + "," + item.pattern + "," + item.querystring + "&"); //组合成字符串
                                    lsItems.Add(item);
                                }
                            }
                            model.url_rewrite_str = Utils.DelLastChar(urlRewriteString.ToString(), "&");
                            model.url_rewrite_items = lsItems;
                            ls.Add(model);
                        }
                    }
                }
            }
            return ls;
        }

        #endregion

    }
}
