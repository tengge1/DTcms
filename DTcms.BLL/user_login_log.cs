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
    ///会员登录日志表
    /// </summary>
    public partial class user_login_log
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();//获得系统配置信息
        private readonly DAL.user_login_log dal;

        public user_login_log()
        {
            dal = new DAL.user_login_log(sysConfig.sysdatabaseprefix);
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
        public int Add(Model.user_login_log model)
        {
            return dal.Add(model);
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
        public Model.user_login_log GetModel(int id)
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
        /// 增加一条数据
        /// </summary>
        public int Add(int user_id, string user_name, string remark)
        {
            Model.user_login_log model = new Model.user_login_log();
            model.user_id = user_id;
            model.user_name = user_name;
            model.remark = remark;
            model.login_ip = DTRequest.GetIP();
            model.login_time = DateTime.Now;
            return dal.Add(model);
        }

        /// <summary>
        /// 同一天内是否有登录过
        /// </summary>
        public bool ExistsDay(string username)
        {
            return dal.ExistsDay(username);
        }

        /// <summary>
        /// 根据用户名取得最后登录的MODEL
        /// </summary>
        public Model.user_login_log GetLastModel(string user_name)
        {
            return dal.GetLastModel(user_name);
        }
        #endregion
    }
}