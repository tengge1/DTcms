<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_channel.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_channel" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>频道URL配置</title>
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    var api = top.dialog.get(window); //获取父窗体对象
    $(function () {
        //设置按钮及事件
        api.button([{
            value: '确定',
            callback: function () {
                submitForm();
                return false;
            },
            autofocus: true
        }, {
            value: '取消',
            callback: function () { }
        }
        ]);

        //添加按钮(点击绑定)
        $("#btnVarAdd").click(function () {
            showUrlDialog();
        });

        //检查是否添加
        if ($(api.data).length > 0) {
            var parentObj = $(api.data).parent().parent(); //取得节点父对象
            var item_type = parentObj.find("input[name='item_type']").val(); //页面类型
            $("#pageType option").each(function (i) {
                if ($(this).attr("value") == item_type) {
                    $(this).prop("selected", true);
                }
            });
            $("#urlKey").val(parentObj.find("input[name='item_name']").val()); //URL名称
            $("#urlPage").val(parentObj.find("input[name='item_page']").val().replace(".aspx", "")); //生成文件名
            $("#pageTemplet").val(parentObj.find("input[name='item_templet']").val().replace(".html", "")); //模板文件名
            $("#urlPageSize").val(parentObj.find("input[name='item_pagesize']").val()); //分页大小
            //URL重写表达式
            var urlRewrite = parentObj.find("input[name='item_rewrite']").val();
            if (urlRewrite.length > 0) {
                var urlRewriteArr = urlRewrite.split("&");
                for (i = 0; i < urlRewriteArr.length; i++) {
                    var itemRewiteArr = urlRewriteArr[i].split(",");
                    if (itemRewiteArr.length > 0) {
                        //插入一行TR并赋值重写表达式
                        var trObj = $("#var_box").append(createUrlHtml());
                        $(trObj).children("tr").eq(i).find("input[name='itemPath']").val(itemRewiteArr[0]);
                        $(trObj).children("tr").eq(i).find("input[name='itemPattern']").val(itemRewiteArr[1]);
                        $(trObj).children("tr").eq(i).find("input[name='itemQuerystring']").val(itemRewiteArr[2]);
                    }
                }
            }
        }

    });

    /*============================以下本页面方法================================*/
    //提交添加一行URL配置
    function submitForm() {
        var parentDocument = parent.window.frames['mainframe'].document;
        var currDocument = $(document);
        var oldkey = ""; //旧的调用名称
        if ($(api.data).length > 0) {
            oldkey = $(api.data).parent().parent().find("input[name='item_name']").val();
        }
        if ($("#pageType").children("option:selected").attr("value") == "") {
            top.dialog({
                title: '提示',
                content: '请选择频道类型！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#pageType", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#urlKey").val() == "") {
            top.dialog({
                title: '提示',
                content: '请输入URL调用名称！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#urlKey", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#urlPage").val() == "") {
            top.dialog({
                title: '提示',
                content: '请输入生成ASPX文件名称！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#urlPage", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        if ($("#pageTemplet").val() == "") {
            top.dialog({
                title: '提示',
                content: '请输入模板文件名称！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#pageTemplet", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        //检查本地是否重复
        var checkKey = true;
        $("#item_box tr", parentDocument).each(function () {
            if ($("#urlKey").val() == $(this).find("input[name='item_name']").val() && $("#urlKey").val() != oldkey) {
                checkKey = false;
            }
        });
        if (!checkKey) {
            top.dialog({
                title: '提示',
                content: '对不起，URL调用名称已重复！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#urlKey", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        //AJAX验证成功后则添加或修改
        var oldItemKey = "";
        if ($(api.data).length > 0) {
            if ($(api.data).parent().parent().find("input[name='old_item_name']").length > 0) {
                oldItemKey = $(api.data).parent().parent().find("input[name='old_item_name']").val();
            }
        }
        $.ajax({
            type: "post",
            url: "../../tools/admin_ajax.ashx?action=urlrewrite_name_validate",
            data: { "param": $("#urlKey").val(), "old_name": oldItemKey },
            dataType: "json",
            success: function (data, textStatus) {
                if (data.status == "y") {
                    if ($(api.data).length > 0) {
                        setNavRow($(api.data).parent().parent());
                    } else {
                        //创建TR
                        $("#item_box", parentDocument).append(getTrHtml());
                        setNavRow($("#item_box tr:last", parentDocument));
                    }
                    api.close().remove();
                } else {
                    top.dialog({
                        title: '提示',
                        content: '对不起，AJAX检测URL调用名称已存在，<br />若旧名称已更改，请保存后方可使用该名称！',
                        okValue: '确定',
                        ok: function () { },
                        onclose: function () {
                            $("#urlKey", currDocument).focus();
                        }
                    }).showModal(api);
                    return false;
                }
            }
        });
        return false;
    }

    //设置页面表格行内容
    function getTrHtml() {
        var navRow = '<tr class="td_c">'
        + '<td><input type="hidden" name="item_rewrite" /><input type="hidden" name="item_type" /><span class="item_type"></span></td>'
        + '<td><input type="hidden" name="item_name" /><span class="item_name"></span></td>'
        + '<td><input type="hidden" name="item_page" /><span class="item_page"></span></td>'
        + '<td><input type="hidden" name="item_templet" /><span class="item_templet"></span></td>'
        + '<td><input type="hidden" name="item_pagesize" /><span class="item_pagesize"></span></td>'
		+ '<td><a title="编辑" class="img-btn" onclick="showChannelDialog(this);"><i class="iconfont icon-edit"></i></a> '
        + '<a title="删除" class="img-btn" onclick="delItemTr(this);"><i class="iconfont icon-delete"></i></a></td>'
		+ '</tr>';
        return navRow;
    }

    //最终赋值给页面
    function setNavRow(obj) {
        //URL重写表达式
        var url_rewrite = "";
        $("#var_box tr").each(function (i) {
            if (i > 0) {
                url_rewrite += "&";
            }
            if ($(this).find("input[name='itemPath']").val().length > 0) {
                url_rewrite += $(this).find("input[name='itemPath']").val() + ","
                + $(this).find("input[name='itemPattern']").val() + ","
                + $(this).find("input[name='itemQuerystring']").val();
            }
        });
        $(obj).find("input[name='item_rewrite']").val(url_rewrite);
        //页面类型
        $(obj).find("input[name='item_type']").val($("#pageType").children("option:selected").attr("value"));
        $(obj).find(".item_type").html($("#pageType").children("option:selected").html());
        //URL名称
        $(obj).find("input[name='item_name']").val($("#urlKey").val());
        $(obj).find(".item_name").html($("#urlKey").val());
        //生成文件名
        $(obj).find("input[name='item_page']").val($("#urlPage").val() + ".aspx");
        $(obj).find(".item_page").html($("#urlPage").val() + ".aspx");
        //模板文件名
        $(obj).find("input[name='item_templet']").val($("#pageTemplet").val() + ".html");
        $(obj).find(".item_templet").html($("#pageTemplet").val() + ".html");
        //分页大小
        $(obj).find("input[name='item_pagesize']").val($("#urlPageSize").val());
        if ($("#urlPageSize").val() == "" || $("#urlPageSize").val() == "0") {
            $(obj).find(".item_pagesize").html("-");
        } else {
            $(obj).find(".item_pagesize").html($("#urlPageSize").val());
        }
    }
    /*============================以上本页面方法================================*/

    /*=============================以下窗口方法=================================*/
    //创建窗口
    function showUrlDialog(obj) {
        var objNum = arguments.length;
        var m = top.dialog({
            title: '重写表达式',
            url: 'dialog/dialog_rewrite.aspx',
            onclose: function () {
                var trHtml = this.returnValue;
                if (trHtml.length > 0) {
                    $("#var_box").append(trHtml);
                    api.height($(document).height()).reset(); //刷新窗口大小
                }
            }
        }).showModal();
        //如果是修改状态，将对象传进去
        if (objNum == 1) {
            m.data = obj;
        }
    }

    //创建URL变量HTML
    function createUrlHtml() {
        var trHtml = '<tr class="td_c">'
            + '<td><input type="text" name="itemPath" class="td-input" style="width:90%;" readonly="readonly" /></td>'
            + '<td><input type="text" name="itemPattern" class="td-input" style="width:90%;" readonly="readonly" /></td>'
            + '<td><input type="text" name="itemQuerystring" class="td-input" style="width:90%;" readonly="readonly" /></td>'
            + '<td>'
            + '<a title="编辑" class="img-btn" onclick="showUrlDialog(this);"><i class="iconfont icon-edit"></i></a>'
            + '<a title="删除" class="img-btn" onclick="delUrlNode(this);"><i class="iconfont icon-delete"></i></a>'
            + '</td>'
            + '</tr>'
        return trHtml;
    }

    //删除节点
    function delUrlNode(obj) {
        $(obj).parent().parent().remove();
        api.height($(document).height() - 32).reset();  //刷新窗口大小
    }
    /*=============================以上窗口方法=================================*/
</script>
</head>

<body>
<div class="div-content">
  <dl>
    <dt>频道类型</dt>
    <dd>
      <select id="pageType" class="select">
        <option value="">请选择类型...</option>
        <option value="index">首页</option>
        <option value="list">列表页</option>
        <option value="detail">详情页</option>
        <option value="category">栏目页</option>
      </select>
    </dd>
  </dl>
  <dl>
    <dt>调用名称</dt>
    <dd>
      <input type="text" id="urlKey" class="input txt" />
      <span>*调用的唯一标识，英文、数字、下划线</span>
    </dd>
  </dl>
  <dl>
    <dt>生成文件名</dt>
    <dd>
      <input type="text" id="urlPage" value="" class="input txt" /> .aspx
    </dd>
  </dl>
  <dl>
    <dt>模板文件名</dt>
    <dd>
      <input type="text" id="pageTemplet" value="" class="input txt" /> .html
    </dd>
  </dl>
  <dl>
    <dt>分页大小</dt>
    <dd>
      <input type="text" id="urlPageSize" value="" class="input small" onkeydown="return checkNumber(event);" /> 条
      <span>只有当需要分页时才设置该值</span>
    </dd>
  </dl>
  <dl>
    <dt>URL表达式</dt>
    <dd>
      <a id="btnVarAdd" class="icon-btn"><i class="iconfont icon-add"></i><span>添加表达式</span></a>
    </dd>
  </dl>
  <dl>
    <dt></dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="550">
        <thead>
          <tr>
            <th width="30%">重写表达式</th>
            <th width="30%">正则表达式</th>
            <th width="30%">传输参数</th>
            <th width="10%">操作</th>
          </tr>
        </thead>
        <tbody id="var_box"></tbody>
      </table>
    </dd>
  </dl>
</div>
</body>
</html>
