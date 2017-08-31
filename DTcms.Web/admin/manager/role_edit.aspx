<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_edit.aspx.cs" Inherits="DTcms.Web.admin.manager.role_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑角色</title>
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
<style type="text/css">
.tree-list .col-1{ padding-left:1%; width:39%; }
.tree-list .col-2{ width:50%; }
.tree-list .col-3{ width:10%; text-align:center; }
</style>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化分类的结构
        initCategoryHtml('.tree-list', 1);
        //初始化分类的事件
        $('.tree-list').initCategoryTree(true);
        //是否启用权限
        if ($("#ddlRoleType").find("option:selected").attr("value") == 1) {
            $(".tree-list").find("input[type='checkbox']").prop("disabled", true);
        }
        $("#ddlRoleType").change(function () {
            if ($(this).find("option:selected").attr("value") == 1) {
                $(".tree-list").find("input[type='checkbox']").prop("checked", false);
                $(".tree-list").find("input[type='checkbox']").prop("disabled", true);
            } else {
                $(".tree-list").find("input[type='checkbox']").prop("disabled", false);
            }
        });
        //权限全选
        $("input[name='checkAll']").click(function () {
            if ($(this).prop("checked") == true) {
                $(this).parent().siblings(".col").find("input[type='checkbox']").prop("checked", true);
            } else {
                $(this).parent().siblings(".col").find("input[type='checkbox']").prop("checked", false);
            }
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="role_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="manager_list.aspx"><span>管理员</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="role_list.aspx"><span>管理角色</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑角色</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">编辑角色信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>角色类型</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlRoleType" runat="server" datatype="*" errormsg="请选择角色类型！" sucmsg=" "></asp:DropDownList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>角色名称</dt>
    <dd><asp:TextBox ID="txtRoleName" runat="server" CssClass="input normal" datatype="*1-100" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*角色中文名称，100字符内</span></dd>
  </dl>   
  <dl>
    <dt>管理权限</dt>
    <dd>

      <div class="tree-list">
        <div class="thead">
          <div class="col col-1">导航名称</div>
          <div class="col col-2">权限分配</div>
          <div class="col col-3">全选</div>
        </div>
        <ul>
          <asp:Repeater ID="rptList" runat="server" onitemdatabound="rptList_ItemDataBound">
          <ItemTemplate>
          <li class="layer-<%#Eval("class_layer")%>">
            <div class="tbody">
              <div class="col index col-1">
                <asp:HiddenField ID="hidName" Value='<%#Eval("name") %>' runat="server" />
                <asp:HiddenField ID="hidActionType" Value='<%#Eval("action_type") %>' runat="server" />
                <%#Eval("title")%>
              </div>
              <div class="col col-2">
                <asp:CheckBoxList ID="cblActionType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="cbllist"></asp:CheckBoxList>
              </div>
              <div class="col col-3">
                <input name="checkAll" type="checkbox" />
              </div>
            </div>
          </li>
          </ItemTemplate>
          </asp:Repeater>
        </ul>
      </div>

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
