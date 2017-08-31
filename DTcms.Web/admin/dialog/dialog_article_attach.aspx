<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_article_attach.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_article_attach" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>上传附件</title>
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
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
                execAttachHtml();
                return false;
            },
            autofocus: true
        }, {
            value: '取消',
            callback: function () { }
        }]);

        //初始化上传控年
        $(".upload-attach").InitUploader({ filesize: "<%=sysConfig.attachsize %>", sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf", filetypes: "<%=sysConfig.fileextension %>" });
        //远程或者本地上传
        $("input[name='attachType']").click(function () {
            var indexNum = $("input[name='attachType']").index($(this));
            $(".dl-attach-box").hide();
            $(".dl-attach-box").eq(indexNum).show();
        });
        //修改状态，赋值给表单
        if ($(api.data).length > 0) {
            var parentObj = $(api.data).parent();
            var filePath = parentObj.find("input[name='hid_attach_filepath']").val();
            var fileName = parentObj.find("input[name='hid_attach_filename']").val();
            var fileSize = parentObj.find("input[name='hid_attach_filesize']").val() * 1024; //转换为字节
            if (filePath.substring(0, 7).toLowerCase() == "http://") {
                $(".rule-multi-radio div a").eq(1).trigger("click"); //触发事件
                $("#txtRemoteTitle").val(fileName);
                $("#txtRemoteUrl").val(filePath);
                $(".dl-attach-box").hide();
                $(".dl-attach-box").eq(1).show();
            } else {
                $(".rule-multi-radio div a").eq(0).trigger("click"); //触发事件
                $("#txtFileName").val(fileName);
                $("#hidFilePath").val(filePath);
                $("#hidFileSize").val(fileSize);
                $(".dl-attach-box").hide();
                $(".dl-attach-box").eq(0).show();
            }
        }
    });

    //创建附件节点
    function execAttachHtml() {
        var currDocument = $(document); //当前文档
        if ($("input[name='attachType']:checked").val() == 0) {
            if ($("#hidFilePath").val() == "" || $("#hidFileSize").val() == "" || $("#txtFileName").val() == "") {
                top.dialog({
                    title: '提示',
                    content: '没有找到已上传附件，请上传！',
                    okValue: '确定',
                    ok: function () { }
                }).showModal(api);
                return false;
            }
            var fileExt = $("#hidFilePath").val().substring($("#hidFilePath").val().lastIndexOf(".") + 1).toUpperCase();
            var fileSize = Math.round($("#hidFileSize").val() / 1024);
            var fileSizeStr = fileSize + "KB";
            if (fileSize >= 1024) {
                fileSizeStr = ForDight((fileSize / 1024), 1) + "MB";
            }
            appendAttachHtml($("#txtFileName").val(), $("#hidFilePath").val(), fileExt, fileSize, fileSizeStr); //插件节点
        } else {
            if ($("#txtRemoteTitle").val() == "" || $("#txtRemoteUrl").val() == "") {
                top.dialog({
                    title: '提示',
                    content: '远程附件地址或文件名为空！',
                    okValue: '确定',
                    ok: function () { },
                    onclose: function () {
                        $("#txtRemoteTitle", currDocument).focus();
                    }
                }).showModal(api);
                return false;
            }
            //获得远程文件信息
            $.ajax({
                type: "post",
                url: "../../tools/admin_ajax.ashx?action=get_remote_fileinfo",
                data: {
                    "remotepath": $("#txtRemoteUrl").val()
                },
                dataType: "json",
                success: function (data, textStatus) {
                    if (data.status == '0') {
                        top.dialog({
                            id: 'reDialogStatus',
                            title: '提示',
                            content: data.msg,
                            okValue: '确定',
                            ok: function () { }
                        }).showModal(api);
                        return false;
                    } else {
                        var fileSize = Math.round(data.size / 1024);
                        var fileSizeStr = fileSize + "KB";
                        if (fileSize >= 1024) {
                            fileSizeStr = ForDight((fileSize / 1024), 1) + "MB";
                        }
                        appendAttachHtml($("#txtRemoteTitle").val(), $("#txtRemoteUrl").val(), data.ext, fileSize, fileSizeStr); //插件节点
                        return false;
                    }
                }
            });
        }
    }

    //创建附件节点的HTML
    function appendAttachHtml(fileName, filePath, fileExt, fileSize, fileSizeStr) {
        if ($(api.data).length > 0) {
            var parentObj = $(api.data).parent();
            parentObj.find("input[name='hid_attach_filename']").val(fileName);
            parentObj.find("input[name='hid_attach_filepath']").val(filePath);
            parentObj.find("input[name='hid_attach_fileSize']").val(fileSize);
            parentObj.find(".title").text(fileName);
            parentObj.find(".info .ext").text(fileExt);
            parentObj.find(".info .size").text(fileSizeStr);
            api.close().remove();
        } else {
            var liHtml = '<li>'
            + '<input name="hid_attach_id" type="hidden" value="0" />'
            + '<input name="hid_attach_filename" type="hidden" value="' + fileName + '" />'
            + '<input name="hid_attach_filepath" type="hidden" value="' + filePath + '" />'
            + '<input name="hid_attach_filesize" type="hidden" value="' + fileSize + '" />'
            + '<i class="iconfont icon-attachment"></i>'
            + '<a href="javascript:;" onclick="delAttachNode(this);" class="del" title="删除附件"><i class="iconfont icon-remove"></i></a>'
            + '<a href="javascript:;" onclick="showAttachDialog(this);" class="edit" title="更新附件"><i class="iconfont icon-pencil"></i></a>'
            + '<div class="title">' + fileName + '</div>'
            + '<div class="info">类型：<span class="ext">' + fileExt + '</span> 大小：<span class="size">' + fileSizeStr + '</span> 下载：<span class="down">0</span>次</div>'
            + '<div class="btns">下载积分：<input type="text" name="txt_attach_point" onkeydown="return checkNumber(event);" value="0" /></div>'
            + '</li>';
            api.close(liHtml).remove();
        }
    }
</script>
</head>

<body>
<form id="form1" runat="server">
<div class="div-content">
  <dl>
    <dt>附件类型</dt>
    <dd>
      <div class="rule-multi-radio">
        <input type="radio" name="attachType" value="0" checked="checked" /><label>本地附件</label><input type="radio" name="attachType" value="1" /><label>远程附件</label>
      </div>
    </dd>
  </dl>
  <div class="dl-attach-box">
    <dl>
      <dt></dt>
      <dd>
        <input type="hidden" id="hidFilePath" class="upload-path" />
        <input type="hidden" id="hidFileSize" class="upload-size" />
        <input type="text" id="txtFileName" class="input txt upload-name" />
        <div class="upload-box upload-attach"></div>
      </dd>
    </dl>
    <dl>
      <dt></dt>
      <dd>提示：上传文件后，可更改附件名称</dd>
    </dl>
  </div>
  <div class="dl-attach-box" style="display:none;">
    <dl>
      <dt>附件名称</dt>
      <dd><input type="text" id="txtRemoteTitle" class="input txt" /></dd>
    </dl>
    <dl>
      <dt>远程地址</dt>
      <dd><input type="text" id="txtRemoteUrl" class="input txt" /> <span>*以“http://”开头</span></dd>
    </dl>
  </div>
</div>
</form>
</body>
</html>