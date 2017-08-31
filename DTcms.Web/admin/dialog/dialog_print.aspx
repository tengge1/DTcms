<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_print.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_print" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>打印订单窗口</title>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    var api = top.dialog.get(window); //获取父窗体对象
    //页面加载完成执行
    $(function () {
        //设置按钮及事件
        api.button([{
            value: '确认打印',
            callback: function () {
                printWin();
            },
            autofocus: true
        }, {
            value: '取消',
            callback: function () { }
        }]);
    });
    //打印方法
    function printWin() {
        var oWin = window.open("", "_blank");
        oWin.document.write(document.getElementById("content").innerHTML);
        oWin.focus();
        oWin.document.close();
        oWin.print();
        oWin.close();
        return false;
    }
</script>
</head>

<body style="margin:0;">
<form id="form1" runat="server">
<div id="content">
<table width="800" border="0" align="center" cellpadding="3" cellspacing="0" style="font-size:12px; font-family:'微软雅黑'; background:#fff;">
<tr>
  <td width="346" height="50" style="font-size:20px;"><%=sysConfig.webname%>商品订单</td>
  <td width="216">订单号：<%=model.order_no%><br />
日&nbsp;&nbsp; 期：<%=model.add_time.ToString("yyyy-MM-dd")%></td>
  <td width="220">操&nbsp; 作 人：<%=adminModel.user_name%><br />打印时间：<%=DateTime.Now%></td>
</tr>
<tr>
  <td colspan="3" style="padding:10px 0; border-top:1px solid #000;">
  <asp:Repeater ID="rptList" runat="server">
      <HeaderTemplate>
        <table width="100%" border="0" cellspacing="0" cellpadding="5" style="font-size:12px; font-family:'微软雅黑'; background:#fff;">
          <tr>
            <td align="left" style="background:#ccc;">商品信息</th>
            <td width="12%" align="left" style="background:#ccc;">销售价</td>
            <td width="12%" align="left" style="background:#ccc;">优惠价</td>
            <td width="10%" align="left" style="background:#ccc;">数量</td>
            <td width="10%" align="left" style="background:#ccc;">积分</td>
            <td width="12%" align="left" style="background:#ccc;">金额合计</td>
          </tr>
      </HeaderTemplate>
      <ItemTemplate>
          <tr>
            <td>
              <%#Eval("goods_title")%>
            </td>
            <td><%#Eval("goods_price")%></td>
            <td><%#Eval("real_price")%></td>
            <td><%#Eval("quantity")%></td>
            <td><%#Convert.ToInt32(Eval("point")) * Convert.ToInt32(Eval("quantity"))%></td>
            <td><%#Convert.ToDecimal(Eval("real_price"))*Convert.ToInt32(Eval("quantity"))%></td>
          </tr>
      </ItemTemplate>
      <FooterTemplate>
            <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
          </table>
     </FooterTemplate>
  </asp:Repeater>
  </td>
  </tr>
<tr>
  <td colspan="3" style="border-top:1px solid #000;">
  <table width="100%" border="0" cellspacing="0" cellpadding="5" style="margin:5px auto; font-size:12px; font-family:'微软雅黑'; background:#fff;">
    <tr>
      <td width="44%">
        会员账户：
        <%if (model.user_id > 0)
          { %>
          <%=model.user_name%>
        <%}
          else
          { %>
          匿名用户
        <%} %>
      </td>
      <td width="56%">客户姓名：<%=model.accept_name%><br /></td>
    </tr>
    <tr>
      <td>开具发票：<%=model.is_invoice == 1 ? "是" : "否"%></td>
      <td>送货地址：<%=model.area%> <%=model.address%><br /></td>
    </tr>
    <tr>
      <td>发票抬头：<%=model.invoice_title%></td>
      <td>邮政编码：<%=model.post_code%></td>
    </tr>
    <tr>
      <td valign="top">支付方式：<%=new DTcms.BLL.site_payment().GetTitle(model.payment_id) %></td>
      <td valign="top">手&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 机：<%=model.mobile%></td>
    </tr>
    <tr>
      <td>配送方式：<%=new DTcms.BLL.express().GetTitle(model.express_id)%></td>
      <td valign="top">固定电话：<%=model.telphone%></td>
    </tr>
    <tr>
      <td valign="top">订单备注：<%=model.remark%></td>
      <td valign="top">
        用户留言：<%=model.message%>
      </td>
    </tr>
  </table>
    <table width="100%" border="0" align="center" cellpadding="5" cellspacing="0" style="border-top:1px solid #000; font-size:12px; font-family:'微软雅黑'; background:#fff;">
      <tr>
        <td align="right">商品金额：￥<%=model.real_amount%> + 配送费：￥<%=model.express_fee%> + 支付手续费：￥<%=model.payment_fee%> + 税金：￥<%=model.invoice_taxes%> = 订单总额：<%=model.order_amount%></td>
      </tr>
    </table></td>
  </tr>
</table>
</div>
</form>
</body>
</html>