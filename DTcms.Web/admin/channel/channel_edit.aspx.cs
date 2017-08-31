using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.channel
{
    public partial class channel_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString();//操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.site_channel().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.View.ToString()); //检查权限
                SiteBind(); //绑定站点
                FieldBind(); //绑定扩展字段
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=channel_name_validate");
                }
            }
        }

        #region 返回页面的类型===========================
        protected string GetPageTypeTxt(string type_name)
        {
            string result = "";
            switch (type_name)
            {
                case "index":
                    result = "首页";
                    break;
                case "category":
                    result = "栏目页";
                    break;
                case "list":
                    result = "列表页";
                    break;
                case "detail":
                    result = "详情页";
                    break;
            }
            return result;
        }
        #endregion

        #region 返回页面继承类===========================
        private string GetInherit(string page_type)
        {
            string result = "";
            switch (page_type)
            {
                case "index":
                    result = "DTcms.Web.UI.Page.article";
                    break;
                case "category":
                    result = "DTcms.Web.UI.Page.category";
                    break;
                case "list":
                    result = "DTcms.Web.UI.Page.article_list";
                    break;
                case "detail":
                    result = "DTcms.Web.UI.Page.article_show";
                    break;
            }
            return result;
        }
        #endregion

        #region 绑定站点=================================
        private void SiteBind()
        {
            BLL.sites bll = new BLL.sites();
            DataTable dt = bll.GetList(0, "is_lock=0", "sort_id asc,id desc").Tables[0];

            this.ddlSiteId.Items.Clear();
            this.ddlSiteId.Items.Add(new ListItem("请选择站点...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                this.ddlSiteId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 绑定扩展字段=============================
        private void FieldBind()
        {
            BLL.article_attribute_field bll = new BLL.article_attribute_field();
            DataTable dt = bll.GetList(0, "", "is_sys desc,sort_id asc,id desc").Tables[0];

            this.cblAttributeField.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                this.cblAttributeField.Items.Add(new ListItem(dr["title"].ToString(), dr["name"].ToString() + "," + dr["id"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.site_channel bll = new BLL.site_channel();
            Model.site_channel model = bll.GetModel(_id);

            txtTitle.Text = model.title;
            txtName.Text = model.name;
            txtName.Focus(); //设置焦点，防止JS无法提交
            txtName.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=channel_name_validate&old_channel_name=" + Utils.UrlEncode(model.name));
            ddlSiteId.SelectedValue = model.site_id.ToString();
            if (model.is_lock == 1)
            {
                cbIsLock.Checked = false;
            }
            if (model.is_comment == 1)
            {
                cbIsComment.Checked = true;
            }
            if (model.is_albums == 1)
            {
                cbIsAlbums.Checked = true;
            }
            if (model.is_attach == 1)
            {
                cbIsAttach.Checked = true;
            }
            if (model.is_spec == 1)
            {
                cbIsSpec.Checked = true;
            }
            txtSortId.Text = model.sort_id.ToString();

            //赋值扩展字段
            if (model.channel_fields != null)
            {
                for (int i = 0; i < cblAttributeField.Items.Count; i++)
                {
                    string[] fieldIdArr = cblAttributeField.Items[i].Value.Split(',');//分解出ID值
                    Model.site_channel_field modelt = model.channel_fields.Find(p => p.field_id == int.Parse(fieldIdArr[1]));//查找对应的字段ID
                    if (modelt != null)
                    {
                        cblAttributeField.Items[i].Selected = true;
                    }
                }
            }

            //绑定URL配置列表
            rptList.DataSource = new BLL.url_rewrite().GetList(model.name);
            rptList.DataBind();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.site_channel model = new Model.site_channel();
            BLL.site_channel bll = new BLL.site_channel();

            model.site_id = Utils.StrToInt(ddlSiteId.SelectedValue, 0);
            model.name = txtName.Text.Trim();
            model.title = txtTitle.Text.Trim();
            if (cbIsLock.Checked == false)
            {
                model.is_lock = 1;
            }
            if (cbIsComment.Checked == true)
            {
                model.is_comment = 1;
            }
            if (cbIsAlbums.Checked == true)
            {
                model.is_albums = 1;
            }
            if (cbIsAttach.Checked == true)
            {
                model.is_attach = 1;
            }
            if (cbIsSpec.Checked == true)
            {
                model.is_spec = 1;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);

            //添加频道扩展字段
            List<Model.site_channel_field> ls = new List<Model.site_channel_field>();
            for (int i = 0; i < cblAttributeField.Items.Count; i++)
            {
                if (cblAttributeField.Items[i].Selected)
                {
                    string[] fieldIdArr = cblAttributeField.Items[i].Value.Split(','); //分解出ID值
                    ls.Add(new Model.site_channel_field { field_id = Utils.StrToInt(fieldIdArr[1], 0) });
                }
            }
            model.channel_fields = ls;

            if (bll.Add(model) < 1)
            {
                return false;
            }

            #region 保存URL配置
            BLL.url_rewrite urlBll = new BLL.url_rewrite();
            urlBll.Remove("channel", model.name); //先删除
            string[] itemTypeArr = Request.Form.GetValues("item_type");
            string[] itemNameArr = Request.Form.GetValues("item_name");
            string[] itemPageArr = Request.Form.GetValues("item_page");
            string[] itemTempletArr = Request.Form.GetValues("item_templet");
            string[] itemPageSizeArr = Request.Form.GetValues("item_pagesize");
            string[] itemRewriteArr = Request.Form.GetValues("item_rewrite");

            if (itemTypeArr != null && itemNameArr != null && itemPageArr != null && itemTempletArr != null && itemPageSizeArr != null && itemRewriteArr != null)
            {
                if ((itemTypeArr.Length == itemNameArr.Length) && (itemNameArr.Length == itemPageArr.Length) && (itemPageArr.Length == itemTempletArr.Length)
                    && (itemTempletArr.Length == itemPageSizeArr.Length) && (itemPageSizeArr.Length == itemRewriteArr.Length))
                {
                    for (int i = 0; i < itemTypeArr.Length; i++)
                    {
                        Model.url_rewrite urlModel = new Model.url_rewrite();
                        urlModel.name = itemNameArr[i].Trim();
                        urlModel.type = itemTypeArr[i].Trim();
                        urlModel.page = itemPageArr[i].Trim();
                        urlModel.inherit = GetInherit(urlModel.type);
                        urlModel.templet = itemTempletArr[i].Trim();
                        if (Utils.StrToInt(itemPageSizeArr[i].Trim(), 0) > 0)
                        {
                            urlModel.pagesize = itemPageSizeArr[i].Trim();
                        }
                        urlModel.channel = model.name;

                        List<Model.url_rewrite_item> itemLs = new List<Model.url_rewrite_item>();
                        string[] urlRewriteArr = itemRewriteArr[i].Split('&'); //分解URL重写字符串
                        for (int j = 0; j < urlRewriteArr.Length; j++)
                        {
                            string[] urlItemArr = urlRewriteArr[j].Split(',');
                            if (urlItemArr.Length == 3)
                            {
                                itemLs.Add(new Model.url_rewrite_item { path = urlItemArr[0], pattern = urlItemArr[1], querystring = urlItemArr[2] });
                            }
                        }
                        urlModel.url_rewrite_items = itemLs;
                        urlBll.Add(urlModel);
                    }
                }
            }
            else
            {
                #region 添加默认URl配置
                //列表url
                Model.url_rewrite urlModelList = new Model.url_rewrite();
                urlModelList.name = model.name;
                urlModelList.type = "list";
                urlModelList.page = model.name + ".aspx";
                urlModelList.inherit = GetInherit(urlModelList.type);
                urlModelList.templet = model.name + ".html";
                urlModelList.pagesize = "20";
                urlModelList.channel = model.name;
                List<Model.url_rewrite_item> listItems = new List<Model.url_rewrite_item>
                    {
                        new Model.url_rewrite_item(){
                            path=model.name+".aspx",
                            pattern=model.name+".aspx"
                        },
                        new Model.url_rewrite_item(){
                            path=model.name+"/{0}.aspx",
                            pattern=model.name+"/(\\d+).aspx",
                            querystring="category_id=$1"
                        },
                        new Model.url_rewrite_item(){
                            path=model.name+"/{0}/{1}.aspx",
                            pattern=model.name+"/(\\d+)/(\\w+).aspx",
                            querystring="category_id=$1^page=$2"
                        }
                     };
                urlModelList.url_rewrite_items = listItems;
                urlBll.Add(urlModelList);
                //详情页url
                Model.url_rewrite urlModelShow = new Model.url_rewrite();
                urlModelShow.name = model.name + "_show";
                urlModelShow.type = "detail";
                urlModelShow.page = model.name + "_show.aspx";
                urlModelShow.inherit = GetInherit(urlModelShow.type);
                urlModelShow.templet = model.name + "_show.html";
                urlModelShow.channel = model.name;
                List<Model.url_rewrite_item> showItems = new List<Model.url_rewrite_item>
                    {
                        new Model.url_rewrite_item(){
                            path=model.name+"/show-{0}.aspx",
                            pattern=model.name+"/show-(\\d+).aspx",
                            querystring="id=$1"
                        }
                     };
                urlModelShow.url_rewrite_items = showItems;
                urlBll.Add(urlModelShow);
                #endregion
            }
            #endregion

            AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加频道" + model.title); //记录日志
            return true;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            BLL.site_channel bll = new BLL.site_channel();
            Model.site_channel model = bll.GetModel(_id);

            string old_name = model.name;
            model.site_id = Utils.StrToInt(ddlSiteId.SelectedValue, 0);
            model.name = txtName.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.is_lock = 0;
            model.is_comment = 0;
            model.is_albums = 0;
            model.is_attach = 0;
            model.is_spec = 0;
            if (cbIsLock.Checked == false)
            {
                model.is_lock = 1;
            }
            if (cbIsComment.Checked == true)
            {
                model.is_comment = 1;
            }
            if (cbIsAlbums.Checked == true)
            {
                model.is_albums = 1;
            }
            if (cbIsAttach.Checked == true)
            {
                model.is_attach = 1;
            }
            if (cbIsSpec.Checked == true)
            {
                model.is_spec = 1;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);

            //添加频道扩展字段
            List<Model.site_channel_field> ls = new List<Model.site_channel_field>();
            for (int i = 0; i < cblAttributeField.Items.Count; i++)
            {
                if (cblAttributeField.Items[i].Selected)
                {
                    string[] fieldIdArr = cblAttributeField.Items[i].Value.Split(','); //分解出ID值
                    ls.Add(new Model.site_channel_field { channel_id = model.id, field_id = Utils.StrToInt(fieldIdArr[1], 0) });
                }
            }
            model.channel_fields = ls;

            if (!bll.Update(model))
            {
                return false;
            }

            #region 保存URL配置
            BLL.url_rewrite urlBll = new BLL.url_rewrite();
            urlBll.Remove("channel", old_name); //先删除旧的
            string[] itemTypeArr = Request.Form.GetValues("item_type");
            string[] itemNameArr = Request.Form.GetValues("item_name");
            string[] itemPageArr = Request.Form.GetValues("item_page");
            string[] itemTempletArr = Request.Form.GetValues("item_templet");
            string[] itemPageSizeArr = Request.Form.GetValues("item_pagesize");
            string[] itemRewriteArr = Request.Form.GetValues("item_rewrite");

            if (itemTypeArr != null && itemNameArr != null && itemPageArr != null && itemTempletArr != null && itemPageSizeArr != null && itemRewriteArr != null)
            {
                if ((itemTypeArr.Length == itemNameArr.Length) && (itemNameArr.Length == itemPageArr.Length) && (itemPageArr.Length == itemTempletArr.Length)
                    && (itemTempletArr.Length == itemPageSizeArr.Length) && (itemPageSizeArr.Length == itemRewriteArr.Length))
                {
                    for (int i = 0; i < itemTypeArr.Length; i++)
                    {
                        Model.url_rewrite urlModel = new Model.url_rewrite();
                        urlModel.name = itemNameArr[i].Trim();
                        urlModel.type = itemTypeArr[i].Trim();
                        urlModel.page = itemPageArr[i].Trim();
                        urlModel.inherit = GetInherit(urlModel.type);
                        urlModel.templet = itemTempletArr[i].Trim();
                        if (Utils.StrToInt(itemPageSizeArr[i].Trim(), 0) > 0)
                        {
                            urlModel.pagesize = itemPageSizeArr[i].Trim();
                        }
                        urlModel.channel = model.name;

                        List<Model.url_rewrite_item> itemLs = new List<Model.url_rewrite_item>();
                        string[] urlRewriteArr = itemRewriteArr[i].Split('&'); //分解URL重写字符串
                        for (int j = 0; j < urlRewriteArr.Length; j++)
                        {
                            string[] urlItemArr = urlRewriteArr[j].Split(',');
                            if (urlItemArr.Length == 3)
                            {
                                itemLs.Add(new Model.url_rewrite_item { path = urlItemArr[0], pattern = urlItemArr[1], querystring = urlItemArr[2] });
                            }
                        }
                        urlModel.url_rewrite_items = itemLs;
                        urlBll.Add(urlModel);
                    }
                }
            }
            #endregion

            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改频道" + model.title); //记录日志
            return true;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改频道成功！", "channel_list.aspx", "parent.loadMenuTree");
            }
            else //添加
            {
                ChkAdminLevel("sys_channel_manage", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加频道成功！", "channel_list.aspx", "parent.loadMenuTree");
            }
            CacheHelper.Remove(DTKeys.CACHE_SITE_CHANNEL_LIST); //更新一下频道的缓存
        }

    }
}