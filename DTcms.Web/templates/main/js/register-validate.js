//=====================初始化代码======================
$(function () {
	//同意条款
	$("#chkAgree").click(function () {
		if ($(this).is(":checked")) {
			$("#btnSubmit").prop("disabled", false);
		} else {
			$("#btnSubmit").prop("disabled", true);
		}
	});
	
    //初始化验证表单
	$("#regform").Validform({
		tiptype:3,
		callback:function(form){
			//AJAX提交表单
            $(form).ajaxSubmit({
                beforeSubmit: showRequest,
                success: showResponse,
                error: showError,
                url: $("#regform").attr("url"),
                type: "post",
                dataType: "json",
                timeout: 60000
            });
            return false;
		}
	});

    //表单提交前
    function showRequest(formData, jqForm, options) {
		$("#btnSubmit").val("正在提交...")
        $("#btnSubmit").prop("disabled", true);
        $("#chkAgree").prop("disabled", true);
    }
    //表单提交后
    function showResponse(data, textStatus) {
        if (data.status == 1) { //成功
			var d = dialog({content:data.msg}).show();
			setTimeout(function () {
				d.close().remove();
				location.href = data.url;
			}, 2000);
        } else { //失败
            dialog({title:'提示', content:data.msg, okValue:'确定', ok:function (){}}).showModal();
            $("#btnSubmit").val("再次提交");
            $("#btnSubmit").prop("disabled", false);
            $("#chkAgree").prop("disabled", false);
        }
    }
    //表单提交出错
    function showError(XMLHttpRequest, textStatus, errorThrown) {
        dialog({title:'提示', content:"状态：" + textStatus + "；出错提示：" + errorThrown, okValue:'确定', ok:function (){}}).showModal();
        $("#btnSubmit").val("再次提交");
        $("#btnSubmit").prop("disabled", false);
        $("#chkAgree").prop("disabled", false);
    }
});