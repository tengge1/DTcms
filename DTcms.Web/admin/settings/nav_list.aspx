<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nav_list.aspx.cs" Inherits="DTcms.Web.admin.settings.nav_list" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>后台导航管理</title>
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<style type="text/css">
.tree-list .col-1{ width:6%; text-align:center; }
.tree-list .col-2{ width:14%; white-space:nowrap;word-break:break-all;overflow:hidden; }
.tree-list .col-3{ width:46%; white-space:nowrap;word-break:break-all;overflow:hidden; }
.tree-list .col-4{ width:6%; text-align:center; }
.tree-list .col-5{ width:8%; text-align:center; }
.tree-list .col-6{ width:8%; }
.tree-list .col-7{ width:12%; text-align:center; }
</style>
<script type="text/javascript">
    $(function () {
        //初始化分类的结构
        initCategoryHtml('.tree-list', 1);
        //初始化分类的事件
        $('.tree-list').initCategoryTree(true);
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
  <span>后台导航管理</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"><i class="iconfont icon-more"></i></a>
      <div class="l-list">
        <ul class="icon-list">
          <li><a href="nav_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>"><i class="iconfont icon-close"></i><span>新增</span></a></li>
          <li><asp:LinkButton ID="btnSave" runat="server" onclick="btnSave_Click"><i class="iconfont icon-save"></i><span>保存</span></asp:LinkButton></li>
          <li><a href="javascript:;" onclick="checkAll(this);"><i class="iconfont icon-check"></i><span>全选</span></a></li>
          <li><asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return ExePostBack('btnDelete','本操作会删除本导航及下属子导航，是否继续？');" onclick="btnDelete_Click"><i class="iconfont icon-delete"></i><span>删除</span></asp:LinkButton></li>
        </ul>
      </div>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<div class="table-container">
  <div class="tree-list">
    <div class="thead">
      <div class="col col-1">选择</div>
      <div class="col col-2">调用名称</div>
      <div class="col col-3">标题</div>
      <div class="col col-4">显示</div>
      <div class="col col-5">默认</div>
      <div class="col col-6">排序</div>
      <div class="col col-7">操作</div>
    </div>
    <ul>
      <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate>
      <li class="layer-<%#Eval("class_layer")%>">
        <div class="tbody">
          <div class="col col-1">
            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" Enabled='<%#bool.Parse((Convert.ToInt32(Eval("is_sys"))==0 ).ToString())%>' style="vertical-align:middle;" />
            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
          </div>
          <div class="col col-2">
            <%#Eval("name")%>
          </div>
          <div class="col index col-3">
            <a href="nav_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("title")%></a>
            <%#Eval("link_url").ToString() == "" ? "" : "(链接：" + Eval("link_url") + ")"%>
          </div>
          <div class="col col-4">
            <%#Convert.ToInt32(Eval("is_lock")) == 0 ? "√" : "×"%>
          </div>
          <div class="col col-5">
            <%#Convert.ToInt32(Eval("is_sys")) == 1 ? "√" : "×"%>
          </div>
          <div class="col col-6">
            <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeydown="return checkNumber(event);" />
          </div>
          <div class="col col-7">
            <a href="nav_edit.aspx?action=<%#DTEnums.ActionEnum.Add %>&id=<%#Eval("id")%>">添子级</a>
            <a href="nav_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a>
          </div>
        </div>
      </li>
      </ItemTemplate>
      </asp:Repeater>
    </ul>
  </div>
</div>
<!--/列表-->

</form>
</body>
</html>
