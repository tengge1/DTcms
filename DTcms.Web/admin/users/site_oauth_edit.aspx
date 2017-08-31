<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="site_oauth_edit.aspx.cs" Inherits="DTcms.Web.admin.users.site_oauth_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑OAuth列表</title>
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
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
  <a href="site_oauth_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>会员设置</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>OAuth列表</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑OAuth信息</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">编辑OAuth信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>所属站点</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlSiteId" runat="server" AutoPostBack="True" onselectedindexchanged="ddlSiteId_SelectedIndexChanged" 
              datatype="*" errormsg="请选择所属站点！" sucmsg=" "></asp:DropDownList>
      </div>
      <span class="Validform_checktip">*选择站点后查询未安装应用</span>
    </dd>
  </dl>
  <dl>
    <dt>所属应用</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlOauthId" runat="server" datatype="*">
              <asp:ListItem Value="">请选择站点...</asp:ListItem>
        </asp:DropDownList>
      </div>
      <span class="Validform_checktip">*请先选择站点后显示应用</span>
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
    <dt>标题名称</dt>
    <dd>
      <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*网站上面显示的标题，100字符内</span>
    </dd>
  </dl>
  <dl>
    <dt>API Key</dt>
    <dd>
      <asp:TextBox ID="txtAppId" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*QQ互联请填写APP ID</span>
    </dd>
  </dl>
  <dl>
    <dt>Secret Key</dt>
    <dd>
      <asp:TextBox ID="txtAppKey" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*QQ互联请填写KEY</span>
    </dd>
  </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:location.href='site_oauth_list.aspx';" />
  </div>
</div>
<!--/工具栏-->

</form>
</body>
</html>