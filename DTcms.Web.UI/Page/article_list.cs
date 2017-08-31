using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class article_list : Web.UI.BasePage
    {
        protected string channel = string.Empty; //频道名称
        protected int page;         //当前页码
        protected int category_id;  //类别ID
        protected int totalcount;   //OUT数据总数
        protected string pagelist;  //分页页码

        protected Model.article_category model = new Model.article_category(); //分类的实体

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(article_list_Init);
        }

        /// <summary>
        /// OnInit事件,让频道名称变量先赋值
        /// </summary>
        void article_list_Init(object sender, EventArgs e)
        {
            page = DTRequest.GetQueryInt("page", 1);
            category_id = DTRequest.GetQueryInt("category_id");
            BLL.article_category bll = new BLL.article_category();
            model.title = "所有类别";
            if (category_id > 0) //如果ID获取到，将使用ID
            {
                if (bll.Exists(category_id))
                {
                    model = bll.GetModel(category_id);
                    if (!string.IsNullOrEmpty(model.link_url))
                    {
                        HttpContext.Current.Response.Redirect(model.link_url);
                    }
                }
            }
        }

    }
}
