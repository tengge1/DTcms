<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="message_edit.aspx.cs" Inherits="DTcms.Web.admin.users.message_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑短消息</title>
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../editor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="../../editor/ueditor.all.min.js"> </script>
<script type="text/javascript" charset="utf-8" src="../../editor/lang/zh-cn/zh-cn.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化编辑器
        var ue = UE.getEditor('txtContent', {
            serverUrl: '../../tools/upload_ajax.ashx',
            toolbars: [[
                'fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', 'strikethrough', 'removeformat', 'pasteplain', '|', 'forecolor', 'insertorderedlist', 'insertunorderedlist', '|',
                'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', '|',
                'link', 'unlink', 'anchor', '|',
                'simpleupload', 'insertimage', 'scrawl', 'insertvideo', 'music', 'attachment'
            ]]
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="message_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="message_list.aspx"><span>站内消息</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑短消息</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">编辑消息内容</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <div id="div_view" runat="server" visible="false">
      <dl>
        <dt>短信类型</dt>
        <dd><asp:Label ID="labType" runat="server" /></dd>
      </dl>
      <dl>
        <dt>发件人</dt>
        <dd><asp:Label ID="labPostUserName" runat="server" /></dd>
      </dl>
      <dl>
        <dt>收件人</dt>
        <dd><asp:Label ID="labAcceptUserName" runat="server" /></dd>
      </dl>
      <dl>
        <dt>发送时间</dt>
        <dd><asp:Label ID="labPostTime" runat="server" /></dd>
      </dl>
      <dl>
        <dt>阅读状态</dt>
        <dd><asp:Label ID="labIsRead" runat="server" /></dd>
      </dl>
      <dl>
        <dt>阅读时间</dt>
        <dd><asp:Label ID="labReadTime" runat="server" /></dd>
      </dl>
      <dl>
        <dt>标题</dt>
        <dd><asp:Label ID="labTitle" runat="server" /></dd>
      </dl>
      <dl>
        <dt>内容</dt>
        <dd><asp:Literal ID="litContent" runat="server"></asp:Literal></dd>
      </dl>
  </div>

  <div id="div_add" runat="server">
      <dl>
        <dt>收件人</dt>
        <dd>
          <asp:TextBox ID="txtUserName" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
          <span class="Validform_checktip">*输入用户名，以英文逗号“,”分隔开</span>
        </dd>
      </dl>
      <dl>
        <dt>标题</dt>
        <dd>
          <asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*1-100" sucmsg=" "></asp:TextBox>
          <span class="Validform_checktip">*100个字符以内</span>
        </dd>
      </dl>
      <dl>
        <dt>短信内容</dt>
        <dd>
          <textarea id="txtContent" runat="server" style="width:100%;height:320px;"></textarea>
        </dd>
      </dl>
  </div>
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
