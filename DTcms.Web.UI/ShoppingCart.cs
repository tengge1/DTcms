using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI
{
    /// <summary>
    /// 购物车帮助类
    /// </summary>
    public partial class ShopCart
    {
        #region 基本增删改方法====================================
        /// <summary>
        /// 获得购物车列表
        /// </summary>
        public static List<Model.cart_items> GetList(int group_id)
        {
            List<Model.cart_keys> ls = GetCart(); //获取购物车商品
            return ToList(ls, group_id);
        }

        /// <summary>
        /// 添加到购物车
        /// </summary>
        public static bool Add(int channel_id, int article_id, int quantity)
        {
            List<Model.cart_keys> ls = GetCart();
            if (ls != null)
            {
                Model.cart_keys modelt = ls.Find(p => p.channel_id == channel_id && p.article_id == article_id);
                if (modelt != null)
                {
                    int i = ls.FindIndex(p => p.channel_id == channel_id && p.article_id == article_id);
                    modelt.quantity += quantity; //更新数量
                    ls[i] = modelt;
                    string jsonStr = JsonHelper.ObjectToJSON(ls); //转换为JSON字符串
                    AddCookies(jsonStr); //重新加入Cookies
                    return true;
                }
            }
            else
            {
                ls = new List<Model.cart_keys>();
            }
            //不存在的则新增
            ls.Add(new Model.cart_keys() { channel_id = channel_id, article_id = article_id, quantity = quantity });
            AddCookies( JsonHelper.ObjectToJSON(ls)); //添加至Cookies
            return true;
        }

        /// <summary>
        /// 更新购物车数量
        /// </summary>
        public static Model.cart_keys Update(int channel_id, int article_id, int quantity)
        {
            //如果数量小于1则移除该项
            if (quantity < 1)
            {
                return null;
            }
            List<Model.cart_keys> ls = GetCart();
            if (ls != null)
            {
                Model.cart_keys modelt = ls.Find(p => p.channel_id == channel_id && p.article_id == article_id);
                if (modelt != null)
                {
                    int i = ls.FindIndex(p => p.channel_id == channel_id && p.article_id == article_id);
                    modelt.quantity = quantity; //更新数量
                    ls[i] = modelt;
                    string jsonStr = JsonHelper.ObjectToJSON(ls); //转换为JSON字符串
                    AddCookies(jsonStr); //重新加入Cookies
                    return modelt;
                }
            }
            return null;
        }

        /// <summary>
        /// 清空购物车
        /// </summary>
        public static void Clear()
        {
            Utils.WriteCookie(DTKeys.COOKIE_SHOPPING_CART, "", -43200);
        }

        /// <summary>
        /// 移除购物车指定项
        /// </summary>
        public static void Clear(int channel_id, int article_id)
        {
            if (channel_id > 0 && article_id > 0)
            {
                List<Model.cart_keys> cartList = GetCart();
                if (cartList == null)
                {
                    return;
                }
                Model.cart_keys modelt = cartList.Find(p => p.channel_id == channel_id && p.article_id == article_id);
                if (modelt != null)
                {
                    cartList.Remove(modelt); //移除指定的项
                    string jsonStr = JsonHelper.ObjectToJSON(cartList);
                    AddCookies(jsonStr);
                }
            }
        }

        /// <summary>
        /// 移除购物车指定项
        /// </summary>
        public static void Clear(List<Model.cart_keys> ls)
        {
            if (ls != null)
            {
                List<Model.cart_keys> cartList = GetCart();
                if (cartList == null)
                {
                    return;
                }
                foreach (Model.cart_keys modelt in ls)
                {
                    Model.cart_keys model = cartList.Find(p => p.channel_id == modelt.channel_id && p.article_id == modelt.article_id);
                    if (model!=null)
                    {
                        cartList.Remove(model);
                    }
                }
                string jsonStr = JsonHelper.ObjectToJSON(cartList);
                AddCookies(jsonStr);
            }
        }
        #endregion

        #region 扩展方法==========================================
        /// <summary>
        /// 转换成List
        /// </summary>
        public static List<Model.cart_items> ToList(List<Model.cart_keys> ls, int group_id)
        {
            if (ls != null)
            {
                List<Model.cart_items> iList = new List<Model.cart_items>();
                foreach (Model.cart_keys item in ls)
                {
                    //查询文章的信息
                    Model.article articleModel = new BLL.article().GetModel(item.channel_id, item.article_id);
                    if (articleModel == null || !articleModel.fields.ContainsKey("sell_price"))
                    {
                        continue;
                    }
                    //开始赋值
                    Model.cart_items modelt = new Model.cart_items();
                    modelt.channel_id = articleModel.channel_id;
                    modelt.article_id = articleModel.id;
                    if (articleModel.fields.ContainsKey("goods_no"))
                    {
                        modelt.goods_no = articleModel.fields["goods_no"];
                    }
                    modelt.title = articleModel.title;
                    modelt.img_url = articleModel.img_url;
                    modelt.sell_price = Utils.StrToDecimal(articleModel.fields["sell_price"], 0);
                    modelt.user_price = Utils.StrToDecimal(articleModel.fields["sell_price"], 0);
                    if (articleModel.fields.ContainsKey("point"))
                    {
                        modelt.point = Utils.StrToInt(articleModel.fields["point"], 0);
                    }
                    if (articleModel.fields.ContainsKey("stock_quantity"))
                    {
                        modelt.stock_quantity = Utils.StrToInt(articleModel.fields["stock_quantity"], 0);
                    }
                    bool setStatus = false; //会员组价格赋值状态
                    if (group_id > 0 && articleModel.group_price != null){
                        Model.user_group_price userPriceModel = articleModel.group_price.Find(p => p.group_id == group_id);
                        if (userPriceModel != null)
                        {
                            setStatus = true; //已赋值
                            modelt.user_price = userPriceModel.price;
                        }
                    }
                    //如果未曾有会员组价格则使用折扣价格
                    if (group_id > 0 && !setStatus)
                    {
                        int discount = new BLL.user_groups().GetDiscount(group_id);
                        if (discount > 0)
                        {
                            modelt.user_price = modelt.sell_price * discount / 100;
                        }
                    }
                    modelt.quantity = item.quantity;
                    //添加入列表
                    iList.Add(modelt);
                }
                return iList;
            }
            return null;
        }

        /// <summary>
        /// 统计购物车
        /// </summary>
        public static Model.cart_total GetTotal(int group_id)
        {
            List<Model.cart_items> ls = GetList(group_id);
            return GetTotal(ls);
        }

        /// <summary>
        /// 统计购物车
        /// </summary>
        public static Model.cart_total GetTotal(List<Model.cart_items> ls)
        {
            Model.cart_total model = new Model.cart_total();
            if (ls != null)
            {
                foreach (Model.cart_items modelt in ls)
                {
                    model.total_num++;
                    model.total_quantity += modelt.quantity;
                    model.payable_amount += modelt.sell_price * modelt.quantity;
                    model.real_amount += modelt.user_price * modelt.quantity;
                    model.total_point += modelt.point * modelt.quantity;
                }
            }
            return model;
        }

        /// <summary>
        /// 只统计购物车数量
        /// </summary>
        /// <returns></returns>
        public static int GetQuantityCount()
        {
            string jsonStr = GetCookies(); //获取Cookies值
            int count = 0;
            if (!string.IsNullOrEmpty(jsonStr))
            {
                List<Model.cart_keys> ls = (List<Model.cart_keys>)JsonHelper.JSONToObject<List<Model.cart_keys>>(jsonStr);
                if (ls != null)
                {
                    foreach (Model.cart_keys modelt in ls)
                    {
                        bool isExists = new BLL.article().Exists(modelt.channel_id, modelt.article_id);
                        if (isExists)
                        {
                            count += modelt.quantity;
                        }
                    }
                }
            }
            return count;
        }
        #endregion

        #region 私有方法==========================================
        /// <summary>
        /// 获取cookies值
        /// </summary>
        private static List<Model.cart_keys> GetCart()
        {
            List<Model.cart_keys> ls = new List<Model.cart_keys>();
            string jsonStr = GetCookies(); //获取Cookies值
            if (!string.IsNullOrEmpty(jsonStr))
            {
                ls = (List<Model.cart_keys>)JsonHelper.JSONToObject<List<Model.cart_keys>>(jsonStr);
                return ls;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加对象到cookies
        /// </summary>
        /// <param name="strValue"></param>
        private static void AddCookies(string strValue)
        {
            Utils.WriteCookie(DTKeys.COOKIE_SHOPPING_CART, strValue, 43200); //存储一个月
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <returns></returns>
        private static string GetCookies()
        {
            return Utils.GetCookie(DTKeys.COOKIE_SHOPPING_CART);
        }

        #endregion
    }

}
