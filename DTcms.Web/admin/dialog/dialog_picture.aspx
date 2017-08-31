<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_picture.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_picture" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>上传图片</title>
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    var api = top.dialog.get(window); //获取父窗体对象
    //页面加载完成执行
    $(function () {
        //设置按钮及事件
        api.button([{
            value: '确定',
            callback: function () {
                execPicHtml();
            },
            autofocus: true
        }, {
            value: '取消',
            callback: function () { return true; }
        }
        ]);
        //初始化上传控件
        $(".upload-img").InitUploader({ sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf" });
        //修改状态，赋值给表单
        if ($(api.data).length > 0) {
            var parentObj = $(api.data).parent().parent();
            $("#txtImgUrl").val(parentObj.find("input[name='item_imgurl']").val());
            $("#txtTitle").val(parentObj.find("input[name='item_title']").val());
            $("#txtContent").val(parentObj.find("input[name='item_content']").val());
            $("#txtLinkUrl").val(parentObj.find("input[name='item_linkurl']").val());
            $("#txtSortId").val(parentObj.find("input[name='item_sortid']").val());
        }
    });

    //创建选项节点
    function execPicHtml() {
        var currDocument = $(document); //当前文档
        if ($("#txtTitle").val() == "") {
            top.dialog({
                title: '提示',
                content: '图片文字不可为空！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtTitle", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#txtImgUrl").val() == "") {
            top.dialog({
                title: '提示',
                content: '图片路径不可为空！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtImgUrl", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#txtLinkUrl").val() == "") {
            top.dialog({
                title: '提示',
                content: '链接地址不可为空！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtLinkUrl", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#txtSortId").val() == "") {
            top.dialog({
                title: '提示',
                content: '图片排序不可为空！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtSortId", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        //创建选项节点的HTML
        if ($(api.data).length > 0) {
            var parentObj = $(api.data).parent().parent();
            parentObj.find("input[name='item_sortid']").val($("#txtSortId").val());
            parentObj.find("input[name='item_title']").val($("#txtTitle").val());
            parentObj.find("input[name='item_imgurl']").val($("#txtImgUrl").val());
            parentObj.find("input[name='item_linkurl']").val($("#txtLinkUrl").val());
            parentObj.find("input[name='item_content']").val($("#txtContent").val());

            parentObj.find(".item_sortid").html($("#txtSortId").val());
            if ($("#txtImgUrl").val() == "") {
                parentObj.find(".item_imgurl").html("-");
            } else {
                parentObj.find(".item_imgurl").html('<img src="' + $("#txtImgUrl").val() + '" width="32" height="32" />');
            }
            parentObj.find(".item_title").html($("#txtTitle").val());
            parentObj.find(".item_linkurl").html($("#txtLinkUrl").val());

            api.close();
        } else {
            var liHtml = '<tr class="td_c">'
            + '<td><input type="hidden" name="item_id" value="0" />'
            + '<input type="hidden" name="item_content" value="' + $("#txtContent").val() + '" />'
            + '<input type="hidden" name="item_imgurl" value="' + $("#txtImgUrl").val() + '" />'
            + '<span class="item_imgurl img-box"><img src="' + $("#txtImgUrl").val() + '" /></span></td>'
            + '<td><input type="hidden" name="item_title" value="' + $("#txtTitle").val() + '" />'
            + '<span class="item_title">' + $("#txtTitle").val() + '</span></td>'
            + '<td><input type="hidden" name="item_linkurl" value="' + $("#txtLinkUrl").val() + '" />'
            + '<span class="item_linkurl">' + $("#txtLinkUrl").val() + '</span></td>'
            + '<td><input type="hidden" name="item_sortid" value="' + $("#txtSortId").val() + '" />'
            + '<span class="item_sortid">' + $("#txtSortId").val() + '</span></td>'
            + '<td><a title="编辑" class="img-btn" onclick="showImgDialog(this);"><i class="iconfont icon-edit"></i></a>'
            + '<a title="删除" class="img-btn" onclick="delItemTr(this);"><i class="iconfont icon-delete"></i></a></td>';
            api.close(liHtml).remove();
        }
        return false;
    }
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="div-content">
  <dl>
    <dt>排序数字</dt>
    <dd><input type="text" id="txtSortId" value="99" class="input txt small" onkeydown="return checkNumber(event);" /></dd>
  </dl>
  <dl>
    <dt>标题文字</dt>
    <dd><input type="text" id="txtTitle" class="input txt" /></dd>
  </dl>
  <dl>
    <dt>上传图片</dt>
    <dd>
      <input type="text" id="txtImgUrl" class="input txt upload-path" />
      <div class="upload-box upload-img"></div>
    </dd>
  </dl>
  <dl>
    <dt>链接地址</dt>
    <dd><input type="text" id="txtLinkUrl" class="input txt" /></dd>
  </dl>
  <dl>
    <dt>图片描述</dt>
    <dd>
        <textarea id="txtContent" class="input"></textarea>
    </dd>
  </dl>
</div>
</form>
</body>
</html>