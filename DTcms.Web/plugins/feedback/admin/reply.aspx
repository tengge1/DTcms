<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="reply.aspx.cs" Inherits="DTcms.Web.Plugin.Feedback.admin.reply" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>回复留言</title>
<link rel="stylesheet" type="text/css" href="../../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../../../<%=sysConfig.webmanagepath %>/skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../../../<%=sysConfig.webmanagepath %>/skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=sysConfig.webmanagepath %>/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=sysConfig.webmanagepath %>/js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
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
  <span>在线留言</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>回复留言</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">查看留言</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>所属站点</dt>
    <dd><%=new DTcms.BLL.sites().GetTitle(model.site_id)%></dd>
  </dl>
  <dl>
    <dt>联系人</dt>
    <dd><%=model.user_name %></dd>
  </dl>
  <dl>
    <dt>联系电话</dt>
    <dd><%=model.user_tel %></dd>
  </dl>
  <dl>
    <dt>联系QQ</dt>
    <dd><%=model.user_qq %></dd>
  </dl>
  <dl>
    <dt>电子邮箱</dt>
    <dd><%=model.user_email %></dd>
  </dl>
  <dl>
    <dt>留言时间</dt>
    <dd><%=model.add_time %></dd>
  </dl>
  <dl>
    <dt>留言标题</dt>
    <dd><%=model.title %></dd>
  </dl>
  <dl>
    <dt>留言内容</dt>
    <dd><%=model.content %></dd>
  </dl>
  <dl>
    <dt>回复留言</dt>
    <dd><asp:TextBox ID="txtReContent" runat="server" CssClass="input" TextMode="MultiLine" datatype="*" sucmsg=" " /></dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="回复留言" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->
</form>
</body>
</html>
