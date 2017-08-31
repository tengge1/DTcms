<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_edit.aspx.cs" Inherits="DTcms.Web.admin.users.user_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑会员</title>
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化上传控件
        $(".upload-img").InitUploader({ sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf" });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="user_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="user_list.aspx"><span>会员管理</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑会员</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">基本资料</a></li>
        <li><a href="javascript:;">账户信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>所属站点</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlSiteId" runat="server" datatype="*" errormsg="请选择所属站点！" sucmsg=" "></asp:DropDownList>
      </div>
      <span class="Validform_checktip">*支持会员跨站登录。</span>
    </dd>
  </dl>
  <dl>
    <dt>所属组别</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlGroupId" runat="server" datatype="*" errormsg="请选择组别" sucmsg=" "></asp:DropDownList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>用户状态</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="0" Selected="True">正常</asp:ListItem>
        <asp:ListItem Value="1">待验证</asp:ListItem>
        <asp:ListItem Value="2">待审核</asp:ListItem>
        <asp:ListItem Value="3">禁用</asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <span class="Validform_checktip">*禁用账户无法登录</span>
    </dd>
  </dl>
  <dl>
    <dt>用户名</dt>
    <dd><asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" " ajaxurl="../../tools/admin_ajax.ashx?action=username_validate"></asp:TextBox> <span class="Validform_checktip">*登录的用户名，支持中文</span></dd>
  </dl> 
  <dl>
    <dt>登录密码</dt>
    <dd><asp:TextBox ID="txtPassword" runat="server" CssClass="input normal" TextMode="Password" datatype="*6-20" nullmsg="请设置密码" errormsg="密码范围在6-20位之间" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*登录的密码，至少6位</span></dd>
  </dl>
  <dl>
    <dt>确认密码</dt>
    <dd><asp:TextBox ID="txtPassword1" runat="server" CssClass="input normal" TextMode="Password" datatype="*" recheck="txtPassword" nullmsg="请再输入一次密码" errormsg="两次输入的密码不一致" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*再次输入密码</span></dd>
  </dl>
  <dl>
    <dt>邮箱账号</dt>
    <dd><asp:TextBox ID="txtEmail" runat="server" CssClass="input normal" datatype="*0-50" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*取回密码时用到</span></dd>
  </dl>
  <dl>
    <dt>用户昵称</dt>
    <dd><asp:TextBox ID="txtNickName" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>上传头像</dt>
    <dd>
      <asp:TextBox ID="txtAvatar" runat="server" CssClass="input normal upload-path"></asp:TextBox>
      <div class="upload-box upload-img"></div>
    </dd>
  </dl>
  <dl>
    <dt>用户性别</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="保密" Selected="True">保密</asp:ListItem>
        <asp:ListItem Value="男">男</asp:ListItem>
        <asp:ListItem Value="女">女</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>生日日期</dt>
    <dd>
      <asp:TextBox ID="txtBirthday" runat="server" CssClass="input rule-date-input" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})" datatype="/^\s*$|^\d{4}\-\d{1,2}\-\d{1,2}$/" errormsg="请选择正确的日期" sucmsg=" " />
    </dd>
  </dl>
  <dl>
    <dt>手机号码</dt>
    <dd><asp:TextBox ID="txtMobile" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>电话号码</dt>
    <dd><asp:TextBox ID="txtTelphone" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>QQ号码</dt>
    <dd><asp:TextBox ID="txtQQ" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>MSN</dt>
    <dd><asp:TextBox ID="txtMsn" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
  <dl>
    <dt>通讯地址</dt>
    <dd><asp:TextBox ID="txtAddress" runat="server" CssClass="input normal"></asp:TextBox></dd>
  </dl>
</div>

<div class="tab-content" style="display:none;">
  <dl>
    <dt>账户金额</dt>
    <dd>
      <asp:TextBox ID="txtAmount" runat="server" CssClass="input small" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" ">0</asp:TextBox> 元
      <span class="Validform_checktip">*账户上的余额</span>
    </dd>
  </dl>
  <dl>
    <dt>账户积分</dt>
    <dd>
      <asp:TextBox ID="txtPoint" runat="server" CssClass="input small" datatype="n" sucmsg=" ">0</asp:TextBox> 分
      <span class="Validform_checktip">*积分也可做为交易</span>
    </dd>
  </dl>
  <dl>
    <dt>升级经验值</dt>
    <dd>
      <asp:TextBox ID="txtExp" runat="server" CssClass="input small" datatype="n" sucmsg=" ">0</asp:TextBox>
      <span class="Validform_checktip">*根据积分计算得来，与积分不同的是只增不减</span>
    </dd>
  </dl>
  <dl>
    <dt>注册时间</dt>
    <dd><asp:Label ID="lblRegTime" Text="-" runat="server"></asp:Label></dd>
  </dl>
  <dl>
    <dt>注册IP</dt>
    <dd><asp:Label ID="lblRegIP" Text="-" runat="server"></asp:Label></dd>
  </dl>
  <dl>
    <dt>最近登录时间</dt>
    <dd><asp:Label ID="lblLastTime" Text="-" runat="server"></asp:Label></dd>
  </dl>
  <dl>
    <dt>最近登录IP</dt>
    <dd><asp:Label ID="lblLastIP" Text="-" runat="server"></asp:Label></dd>
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