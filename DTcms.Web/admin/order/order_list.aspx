<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_list.aspx.cs" Inherits="DTcms.Web.admin.order.order_list" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>订单管理</title>
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../../css/pagination.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i class="iconfont icon-up"></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>订单列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"><i class="iconfont icon-more"></i></a>
      <div class="l-list">
        <ul class="icon-list">
          <li><a href="javascript:;" onclick="checkAll(this);"><i class="iconfont icon-check"></i><span>全选</span></a></li>
          <li><asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return ExePostBack('btnDelete','只允许删除已作废订单，是否继续？');" onclick="btnDelete_Click"><i class="iconfont icon-delete"></i><span>删除</span></asp:LinkButton></li>
        </ul>
        <div class="menu-list">
          <div class="rule-single-select">
            <asp:DropDownList ID="ddlSiteId" runat="server" AutoPostBack="True" onselectedindexchanged="ddlSiteId_SelectedIndexChanged"></asp:DropDownList>
          </div>
          <div class="rule-single-select">
            <asp:DropDownList ID="ddlStatus" runat="server" AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged">
              <asp:ListItem Value="" Selected="True">订单状态</asp:ListItem>
              <asp:ListItem Value="1">已生成</asp:ListItem>
              <asp:ListItem Value="2">已确认</asp:ListItem>
              <asp:ListItem Value="3">已完成</asp:ListItem>
              <asp:ListItem Value="4">已取消</asp:ListItem>
              <asp:ListItem Value="5">已作废</asp:ListItem>
            </asp:DropDownList>
          </div>
          <div class="rule-single-select">
            <asp:DropDownList ID="ddlPaymentStatus" runat="server" AutoPostBack="True" onselectedindexchanged="ddlPaymentStatus_SelectedIndexChanged">
              <asp:ListItem Value="0" Selected="True">支付状态</asp:ListItem>
              <asp:ListItem Value="1">待支付</asp:ListItem>
              <asp:ListItem Value="2">已支付</asp:ListItem>
            </asp:DropDownList>
          </div>
          <div class="rule-single-select">
            <asp:DropDownList ID="ddlExpressStatus" runat="server" AutoPostBack="True" onselectedindexchanged="ddlExpressStatus_SelectedIndexChanged">
              <asp:ListItem Value="0" Selected="True">发货状态</asp:ListItem>
              <asp:ListItem Value="1">待发货</asp:ListItem>
              <asp:ListItem Value="2">已发货</asp:ListItem>
            </asp:DropDownList>
          </div>
        </div>

      </div>
      <div class="r-list">
        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" onclick="btnSearch_Click"><i class="iconfont icon-search"></i></asp:LinkButton>
      </div>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<div class="table-container">
<asp:Repeater ID="rptList" runat="server">
<HeaderTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
  <tr>
    <th width="8%">选择</th>
    <th align="left">订单号</th>
    <th align="left" width="12%">会员账号</th>
    <th align="left" width="10%">支付方式</th>
    <th align="left" width="10%">配送方式</th>
    <th width="8%">订单状态</th>
    <th width="10%">总金额</th>
    <th align="left" width="16%">下单时间</th>
    <th width="8%">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Enabled='<%#bool.Parse((Convert.ToInt32(Eval("status")) > 3 ).ToString())%>' style="vertical-align:middle;" />
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
    <td><a href="order_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("order_no")%></a></td>
    <td><%#Eval("user_name").ToString() == "" ? "匿名用户" : Eval("user_name").ToString()%></td>
    <td><%#new DTcms.BLL.site_payment().GetTitle(Convert.ToInt32(Eval("payment_id")))%></td>
    <td><%#new DTcms.BLL.express().GetTitle(Convert.ToInt32(Eval("express_id")))%></td>
    <td align="center"><%#GetOrderStatus(Convert.ToInt32(Eval("id")))%></td>
    <td align="center"><%#Eval("order_amount")%></td>
    <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
    <td align="center"><a href="order_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">详细</a></td>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
</table>
</FooterTemplate>
</asp:Repeater>
</div>
<!--/列表-->

<!--内容底部-->
<div class="line20"></div>
<div class="pagelist">
  <div class="l-btns">
    <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);"
                OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
  </div>
  <div id="PageContent" runat="server" class="default"></div>
</div>
<!--/内容底部-->

</form>
</body>
</html>