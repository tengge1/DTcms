<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_group_price.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_group_price" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>会员组价格</title>
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    var api = top.dialog.get(window); //获取父窗体对象
    $(function () {
        //设置窗口按钮及事件
        api.button([{
            value: '确定',
            callback: function () {
                setGroupPrice();
            },
            autofocus: true
        }, {
            value: '取消',
            callback: function () { }
        }
        ]);
        //初始化默认价格
        var sellprice = parseFloat($(api.data).parent().parent().find("input[name='spec_sell_price']").val()); //获取父对象的销售价
        if (sellprice > 0) {
            $("input[name='txtGroupPrice']").each(function () {
                var discount = parseFloat($(this).siblings("input[name='hideDiscount']").val()) / 100; //获得折扣
                $(this).val(ForDight(sellprice * discount, 2)); //计算出价格
            });
        }
        //获取父对象的价格
        var hideGroupPriceStr = $(api.data).parent().find("input[name='hide_group_price']").val();
        if (hideGroupPriceStr.length > 0) { //如果默认有值则用值来赋值
            var json = $.parseJSON(hideGroupPriceStr);
            for (var i = 0; i < json.length; i++) {
                $("input[name='hideGroupId'][value='" + json[i].group_id + "']").siblings("input[name='txtGroupPrice']").val(json[i].price);
            }
        }
    });

    //计算及赋值
    function setGroupPrice() {
        //组合的会员组价格参数
        var groupPriceStr = '';
        //遍历会员组价格
        $("input[name='txtGroupPrice']").each(function (i) {
            var groupid = $(this).siblings("input[name='hideGroupId']").val();
            groupPriceStr += '{"group_id": "' + groupid + '", "price": "' + $(this).val() + '"}';
            if (i < $("input[name='txtGroupPrice']").length - 1) {
                groupPriceStr += ",";
            }
        });
        //赋值给父对象隐藏域
        if (groupPriceStr.length > 0) {
            $(api.data).parent().find("input[name='hide_group_price']").val("[" + groupPriceStr + "]");
        }
        api.close().remove();
        return false;
    }
</script>
</head>

<body>
<form id="form1" runat="server">
<div class="div-content">
<asp:Repeater ID="rptList" runat="server">
  <ItemTemplate>
  <dl>
    <dt><%#Eval("title")%></dt>
    <dd>
      <input type="hidden" name="hideDiscount" value="<%#Eval("discount") %>" />
      <input type="hidden" name="hideGroupId" value="<%#Eval("id") %>" />
      <input type="text" name="txtGroupPrice" class="input small" value="0" onkeydown="return checkForFloat(this,event);" /> 元
      <span class="Validform_checktip">*享受<%#Eval("discount") %>折优惠</span>
    </dd>
  </dl>
  </ItemTemplate>
</asp:Repeater>
</div>
</form>
</body>
</html>