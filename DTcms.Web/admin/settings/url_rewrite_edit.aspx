<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="url_rewrite_edit.aspx.cs" Inherits="DTcms.Web.admin.settings.url_rewrite_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑URL配置</title>
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
        //添加按钮(点击绑定)
        $("#itemAddButton").click(function () {
            showUrlDialog();
        });
    });

    //创建窗口
    function showUrlDialog(obj) {
        var objNum = arguments.length;
        var d = top.dialog({
            title: 'URL配置信息',
            url: 'dialog/dialog_rewrite.aspx',
            onclose: function () {
                var trHtml = this.returnValue;
                if (trHtml.length > 0) {
                    $("#var_box").append(trHtml);
                }
            }
        }).showModal();
        //检查是否修改状态
        if (objNum == 1) {
            d.data = obj;
        }
    }

    //删除节点
    function delUrlNode(obj) {
        $(obj).parent().parent().remove();
    }
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="url_rewrite_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="url_rewrite_list.aspx"><span>URL配置</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑URL配置</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">URL配置信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>所属频道</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlChannel" runat="server"></asp:DropDownList>
      </div>
      <span class="Validform_checktip">如果该页面不属于任何频道，可以忽略该项</span>
    </dd>
  </dl>
  <dl>
    <dt>页面类型</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlType" runat="server" datatype="*" sucmsg=" ">
            <asp:ListItem Value="">请选择类型</asp:ListItem>
            <asp:ListItem Value="index">首页</asp:ListItem>
            <asp:ListItem Value="list">列表页</asp:ListItem>
            <asp:ListItem Value="category">栏目页</asp:ListItem>
            <asp:ListItem Value="detail">详细页</asp:ListItem>
            <asp:ListItem Value="plugin">插件页</asp:ListItem>
            <asp:ListItem Value="other">其它页</asp:ListItem>
          </asp:DropDownList>
      </div>
      <span class="Validform_checktip">*注意选择正确的面页类型</span>
    </dd>
  </dl>
  <dl>
    <dt>调用名称</dt>
    <dd>
      <asp:TextBox ID="txtName" runat="server" CssClass="input txt" datatype="/^[a-zA-Z0-9\-\_]{2,50}$/" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*调用该条URL配置信息的名称，不可重复</span>
    </dd>
  </dl>
  <dl>
    <dt>生成文件名</dt>
    <dd>
      <asp:TextBox ID="txtPage" runat="server" CssClass="input txt" datatype="*" sucmsg=" " />
      <span class="Validform_checktip">*生成的ASPX页面名称，扩展名必须为.aspx</span>
    </dd>
  </dl>
  <dl>
    <dt>继承类名</dt>
    <dd>
      <asp:TextBox ID="txtInherit" runat="server" CssClass="input txt" datatype="*" sucmsg=" " />
      <span class="Validform_checktip">*该ASPX页面所要继承的类名</span>
    </dd>
  </dl>
  <dl>
    <dt>模板文件名</dt>
    <dd>
      <asp:TextBox ID="txtTemplet" runat="server" CssClass="input txt" datatype="*" sucmsg=" " />
      <span class="Validform_checktip">*该页面的模板名称，扩展名一般是.html</span>
    </dd>
  </dl>
  <dl>
    <dt>每页显示</dt>
    <dd>
      <asp:TextBox ID="txtPageSize" runat="server" CssClass="input small" datatype="n0-9" sucmsg=" " /> 条
      <span class="Validform_checktip">*当该页面进行分页时启用</span>
    </dd>
  </dl>
  <dl>
    <dt>URL表达式</dt>
    <dd>
      <a id="itemAddButton" class="icon-btn"><i class="iconfont icon-add"></i><span>添加表达式</span></a>
      <span class="Validform_checktip">*注意，不添加任何表达式则视为不重写</span>
    </dd>
  </dl>
  <dl>
    <dt></dt>
    <dd>
      <div class="table-container">
          <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="100%">
            <thead>
              <tr>
                <th width="30%">重写表达式</th>
                <th width="30%">正则表达式</th>
                <th width="30%">传输参数</th>
                <th width="10%">操作</th>
              </tr>
            </thead>
            <tbody id="var_box">
              <asp:Repeater ID="rptList" runat="server">
              <ItemTemplate>
              <tr class="td_c">
                <td>
                  <input type="text" name="itemPath" class="td-input" value="<%#Eval("path")%>" style="width:90%;" readonly="readonly" />
                </td>
                <td>
                  <input type="text" name="itemPattern" class="td-input" value="<%#Eval("pattern")%>" style="width:90%;" readonly="readonly" />
                </td>
                <td>
                  <input type="text" name="itemQuerystring" class="td-input" value="<%#Eval("querystring")%>" style="width:90%;" readonly="readonly" />
                </td>
                <td>
                  <a title="编辑" class="img-btn" onclick="showUrlDialog(this);"><i class="iconfont icon-edit"></i></a>
                  <a title="删除" class="img-btn" onclick="delUrlNode(this);"><i class="iconfont icon-delete"></i></a>
                </td>
              </tr>
              </ItemTemplate>
              </asp:Repeater>
            </tbody>
          </table>
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