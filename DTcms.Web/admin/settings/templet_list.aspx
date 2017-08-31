<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="templet_list.aspx.cs" Inherits="DTcms.Web.admin.settings.templet_list" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>模板管理</title>
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //只能选中一项
        $(".checkall input").click(function () {
            $(".checkall input").prop("checked", false);
            $(this).prop("checked", true);
        });
        //管理模板检测
        $("#btnManage").click(function () {
            if ($(".checkall input:checked").size() < 1) {
                top.dialog({
                    title: '提示',
                    content: '对不起，请选中您要管理的模板！',
                    okValue: '确定',
                    ok: function () { }
                }).showModal();
                return false;
            }
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i class="iconfont icon-up"></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>界面管理</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>模板管理</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"><i class="iconfont icon-more"></i></a>
      <div class="l-list">
        <ul class="icon-list">
          <li><asp:LinkButton ID="btnManage" runat="server" onclick="btnManage_Click"><i class="iconfont icon-catalog"></i><span>管理</span></asp:LinkButton></li>
        </ul>
        <div class="menu-list">
          <div class="rule-single-select">
            <asp:DropDownList ID="ddlSitePath" runat="server" AutoPostBack="True" onselectedindexchanged="ddlSitePath_SelectedIndexChanged"></asp:DropDownList>
          </div>
        </div>
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
      <th align="left" width="20%">模板名称</th>
      <th width="13%">作者</th>
      <th width="16%">创建日期</th>
      <th width="12%">版本号</th>
      <th align="left">适用版本</th>
      <th width="10%">操作</th>
    </tr>
  </HeaderTemplate>
  <ItemTemplate>
    <tr>
      <td align="center">
        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
        <asp:HiddenField ID="hideSkinName" runat="server" Value='<%#Eval("skinname")%>' />
      </td>
      <td><%#Eval("name")%>(<%#Eval("skinname")%>)</td>
      <td align="center"><%#Eval("author")%></td>
      <td align="center"><%#Eval("createdate")%></td>
      <td align="center"><%#Eval("version")%></td>
      <td><%#Eval("fordntver")%></td>
      <td align="center"><a href="templet_file_list.aspx?skin=<%#Eval("skinname")%>">管理</a></td>
    </tr>
  </ItemTemplate>
  <FooterTemplate>
    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
  </table>
  </FooterTemplate>
  </asp:Repeater>
</div>
<!--/列表-->

</form>
</body>
</html>