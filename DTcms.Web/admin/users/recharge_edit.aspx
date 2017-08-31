<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="recharge_edit.aspx.cs" Inherits="DTcms.Web.admin.users.recharge_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>会员充值</title>
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
  <a href="recharge_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="recharge_list.aspx"><span>充值记录</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>会员充值</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">充值信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>用户名</dt>
    <dd>
      <asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*需要充值的会员用户名</span>
    </dd>
  </dl>
  <dl>
    <dt>支付方式</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlPaymentId" runat="server" datatype="*" errormsg="请选择支付方式" sucmsg=" "></asp:DropDownList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>订单号</dt>
    <dd>
      <asp:TextBox ID="txtRechargeNo" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*系统随机生成订单号</span>
    </dd>
  </dl>
  <dl>
    <dt>充值金额</dt>
    <dd>
      <asp:TextBox ID="txtAmount" runat="server" CssClass="input small" datatype="/^-?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*给该会员充值的金额，负数则扣减。</span>
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