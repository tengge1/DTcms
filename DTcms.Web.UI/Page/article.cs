using System;
using System.Collections.Generic;
using System.Text;

namespace DTcms.Web.UI.Page
{
    public partial class article : Web.UI.BasePage
    {
        protected string channel = string.Empty; //频道名称

        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(article_Init);
        }

        /// <summary>
        /// OnInit事件,让频道名称变量先赋值
        /// </summary>
        void article_Init(object sender, EventArgs e)
        {
            //所有方法都写在这里
        }
    }
}
