<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="attribute_field_edit.aspx.cs" Inherits="DTcms.Web.admin.channel.attribute_field_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑字段</title>
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
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="attribute_field_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="attribute_field_list.aspx"><span>扩展字段管理</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑字段</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">字段信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>控件类型</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList ID="ddlControlType" runat="server" datatype="*" 
              errormsg="请选择控件类型！" sucmsg=" " AutoPostBack="True" 
              onselectedindexchanged="ddlControlType_SelectedIndexChanged">
              <asp:ListItem Value="">请选择类型...</asp:ListItem>
              <asp:ListItem Value="single-text">单行文本</asp:ListItem>
              <asp:ListItem Value="multi-text">多行文本</asp:ListItem>
              <asp:ListItem Value="editor">编辑器</asp:ListItem>
              <asp:ListItem Value="images">图片上传</asp:ListItem>
              <asp:ListItem Value="video">视频上传</asp:ListItem>
              <asp:ListItem Value="number">数字</asp:ListItem>
              <asp:ListItem Value="datetime">时间日期</asp:ListItem>
              <asp:ListItem Value="checkbox">复选框</asp:ListItem>
              <asp:ListItem Value="multi-radio">多项单选</asp:ListItem>
              <asp:ListItem Value="multi-checkbox">多项多选</asp:ListItem>
          </asp:DropDownList>
       </div>
     </dd>
  </dl>

  <dl>
    <dt>排序数字</dt>
    <dd><asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox> <span class="Validform_checktip">*数字，越小越向前</span></dd>
  </dl>

  <dl>
    <dt>字段列名</dt>
    <dd><asp:TextBox ID="txtName" runat="server" CssClass="input normal" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" sucmsg=" " ajaxurl="../../tools/admin_ajax.ashx?action=attribute_field_validate"></asp:TextBox> <span class="Validform_checktip">*字母、下划线，不可修改</span></dd>
  </dl>

  <dl>
    <dt>字段标题</dt>
    <dd><asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*中文标题，做为备注</span></dd>
  </dl>
  
  <dl>
    <dt>是否必填</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsRequired" runat="server" />
      </div>
    </dd>
  </dl>

  <dl id="dlIsPassWord" runat="server" class="single-text-tr">
    <dt>是否密码框</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsPassword" runat="server" />
      </div>
    </dd>
  </dl>

  <dl id="dlIsHtml" runat="server" class="multi-text-tr">
    <dt>是否允许HTML</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsHtml" runat="server" />
      </div>
    </dd>
  </dl>

  <dl id="dlEditorType" runat="server" class="editor-tr">
    <dt>编辑器类型</dt>
    <dd>
      <div class="rule-multi-radio">
          <asp:RadioButtonList ID="rblEditorType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
              <asp:ListItem Value="0" Selected="True">标准型</asp:ListItem>
              <asp:ListItem Value="1">简洁型</asp:ListItem>
          </asp:RadioButtonList>
      </div>
    </dd>
  </dl>

  <dl id="dlDataType" runat="server" class="multi-radio-tr">
    <dt>字段类型</dt>
    <dd>
       <div class="rule-multi-radio">
          <asp:RadioButtonList ID="rblDataType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
              <asp:ListItem Value="nvarchar" Selected="True">字符串</asp:ListItem>
              <asp:ListItem Value="int">整型数字</asp:ListItem>
          </asp:RadioButtonList>
      </div>
     </dd>
   </dl>

   <dl id="dlDataLength" runat="server" class="single-text-tr multi-text-tr multi-radio-tr multi-checkbox-tr">
    <dt>字符串长度</dt>
    <dd><asp:TextBox ID="txtDataLength" runat="server" CssClass="input small" datatype="n" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*数字，默认50个字符</span></dd>
   </dl>

   <dl id="dlDataPlace" runat="server" class="number-tr">
    <dt>小数点位数</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList ID="ddlDataPlace" runat="server">
              <asp:ListItem Value="0">无小数点</asp:ListItem>
              <asp:ListItem Value="1">一位</asp:ListItem>
              <asp:ListItem Value="2">二位</asp:ListItem>
              <asp:ListItem Value="3">三位</asp:ListItem>
              <asp:ListItem Value="4">四位</asp:ListItem>
              <asp:ListItem Value="5">五位</asp:ListItem>
          </asp:DropDownList>
       </div>
       <span class="Validform_checktip">*无小数点为整型，否则浮点数</span>
     </dd>
   </dl>

   <dl id="dlItemOption" runat="server" class="multi-radio-tr multi-checkbox-tr">
    <dt>选项列表</dt>
    <dd>
      <asp:TextBox ID="txtItemOption" runat="server" CssClass="input" TextMode="MultiLine" datatype="*" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*以换行为一行</span>
      <div>*填写说明：选项名称|值，以回车换行为一行。</div>
    </dd>
  </dl>

   <dl>
    <dt>默认值</dt>
    <dd><asp:TextBox ID="txtDefaultValue" runat="server" CssClass="input normal"></asp:TextBox> <span class="Validform_checktip">*控件的默认字符，可为空</span></dd>
  </dl>

  <dl id="dlValidPattern" runat="server">
    <dt>验证正则表达式</dt>
    <dd><asp:TextBox ID="txtValidPattern" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox> <span class="Validform_checktip">*不填写则不验证</span></dd>
  </dl>

  <dl>
    <dt>验证提示信息</dt>
    <dd><asp:TextBox ID="txtValidTipMsg" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox></dd>
  </dl>

  <dl id="dlValidErrorMsg" runat="server">
    <dt>验证失败信息</dt>
    <dd><asp:TextBox ID="txtValidErrorMsg" runat="server" CssClass="input" TextMode="MultiLine"></asp:TextBox></dd>
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
