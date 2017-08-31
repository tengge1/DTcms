<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="order_edit.aspx.cs" Inherits="DTcms.Web.admin.order.order_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>查看订单</title>
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
        $("#btnConfirm").click(function () { OrderConfirm(); });   //确认订单
        $("#btnPayment").click(function () { OrderPayment(); });   //确认付款
        $("#btnExpress").click(function () { OrderExpress(); });   //确认发货
        $("#btnComplete").click(function () { OrderComplete(); }); //完成订单
        $("#btnCancel").click(function () { OrderCancel(); });     //取消订单
        $("#btnInvalid").click(function () { OrderInvalid(); });   //作废订单
        $("#btnPrint").click(function () { OrderPrint(); });       //打印订单

        $("#btnEditAcceptInfo").click(function () { EditAcceptInfo(); }); //修改收货信息
        $("#btnEditRemark").click(function () { EditOrderRemark(); });    //修改订单备注
        $("#btnEditRealAmount").click(function () { EditRealAmount(); }); //修改商品总金额
        $("#btnEditExpressFee").click(function () { EditExpressFee(); }); //修改配送费用
        $("#btnEditPaymentFee").click(function () { EditPaymentFee(); }); //修改支付手续费
        $("#btnEditInvoiceTaxes").click(function () { EditInvoiceTaxes(); }); //修改发票税金
    });
    //确认订单
    function OrderConfirm() {
        var winDialog = top.dialog({
            title: '提示',
            content: '确认订单后将无法修改金额，确认要继续吗？',
            okValue: '确定',
            ok: function () {
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_confirm" };
                //发送AJAX请求
                sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }
    //确认付款
    function OrderPayment() {
        var winDialog = top.dialog({
            title: '提示',
            content: '操作提示信息：<br />1、该订单使用在线支付方式，付款成功后自动确认；<br />2、如客户确实已打款而没有自动确认可使用该功能；<br />3、确认付款后无法修改金额，确认要继续吗？',
            okValue: '确定',
            ok: function () {
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_payment" };
                //发送AJAX请求
                sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }
    //确认发货
    function OrderExpress() {
        var winDialog = top.dialog({
            title: '提示',
            url: 'dialog/dialog_express.aspx?order_no=' + $("#spanOrderNo").text(),
            width: 450,
            data: window //传入当前窗口
        }).showModal();
    }
    //完成订单
    function OrderComplete() {
        var winDialog = top.dialog({
            title: '完成订单',
            content: '订单完成后，订单处理完毕，确认要继续吗？',
            button: [{
                value: '确定',
                callback: function () {
                    var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_complete" };
                    //发送AJAX请求
                    sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                    return false;
                },
                autofocus: true
            }, {
                value: '取消',
                callback: function () { }
            }]
        }).showModal();
    }
    //取消订单
    function OrderCancel() {
        var winDialog = top.dialog({
            title: '取消订单',
            content: '操作提示信息：<br />1、匿名用户，请线下与客户沟通；<br />2、会员用户，自动检测退还金额或积分到账户；<br />3、请单击相应按钮继续下一步操作！',
            button: [{
                value: '检测退还',
                callback: function () {
                    var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_cancel", "check_revert": 1 };
                    //发送AJAX请求
                    sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                    return false;
                },
                autofocus: true
            }, {
                value: '直接取消',
                callback: function () {
                    var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_cancel", "check_revert": 0 };
                    //发送AJAX请求
                    sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                    return false;
                }
            }, {
                value: '关闭',
                callback: function () { }
            }]
        }).showModal();
    }
    //作废订单
    function OrderInvalid() {
        var winDialog = top.dialog({
            title: '取消订单',
            content: '操作提示信息：<br />1、匿名用户，请线下与客户沟通；<br />2、会员用户，自动检测退还金额或积分到账户；<br />3、请单击相应按钮继续下一步操作！',
            button: [{
                value: '检测退还',
                callback: function () {
                    var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_invalid", "check_revert": 1 };
                    //发送AJAX请求
                    sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                    return false;
                },
                autofocus: true
            }, {
                value: '直接作废',
                callback: function () {
                    var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "order_invalid", "check_revert": 0 };
                    //发送AJAX请求
                    sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                    return false;
                }
            }, {
                value: '关闭',
                callback: function () { }
            }]
        }).showModal();
    }
    //打印订单
    function OrderPrint() {
        var winDialog = top.dialog({
            title: '打印订单',
            url: 'dialog/dialog_print.aspx?order_no=' + $("#spanOrderNo").text(),
            width: 850
        }).showModal();
    }
    //修改收货信息
    function EditAcceptInfo() {
        var winDialog = top.dialog({
            title: '修改收货信息',
            url: 'dialog/dialog_accept.aspx',
            width: 550,
            height: 320,
            data: window //传入当前窗口
        }).showModal();
    }
    //修改订单备注
    function EditOrderRemark() {
        var winDialog = top.dialog({
            title: '订单备注',
            content: '<textarea id="txtOrderRemark" name="txtOrderRemark" rows="2" cols="20" class="input">' + $("#divRemark").html() + '</textarea>',
            okValue: '确定',
            ok: function () {
                var remark = $("#txtOrderRemark", parent.document).val();
                if (remark == "") {
                    top.dialog({
                        title: '提示',
                        content: '对不起，请输入订单备注内容！',
                        okValue: '确定',
                        ok: function () { }
                    }).showModal(winDialog);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_order_remark", "remark": remark };
                //发送AJAX请求
                sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }

    //修改商品总金额
    function EditRealAmount() {
        var winDialog = top.dialog({
            title: '请修改商品总金额',
            content: '<input id="txtDialogAmount" type="text" value="' + $("#spanRealAmountValue").text() + '" class="input" />',
            okValue: '确定',
            ok: function () {
                var amount = $("#txtDialogAmount", parent.document).val();
                if (!checkIsMoney(amount)) {
                    top.dialog({
                        title: '提示',
                        content: '对不起，请输入正确的商品金额！',
                        okValue: '确定',
                        ok: function () { }
                    }).showModal(winDialog);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_real_amount", "real_amount": amount };
                //发送AJAX请求
                sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }
    //修改配送费用
    function EditExpressFee() {
        var winDialog = top.dialog({
            title: '请修改配送费用',
            content: '<input id="txtDialogAmount" type="text" value="' + $("#spanExpressFeeValue").text() + '" class="input" />',
            okValue: '确定',
            ok: function () {
                var amount = $("#txtDialogAmount", parent.document).val();
                if (!checkIsMoney(amount)) {
                    top.dialog({
                        title: '提示',
                        content: '对不起，请输入正确的配送金额！',
                        okValue: '确定',
                        ok: function () { }
                    }).showModal(winDialog);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_express_fee", "express_fee": amount };
                //发送AJAX请求
                sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }
    //修改手续费用
    function EditPaymentFee() {
        var winDialog = top.dialog({
            title: '请修改支付手续费用',
            content: '<input id="txtDialogAmount" type="text" value="' + $("#spanPaymentFeeValue").text() + '" class="input" />',
            okValue: '确定',
            ok: function () {
                var amount = $("#txtDialogAmount", parent.document).val();
                if (!checkIsMoney(amount)) {
                    top.dialog({
                        title: '提示',
                        content: '对不起，请输入正确的手续费用！',
                        okValue: '确定',
                        ok: function () { }
                    }).showModal(winDialog);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_payment_fee", "payment_fee": amount };
                //发送AJAX请求
                sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }
    //修改税金费用
    function EditInvoiceTaxes() {
        var winDialog = top.dialog({
            title: '请修改发票税金费用',
            content: '<input id="txtDialogAmount" type="text" value="' + $("#spanInvoiceTaxesValue").text() + '" class="input" />',
            okValue: '确定',
            ok: function () {
                var amount = $("#txtDialogAmount", parent.document).val();
                if (!checkIsMoney(amount)) {
                    top.dialog({
                        title: '提示',
                        content: '对不起，请输入正确的税金费用！',
                        okValue: '确定',
                        ok: function () { }
                    }).showModal(winDialog);
                    return false;
                }
                var postData = { "order_no": $("#spanOrderNo").text(), "edit_type": "edit_invoice_taxes", "invoice_taxes": amount };
                //发送AJAX请求
                sendAjaxUrl(winDialog, postData, "../../tools/admin_ajax.ashx?action=edit_order_status");
                return false;
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }

    //=================================工具类的JS函数====================================
    //检查是否货币格式
    function checkIsMoney(val) {
        var regtxt = /^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/;
        if (!regtxt.test(val)) {
            return false;
        }
        return true;
    }
    //发送AJAX请求
    function sendAjaxUrl(winObj, postData, sendUrl) {
        $.ajax({
            type: "post",
            url: sendUrl,
            data: postData,
            dataType: "json",
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                top.dialog({
                    title: '提示',
                    content: '尝试发送失败，错误信息：' + errorThrown,
                    okValue: '确定',
                    ok: function () { }
                }).showModal(winObj);
            },
            success: function (data, textStatus) {
                if (data.status == 1) {
                    winObj.close().remove();
                    var d = dialog({ content: data.msg }).show();
                    setTimeout(function () {
                        d.close().remove();
                        location.reload(); //刷新页面
                    }, 2000);
                } else {
                    top.dialog({
                        title: '提示',
                        content: '错误提示：' + data.msg,
                        okValue: '确定',
                        ok: function () { }
                    }).showModal(winObj);
                }
            }
        });
    }
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="order_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <a href="order_list.aspx"><span>订单管理</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>订单详细</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">订单详细信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dd style="margin-left:50px;text-align:center;">
      <div class="order-flow" style="width:560px">
        <%if (model.status < 4)
          { %>
        <div class="item-box left arrive">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>订单已生成</b>
            <p><%=model.add_time%></p>
          </div>
        </div>
        <%if (model.payment_status == 1)
          { %>
        <div class="item-box">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>等待付款</b>
          </div>
        </div>
        <%}
          else if (model.payment_status == 2)
          { %>
        <div class="item-box arrive">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>已付款</b>
            <p><%=model.payment_time%></p>
          </div>
        </div>
        <%} %>
        <%if (model.payment_status == 0 && model.status == 1)
          { %>
        <div class="item-box">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>等待确认</b>
          </div>
        </div>
        <%}
          else if (model.payment_status == 0 && model.status > 1)
          { %>
        <div class="item-box arrive">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>已确认</b>
            <p><%=model.confirm_time%></p>
          </div>
        </div>
        <%} %>
        <%if (model.express_status == 1)
          { %>
        <div class="item-box">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>等待发货</b>
          </div>
        </div>
        <%}
          else if (model.express_status == 2)
          { %>
        <div class="item-box arrive">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>已发货</b>
            <p><%=model.express_time%></p>
          </div>
        </div>
         <%} %>
         <%if (model.status == 3)
           { %>
         <div class="item-box arrive right">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>订单完成</b>
            <p><%=model.complete_time%></p>
          </div>
        </div>
         <%}
           else
           { %>
         <div class="item-box right">
          <div class="line"></div>
          <div class="icon"><i class="iconfont icon-confirm-full"></i></div>
          <div class="txt">
            <b>等待完成</b>
          </div>
        </div>
         <%} %>
         <%}
          else if (model.status == 4)
          {%>
          <div style="text-align:center;line-height:30px; font-size:20px; color:Red;">该订单已取消</div>
         <%}
          else if (model.status == 5)
          { %>
          <div style="text-align:center;line-height:30px; font-size:20px; color:Red;">该订单已作废</div>
         <%} %>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>订单号</dt>
    <dd><span id="spanOrderNo"><%=model.order_no %></span></dd>
  </dl>
  <asp:Repeater ID="rptList" runat="server">
  <HeaderTemplate>
  <dl>
    <dt>商品列表</dt>
    <dd>
      <div class="table-container">
        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="100%">
        <thead>
          <tr>
            <th style="text-align:left;">商品信息</th>
            <th width="8%">销售价</th>
            <th width="8%">优惠价</th>
            <th width="8%">积分</th>
            <th width="8%">数量</th>
            <th width="12%">金额合计</th>
            <th width="8%">积分合计</th>
          </tr>
        </thead>
        <tbody>
        </HeaderTemplate>
        <ItemTemplate>
          <tr class="td_c">
            <td style="text-align:left;white-space:inherit;word-break:break-all;line-height:20px;">
              <%#Eval("goods_title")%>
            </td>
            <td><%#Eval("goods_price")%></td>
            <td><%#Eval("real_price")%></td>
            <td><%#Eval("point")%></td>
            <td><%#Eval("quantity")%></td>
            <td><%#Convert.ToDecimal(Eval("real_price"))*Convert.ToInt32(Eval("quantity"))%></td>
            <td><%#Convert.ToInt32(Eval("point")) * Convert.ToInt32(Eval("quantity"))%></td>
          </tr>
          </ItemTemplate>
          <FooterTemplate>
        </tbody>
        </table>
      </div>
    </dd>
  </dl>
  </FooterTemplate>
  </asp:Repeater>
  <dl>
    <dt>收货信息</dt>
    <dd>
      <div class="table-container">
        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="100%">
        <tr>
          <th width="20%">收件人</th>
          <td>
            <div class="position">
              <span id="spanAcceptName"><%=model.accept_name %></span>
              <input id="btnEditAcceptInfo" runat="server" visible="false" type="button" class="ibtn" value="修改" />
            </div>
          </td>
        </tr>
        <tr>
          <th>发货地址</th>
          <td><span id="spanArea"><%=model.area %></span> <span id="spanAddress"><%=model.address %></span></td>
        </tr>
        <tr>
          <th>邮政编码</th>
          <td><span id="spanPostCode"><%=model.post_code %></span></td>
        </tr>
        <tr>
          <th>手机</th>
          <td><span id="spanMobile"><%=model.mobile %></span></td>
        </tr>
        <tr>
          <th>电话</th>
          <td><span id="spanTelphone"><%=model.telphone %></span></td>
        </tr>
        <tr>
          <th>邮箱</th>
          <td><span id="spanEmail"><%=model.email %></span></td>
        </tr>
        </table>
      </div>
    </dd>
  </dl>
  <dl id="dlUserInfo" runat="server" visible="false">
    <dt>会员信息</dt>
    <dd>
      <div class="table-container">
        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="100%">
        <tr>
          <th width="20%">会员账户</th>
          <td><asp:Label ID="lbUserName" runat="server" /></td>
        </tr>
        <tr>
          <th>会员组别</th>
          <td><asp:Label ID="lbUserGroup" runat="server" Text="-" /></td>
        </tr>
        <tr>
          <th>购物折扣</th>
          <td><asp:Label ID="lbUserDiscount" runat="server" Text="100" /></td>
        </tr>
        <tr>
          <th>账户余额</th>
          <td><asp:Label ID="lbUserAmount" runat="server" Text="0" /> 元</td>
        </tr>
        <tr>
          <th>账户积分</th>
          <td><asp:Label ID="lbUserPoint" runat="server" Text="0" /> 分</td>
        </tr>
        </table>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>支付配送</dt>
    <dd>
      <div class="table-container">
        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="100%">
        <tr>
          <th width="20%">支付方式</th>
          <td><%=new DTcms.BLL.site_payment().GetTitle(model.payment_id) %></td>
        </tr>
        <tr>
          <th>配送方式</th>
          <td><%=new DTcms.BLL.express().GetTitle(model.express_id) %></td>
        </tr>
        <tr>
          <th>是否开具发票</th>
          <td><%=model.is_invoice == 1 ? "是" : "否"%></td>
        </tr>
        <%if (model.is_invoice == 1)
          { %>
        <tr>
          <th>发票抬头</th>
          <td><%=model.invoice_title%></td>
        </tr>
        <%} %>
        <tr>
          <th>用户留言</th>
          <td><%=model.message %></td>
        </tr>
        <tr>
          <th valign="top">订单备注</th>
          <td>
            <div class="position">
              <div id="divRemark"><%=model.remark %></div>
              <input id="btnEditRemark" runat="server" visible="false" type="button" class="ibtn" value="修改" />
            </div>
          </td>
        </tr>
        <%if (model.express_status == 2 && model.express_no.Length > 0)
          {%>
        <tr>
          <th>物流单号</th>
          <td><%=model.express_no%></td>
        </tr>
        <%} %>
        </table>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>订单统计</dt>
    <dd>
      <div class="table-container">
        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="100%">
        <tr>
          <th width="20%">商品总金额</th>
          <td>
            <div class="position">
              <span id="spanRealAmountValue"><%=model.real_amount %></span> 元
              <input id="btnEditRealAmount" runat="server" visible="false" type="button" class="ibtn" value="调价" />
            </div>
          </td>
        </tr>
        <tr>
          <th>配送费用</th>
          <td>
            <div class="position">
              <span id="spanExpressFeeValue"><%=model.express_fee %></span> 元
              <input id="btnEditExpressFee" runat="server" visible="false" type="button" class="ibtn" value="调价" />
            </div>
          </td>
        </tr>
        <tr>
          <th>支付手续费</th>
          <td>
            <div class="position">
              <span id="spanPaymentFeeValue"><%=model.payment_fee %></span> 元
              <input id="btnEditPaymentFee" runat="server" visible="false" type="button" class="ibtn" value="调价" />
            </div>
          </td>
        </tr>
        <tr>
          <th>发票税金</th>
          <td>
            <div class="position">
              <span id="spanInvoiceTaxesValue"><%=model.invoice_taxes %></span> 元
              <input id="btnEditInvoiceTaxes" runat="server" visible="false" type="button" class="ibtn" value="调价" />
            </div>
          </td>
        </tr>
        <tr>
          <th>积分总计</th>
          <td>
            <div class="position">
              <%=model.point > 0 ? "+" + model.point.ToString() : model.point.ToString()%> 分
            </div>
          </td>
        </tr>
        <tr>
          <th>订单总金额</th>
          <td><%=model.order_amount %> 元</td>
        </tr>
        </table>
      </div>
    </dd>
  </dl>
</div>
<!--/内容-->


<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <input id="btnConfirm" runat="server" visible="false" type="button" value="确认订单" class="btn" />
    <input id="btnPayment" runat="server" visible="false" type="button" value="确认付款" class="btn" />
    <input id="btnExpress" runat="server" visible="false" type="button" value="确认发货" class="btn" />
    <input id="btnComplete" runat="server" visible="false" type="button" value="完成订单" class="btn" />
    <input id="btnCancel" runat="server" visible="false" type="button" value="取消订单" class="btn green" />
    <input id="btnInvalid" runat="server" visible="false" type="button" value="作废订单" class="btn green" />
    <input id="btnPrint" type="button" value="打印订单" class="btn violet" />
    <input id="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->

</form>
</body>
</html>