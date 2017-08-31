<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="builder_html.aspx.cs" Inherits="DTcms.Web.admin.settings.builder_html" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>生成静态页面</title>
<link rel="stylesheet" type="text/css" href="../../scripts/artdialog/ui-dialog.css" />
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    //全局变量声明
    var isLock = false; //是否锁定正在执行操作
    var dialogDG; //dialog窗口实例

    //①提示且生成相应的频道
    function builerTip(obj) {
        //检查是否正在执行操作
        if (isLock) {
            top.dialog({
                title: '提示',
                content: '上次操作未完成，不可同时执行！',
                okValue: '确定',
                ok: function () { }
            }).showModal();
            return false;
        }
        //提示是否执行
        top.dialog({
            title: '提示',
            content: '此操作将会消耗大量的资源，确认要继续吗？',
            okValue: '确定',
            ok: function () {
                getBuilerUrl(obj);
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }
    //②发送AJAX请求获取生成地址
    function getBuilerUrl(obj) {
        //如dialog窗口不存在则创建
        if (!dialogDG) {
            createDialogObj();
        }
        //重置dialog窗口的值
        dialogDG.find('.title h2').html('正在获取信息...');
        dialogDG.find('.content').html('正在加载，请稍候...');
        isLock = true; //锁定操作
        //发送AJAX请求
        $.ajax({
            url: $(obj).attr("url"),
            type: "POST",
            success: function (data) {
                if (data == 0) {
                    dialogDG.find('.title h2').html('执行生成处理完毕');
                    dialogDG.find('.content').html('该栏目下没有内容！');
                    isLock = false; //解除锁定
                }
                else if (data == -1) {
                    dialogDG.find('.title h2').html('执行请求完毕');
                    dialogDG.find('.content').html('<font color=red>登陆超时！</font>');
                    isLock = false; //解除锁定
                }
                else if (data == -2) {
                    dialogDG.find('.title h2').html('执行请求完毕');
                    dialogDG.find('.content').html('<font color=red>您没有操作生成静态的权限！</font>');
                    isLock = false; //解除锁定
                }
                else if (data == -3) {
                    dialogDG.find('.title h2').html('执行请求完毕');
                    dialogDG.find('.content').html('<font color=red>您还未开启生成静态功能！<a navid=\"site_config\" href=\"../settings/sys_config.aspx\" target=\"mainframe\">立即开启</a></font>');
                    isLock = false; //解除锁定
                }
                else {
                    var json = eval(data);
                    if (json == "") {
                        dialogDG.find('.title h2').html('执行生成处理完毕');
                        dialogDG.find('.content').html('<font color=red>没有需要生成数据！</font>');
                        isLock = false; //解除锁定
                    }
                    else {
                        execBuilerHtml(json, 0);
                    }
                }
            }
        });
    }
    //③迭代执行生成
    function execBuilerHtml(jsonUrl, k) {
        $.ajax({
            url: jsonUrl[k],
            type: "POST",
            contentType: "application/x-www-form-urlencoded; charset=UTF-8",
            error: function () {
                getBuilerStatus(jsonUrl, k, "需要生成的静态页面路径有误！");
            },
            success: function (data) {
                if (data != 1 && data != 2 && data != 0)
                    data = "错误";
                getBuilerStatus(jsonUrl, k, data);
            }
        });
    }
    //④返回执行结果及状态
    function getBuilerStatus(jsonUrl, k, msg) {
        var fodname = jsonUrl[k].split('&catalogue=');
        var fname = jsonUrl[k].split('&html_filename=');
        fname = fname[1].split('&catalogue=');
        fname[0] = unescape(fname[0]);
        var finame = !fodname[1] ? fname[0] + '.html' : fodname[1];

        var spanTxt = msg == 0 ? '<span class="suc">成功</span>' : '<span class="error">失败</span>';
        var linkTxt = spanTxt + '<a href="<%=sysConfig.webpath %>' + finame + '" target="_blank">/' + finame + '</a>';

        dialogDG.find('.title h2').html('已完成页面生成' + '[' + jsonUrl.length + '/' + (k + 1) + ']');
        if (dialogDG.find('.content .list').length == 0) {
            dialogDG.find('.content').html('');
        }
        dialogDG.find('.content').append('<div class="list">' + linkTxt + '</div>');
        if (k == jsonUrl.length - 1) {
            isLock = false; //解除锁定
            //完成提示
            var d = top.dialog({ content: '页面全部生成完毕' }).show();
            setTimeout(function () {
                d.close().remove();
            }, 2000);
        } else {
            k++;
            execBuilerHtml(jsonUrl, k);
        }
    }
    //创建dialog窗口
    function createDialogObj() {
        dialogDG = $('<div id="buildDialog" class="builder-box">'
                   + '<div class="title">'
                   + '<a class="close" onclick="closeDialogObj();"><i class="iconfont icon-remove"></i></a>'
                   + '<h2>请稍候...</h2></div>'
                   + '<div class="content"></div></div>');
        dialogDG.appendTo($('body'));
    }
    //删除dialog窗口
    function closeDialogObj() {
        dialogDG.remove();
        dialogDG = null;
    }
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i class="iconfont icon-up"></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i class="iconfont icon-home"></i><span>首页</span></a>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>界面管理</span>
  <i class="arrow iconfont icon-arrow-right"></i>
  <span>生成静态</span>
</div>
<!--/导航栏-->
<div class="line20"></div>

<!--列表-->
<div class="table-container">
<asp:Repeater ID="rptList" runat="server" onitemdatabound="rptList_ItemDataBound">
<HeaderTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
  <tr>
    <th align="left" width="50%" style="padding-left:10px;">频道列表</th>
    <th align="left">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td style="padding-left:10px;white-space:nowrap;word-break:break-all;overflow:hidden;">
      <span class="folder-open"></span>
      <%#Eval("title")%>
    </td>
    <td>
      <a href="javascript:;" url="../../tools/admin_ajax.ashx?action=get_builder_urls&lang=<%#Eval("build_path")%>&name=&type=index" onclick="builerTip(this);">生成首页</a>
    </td>
  </tr>
  <asp:Repeater ID="rptChannel" runat="server">
  <ItemTemplate>
  <tr>
    <td style="padding-left:10px;">
      <span class="folder-line"></span>
      <span class="folder-open"></span>
      <%#Eval("title")%>
    </td>
    <td>
      <a href="javascript:;" url="../../tools/admin_ajax.ashx?action=get_builder_urls&lang=<%# DataBinder.Eval((Container.NamingContainer.NamingContainer as RepeaterItem).DataItem, "build_path") %>&name=<%#Eval("name")%>" onclick="builerTip(this);">全部生成</a>
      | <a href="javascript:;" url="../../tools/admin_ajax.ashx?action=get_builder_urls&lang=<%# DataBinder.Eval((Container.NamingContainer.NamingContainer as RepeaterItem).DataItem, "build_path") %>&name=<%#Eval("name")%>&type=indexlist" onclick="builerTip(this);">生成首页带列表</a>
      | <a href="javascript:;" url="../../tools/admin_ajax.ashx?action=get_builder_urls&lang=<%# DataBinder.Eval((Container.NamingContainer.NamingContainer as RepeaterItem).DataItem, "build_path") %>&name=<%#Eval("name")%>&type=list" onclick="builerTip(this);">生成列表页</a>
      | <a href="javascript:;" url="../../tools/admin_ajax.ashx?action=get_builder_urls&lang=<%# DataBinder.Eval((Container.NamingContainer.NamingContainer as RepeaterItem).DataItem, "build_path") %>&name=<%#Eval("name")%>&type=category" onclick="builerTip(this);">生成栏目页</a>
      | <a href="javascript:;" url="../../tools/admin_ajax.ashx?action=get_builder_urls&lang=<%# DataBinder.Eval((Container.NamingContainer.NamingContainer as RepeaterItem).DataItem, "build_path") %>&name=<%#Eval("name")%>&type=detail" onclick="builerTip(this);">生成详细页</a>
    </td>
  </tr>
  </ItemTemplate>
  </asp:Repeater>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"2\">暂无记录</td></tr>" : ""%>
</table>
</FooterTemplate>
</asp:Repeater>
</div>
<!--/列表-->

</form>
</body>
</html>