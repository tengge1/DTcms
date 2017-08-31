using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class cart : Web.UI.BasePage
    {
        protected List<Model.cart_items> goodsList = new List<Model.cart_items>();
        protected Model.cart_total goodsTotal = new Model.cart_total();

        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(cart_Init); //加入Init事件
        }

        /// <summary>
        /// 将在Init事件执行
        /// </summary>
        protected void cart_Init(object sender, EventArgs e)
        {
            int group_id = 0; //会员组ID
            Model.users userModel = GetUserInfo(); //会员信息
            if (userModel != null)
            {
                group_id = userModel.group_id; //如果是已登录则将会员组ID赋值
            }
            goodsList = ShopCart.GetList(group_id); //商品列表
            if (goodsList != null)
            {
                goodsTotal = ShopCart.GetTotal(goodsList); //商品统计
            }
            else
            {
                goodsList = new List<Model.cart_items>();
                goodsTotal = new Model.cart_total();
            }
        }
    }
}
