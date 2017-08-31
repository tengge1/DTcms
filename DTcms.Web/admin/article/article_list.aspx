<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="article_list.aspx.cs" Inherits="DTcms.Web.admin.article.article_list" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>内容管理</title>
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../../css/pagination.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/jquery.lazyload.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //图片延迟加载
        $(".pic img").lazyload({ effect: "fadeIn" });
        //点击图片链接
        $(".pic img").click(function () {
            var linkUrl = $(this).parent().parent().find(".foot a").attr("href");
            if (linkUrl != "") {
                location.href = linkUrl; //跳转到修改页面
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
  <span>内容列表</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"><i class="iconfont icon-more"></i></a>
      <div class="l-list">
        <ul class="icon-list">
          <li><a href="article_edit.aspx?channel_id=<%=this.channel_id %>&action=<%=DTEnums.ActionEnum.Add %>"><i class="iconfont icon-close"></i><span>新增</span></a></li>
          <li><asp:LinkButton ID="btnSave" runat="server" onclick="btnSave_Click"><i class="iconfont icon-save"></i><span>保存</span></asp:LinkButton></li>
          <li><asp:LinkButton ID="btnAudit" runat="server" OnClientClick="return ExePostBack('btnAudit','审核后前台将显示该信息，确定继续吗？');" onclick="btnAudit_Click"><i class="iconfont icon-survey"></i><span>审核</span></asp:LinkButton></li>
          <li><a href="javascript:;" onclick="checkAll(this);"><i class="iconfont icon-check"></i><span>全选</span></a></li>
          <li><asp:LinkButton ID="btnDelete" runat="server" OnClientClick="return ExePostBack('btnDelete','删除则无法恢复，是否继续？');" onclick="btnDelete_Click"><i class="iconfont icon-delete"></i><span>删除</span></asp:LinkButton></li>
        </ul>
        <div class="menu-list">
          <div class="rule-single-select">
            <asp:DropDownList ID="ddlCategoryId" runat="server" AutoPostBack="True" onselectedindexchanged="ddlCategoryId_SelectedIndexChanged"></asp:DropDownList>
          </div>
          <div class="rule-single-select">
            <asp:DropDownList ID="ddlProperty" runat="server" AutoPostBack="True" onselectedindexchanged="ddlProperty_SelectedIndexChanged">
              <asp:ListItem Value="" Selected="True">所有属性</asp:ListItem>
              <asp:ListItem Value="isLock">待审核</asp:ListItem>
              <asp:ListItem Value="unIsLock">已审核</asp:ListItem>
              <asp:ListItem Value="isMsg">可评论</asp:ListItem>
              <asp:ListItem Value="isTop">置顶</asp:ListItem>
              <asp:ListItem Value="isRed">推荐</asp:ListItem>
              <asp:ListItem Value="isHot">热门</asp:ListItem>
              <asp:ListItem Value="isSlide">幻灯片</asp:ListItem>
            </asp:DropDownList>
          </div>
        </div>
      </div>
      <div class="r-list">
        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" onclick="btnSearch_Click"><i class="iconfont icon-search"></i></asp:LinkButton>
        <asp:LinkButton ID="lbtnViewImg" runat="server" CssClass="img-view" onclick="lbtnViewImg_Click" ToolTip="图像列表视图"><i class="iconfont icon-list-img"></i></asp:LinkButton>
        <asp:LinkButton ID="lbtnViewTxt" runat="server" CssClass="txt-view" onclick="lbtnViewTxt_Click" ToolTip="文字列表视图"><i class="iconfont icon-list-txt"></i></asp:LinkButton>
      </div>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<div class="table-container">
  <!--文字列表-->
  <asp:Repeater ID="rptList1" runat="server" onitemcommand="rptList_ItemCommand">
  <HeaderTemplate>
  <table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
    <tr>
      <th width="6%">选择</th>
      <th align="left">标题</th>
      <th align="left" width="12%">所属类别</th>
      <th align="left" width="16%">发布时间</th>
      <th align="left" width="65">排序</th>
      <th align="left" width="120">属性</th>
      <th width="10%">操作</th>
    </tr>
  </HeaderTemplate>
  <ItemTemplate>
    <tr>
      <td align="center">
        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
        <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
      </td>
      <td><a href="article_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></td>
      <td><%#new DTcms.BLL.article_category().GetTitle(Convert.ToInt32(Eval("category_id")))%></td>
      <td><%#string.Format("{0:g}",Eval("add_time"))%></td>
      <td><asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeydown="return checkNumber(event);" /></td>
      <td>
        <div class="btn-tools">
          <asp:LinkButton ID="lbtnIsMsg" CommandName="lbtnIsMsg" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_msg")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_msg")) == 1 ? "取消评论" : "设置评论"%>'><i class="iconfont icon-comment"></i></asp:LinkButton>
          <asp:LinkButton ID="lbtnIsTop" CommandName="lbtnIsTop" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_top")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_top")) == 1 ? "取消置顶" : "设置置顶"%>'><i class="iconfont icon-top"></i></asp:LinkButton>
          <asp:LinkButton ID="lbtnIsRed" CommandName="lbtnIsRed" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_red")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_red")) == 1 ? "取消推荐" : "设置推荐"%>'><i class="iconfont icon-good"></i></asp:LinkButton>
          <asp:LinkButton ID="lbtnIsHot" CommandName="lbtnIsHot" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_hot")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_hot")) == 1 ? "取消热门" : "设置热门"%>'><i class="iconfont icon-hot"></i></asp:LinkButton>
          <asp:LinkButton ID="lbtnIsSlide" CommandName="lbtnIsSlide" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_slide")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_slide")) == 1 ? "取消幻灯片" : "设置幻灯片"%>'><i class="iconfont icon-pic"></i></asp:LinkButton>
        </div>
      </td>
      <td align="center">
        <a href="article_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>">修改</a>
      </td>
    </tr>
  </ItemTemplate>
  <FooterTemplate>
  <%#rptList1.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
  </table>
  </FooterTemplate>
  </asp:Repeater>
  <!--/文字列表-->

  <!--图片列表-->
  <asp:Repeater ID="rptList2" runat="server" onitemcommand="rptList_ItemCommand">
  <HeaderTemplate>
  <div class="imglist">
    <ul>
  </HeaderTemplate>
  <ItemTemplate>
      <li>
        <div class="details<%#Eval("img_url").ToString() != "" ? "" : " nopic"%>">
          <div class="check">
            <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" />
            <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
          </div>
          <%#Eval("img_url").ToString() != "" ? "<div class=\"pic\"><img src=\"../skin/default/loadimg.gif\" data-original=\"" + Eval("img_url") + "\" /></div><i class=\"absbg\"></i>" : ""%>
          <h1><span><a href="article_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>"><%#Eval("title")%></a></span></h1>
          <div class="remark">
            <%#Eval("zhaiyao").ToString() == "" ? "暂无内容摘要说明..." : Eval("zhaiyao").ToString()%>
          </div>
          <div class="tools">
            <asp:LinkButton ID="lbtnIsMsg" CommandName="lbtnIsMsg" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_msg")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_msg")) == 1 ? "取消评论" : "设置评论"%>'><i class="iconfont icon-comment"></i></asp:LinkButton>
            <asp:LinkButton ID="lbtnIsTop" CommandName="lbtnIsTop" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_top")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_top")) == 1 ? "取消置顶" : "设置置顶"%>'><i class="iconfont icon-top"></i></asp:LinkButton>
            <asp:LinkButton ID="lbtnIsRed" CommandName="lbtnIsRed" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_red")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_red")) == 1 ? "取消推荐" : "设置推荐"%>'><i class="iconfont icon-good"></i></asp:LinkButton>
            <asp:LinkButton ID="lbtnIsHot" CommandName="lbtnIsHot" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_hot")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_hot")) == 1 ? "取消热门" : "设置热门"%>'><i class="iconfont icon-hot"></i></asp:LinkButton>
            <asp:LinkButton ID="lbtnIsSlide" CommandName="lbtnIsSlide" runat="server" CssClass='<%# Convert.ToInt32(Eval("is_slide")) == 1 ? "selected" : ""%>' ToolTip='<%# Convert.ToInt32(Eval("is_slide")) == 1 ? "取消幻灯片" : "设置幻灯片"%>'><i class="iconfont icon-pic"></i></asp:LinkButton>
            <asp:TextBox ID="txtSortId" runat="server" Text='<%#Eval("sort_id")%>' CssClass="sort" onkeydown="return checkNumber(event);" />
          </div>
          <div class="foot">
            <p class="time"><%#string.Format("{0:yyyy-MM-dd HH:mm:ss}", Eval("add_time"))%></p>
            <a href="article_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&channel_id=<%#this.channel_id %>&id=<%#Eval("id")%>" title="编辑"><i class="iconfont icon-pencil"></i></a>
          </div>
        </div>
      </li>
  </ItemTemplate>
  <FooterTemplate>
      <%#rptList2.Items.Count == 0 ? "<div align=\"center\" style=\"font-size:12px;line-height:30px;color:#666;\">暂无记录</div>" : ""%>
    </ul>
  </div>
  </FooterTemplate>
  </asp:Repeater>
  <!--/图片列表-->
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
