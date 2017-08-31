/* 
*作者：一些事情
*时间：2015-4-28
*需要结合jquery.Jcrop一起使用
----------------------------------------------------------*/
var jcrop_api, boundx, boundy;
//初始化WebUploader
function initWebUploader(webpath){
	var parentObj = $("#upload-box");
	var btnObj = $('<div class="upload-btn">上传图片</div>').appendTo(parentObj);
	var uploader = WebUploader.create({
		auto: true, //自动上传
		swf: webpath + 'scripts/webuploader/uploader.swf', //SWF路径
		server: webpath + 'tools/upload_ajax.ashx?action=SingleFile', //上传地址
		pick: {
            id: btnObj,
            multiple: false
        },
		accept: {
            title: 'Images',
            extensions: 'jpg,jpge,png,gif',
            mimeTypes: 'image/*'
        },
		formData: {
        	'DelFilePath': '' //定义参数
        },
		fileVal: 'Filedata', //上传域的名称
		sendAsBinary: true, //二进制流上传
		fileSingleSizeLimit: 2048 * 1024 //文件大小
    });
	//当validate不通过时，会以派送错误事件的形式通知
	uploader.on('error', function (type) {
		switch (type) {
			case 'Q_EXCEED_NUM_LIMIT':
				alert("错误：上传文件数量过多！");
				break;
			case 'Q_EXCEED_SIZE_LIMIT':
				alert("错误：文件总大小超出限制！");
				break;
			case 'F_EXCEED_SIZE':
				alert("错误：文件大小超出限制！");
				break;
			case 'Q_TYPE_DENIED':
				alert("错误：禁止上传该类型文件！");
				break;
			case 'F_DUPLICATE':
				alert("错误：请勿重复上传该文件！");
				break;
			default:
				alert('错误代码：' + type);
				break;
		}
	});
	//当有文件添加进来的时候
	uploader.on('fileQueued',function (file){
		uploader.options.formData.DelFilePath = $('#hideFileName').val();
		//防止重复创建
		if (parentObj.children(".upload-progress").length == 0) {
			//创建进度条
			var fileProgressObj = $('<div class="upload-progress"></div>').appendTo(parentObj);
			var progressText = $('<span class="txt">正在上传，请稍候...</span>').appendTo(fileProgressObj);
			var progressBar = $('<span class="bar"><b></b></span>').appendTo(fileProgressObj);
			var progressCancel = $('<a class="close" title="取消上传">关闭</a>').appendTo(fileProgressObj);
			//绑定点击事件
			progressCancel.click(function () {
				uploader.cancelFile(file);
				fileProgressObj.remove();
			});
		}
	});
	//文件上传过程中创建进度条实时显示
    uploader.on('uploadProgress',function(file,percentage){
		var progressObj = parentObj.children(".upload-progress");
        progressObj.children(".txt").html(file.name);
        progressObj.find(".bar b").width(percentage * 100 + "%");
	});
	//当文件上传出错时触发
    uploader.on('uploadError',function(file,reason){
		uploader.removeFile(file); //从队列中移除
        alert(file.name + "上传失败，错误代码：" + reason);
	});
	//当文件上传成功时触发
    uploader.on('uploadSuccess',function(file,data){
		//初始化裁剪插件
		if(!jcrop_api){
			InitJcrop();
		}
		jcrop_api.setImage(data.path, function () {
			$('#hideFileName').val(data.path);
			$('#preview').attr('src', data.path);
			var bounds = jcrop_api.getBounds();
			boundx = bounds[0];
			boundy = bounds[1];
			jcrop_api.setSelect([0, 0, 180, 180]);
		});
	});
	//不管成功或者失败，文件上传完成时触发
    uploader.on('uploadComplete',function(file){
		var progressObj = parentObj.children(".upload-progress");
		progressObj.children(".txt").html("上传完成");
		//如果队列为空，则移除进度条
		if (uploader.getStats().queueNum == 0) {
			progressObj.remove();
		}
	});
}

//提交裁剪
function CropSubmit(obj) {
	if($("#hideFileName").val()==''){
		dialog({title:'提示', content:'请先上传一张图片再提交保存！', okValue:'确定', ok:function (){}}).showModal();
		return false;
	}
	var btnTxt = $(obj).val();
	$.ajax({
		type:"post",
		url:$("#uploadForm").attr("url"),
		data:$("#uploadForm").serialize(),
		dataType:"json",
		beforeSend:function(formData, jqForm, options){
			$(obj).prop("disabled", true).val("请稍候..");
		},
		success:function(data, textStatus){
			if (data.status == 1) {
				var d = dialog({content:'头像上传成功！'}).show();
				setTimeout(function () {
					d.close().remove();
					location.reload();
				}, 2000);
			}else{
				dialog({title:'提示', content:data.msg, okValue:'确定', ok:function (){}}).showModal();
			}
		},
		error:function(XMLHttpRequest, textStatus, errorThrown){
			dialog({title:'提示', content:"状态：" + textStatus + "；出错提示：" + errorThrown, okValue:'确定', ok:function (){}}).showModal();
		},
		complete: function (XMLHttpRequest, textStatus) {
			$(obj).prop("disabled",false).val(btnTxt);
		},
		timeout: 20000
	});
	return false;
}

//初始化Jcrop
function InitJcrop(){
	$("#target").Jcrop({
		onChange: updatePreview,
		onSelect: updatePreview,
		aspectRatio: 1,
		boxWidth: 350,
		boxHeight: 350
	},function(){
		jcrop_api = this;
	});
};
//头像预览图
function updatePreview(c){
	if (parseInt(c.w) > 0){
		var rx = 180 / c.w;
		var ry = 180 / c.h;
		$('#preview').css({
			width: Math.round(rx * boundx) + 'px',
			height: Math.round(ry * boundy) + 'px',
			marginLeft: '-' + Math.round(rx * c.x) + 'px',
			marginTop: '-' + Math.round(ry * c.y) + 'px'
		});
		$('#hideX1').val(Math.round(c.x));
        $('#hideY1').val(Math.round(c.y));
        $('#hideWidth').val(Math.round(c.w));
        $('#hideHeight').val(Math.round(c.h));
	}
};