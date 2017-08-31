<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_config.aspx.cs" Inherits="DTcms.Web.admin.order.order_config" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>订单参数设置</title>
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
  <a href="javascript:history.back(-1);" class="back"><i class="iconfont icon-up"></i><span>返回上一页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>订单设置</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>参数设置</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">基本参数设置</a></li>
      </ul>
    </div>
  </div>
</div>

<!--订单参数设置-->
<div class="tab-content">
  <dl>
    <dt>开启匿名购物</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="anonymous" runat="server" />
      </div>
      <span class="Validform_checktip">*注意：开启匿名后无需登录即可下订单</span>
    </dd>
  </dl>
  <dl>
    <dt>发票税费类型</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="taxtype" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">百分比</asp:ListItem>
        <asp:ListItem Value="2">固定金额</asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <span class="Validform_checktip">*百分比的计算公式：商品总金额+(商品总金额*百分比)+配送+支付费用=订单总金额</span>
    </dd>
  </dl>
  <dl>
    <dt>发票税金费用</dt>
    <dd>
      <asp:TextBox ID="taxamount" runat="server" CssClass="input small" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" ">0</asp:TextBox>
      <span class="Validform_checktip">*注意：百分比取值范围：0-100，固定金额单位为“元”</span>
    </dd>
  </dl>
  <dl>
    <dt>订单确认通知</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="confirmmsg" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="0" Selected="True">关闭通知</asp:ListItem>
        <asp:ListItem Value="1">短信通知</asp:ListItem>
        <asp:ListItem Value="2">邮件通知</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>确认模板别名</dt>
    <dd>
      <asp:TextBox ID="confirmcallindex" runat="server" CssClass="input normal" datatype="*" sucmsg=" " />
      <span class="Validform_checktip">*订单确认通知模板调用别名</span>
    </dd>
  </dl>
  <dl>
    <dt>订单发货通知</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="expressmsg" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="0" Selected="True">关闭通知</asp:ListItem>
        <asp:ListItem Value="1">短信通知</asp:ListItem>
        <asp:ListItem Value="2">邮件通知</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>发货模板别名</dt>
    <dd>
      <asp:TextBox ID="expresscallindex" runat="server" CssClass="input normal" datatype="*" sucmsg=" " />
      <span class="Validform_checktip">*订单发货通知模板调用别名</span>
    </dd>
  </dl>
  <dl>
    <dt>订单完成通知</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="completemsg" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="0" Selected="True">关闭通知</asp:ListItem>
        <asp:ListItem Value="1">短信通知</asp:ListItem>
        <asp:ListItem Value="2">邮件通知</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>完成模板别名</dt>
    <dd>
      <asp:TextBox ID="completecallindex" runat="server" CssClass="input normal" datatype="*" sucmsg=" " />
      <span class="Validform_checktip">*订单完成通知模板调用别名</span>
    </dd>
  </dl>
</div>
<!--/订单参数设置-->
<!--内容-->

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