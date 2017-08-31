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
    ///附件表
    /// </summary>
    public partial class article_attach
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();//获得系统配置信息
        private readonly DAL.article_attach dal;

        public article_attach()
        {
            dal = new DAL.article_attach(sysConfig.sysdatabaseprefix);
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
        /// 得到一个对象实体
        /// </summary>
        public Model.article_attach GetModel(int id)
        {
            return dal.GetModel(id);
        }
        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 检查用户是否下载过该附件
        /// </summary>
        public bool ExistsLog(int attach_id, int user_id)
        {
            return dal.ExistsLog(attach_id, user_id);
        }

        /// <summary>
        /// 获取下载次数
        /// </summary>
        public int GetDownNum(int id)
        {
            return dal.GetDownNum(id);
        }

        /// <summary>
        /// 获取总下载次数
        /// </summary>
        public int GetCountNum(int channel_id, int article_id)
        {
            return dal.GetCountNum(channel_id, article_id);
        }

        /// <summary>
        /// 插入一条下载附件记录
        /// </summary>
        public int AddLog(int user_id, string user_name, int attach_id, string file_name)
        {
            Model.user_attach_log model = new Model.user_attach_log();
            model.user_id = user_id;
            model.user_name = user_name;
            model.attach_id = attach_id;
            model.file_name = file_name;
            model.add_time = DateTime.Now;
            return dal.AddLog(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        //删除更新的旧文件
        public void DeleteFile(int id, string filePath)
        {
            Model.article_attach model = GetModel(id);
            if (model != null && model.file_path != filePath)
            {
                FileHelper.DeleteFile(model.file_path);
            }
        }
        #endregion
    }
}