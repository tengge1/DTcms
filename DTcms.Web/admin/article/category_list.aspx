<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="category_list.aspx.cs" Inherits="DTcms.Web.admin.article.category_list" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>栏目分类</title>
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<style type="text/css">
.tree-list .col-1{ width:6%;text-align:center; }
.tree-list .col-2{ width:6%; }
.tree-list .col-3{ width:52%; }
.tree-list .col-4{ width:12%; }
.tree-list .col-5{ width:12%; }
.tree-list .col-6{ width:12%; text-align:center; }
</style>
<script type="text/javascript">
    $(function () {
        initCategoryHtml('.tree-list', 1); //初始化分类的结构
        $('.tree-list').initCategoryTree(true); //初始化分类的事件
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
  <span>内容管理</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>栏目类别</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"><i class="iconfont icon-more"></i></a>
      <div class="l-list">
        <ul class="icon-list">
          <li><a href="category_edit.aspx?channel_id=<%=this.channel_id %>&action=<%=DTEnums.ActionEnum.Add %>"><i class="iconfont icon-close"></i><span>新增</span></a></li>
          <li><asp:LinkButton ID="btnSave" runat="server" onclick="btnSave_Click"><i class="iconfont icon-save"></i><span>保存</span></asp:LinkButton></li>
          <li><a href="javascript:;" onclick="checkAll(this);"><i class="iconfont icon-check"></i><span>全选</span></a></li>
          <li><asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return ExePostBack('btnDelete','本操作会删除本类别及下属子类别，是否继续？');" onclick="btnDelete_Click"><i class="iconfont icon-delete"></i><span>删除</span></asp:LinkButton></li>
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
      <div class="col col-2">编号</div>
      <div class="col col-3">类别名称</div>
      <div class="col col-4">调用别名</div>
      <div class="col col-5">排序</div>
      <div class="col col-6">操作</div>
    </div>
    <ul>
      <asp:Repeater ID="rptList" runat="server">
      <ItemTemplate>
      <li class="layer-<%#Eval("class_layer")%>">
        <div class="tbody">
          <div class="col col-1">
            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" />
            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
          </div>
          <div class="col col-2">
            <%#Eval("id")%>
          </div>
          <div class="col index col-3">
            <a href="category_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>"><%#Eval("title")%></a>
          </div>
          <div class="col col-4">
            <%#Eval("call_index")%>
          </div>
          <div class="col col-5">
            <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeydown="return checkNumber(event);" />
          </div>
          <div class="col col-6">
            <a href="category_edit.aspx?action=<%#DTEnums.ActionEnum.Add %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>">添加子类</a>
            <a href="category_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>">修改</a>
          </div>
        </div>  
      </li>
      </ItemTemplate>
      </asp:Repeater>
    </ul>
  </div>
</div>

</form>
</body>
</html>
