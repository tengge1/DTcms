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
    ///扩展字段表
    /// </summary>
    public partial class article_attribute_field
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();//获得系统配置信息
        private readonly DAL.article_attribute_field dal;

        public article_attribute_field()
        {
            dal = new DAL.article_attribute_field(sysConfig.sysdatabaseprefix);
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
        public int Add(Model.article_attribute_field model)
        {
            switch (model.control_type)
            {
                case "single-text": //单行文本
                    if (model.data_length > 0 && model.data_length <= 4000)
                    {
                        model.data_type = "nvarchar(" + model.data_length + ")";
                    }
                    else if (model.data_length > 4000)
                    {
                        model.data_type = "ntext";
                    }
                    else
                    {
                        model.data_length = 50;
                        model.data_type = "nvarchar(50)";
                    }
                    break;
                case "multi-text": //多行文本
                    goto case "single-text";
                case "editor": //编辑器
                    model.data_type = "ntext";
                    break;
                case "images": //图片
                    model.data_type = "nvarchar(255)";
                    break;
                case "video": //视频
                    model.data_type = "nvarchar(255)";
                    break;
                case "number": //数字
                    if (model.data_place > 0)
                    {
                        model.data_type = "decimal(9," + model.data_place + ")";
                    }
                    else
                    {
                        model.data_type = "int";
                    }
                    break;
                case "datetime": //时间日期
                    model.data_type = "datetime";
                    break;
                case "checkbox": //复选框
                    model.data_type = "tinyint";
                    break;
                case "multi-radio": //多项单选
                    if (model.data_type == "int")
                    {
                        model.data_length = 4;
                        model.data_type = "int";
                    }
                    else
                    {
                        if (model.data_length > 0 && model.data_length <= 4000)
                        {
                            model.data_type = "nvarchar(" + model.data_length + ")";
                        }
                        else if (model.data_length > 4000)
                        {
                            model.data_type = "ntext";
                        }
                        else
                        {
                            model.data_length = 50;
                            model.data_type = "nvarchar(50)";
                        }
                    }

                    break;
                case "multi-checkbox": //多项多选
                    goto case "single-text";
            }
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_attribute_field model)
        {
            switch (model.control_type)
            {
                case "single-text": //单行文本
                    if (model.data_length > 0 && model.data_length <= 4000)
                    {
                        model.data_type = "nvarchar(" + model.data_length + ")";
                    }
                    else if (model.data_length > 4000)
                    {
                        model.data_type = "ntext";
                    }
                    else
                    {
                        model.data_length = 50;
                        model.data_type = "nvarchar(50)";
                    }
                    break;
                case "multi-text": //多行文本
                    goto case "single-text";
                case "editor": //编辑器
                    model.data_type = "ntext";
                    break;
                case "images": //图片
                    model.data_type = "nvarchar(255)";
                    break;
                case "video": //视频
                    model.data_type = "nvarchar(255)";
                    break;
                case "number": //数字
                    if (model.data_place > 0)
                    {
                        model.data_type = "decimal(9," + model.data_place + ")";
                    }
                    else
                    {
                        model.data_type = "int";
                    }
                    break;
                case "datetime": //时间日期
                    model.data_type = "datetime";
                    break;
                case "checkbox": //复选框
                    model.data_type = "tinyint";
                    break;
                case "multi-radio": //多项单选
                    if (model.data_type == "int")
                    {
                        model.data_length = 4;
                        model.data_type = "int";
                    }
                    else
                    {
                        if (model.data_length > 0 && model.data_length <= 4000)
                        {
                            model.data_type = "nvarchar(" + model.data_length + ")";
                        }
                        else if (model.data_length > 4000)
                        {
                            model.data_type = "ntext";
                        }
                        else
                        {
                            model.data_length = 50;
                            model.data_type = "nvarchar(50)";
                        }
                    }

                    break;
                case "multi-checkbox": //多项多选
                    goto case "single-text";
            }
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
        public Model.article_attribute_field GetModel(int id)
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
        /// 查询是否存在列
        /// </summary>
        public bool Exists(string column_name)
        {
            return dal.Exists(column_name);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 获得频道对应的数据
        /// </summary>
        public DataSet GetList(int channel_id, string strWhere)
        {
            return dal.GetList(channel_id, strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_attribute_field> GetModelList(int channel_id, string strWhere)
        {
            DataSet ds = dal.GetList(channel_id, strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// DataTable转List
        /// </summary>
        public List<Model.article_attribute_field> DataTableToList(DataTable dt)
        {
            List<Model.article_attribute_field> modelList = new List<Model.article_attribute_field>();
            //datatable没有数据 返回null
            if (dt.Rows.Count > 0)
            {
                //datatable可能有很多行数据，遍历每一行
                foreach (DataRow dr in dt.Rows)
                {
                    Model.article_attribute_field model = new Model.article_attribute_field();
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        PropertyInfo propertyInfo = model.GetType().GetProperty(dr.Table.Columns[i].ColumnName);
                        if (propertyInfo != null && dr[i] != DBNull.Value)
                        {
                            propertyInfo.SetValue(model, dr[i], null);//用索引值设置属性值
                        }
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion
    }
}