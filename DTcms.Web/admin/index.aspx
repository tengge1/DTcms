<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="DTcms.Web.admin.index" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no">
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>后台管理中心</title>
<link rel="stylesheet" type="text/css" href="../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../scripts/jquery/jquery.nicescroll.js"></script>
<script type="text/javascript" charset="utf-8" src="../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="js/layindex.js"></script>
<script type="text/javascript" charset="utf-8" src="js/common.js"></script>
<script type="text/javascript">
    //页面加载完成时
    $(function () {
        //检测IE
        if ('undefined' == typeof (document.body.style.maxHeight)) {
            window.location.href = 'ie6update.html';
        }
    });
</script>
</head>

<body class="indexbody">
<form id="form1" runat="server">
  <!--全局菜单-->
  <a class="btn-paograms" href="javascript:;" onclick="togglePopMenu();">
    <i class="iconfont icon-list-fill"></i>
  </a>
  <div id="pop-menu" class="pop-menu">
    <div class="pop-box">
      <h1 class="title"><i class="iconfont icon-setting"></i>导航菜单</h1>
      <i class="close iconfont icon-remove" onclick="togglePopMenu();"></i>
      <div class="list-box"></div>
    </div>
  </div>
  <!--/全局菜单-->

  <div class="main-top">
    <a class="icon-menu"><i class="iconfont icon-nav"></i></a>
    <div id="main-nav" class="main-nav"></div>
    <div class="nav-right">
      <div class="info">
        <h4>
          <%if (!string.IsNullOrEmpty(admin_info.avatar)){%>
          <img src="<%=admin_info.avatar%>" />
          <%}else{%>
          <i class="iconfont icon-user"></i>
          <%}%>
        </h4>
        <span>
          您好，<%=admin_info.user_name%><br />
          <%=new DTcms.BLL.manager_role().GetTitle(admin_info.role_id) %>
        </span>
      </div>
      <div class="option">
        <i class="iconfont icon-arrow-down"></i>
        <div class="drop-wrap">
          <ul class="item">
            <li>
              <a href="../" target="_blank">预览网站</a>
            </li>
            <li>
              <a href="center.aspx" target="mainframe">管理中心</a>
            </li>
            <li>
              <a href="manager/manager_pwd.aspx" onclick="linkMenuTree(false, '');" target="mainframe">修改密码</a>
            </li>
            <li>
              <asp:LinkButton ID="lbtnExit" runat="server" onclick="lbtnExit_Click">注销登录</asp:LinkButton>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </div>
  
  <div class="main-left">
    <h1 class="logo"></h1>
    <div id="sidebar-nav" class="sidebar-nav"></div>
  </div>
  
  <div class="main-container">
    <iframe id="mainframe" name="mainframe" frameborder="0" src="center.aspx"></iframe>
  </div>

</form>
</body>
</html>