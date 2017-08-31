using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    ///文章类别表
    /// </summary>
    public partial class article_category
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig(); //获得系统配置信息
        private readonly DAL.article_category dal;

        public article_category()
        {
            dal = new DAL.article_category(sysConfig.sysdatabaseprefix);
        }

        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_category model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_category model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            return dal.Delete(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_category GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id, int channel_id)
        {
            return dal.GetList(parent_id, channel_id);
        }

        /// <summary>
        /// 取得该频道下所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_name">频道名称</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id, string channel_name)
        {
            int channel_id = new BLL.site_channel().GetChannelId(channel_name);
            return dal.GetList(parent_id, channel_id);
        }
        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 取得指定类别下的列表(一层)
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns>DataTable</returns>
        public DataTable GetChildList(int top, int parent_id, int channel_id)
        {
            return dal.GetChildList(top, parent_id, channel_id);
        }

        /// <summary>
        /// 取得该频道指定类别下的列表
        /// </summary>
        /// <param name="parent_id"></param>
        /// <param name="channel_name"></param>
        /// <returns></returns>
        public DataTable GetChildList(int top, int parent_id, string channel_name)
        {
            int channel_id = new BLL.site_channel().GetChannelId(channel_name);
            return dal.GetChildList(top, parent_id, channel_id);
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// 返回父节点的ID
        /// </summary>
        public int GetParentId(int id)
        {
            return dal.GetParentId(id);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }
        #endregion
    }
}