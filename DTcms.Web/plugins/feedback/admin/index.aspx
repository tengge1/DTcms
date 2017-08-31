<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.Plugin.Feedback.admin.index" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>在线留言管理</title>
<link rel="stylesheet" type="text/css" href="../../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../../../css/pagination.css" />
<link rel="stylesheet" type="text/css" href="../../../<%=sysConfig.webmanagepath %>/skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../../../<%=sysConfig.webmanagepath %>/skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=sysConfig.webmanagepath %>/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=sysConfig.webmanagepath %>/js/common.js"></script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i class="iconfont icon-up"></i><span>返回上一页</span></a>
  <a href="../../../<%=sysConfig.webmanagepath %>/center.aspx" class="home"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>插件管理</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>在线留言</span>
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
          <li><asp:LinkButton ID="lbtnUnLock" runat="server" OnClientClick="return ExePostBack('lbtnUnLock','审核后前台将显示该信息，确定继续吗？');" onclick="lbtnUnLock_Click"><i class="iconfont icon-survey"></i><span>审核</span></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick="return ExePostBack('lbtnDelete');" onclick="lbtnDelete_Click"><i class="iconfont icon-delete"></i><span>删除</span></asp:LinkButton></li>
        </ul>
      </div>
      <div class="r-list">
        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" onclick="lbtnSearch_Click"><i class="iconfont icon-search"></i></asp:LinkButton>
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
    <th align="left">标题</th>
    <th width="16%" align="left">所属站点</th>
    <th width="12%" align="left">用户</th>
    <th width="16%" align="left">发布时间</th>
    <th width="8%">状态</th>
    <th width="10%">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
    <td><a href="reply.aspx?id=<%#Eval("id")%>"><%#Eval("title")%><%# Convert.ToInt32(Eval("is_lock")) == 1 ? "(未审核)" : ""%></a></td>
    <td><%#new DTcms.BLL.sites().GetTitle(int.Parse(Eval("site_id").ToString()))%></td>
    <td><%#Eval("user_name")%></td>
    <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
    <td align="center">
      <%#Eval("reply_content").ToString() == "" ? "未回复" : "已回复"%>
    </td>
    <td align="center"><a href="reply.aspx?id=<%#Eval("id")%>">回复</a></td>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
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
