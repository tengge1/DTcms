<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site_edit.aspx.cs" Inherits="DTcms.Web.admin.channel.site_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑站点</title>
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化上传控件
        $(".upload-img").InitUploader({ sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf" });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="site_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="site_list.aspx"><span>站点管理</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑站点</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">站点设置</a></li>
        <li><a href="javascript:;">网站信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>站点名称</dt>
    <dd>
      <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal"  datatype="*2-100" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*标题备注，允许中文。</span>
    </dd>
  </dl>
  <dl>
    <dt>生成目录名</dt>
    <dd>
      <asp:TextBox ID="txtBuildPath" runat="server" CssClass="input normal" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" errormsg="请填写正确的名称！" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*频道分类目录，只允许使用英文、下划线。</span>
    </dd>
  </dl>
  <dl>
    <dt>绑定域名</dt>
    <dd>
      <asp:TextBox ID="txtDomain" runat="server" CssClass="input normal" datatype="/^\s*$|^([a-zA-Z0-9\-\u4e00-\u9fa5]+\.)+[a-zA-Z\u4e00-\u9fa5]+$/" errormsg="请输入正确的域名！" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*域名不可重复，去除“http://”部分，不绑定请留空。</span>
    </dd>
  </dl>
  <dl>
    <dt>是否启用</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsLock" runat="server" Checked="True" />
      </div>
      <span class="Validform_checktip">*不启用则不加载该站点</span>
    </dd>
  </dl>
  <dl>
    <dt>是否默认</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsDefault" runat="server" />
      </div>
      <span class="Validform_checktip">*只允许一个默认站点，默认则不能绑定域名。</span>
    </dd>
  </dl>
  <dl>
    <dt>排序数字</dt>
    <dd>
      <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
      <span class="Validform_checktip">*数字，越小越向前</span>
    </dd>
  </dl>
</div>

<div class="tab-content" style="display:none;">
  <dl>
    <dt>网站名称</dt>
    <dd>
      <asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="*2-255" sucmsg=" " />
      <span class="Validform_checktip">*任意字符，控制在255个字符内</span>
    </dd>
  </dl>
  <dl>
    <dt>网站LOGO</dt>
    <dd>
      <asp:TextBox ID="txtLogo" runat="server" CssClass="input normal upload-path" />
      <div class="upload-box upload-img"></div>
    </dd>
  </dl>
  <dl>
    <dt>公司名称</dt>
    <dd>
      <asp:TextBox ID="txtCompany" runat="server" CssClass="input normal" />
    </dd>
  </dl>
  <dl>
    <dt>通讯地址</dt>
    <dd>
      <asp:TextBox ID="txtAddress" runat="server" CssClass="input normal" />
    </dd>
  </dl>
  <dl>
    <dt>联系电话</dt>
    <dd>
      <asp:TextBox ID="txtTel" runat="server" CssClass="input normal" />
      <span class="Validform_checktip">*非必填，区号+电话号码</span>
    </dd>
  </dl>
  <dl>
    <dt>传真号码</dt>
    <dd>
      <asp:TextBox ID="txtFax" runat="server" CssClass="input normal" />
      <span class="Validform_checktip">*非必填，区号+传真号码</span>
    </dd>
  </dl>
  <dl>
    <dt>电子邮箱</dt>
    <dd>
      <asp:TextBox ID="txtEmail" runat="server" CssClass="input normal" />
    </dd>
  </dl>
  <dl>
    <dt>网站备案号</dt>
    <dd>
      <asp:TextBox ID="txtCrod" runat="server" CssClass="input normal" />
    </dd>
  </dl>
  <dl>
    <dt>首页标题(SEO)</dt>
    <dd>
      <asp:TextBox ID="txtSeoTitle" runat="server" CssClass="input normal" />
      <span class="Validform_checktip">*自定义的首页标题</span>
    </dd>
  </dl>
  <dl>
    <dt>页面关健词(SEO)</dt>
    <dd>
      <asp:TextBox ID="txtSeoKeyword" runat="server" CssClass="input normal" />
      <span class="Validform_checktip">页面关键词(keyword)</span>
    </dd>
  </dl>
  <dl>
    <dt>页面描述(SEO)</dt>
    <dd>
      <asp:TextBox ID="txtSeoDescription" runat="server" CssClass="input normal" />
      <span class="Validform_checktip">页面描述(description)</span>
    </dd>
  </dl>
  <dl>
    <dt>底部版权信息</dt>
    <dd>
      <asp:TextBox ID="txtCopyright" runat="server" CssClass="input" TextMode="MultiLine" />
      <span class="Validform_checktip">支持HTML</span>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->

</form>
</body>
</html>
