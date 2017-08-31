<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="group_edit.aspx.cs" Inherits="DTcms.Web.admin.users.group_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑会员组</title>
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
  <a href="group_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="group_list.aspx"><span>会员组别</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑级别</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">基本信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>组别名称：</dt>
    <dd>
      <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-100" sucmsg=" " minlength="2" MaxLength="100"></asp:TextBox>
      <span class="Validform_checktip">*</span>
    </dd>
  </dl>
  <dl>
    <dt>是否隐藏：</dt>
    <dd>
      <div class="rule-single-checkbox">
        <asp:CheckBox ID="rblIsLock" runat="server" />
      </div>
      <span class="Validform_checktip">*隐藏后，用户将无法升级或显示该组别。</span>
    </dd>
  </dl>
  <dl>
    <dt>注册默认会员组：</dt>
    <dd>
      <div class="rule-single-checkbox">
        <asp:CheckBox ID="rblIsDefault" runat="server" />
      </div>
      <span class="Validform_checktip">*用户注册成功后自动默认为该会员组，如果存在多条，则以等级值最小的为准。</span>
    </dd>
  </dl>
  <dl>
    <dt>参与自动升级：</dt>
    <dd>
      <div class="rule-single-checkbox">
        <asp:CheckBox ID="rblIsUpgrade" runat="server" />
      </div>
      <span class="Validform_checktip">*如果是否，在满足升级条件下系统则不会自动升级为该会员组。</span>
    </dd>
  </dl>
  <dl>
    <dt>等级值：</dt>
    <dd>
      <asp:TextBox ID="txtGrade" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*升级顺序，取值范围1-100，等级值越大，会员等级越高。</span>
    </dd>
  </dl>
  <dl>
    <dt>升级所需积分：</dt>
    <dd>
      <asp:TextBox ID="txtUpgradeExp" runat="server" CssClass="input small" datatype="/^-?\d+$/" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*自动升级所需要的积分。</span>
    </dd>
  </dl>
  <dl>
    <dt>初始金额：</dt>
    <dd>
      <asp:TextBox ID="txtAmount" runat="server" CssClass="input small" datatype="/^-?(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*自动到该会员组赠送的金额，负数则扣减。</span>
    </dd>
  </dl>
  <dl>
    <dt>初始积分：</dt>
    <dd>
      <asp:TextBox ID="txtPoint" runat="server" CssClass="input small" datatype="/^-?\d+$/" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*自动到该会员组赠送的积分，负数则扣减。</span>
    </dd>
  </dl>
  <dl>
    <dt>购物折扣：</dt>
    <dd>
      <asp:TextBox ID="txtDiscount" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*购物享受的折扣，取值范围：1-100。</span>
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