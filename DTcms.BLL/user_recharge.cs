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
    ///用户充值表
    /// </summary>
    public partial class user_recharge
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();//获得系统配置信息
        private readonly DAL.user_recharge dal;

        public user_recharge()
        {
            dal = new DAL.user_recharge(sysConfig.sysdatabaseprefix);
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
        public int Add(Model.user_recharge model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_recharge model)
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
        public Model.user_recharge GetModel(int id)
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
        public int Add(int user_id, string user_name, string recharge_no, int payment_id, decimal amount)
        {
            Model.user_recharge model = new Model.user_recharge();
            model.user_id = user_id;
            model.user_name = user_name;
            model.recharge_no = recharge_no;
            model.payment_id = payment_id;
            model.amount = amount;
            model.status = 0;
            model.add_time = DateTime.Now;
            return dal.Add(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id, string user_name)
        {
            return dal.Delete(id, user_name);
        }

        /// <summary>
        /// 根据单号得到一个对象实体
        /// </summary>
        public Model.user_recharge GetModel(string recharge_no)
        {
            return dal.GetModel(recharge_no);
        }

        /// <summary>
        /// 直接充值订单
        /// </summary>
        public bool Recharge(Model.user_recharge model)
        {
            return dal.Recharge(model);
        }

        /// <summary>
        /// 确认充值订单
        /// </summary>
        public bool Confirm(string recharge_no)
        {
            return dal.Confirm(recharge_no);
        }

        /// <summary>
        /// 根据充值单号获取支付方式ID
        /// </summary>
        public int GetPaymentId(string recharge_no)
        {
            return dal.GetPaymentId(recharge_no);
        }
        #endregion
    }
}