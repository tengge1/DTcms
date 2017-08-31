<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_accept.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_accept" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>修改收货信息窗口</title>
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/PCASClass.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    var api = top.dialog.get(window); //获取窗体对象
    var W = api.data;
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
        //初始化省市区
        var mypcas = new PCAS("txtProvince,所属省份", "txtCity,所属城市", "txtArea,所属地区");
        var areaArr = $("#spanArea", W.document).text().split(",");
        if (areaArr.length == 3) {
            mypcas.SetValue(areaArr[0], areaArr[1], areaArr[2]);
        }
        $("#txtAcceptName").val($("#spanAcceptName", W.document).text());
        $("#txtAddress").val($("#spanAddress", W.document).text());
        $("#txtPostCode").val($("#spanPostCode", W.document).text());
        $("#txtMobile").val($("#spanMobile", W.document).text());
        $("#txtTelphone").val($("#spanTelphone", W.document).text());
        $("#txtEmail").val($("#spanEmail", W.document).text());
    });

    //提交表单处理
    function submitForm() {
        var currDocument = $(document); //当前文档
        //验证表单
        if ($("#txtAcceptName").val() == "") {
            top.dialog({
                title: '提示',
                content: '请填写收货人姓名！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtAcceptName", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#txtArea").val() == "") {
            top.dialog({
                title: '提示',
                content: '请选择所在地区！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtProvince", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#txtAddress").val() == "") {
            top.dialog({
                title: '提示',
                content: '请填写详细的送货地址！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtAddress", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#txtMobile").val() == "" && $("#txtTelphone").val() == "") {
            top.dialog({
                title: '提示',
                content: '联系手机或电话至少填写一项！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtMobile", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        //下一步，AJAX提交表单
        var postData = {
            "order_no": $("#spanOrderNo", W.document).text(), "edit_type": "edit_accept_info",
            "accept_name": $("#txtAcceptName").val(), "province": $("#txtProvince").val(),
            "city": $("#txtCity").val(), "area": $("#txtArea").val(), "address": $("#txtAddress").val(),
            "post_code": $("#txtPostCode").val(), "mobile": $("#txtMobile").val(), "telphone": $("#txtTelphone").val(),
            "email": $("#txtEmail").val()
        };
        //发送AJAX请求
        W.sendAjaxUrl(api, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
        return false;
    }

</script>
</head>

<body>
<div class="div-content">
    <dl>
      <dt>收件人姓名</dt>
      <dd><input type="text" id="txtAcceptName" class="input txt" /> <span class="Validform_checktip">*</span></dd>
    </dl>
    <dl>
      <dt>所属省市</dt>
      <dd>
        <select id="txtProvince" name="txtProvince" class="select"></select>
        <select id="txtCity" name="txtCity" class="select"></select>
        <select id="txtArea" name="txtArea" class="select"></select>
      </dd>
    </dl>
    <dl>
      <dt>详细地址</dt>
      <dd><input type="text" id="txtAddress" class="input normal" /> <span class="Validform_checktip">*</span></dd>
    </dl>
    <dl>
      <dt>邮政编码</dt>
      <dd><input type="text" id="txtPostCode" class="input txt" /></dd>
    </dl>
    <dl>
      <dt>联系手机</dt>
      <dd><input type="text" id="txtMobile" class="input txt" /> <span class="Validform_checktip">*</span></dd>
    </dl>
    <dl>
      <dt>联系电话</dt>
      <dd><input type="text" id="txtTelphone" class="input txt" /></dd>
    </dl>
    <dl>
      <dt>电子邮箱</dt>
      <dd><input type="text" id="txtEmail" class="input txt" /></dd>
    </dl>
</div>
</body>
</html>