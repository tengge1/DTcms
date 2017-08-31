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
    ///用户下载记录
    /// </summary>
    public partial class user_attach_log
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();//获得系统配置信息
        private readonly DAL.user_attach_log dal;

        public user_attach_log()
        {
            dal = new DAL.user_attach_log(sysConfig.sysdatabaseprefix);
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
        public int Add(Model.user_attach_log model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_attach_log model)
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
        public Model.user_attach_log GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
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
        /// 获得数据列表
        /// </summary>
        public List<Model.user_attach_log> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// DataTable转List
        /// </summary>
        public List<Model.user_attach_log> DataTableToList(DataTable dt)
        {
            //datatable没有数据 返回null
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }
            //model的容器
            List<Model.user_attach_log> modelList = new List<Model.user_attach_log>();
            //datatable可能有很多行数据，遍历每一行
            foreach (DataRow dr in dt.Rows)
            {
                Model.user_attach_log model = new Model.user_attach_log();
                for (int i = 0; i < dr.Table.Columns.Count; i++)
                {
                    //发现datatable的列属性
                    PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                    if (propertyInfo != null && dr[i] != DBNull.Value)
                        //用索引值设置属性值
                        propertyInfo.SetValue(model, dr[i], null);
                }
                modelList.Add(model);
            }
            return modelList;
        }
        #endregion
    }
}