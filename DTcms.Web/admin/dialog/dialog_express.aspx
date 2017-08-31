<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_express.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_express" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>订单发货窗口</title>
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    var api = top.dialog.get(window); //获取窗体对象
    var W = api.data; //获取父对象
    //页面加载完成执行
    $(function () {
        //设置按钮及事件
        api.button([{
            value: '确定',
            callback: function () {
                submitForm();
            },
            autofocus: true
        }, {
            value: '取消',
            callback: function () { }
        }]);
    });

    //提交表单处理
    function submitForm() {
        var currDocument = $(document); //当前文档
        //验证表单
        if ($("#ddlExpressId").val() == "") {
            top.dialog({
                title: '提示',
                content: '请选择配送方式！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#ddlExpressId", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        //组合参数
        var postData = {
            "order_no": $("#spanOrderNo", W.document).text(), "edit_type": "order_express",
            "express_id": $("#ddlExpressId").val(), "express_no": $("#txtExpressNo").val()
        };
        //判断是否需要输入物流单号
        if ($("#txtExpressNo").val() == "") {
            top.dialog({
                title: '提示',
                content: '您确定不填写物流单号吗？',
                okValue: '确定',
                ok: function () {
                    //发送AJAX请求
                    W.sendAjaxUrl(api, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                },
                cancelValue: '取消',
                cancel: function () {
                    $("#txtExpressNo", currDocument).focus();
                }
            }).showModal(api);
            return false;
        } else {
            //发送AJAX请求
            W.sendAjaxUrl(api, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
            return false;
        }
        return false;
    }
</script>
</head>

<body>
<form id="form1" runat="server">
<div class="div-content">
  <dl>
    <dt>更改配送方式</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlExpressId" runat="server"></asp:DropDownList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>发货物流单号</dt>
    <dd><asp:TextBox ID="txtExpressNo" runat="server" CssClass="input txt"></asp:TextBox></dd>
  </dl>
</div>
</form>
</body>
</html>