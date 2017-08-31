<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="link_edit.aspx.cs" Inherits="DTcms.Web.Plugin.Link.admin.link_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑友情链接</title>
<link rel="stylesheet" type="text/css" href="../../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../../../<%=sysConfig.webmanagepath %>/skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../../../<%=sysConfig.webmanagepath %>/skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=sysConfig.webmanagepath %>/js/uploader.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=sysConfig.webmanagepath %>/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=sysConfig.webmanagepath %>/js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化上传控件
        $(".upload-img").InitUploader({ sendurl: "../../../tools/upload_ajax.ashx", swf: "../../../scripts/webuploader/uploader.swf" });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="index.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../../../<%=sysConfig.webmanagepath %>/center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>插件管理</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>友情链接</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑链接</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">编辑友情链接</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>所属站点</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlSiteId" runat="server" datatype="*" errormsg="请选择所属站点！" sucmsg=" "></asp:DropDownList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>标题名称</dt>
    <dd><asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" "></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>审核状态</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblIsLock" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="0" Selected="True">正常</asp:ListItem>
        <asp:ListItem Value="1">待审核</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>推荐到首页</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsRed" runat="server" />
      </div>
    </dd>
  </dl>
  <dl>
    <dt>排序数字</dt>
    <dd>
      <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
      <span class="Validform_checktip">*数字，越小越向前</span>
    </dd>
  </dl>
  <dl>
    <dt>联系人</dt>
    <dd><asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="*0-50" sucmsg=" "></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>联系电话</dt>
    <dd><asp:TextBox ID="txtUserTel" runat="server" CssClass="input normal" datatype="*0-30" sucmsg=" "></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>电子邮箱</dt>
    <dd><asp:TextBox ID="txtEmail" runat="server" CssClass="input normal" datatype="*0-50" sucmsg=" "></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>链接网址</dt>
    <dd><asp:TextBox ID="txtSiteUrl" runat="server" CssClass="input normal" datatype="url" sucmsg=" "></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>Logo图片</dt>
    <dd>
      <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" />
      <div class="upload-box upload-img"></div>
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
