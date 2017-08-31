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
    ///OAuth应用
    /// </summary>
    public partial class oauth_app
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();//获得系统配置信息
        private readonly DAL.oauth_app dal;

        public oauth_app()
        {
            dal = new DAL.oauth_app(sysConfig.sysdatabaseprefix);
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
        public int Add(Model.oauth_app model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.oauth_app model)
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
        public Model.oauth_app GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.oauth_app GetModel(string api_path)
        {
            return dal.GetModel(api_path);
        }

        /// <summary>
        /// 获取站点未添加数据
        /// </summary>
        public DataSet GetList(int site_id, int oauth_id)
        {
            return dal.GetList(site_id, oauth_id);
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