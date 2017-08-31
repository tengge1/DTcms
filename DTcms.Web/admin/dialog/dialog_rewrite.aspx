<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog_rewrite.aspx.cs" Inherits="DTcms.Web.admin.dialog.dialog_rewrite" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>URL重写表达式</title>
<link rel="stylesheet" type="text/css" href="../skin/icon/iconfont.css" />
<link rel="stylesheet" type="text/css" href="../skin/default/style.css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    var api = top.dialog.get(window); //获取父窗体对象
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
        }
        ]);

        //如果有传入对象则赋值
        if ($(api.data).length > 0) {
            var parentObj = $(api.data).parent().parent(); //取得节点父对象
            //分析正则表达式
            var strPath = $(parentObj).find("input[name='itemPath']").val().replace(new RegExp("{\\d+}", "g"), "(.*)"); //替换成正则表达式
            var strPattern = $(parentObj).find("input[name='itemPattern']").val();
            var pathArr = strPattern.match(strPath);
            //开始赋值
            $("#txtItemPath").val($(parentObj).find("input[name='itemPath']").val());
            if ($(parentObj).find("input[name='itemQuerystring']").val() != "") {
                var querystrArr = $(parentObj).find("input[name='itemQuerystring']").val().split("^");
                for (i = 0; i < querystrArr.length; i++) {
                    //插入一行TR并赋值变量
                    var trObj = $("#tr_box").append(createVarHtml());
                    var strArr = querystrArr[i].split("=");
                    $(trObj).children("tr").eq(i).find("input[name='varName']").val(strArr[0]);
                    //赋值正则表达式
                    $(trObj).children("tr").eq(i).find("input[name='varExp']").val(pathArr[i + 1]);
                }
            }
        }
    });

    //提交表单处理
    function submitForm() {
        var currDocument = $(document);
        //验证表单
        if ($("#txtItemPath").val() == "") {
            top.dialog({
                title: '提示',
                content: '请填写重写表达式！',
                okValue: '确定',
                ok: function () { },
                onclose: function () {
                    $("#txtItemPath", currDocument).focus();
                }
            }).showModal(api);
            return false;
        }
        //查找变量表达式
        var patternStr = $("#txtItemPath").val();
        var querystringStr = "";
        $("#tr_box tr").each(function (i) {
            if ($(this).find("input[name='varName']").val() != "" && $(this).find("input[name='varExp']").val() != "") {
                patternStr = patternStr.replace("{" + i + "}", $(this).find("input[name='varExp']").val());
                querystringStr += $(this).find("input[name='varName']").val() + "=$" + (i + 1);
                if (i < $("#tr_box tr").length - 1) {
                    querystringStr += "^";
                }
            }
        });
        //添加或修改
        if ($(api.data).length > 0) {
            var parentObj = $(api.data).parent().parent();
            parentObj.find("input[name='itemPath']").val($("#txtItemPath").val());
            parentObj.find("input[name='itemPattern']").val(patternStr);
            parentObj.find("input[name='itemQuerystring']").val(querystringStr);
            api.close();
        } else {
            var trHtml = '<tr class="td_c">'
            + '<td><input type="text" name="itemPath" class="td-input" value="' + $("#txtItemPath").val() + '" style="width:90%;" readonly="readonly" /></td>'
            + '<td><input type="text" name="itemPattern" class="td-input" value="' + patternStr + '" style="width:90%;" readonly="readonly" /></td>'
            + '<td><input type="text" name="itemQuerystring" class="td-input" value="' + querystringStr + '" style="width:90%;" readonly="readonly" /></td>'
            + '<td>'
            + '<i class="icon"></i>'
            + '<a title="编辑" class="img-btn" onclick="showUrlDialog(this);"><i class="iconfont icon-edit"></i></a>'
            + '<a title="删除" class="img-btn" onclick="delUrlNode(this);"><i class="iconfont icon-delete"></i></a>'
            + '</td>'
            + '</tr>'
            api.close(trHtml).remove();
        }
        return false;
    }

    //创建URL变量HTML
    function createVarHtml() {
        varHtml = '<tr class="td_c">'
        + '<td><select class="select1" onchange="setVal(this, \'varName\');"><option value="">@规定参数</option><option value="id">文章ID</option><option value="category_id">类别ID</option><option value="page">分页页码</option></select>\n'
        + '<input type="text" name="varName" class="td-input" style="width:50%;" /></td>'
        + '<td><select class="select1" onchange="setVal(this, \'varExp\');"><option value="">@参考正则</option><option value="(\\w+)">字符串</option><option value="(\\d+)">数字</option></select>\n'
        + '<input type="text" name="varExp" class="td-input" style="width:50%;" /></td>'
        + '<td><a title="删除" class="img-btn" onclick="delVarTr(this);"><i class="iconfont icon-delete"></i></a></td>'
        + '</tr>'
        return varHtml;
    }

    //添加一行变量
    function addVarTr() {
        varHtml = createVarHtml();
        $("#tr_box").append(varHtml);
        api.height($(document).height()).reset(); //刷新窗口大小
    }
    //删除一行变量
    function delVarTr(obj) {
        $(obj).parent().parent().remove();
        api.height($(document).height() - 32).reset(); //刷新窗口大小
    }

    //赋值参考选项
    function setVal(obj, objName) {
        var value = $(obj).children("option:selected").attr("value");
        if (value != "") {
            $(obj).next("input[name='" + objName + "']").val(value);
        }
    }
</script>
</head>

<body>
<div class="div-content">
    <dl>
      <dt>重写表达式</dt>
      <dd>
        <input type="text" id="txtItemPath" class="input normal" />
        <span class="Validform_checktip">*如：article-{0}-{1}.aspx，{n}表示第N个变量</span>
      </dd>
    </dl>
    <dl>
    <dt>传输参数</dt>
    <dd>
      <a class="icon-btn" onclick="addVarTr();"><i class="iconfont icon-add"></i><span>添加变量</span></a>
    </dd>
  </dl>
    <dl>
      <dt></dt>
      <dd>
        <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="100%">
        <thead>
          <tr>
            <th width="45%">变量名称</th>
            <th width="45%">正则表达式</th>
            <th width="8%">操作</th>
          </tr>
        </thead>
        <tbody id="tr_box"></tbody>
      </table>
      </dd>
    </dl>
</div>
</body>
</html>
