using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class shopping : Web.UI.BasePage
    {
        protected string goodsJsonValue = string.Empty;
        protected Model.users userModel;
        protected List<Model.cart_items> goodsList = new List<Model.cart_items>();
        protected Model.cart_total goodsTotal = new Model.cart_total();
        protected Model.orderconfig orderConfig = new BLL.orderconfig().loadConfig();

        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            goodsJsonValue = Utils.GetCookie(DTKeys.COOKIE_SHOPPING_BUY); //获取商品JSON数据
            this.Init += new EventHandler(shopping_Init); //加入Init事件
        }
        /// <summary>
        /// 将在Init事件执行
        /// </summary>
        protected void shopping_Init(object sender, EventArgs e)
        {
            int group_id = 0; //会员组ID
            userModel = GetUserInfo(); //获取会员信息
            if (userModel == null)
            {
                //如果不支持匿名购物则跳转到登录页面
                if (orderConfig.anonymous == 0)
                {
                    HttpContext.Current.Response.Redirect(linkurl("login")); //自动跳转URL
                }
            }
            else
            {
                group_id = userModel.group_id;
            }

            //获取商品列表
            if (string.IsNullOrEmpty(goodsJsonValue))
            {
                HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("对不起，无法获取您要购买的商品！")));
                return;
            }
            try
            {
                List<Model.cart_keys> ls = (List<Model.cart_keys>)JsonHelper.JSONToObject<List<Model.cart_keys>>(goodsJsonValue);
                goodsList = ShopCart.ToList(ls, group_id); //商品列表
                goodsTotal = ShopCart.GetTotal(goodsList); //商品统计
            }
            catch
            {
                HttpContext.Current.Response.Redirect(linkurl("error", "?msg=" + Utils.UrlEncode("对不起，商品的传输参数有误！")));
                return;
            }

        }
    }
}
