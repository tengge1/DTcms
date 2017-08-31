using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.article
{
    public partial class article_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        protected int channel_id; //频道ID
        protected Model.site_channel channelModel; //频道实体
        private int id = 0; //内容ID

        //页面初始化事件
        protected void Page_Init(object sernder, EventArgs e)
        {
            this.channel_id = DTRequest.GetQueryInt("channel_id");
            CreateOtherField(this.channel_id); //动态生成相应的扩展字段
        }

        //页面加载事件
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");

            if (this.channel_id == 0)
            {
                JscriptMsg("频道参数不正确！", "back");
                return;
            }
            this.channelModel = new BLL.site_channel().GetModel(this.channel_id); //取得频道实体

            //如果是编辑或复制则检查信息是否存在
            if (_action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = _action;//修改或复制类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back");
                    return;
                }
                if (!new BLL.article().Exists(this.channel_id, this.id))
                {
                    JscriptMsg("信息不存在或已被删除！", "back");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("channel_" + this.channelModel.name + "_list", DTEnums.ActionEnum.View.ToString()); //检查权限
                ShowSysField(this.channel_id); //显示相应的默认控件
                GroupBind(string.Empty); //绑定用户组
                TreeBind(this.channel_id); //绑定类别
                if (action == DTEnums.ActionEnum.Edit.ToString() || action == DTEnums.ActionEnum.Copy.ToString()) //修改
                {
                    ShowInfo(this.channel_id, this.id);
                }
            }
        }

        #region 创建其它扩展字段=========================
        private void CreateOtherField(int _channel_id)
        {
            List<Model.article_attribute_field> ls = new BLL.article_attribute_field().GetModelList(_channel_id, "is_sys=0");
            if (ls.Count > 0)
            {
                field_tab_item.Visible = true;
                field_tab_content.Visible = true;
            }
            foreach (Model.article_attribute_field modelt in ls)
            {
                //创建一个dl标签
                HtmlGenericControl htmlDL = new HtmlGenericControl("dl");
                HtmlGenericControl htmlDT = new HtmlGenericControl("dt");
                HtmlGenericControl htmlDD = new HtmlGenericControl("dd");
                htmlDT.InnerHtml = modelt.title;

                switch (modelt.control_type)
                {
                    case "single-text": //单行文本
                        //创建一个TextBox控件
                        TextBox txtControl = new TextBox();
                        txtControl.ID = "field_control_" + modelt.name;
                        //CSS样式及TextMode设置
                        if (modelt.control_type == "single-text") //单行
                        {
                            txtControl.CssClass = "input normal";
                            //是否密码框
                            if (modelt.is_password == 1)
                            {
                                txtControl.TextMode = TextBoxMode.Password;
                            }
                        }
                        else if (modelt.control_type == "multi-text") //多行
                        {
                            txtControl.CssClass = "input";
                            txtControl.TextMode = TextBoxMode.MultiLine;
                        }
                        else if (modelt.control_type == "number") //数字
                        {
                            txtControl.CssClass = "input small";
                        }
                        else if (modelt.control_type == "datetime") //时间日期
                        {
                            txtControl.CssClass = "input rule-date-input";
                            txtControl.Attributes.Add("onfocus", "WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})");
                        }
                        else if (modelt.control_type == "images" || modelt.control_type == "video") //图片视频
                        {
                            txtControl.CssClass = "input normal upload-path";
                        }
                        //设置默认值
                        txtControl.Text = modelt.default_value;
                        //验证提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                        {
                            txtControl.Attributes.Add("tipmsg", modelt.valid_tip_msg);
                        }
                        //验证失败提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                        {
                            txtControl.Attributes.Add("errormsg", modelt.valid_error_msg);
                        }
                        //验证表达式
                        if (!string.IsNullOrEmpty(modelt.valid_pattern))
                        {
                            txtControl.Attributes.Add("datatype", modelt.valid_pattern);
                            txtControl.Attributes.Add("sucmsg", " ");
                        }
                        //创建一个Label控件
                        Label labelControl = new Label();
                        labelControl.CssClass = "Validform_checktip";
                        labelControl.Text = modelt.valid_tip_msg;

                        //将控件添加至DD中
                        htmlDD.Controls.Add(txtControl);
                        //如果是图片则添加上传按钮
                        if (modelt.control_type == "images")
                        {
                            HtmlGenericControl htmlBtn = new HtmlGenericControl("div");
                            htmlBtn.Attributes.Add("class", "upload-box upload-img");
                            htmlBtn.Attributes.Add("style", "margin-left:4px;");
                            htmlDD.Controls.Add(htmlBtn);
                        }
                        //如果是视频则添加上传按钮
                        if (modelt.control_type == "video")
                        {
                            HtmlGenericControl htmlBtn = new HtmlGenericControl("div");
                            htmlBtn.Attributes.Add("class", "upload-box upload-video");
                            htmlBtn.Attributes.Add("style", "margin-left:4px;");
                            htmlDD.Controls.Add(htmlBtn);
                        }
                        htmlDD.Controls.Add(labelControl);
                        break;
                    case "multi-text": //多行文本
                        goto case "single-text";
                    case "editor": //编辑器
                        HtmlTextArea txtTextArea = new HtmlTextArea();
                        txtTextArea.ID = "field_control_" + modelt.name;
                        //txtTextArea.Attributes.Add("style", "visibility:hidden;");
                        //是否简洁型编辑器
                        if (modelt.editor_type == 1)
                        {
                            txtTextArea.Attributes.Add("class", "editor-mini");
                        }
                        else
                        {
                            txtTextArea.Attributes.Add("class", "editor");
                        }
                        txtTextArea.Value = modelt.default_value; //默认值
                        //验证提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                        {
                            txtTextArea.Attributes.Add("tipmsg", modelt.valid_tip_msg);
                        }
                        //验证失败提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                        {
                            txtTextArea.Attributes.Add("errormsg", modelt.valid_error_msg);
                        }
                        //验证表达式
                        if (!string.IsNullOrEmpty(modelt.valid_pattern))
                        {
                            txtTextArea.Attributes.Add("datatype", modelt.valid_pattern);
                            txtTextArea.Attributes.Add("sucmsg", " ");
                        }
                        //创建一个Label控件
                        Label labelControl2 = new Label();
                        labelControl2.CssClass = "Validform_checktip";
                        labelControl2.Text = modelt.valid_tip_msg;
                        //将控件添加至DD中
                        htmlDD.Controls.Add(txtTextArea);
                        htmlDD.Controls.Add(labelControl2);
                        break;
                    case "images": //图片上传
                        goto case "single-text";
                    case "video": //视频上传
                        goto case "single-text";
                    case "number": //数字
                        goto case "single-text";
                    case "datetime": //时间日期
                        goto case "single-text";
                    case "checkbox": //复选框
                        CheckBox cbControl = new CheckBox();
                        cbControl.ID = "field_control_" + modelt.name;
                        //默认值
                        if (modelt.default_value == "1")
                        {
                            cbControl.Checked = true;
                        }
                        HtmlGenericControl htmlDiv1 = new HtmlGenericControl("div");
                        htmlDiv1.Attributes.Add("class", "rule-single-checkbox");
                        htmlDiv1.Controls.Add(cbControl);
                        //将控件添加至DD中
                        htmlDD.Controls.Add(htmlDiv1);
                        if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                        {
                            //创建一个Label控件
                            Label labelControl3 = new Label();
                            labelControl3.CssClass = "Validform_checktip";
                            labelControl3.Text = modelt.valid_tip_msg;
                            htmlDD.Controls.Add(labelControl3);
                        }
                        break;
                    case "multi-radio": //多项单选
                        RadioButtonList rblControl = new RadioButtonList();
                        rblControl.ID = "field_control_" + modelt.name;
                        rblControl.RepeatDirection = RepeatDirection.Horizontal;
                        rblControl.RepeatLayout = RepeatLayout.Flow;
                        HtmlGenericControl htmlDiv2 = new HtmlGenericControl("div");
                        htmlDiv2.Attributes.Add("class", "rule-multi-radio");
                        htmlDiv2.Controls.Add(rblControl);
                        //赋值选项
                        string[] valArr = modelt.item_option.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        for (int i = 0; i < valArr.Length; i++)
                        {
                            string[] valItemArr = valArr[i].Split('|');
                            if (valItemArr.Length == 2)
                            {
                                rblControl.Items.Add(new ListItem(valItemArr[0], valItemArr[1]));
                            }
                        }
                        rblControl.SelectedValue = modelt.default_value; //默认值
                        //创建一个Label控件
                        Label labelControl4 = new Label();
                        labelControl4.CssClass = "Validform_checktip";
                        labelControl4.Text = modelt.valid_tip_msg;
                        //将控件添加至DD中
                        htmlDD.Controls.Add(htmlDiv2);
                        htmlDD.Controls.Add(labelControl4);
                        break;
                    case "multi-checkbox": //多项多选
                        CheckBoxList cblControl = new CheckBoxList();
                        cblControl.ID = "field_control_" + modelt.name;
                        cblControl.RepeatDirection = RepeatDirection.Horizontal;
                        cblControl.RepeatLayout = RepeatLayout.Flow;
                        HtmlGenericControl htmlDiv3 = new HtmlGenericControl("div");
                        htmlDiv3.Attributes.Add("class", "rule-multi-checkbox");
                        htmlDiv3.Controls.Add(cblControl);
                        //赋值选项
                        string[] valArr2 = modelt.item_option.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        for (int i = 0; i < valArr2.Length; i++)
                        {
                            string[] valItemArr2 = valArr2[i].Split('|');
                            if (valItemArr2.Length == 2)
                            {
                                cblControl.Items.Add(new ListItem(valItemArr2[0], valItemArr2[1]));
                            }
                        }
                        cblControl.SelectedValue = modelt.default_value; //默认值
                        //创建一个Label控件
                        Label labelControl5 = new Label();
                        labelControl5.CssClass = "Validform_checktip";
                        labelControl5.Text = modelt.valid_tip_msg;
                        //将控件添加至DD中
                        htmlDD.Controls.Add(htmlDiv3);
                        htmlDD.Controls.Add(labelControl5);
                        break;
                }

                //将DT和DD添加到DL中
                htmlDL.Controls.Add(htmlDT);
                htmlDL.Controls.Add(htmlDD);
                //将DL添加至field_tab_content中
                field_tab_content.Controls.Add(htmlDL);
            }
        }
        #endregion

        #region 显示默认扩展字段=========================
        private void ShowSysField(int _channel_id)
        {
            //查找该频道所选的默认字段
            List<Model.article_attribute_field> ls = new BLL.article_attribute_field().GetModelList(_channel_id, "is_sys=1");
            foreach (Model.article_attribute_field modelt in ls)
            {
                //查找相应的控件，如找到则显示
                HtmlGenericControl htmlDiv = FindControl("div_" + modelt.name) as HtmlGenericControl;
                if (htmlDiv != null)
                {
                    htmlDiv.Visible = true;
                    ((Label)htmlDiv.FindControl("div_" + modelt.name + "_title")).Text = modelt.title;
                    ((TextBox)htmlDiv.FindControl("field_control_" + modelt.name)).Text = modelt.default_value;
                    ((Label)htmlDiv.FindControl("div_" + modelt.name + "_tip")).Text = modelt.valid_tip_msg;
                }
            }
            //查找该频道所开启的功能
            if (channelModel.is_albums == 1)
            {
                div_albums_container.Visible = true;
            }
            if (channelModel.is_attach == 1)
            {
                div_attach_container.Visible = true;
            }
        }
        #endregion

        #region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            BLL.article_category bll = new BLL.article_category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("请选择类别...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 绑定会员组===============================
        private void GroupBind(string strWhere)
        {
            //检查网站是否开启会员功能
            if (sysConfig.memberstatus == 0)
            {
                return;
            }
            //检查该频道是否开启会员组价格
            if (channelModel.is_spec == 0)
            {
                return;
            }
            BLL.user_groups bll = new BLL.user_groups();
            DataSet ds = bll.GetList(0, strWhere, "grade asc,id desc");
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.rptPrice.DataSource = ds;
                this.rptPrice.DataBind();
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _channel_id, int _id)
        {
            BLL.article bll = new BLL.article();
            Model.article model = bll.GetModel(_channel_id, _id);

            ddlCategoryId.SelectedValue = model.category_id.ToString();
            txtCallIndex.Text = model.call_index;
            txtTitle.Text = model.title;
            txtTags.Text = model.tags;
            txtLinkUrl.Text = model.link_url;
            //不是相册图片就绑定
            string filename = model.img_url.Substring(model.img_url.LastIndexOf("/") + 1);
            if (!filename.StartsWith("thumb_"))
            {
                txtImgUrl.Text = model.img_url;
            }
            txtSeoTitle.Text = model.seo_title;
            txtSeoKeywords.Text = model.seo_keywords;
            txtSeoDescription.Text = model.seo_description;
            txtZhaiyao.Text = model.zhaiyao;
            txtContent.Value = model.content;
            txtSortId.Text = model.sort_id.ToString();
            txtClick.Text = model.click.ToString();
            if (model.status == 2)
            {
                cbStatus.Checked = false;
            }
            else
            {
                cbStatus.Checked = true;
            }
            if (action == DTEnums.ActionEnum.Edit.ToString())
            {
                txtAddTime.Text = model.add_time.ToString("yyyy-MM-dd HH:mm:ss");
            }
            if (model.is_msg == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.is_top == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            if (model.is_red == 1)
            {
                cblItem.Items[2].Selected = true;
            }
            if (model.is_hot == 1)
            {
                cblItem.Items[3].Selected = true;
            }
            if (model.is_slide == 1)
            {
                cblItem.Items[4].Selected = true;
            }
            //扩展字段赋值
            List<Model.article_attribute_field> ls1 = new BLL.article_attribute_field().GetModelList(this.channel_id, "");
            foreach (Model.article_attribute_field modelt1 in ls1)
            {
                switch (modelt1.control_type)
                {
                    case "single-text": //单行文本
                        TextBox txtControl = FindControl("field_control_" + modelt1.name) as TextBox;
                        if (txtControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            if (modelt1.is_password == 1)
                            {
                                txtControl.Attributes.Add("value", model.fields[modelt1.name]);
                            }
                            else
                            {
                                txtControl.Text = model.fields[modelt1.name];
                            }
                        }
                        break;
                    case "multi-text": //多行文本
                        goto case "single-text";
                    case "editor": //编辑器
                        HtmlTextArea txtAreaControl = FindControl("field_control_" + modelt1.name) as HtmlTextArea;
                        if (txtAreaControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            txtAreaControl.Value = model.fields[modelt1.name];
                        }
                        break;
                    case "images": //图片上传
                        goto case "single-text";
                    case "video": //视频上传
                        goto case "single-text";
                    case "number": //数字
                        goto case "single-text";
                    case "datetime": //时间日期
                        goto case "single-text";
                    case "checkbox": //复选框
                        CheckBox cbControl = FindControl("field_control_" + modelt1.name) as CheckBox;
                        if (cbControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            if (model.fields[modelt1.name] == "1")
                            {
                                cbControl.Checked = true;
                            }
                            else
                            {
                                cbControl.Checked = false;
                            }
                        }
                        break;
                    case "multi-radio": //多项单选
                        RadioButtonList rblControl = FindControl("field_control_" + modelt1.name) as RadioButtonList;
                        if (rblControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            rblControl.SelectedValue = model.fields[modelt1.name];
                        }
                        break;
                    case "multi-checkbox": //多项多选
                        CheckBoxList cblControl = FindControl("field_control_" + modelt1.name) as CheckBoxList;
                        if (cblControl != null && model.fields.ContainsKey(modelt1.name))
                        {
                            string[] valArr = model.fields[modelt1.name].Split(',');
                            for (int i = 0; i < cblControl.Items.Count; i++)
                            {
                                cblControl.Items[i].Selected = false; //先取消默认的选中
                                foreach (string str in valArr)
                                {
                                    if (cblControl.Items[i].Value == str)
                                    {
                                        cblControl.Items[i].Selected = true;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            //绑定图片相册
            if (filename.StartsWith("thumb_"))
            {
                hidFocusPhoto.Value = model.img_url; //封面图片
            }
            rptAlbumList.DataSource = model.albums;
            rptAlbumList.DataBind();
            //绑定内容附件
            rptAttachList.DataSource = model.attach;
            rptAttachList.DataBind();
            //赋值用户组价格
            if (model.group_price != null)
            {
                for (int i = 0; i < this.rptPrice.Items.Count; i++)
                {
                    int hideId = Convert.ToInt32(((HiddenField)this.rptPrice.Items[i].FindControl("hideGroupId")).Value);
                    foreach (Model.user_group_price modelt in model.group_price)
                    {
                        if (hideId == modelt.group_id)
                        {
                            ((HiddenField)this.rptPrice.Items[i].FindControl("hidePriceId")).Value = modelt.id.ToString();
                            ((TextBox)this.rptPrice.Items[i].FindControl("txtGroupPrice")).Text = modelt.price.ToString();
                        }
                    }
                }
            }
        }
        #endregion

        #region 扩展字段赋值=============================
        private Dictionary<string, string> SetFieldValues(int _channel_id)
        {
            DataTable dt = new BLL.article_attribute_field().GetList(_channel_id, "").Tables[0];
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                //查找相应的控件
                switch (dr["control_type"].ToString())
                {
                    case "single-text": //单行文本
                        TextBox txtControl = FindControl("field_control_" + dr["name"].ToString()) as TextBox;
                        if (txtControl != null)
                        {
                            dic.Add(dr["name"].ToString(), txtControl.Text.Trim());

                        }
                        break;
                    case "multi-text": //多行文本
                        goto case "single-text";
                    case "editor": //编辑器
                        HtmlTextArea htmlTextAreaControl = FindControl("field_control_" + dr["name"].ToString()) as HtmlTextArea;
                        if (htmlTextAreaControl != null)
                        {
                            dic.Add(dr["name"].ToString(), htmlTextAreaControl.Value);
                        }
                        break;
                    case "images": //图片上传
                        goto case "single-text";
                    case "video": //视频上传
                        goto case "single-text";
                    case "number": //数字
                        goto case "single-text";
                    case "datetime": //时间日期
                        goto case "single-text";
                    case "checkbox": //复选框
                        CheckBox cbControl = FindControl("field_control_" + dr["name"].ToString()) as CheckBox;
                        if (cbControl != null)
                        {
                            if (cbControl.Checked == true)
                            {
                                dic.Add(dr["name"].ToString(), "1");
                            }
                            else
                            {
                                dic.Add(dr["name"].ToString(), "0");
                            }
                        }
                        break;
                    case "multi-radio": //多项单选
                        RadioButtonList rblControl = FindControl("field_control_" + dr["name"].ToString()) as RadioButtonList;
                        if (rblControl != null)
                        {
                            dic.Add(dr["name"].ToString(), rblControl.SelectedValue);
                        }
                        break;
                    case "multi-checkbox": //多项多选
                        CheckBoxList cblControl = FindControl("field_control_" + dr["name"].ToString()) as CheckBoxList;
                        if (cblControl != null)
                        {
                            StringBuilder tempStr = new StringBuilder();
                            for (int i = 0; i < cblControl.Items.Count; i++)
                            {
                                if (cblControl.Items[i].Selected)
                                {
                                    tempStr.Append(cblControl.Items[i].Value.Replace(',', '，') + ",");
                                }
                            }
                            dic.Add(dr["name"].ToString(), Utils.DelLastComma(tempStr.ToString()));
                        }
                        break;
                }
            }
            return dic;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.article model = new Model.article();
            BLL.article bll = new BLL.article();

            model.site_id = this.channelModel.site_id;
            model.channel_id = this.channel_id;
            model.category_id = Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.call_index = txtCallIndex.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.tags = txtTags.Text.Trim();
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = txtImgUrl.Text;
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keywords = txtSeoKeywords.Text.Trim();
            model.seo_description = txtSeoDescription.Text.Trim();
            //内容摘要提取内容前255个字符
            if (string.IsNullOrEmpty(txtZhaiyao.Text.Trim()))
            {
                model.zhaiyao = Utils.DropHTML(txtContent.Value, 250);
            }
            else
            {
                model.zhaiyao = Utils.DropHTML(txtZhaiyao.Text, 250);
            }
            
            model.content = txtContent.Value;
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.click = int.Parse(txtClick.Text.Trim());
            model.is_msg = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            model.is_slide = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_msg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_top = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[3].Selected == true)
            {
                model.is_hot = 1;
            }
            if (cblItem.Items[4].Selected == true)
            {
                model.is_slide = 1;
            }
            model.is_sys = 1; //管理员发布
            model.user_name = GetAdminInfo().user_name; //获得当前登录用户名
            if (cbStatus.Checked == false)
            {
                model.status = 2;
            }
            else
            {
                model.status = GetAdminInfo().is_audit;
            }
            model.add_time = Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.fields = SetFieldValues(this.channel_id); //扩展字段赋值

            #region 保存相册====================
            //检查是否有自定义图片
            if (txtImgUrl.Text.Trim() == "")
            {
                model.img_url = hidFocusPhoto.Value;
            }
            string[] albumArr = Request.Form.GetValues("hid_photo_name");
            string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
            if (albumArr != null && albumArr.Length > 0)
            {
                List<Model.article_albums> ls = new List<Model.article_albums>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.article_albums { channel_id = this.channel_id, original_path = imgArr[1], thumb_path = imgArr[2], remark = remarkArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.article_albums { channel_id = this.channel_id, original_path = imgArr[1], thumb_path = imgArr[2] });
                        }
                    }
                }
                model.albums = ls;
            }
            #endregion

            #region 保存附件====================
            //保存附件
            string[] attachFileNameArr = Request.Form.GetValues("hid_attach_filename");
            string[] attachFilePathArr = Request.Form.GetValues("hid_attach_filepath");
            string[] attachFileSizeArr = Request.Form.GetValues("hid_attach_filesize");
            string[] attachPointArr = Request.Form.GetValues("txt_attach_point");
            if (attachFileNameArr != null && attachFilePathArr != null && attachFileSizeArr != null && attachPointArr != null
                && attachFileNameArr.Length > 0 && attachFilePathArr.Length > 0 && attachFileSizeArr.Length > 0 && attachPointArr.Length > 0)
            {
                List<Model.article_attach> ls = new List<Model.article_attach>();
                for (int i = 0; i < attachFileNameArr.Length; i++)
                {
                    int fileSize = Utils.StrToInt(attachFileSizeArr[i], 0);
                    string fileExt = FileHelper.GetFileExt(attachFilePathArr[i]);
                    int _point = Utils.StrToInt(attachPointArr[i], 0);
                    ls.Add(new Model.article_attach { channel_id = this.channel_id, file_name = attachFileNameArr[i], file_path = attachFilePathArr[i], file_size = fileSize, file_ext = fileExt, point = _point });
                }
                model.attach = ls;
            }
            #endregion

            #region 保存会员组价格==============
            List<Model.user_group_price> priceList = new List<Model.user_group_price>();
            for (int i = 0; i < rptPrice.Items.Count; i++)
            {
                int _groupid = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hideGroupId")).Value);
                decimal _price = Convert.ToDecimal(((TextBox)rptPrice.Items[i].FindControl("txtGroupPrice")).Text.Trim());
                priceList.Add(new Model.user_group_price { channel_id = this.channel_id, group_id = _groupid, price = _price });
            }
            model.group_price = priceList;

            #endregion

            if (bll.Add(model) > 0)
            {
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加" + this.channelModel.title + "频道内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.article bll = new BLL.article();
            Model.article model = bll.GetModel(this.channel_id, _id);

            model.site_id = this.channelModel.site_id;
            model.channel_id = this.channel_id;
            model.category_id = Utils.StrToInt(ddlCategoryId.SelectedValue, 0);
            model.call_index = txtCallIndex.Text.Trim();
            model.title = txtTitle.Text.Trim();
            model.tags = txtTags.Text.Trim();
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = txtImgUrl.Text;
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keywords = txtSeoKeywords.Text.Trim();
            model.seo_description = txtSeoDescription.Text.Trim();
            //内容摘要提取内容前255个字符
            if (string.IsNullOrEmpty(txtZhaiyao.Text.Trim()))
            {
                model.zhaiyao = Utils.DropHTML(txtContent.Value, 250);
            }
            else
            {
                model.zhaiyao = Utils.DropHTML(txtZhaiyao.Text, 250);
            }
            
            model.content = txtContent.Value;
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.click = int.Parse(txtClick.Text.Trim());
            model.is_msg = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            model.is_slide = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_msg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_top = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[3].Selected == true)
            {
                model.is_hot = 1;
            }
            if (cblItem.Items[4].Selected == true)
            {
                model.is_slide = 1;
            }
            if (cbStatus.Checked == false)
            {
                model.status = 2;
            }
            else
            {
                model.status = GetAdminInfo().is_audit;
            }
            model.add_time = Utils.StrToDateTime(txtAddTime.Text.Trim());
            model.update_time = DateTime.Now;
            model.fields = SetFieldValues(this.channel_id); //扩展字段赋值

            #region 保存相册====================
            //检查是否有自定义图片
            if (txtImgUrl.Text.Trim() == "")
            {
                model.img_url = hidFocusPhoto.Value;
            }
            if (model.albums != null)
            {
                model.albums.Clear();
            }
            string[] albumArr = Request.Form.GetValues("hid_photo_name");
            string[] remarkArr = Request.Form.GetValues("hid_photo_remark");
            if (albumArr != null)
            {
                List<Model.article_albums> ls = new List<Model.article_albums>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    int img_id = Utils.StrToInt(imgArr[0], 0);
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.article_albums { id = img_id, channel_id = this.channel_id, article_id = _id, original_path = imgArr[1], thumb_path = imgArr[2], remark = remarkArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.article_albums { id = img_id, channel_id = this.channel_id, article_id = _id, original_path = imgArr[1], thumb_path = imgArr[2] });
                        }
                    }
                }
                model.albums = ls;
            }
            #endregion

            #region 保存附件====================
            if (model.attach != null)
            {
                model.attach.Clear();
            }
            string[] attachIdArr = Request.Form.GetValues("hid_attach_id");
            string[] attachFileNameArr = Request.Form.GetValues("hid_attach_filename");
            string[] attachFilePathArr = Request.Form.GetValues("hid_attach_filepath");
            string[] attachFileSizeArr = Request.Form.GetValues("hid_attach_filesize");
            string[] attachPointArr = Request.Form.GetValues("txt_attach_point");
            if (attachIdArr != null && attachFileNameArr != null && attachFilePathArr != null && attachFileSizeArr != null && attachPointArr != null
                && attachIdArr.Length > 0 && attachFileNameArr.Length > 0 && attachFilePathArr.Length > 0 && attachFileSizeArr.Length > 0 && attachPointArr.Length > 0)
            {
                List<Model.article_attach> ls = new List<Model.article_attach>();
                for (int i = 0; i < attachFileNameArr.Length; i++)
                {
                    int attachId = Utils.StrToInt(attachIdArr[i], 0);
                    int fileSize = Utils.StrToInt(attachFileSizeArr[i], 0);
                    string fileExt = FileHelper.GetFileExt(attachFilePathArr[i]);
                    int _point = Utils.StrToInt(attachPointArr[i], 0);
                    ls.Add(new Model.article_attach { id = attachId, channel_id = this.channel_id, article_id = _id, file_name = attachFileNameArr[i], file_path = attachFilePathArr[i], file_size = fileSize, file_ext = fileExt, point = _point, });
                }
                model.attach = ls;
            }
            #endregion

            #region 保存会员组价格==============
            List<Model.user_group_price> priceList = new List<Model.user_group_price>();
            for (int i = 0; i < rptPrice.Items.Count; i++)
            {
                int hidPriceId = 0;
                if (!string.IsNullOrEmpty(((HiddenField)rptPrice.Items[i].FindControl("hidePriceId")).Value))
                {
                    hidPriceId = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hidePriceId")).Value);
                }
                int hidGroupId = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hideGroupId")).Value);
                decimal _price = Convert.ToDecimal(((TextBox)rptPrice.Items[i].FindControl("txtGroupPrice")).Text.Trim());
                priceList.Add(new Model.user_group_price { id = hidPriceId, channel_id = this.channel_id, article_id = _id, group_id = hidGroupId, price = _price });
            }
            model.group_price = priceList;
            #endregion

            if (bll.Update(model))
            {

                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改" + this.channelModel.title + "频道内容:" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("channel_" + this.channelModel.name + "_list", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误啦！", string.Empty);
                    return;
                }
                JscriptMsg("修改信息成功！", "article_list.aspx?channel_id=" + this.channel_id);
            }
            else //添加
            {
                ChkAdminLevel("channel_" + this.channelModel.name + "_list", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", string.Empty);
                    return;
                }
                JscriptMsg("添加信息成功！", "article_list.aspx?channel_id=" + this.channel_id);
            }
        }

    }
}