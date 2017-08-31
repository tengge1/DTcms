<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="express_edit.aspx.cs" Inherits="DTcms.Web.admin.order.express_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>编辑物流配送</title>
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
        //物流编码赋值
        $("#ddlExpressCode").change(function () {
            if ($(this).find("option:selected").attr("value") != "") {
                $("#txtExpressCode").val($(this).find("option:selected").attr("value"));
                $("#txtExpressCode").focus();
            }
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="site_oauth_list.aspx" class="back"><i class="iconfont icon-up"></i><span>返回列表页</span></a>
  <a href="../center.aspx"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>订单设置</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>编辑物流配送</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">编辑物流配送</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>标题名称</dt>
    <dd><asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*物流公司的中文名称</span></dd>
  </dl>
  <dl>
    <dt>物流编码</dt>
    <dd>
      <asp:TextBox ID="txtExpressCode" runat="server" CssClass="input normal" datatype="s0-100" sucmsg=" "></asp:TextBox>
      <div class="rule-single-select">
        <select id="ddlExpressCode" name="ddlExpressCode">
            <option value="">@参考编码</option>
            <option value="aae">A-aae全球专递</option>
            <option value="anjie">A-安捷快递</option>
            <option value="anxindakuaixi">A-安信达快递</option>
            <option value="biaojikuaidi">B-彪记快递</option>
            <option value="bht">B-bht</option>
            <option value="baifudongfang">B-百福东方国际物流</option>
            <option value="coe">C-中国东方（COE）</option>
            <option value="changyuwuliu">C-长宇物流</option>
            <option value="datianwuliu">D-大田物流</option>
            <option value="debangwuliu">D-德邦物流</option>
            <option value="dhl">D-dhl</option>
            <option value="dpex">D-dpex</option>
            <option value="dsukuaidi">D-d速快递</option>
            <option value="disifang">D-递四方</option>
            <option value="ems">E-ems快递</option>
            <option value="fedex">F-fedex（国外）</option>
            <option value="feikangda">F-飞康达物流</option>
            <option value="fenghuangkuaidi">F-凤凰快递</option>
            <option value="feikuaida">F-飞快达</option>
            <option value="guotongkuaidi">G-国通快递</option>
            <option value="ganzhongnengda">G-港中能达物流</option>
            <option value="guangdongyouzhengwuliu">G-广东邮政物流</option>
            <option value="gongsuda">G-共速达</option>
            <option value="huitongkuaidi">H-汇通快运</option>
            <option value="hengluwuliu">H-恒路物流</option>
            <option value="huaxialongwuliu">H-华夏龙物流</option>
            <option value="haihongwangsong">H-海红</option>
            <option value="haiwaihuanqiu">H-海外环球</option>
            <option value="jiayiwuliu">J-佳怡物流</option>
            <option value="jinguangsudikuaijian">J-京广速递</option>
            <option value="jixianda">J-急先达</option>
            <option value="jjwl">J-佳吉物流</option>
            <option value="jymwl">J-加运美物流</option>
            <option value="jindawuliu">J-金大物流</option>
            <option value="jialidatong">J-嘉里大通</option>
            <option value="jykd">J-晋越快递</option>
            <option value="kuaijiesudi">K-快捷速递</option>
            <option value="lianb">L-联邦快递（国内）</option>
            <option value="lianhaowuliu">L-联昊通物流</option>
            <option value="longbanwuliu">L-龙邦物流</option>
            <option value="lijisong">L-立即送</option>
            <option value="lejiedi">L-乐捷递</option>
            <option value="minghangkuaidi">M-民航快递</option>
            <option value="meiguokuaidi">M-美国快递</option>
            <option value="menduimen">M-门对门</option>
            <option value="ocs">O-OCS</option>
            <option value="peisihuoyunkuaidi">P-配思货运</option>
            <option value="quanchenkuaidi">Q-全晨快递</option>
            <option value="quanfengkuaidi">Q-全峰快递</option>
            <option value="quanjitong">Q-全际通物流</option>
            <option value="quanritongkuaidi">Q-全日通快递</option>
            <option value="quanyikuaidi">Q-全一快递</option>
            <option value="rufengda">R-如风达</option>
            <option value="santaisudi">S-三态速递</option>
            <option value="shenghuiwuliu">S-盛辉物流</option>
            <option value="shentong">S-申通</option>
            <option value="shunfeng">S-顺丰</option>
            <option value="sue">S-速尔物流</option>
            <option value="shengfeng">S-盛丰物流</option>
            <option value="saiaodi">S-赛澳递</option>
            <option value="tiandihuayu">T-天地华宇</option>
            <option value="tiantian">T-天天快递</option>
            <option value="tnt">T-tnt</option>
            <option value="ups">U-ups</option>
            <option value="wanjiawuliu">W-万家物流</option>
            <option value="wenjiesudi">W-文捷航空速递</option>
            <option value="wuyuan">W-伍圆</option>
            <option value="wxwl">W-万象物流</option>
            <option value="xinbangwuliu">X-新邦物流</option>
            <option value="xinfengwuliu">X-信丰物流</option>
            <option value="yafengsudi">Y-亚风速递</option>
            <option value="yibangwuliu">Y-一邦速递</option>
            <option value="youshuwuliu">Y-优速物流</option>
            <option value="youzhengguonei">Y-邮政包裹挂号信</option>
            <option value="youzhengguoji">Y-邮政国际包裹挂号信</option>
            <option value="yuanchengwuliu">Y-远成物流</option>
            <option value="yuantong">Y-圆通速递</option>
            <option value="yuanweifeng">Y-源伟丰快递</option>
            <option value="yuanzhijiecheng">Y-元智捷诚快递</option>
            <option value="yunda">Y-韵达快运</option>
            <option value="yuntongkuaidi">Y-运通快递</option>
            <option value="yuefengwuliu">Y-越丰物流</option>
            <option value="yad">Y-源安达</option>
            <option value="yinjiesudi">Y-银捷速递</option>
            <option value="zhaijisong">Z-宅急送</option>
            <option value="zhongtiekuaiyun">Z-中铁快运</option>
            <option value="zhongtong">Z-中通速递</option>
            <option value="zhongyouwuliu">Z-中邮物流</option>
            <option value="zhongxinda">Z-忠信达</option>
            <option value="zhimakaimen">Z-芝麻开门</option>
        </select>
      </div>
      <span class="Validform_checktip">快递100物流编码 <a target="_blank" href="http://code.google.com/p/kuaidi-api/wiki/Open_API_Chaxun_URL"> 点击查看</a></span>
    </dd>
  </dl>
  <dl>
    <dt>物流网址</dt>
    <dd>
      <asp:TextBox ID="txtWebSite" runat="server" CssClass="input normal" datatype="url" errormsg="请输入正确的网址" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">物流快递的官网，以“http://”开头</span>
    </dd>
  </dl>
  <dl>
    <dt>配送费用</dt>
    <dd>
      <asp:TextBox ID="txtExpressFee" runat="server" CssClass="input small" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" ">0</asp:TextBox>
      <span class="Validform_checktip">*货币格式，单位为元</span>
    </dd>
  </dl>
  <dl>
    <dt>是否启用</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsLock" runat="server" />
      </div>
      <span class="Validform_checktip">*不启用则不显示该配送方式</span>
    </dd>
  </dl>
  <dl>
    <dt>排序数字</dt>
    <dd>
      <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
      <span class="Validform_checktip">*数字，越小越向前</span>
    </dd>
  </dl>
  <dl>
    <dt>描述说明</dt>
    <dd>
      <asp:TextBox ID="txtRemark" runat="server" CssClass="input normal" TextMode="MultiLine" />
      <span class="Validform_checktip">物流配送的描述说明，支持HTML代码</span>
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