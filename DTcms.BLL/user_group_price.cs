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
    ///会员组价格
    /// </summary>
    public partial class user_group_price
    {
        private readonly Model.sysconfig sysConfig = new BLL.sysconfig().loadConfig();//获得系统配置信息
        private readonly DAL.user_group_price dal;

        public user_group_price()
        {
            dal = new DAL.user_group_price(sysConfig.sysdatabaseprefix);
        }

        #region 基本方法================================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_group_price GetModel(int group_id, int channel_id,int article_id)
        {
            return dal.GetModel(group_id, channel_id, article_id);
        }
        #endregion
    }
}