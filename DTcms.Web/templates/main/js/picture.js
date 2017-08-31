//==================图文详细页函数=====================

$(function(){
	//载入第一张图片
    preview();
	//图片放大镜效果
	$(".jqzoom").jqueryzoom({xzoom:368,yzoom:438});
});

//图片预览小图移动效果,页面加载时触发
$(function(){
	var tempLength = 0; //临时变量,当前移动的长度
	var viewNum = 5; //设置每次显示图片的个数量
	var moveNum = 2; //每次移动的数量
	var moveTime = 300; //移动速度,毫秒
	var scrollDiv = $(".pic-scroll .items ul"); //进行移动动画的容器
	var scrollItems = $(".pic-scroll .items ul li"); //移动容器里的集合
	var moveLength = scrollItems.eq(0).width() * moveNum; //计算每次移动的长度
	var countLength = (scrollItems.length - viewNum) * scrollItems.eq(0).width(); //计算总长度,总个数*单个长度
	  
	//下一张
	$(".pic-scroll .next").bind("click",function(){
		if(tempLength < countLength){
			if((countLength - tempLength) > moveLength){
				scrollDiv.animate({left:"-=" + moveLength + "px"}, moveTime);
				tempLength += moveLength;
			}else{
				scrollDiv.animate({left:"-=" + (countLength - tempLength) + "px"}, moveTime);
				tempLength += (countLength - tempLength);
			}
		}
	});
	//上一张
	$(".pic-scroll .prev").bind("click",function(){
		if(tempLength > 0){
			if(tempLength > moveLength){
				scrollDiv.animate({left: "+=" + moveLength + "px"}, moveTime);
				tempLength -= moveLength;
			}else{
				scrollDiv.animate({left: "+=" + tempLength + "px"}, moveTime);
				tempLength = 0;
			}
		}
	});
});

//鼠标经过预览图片函数
function preview(obj){
	var argnum = arguments.length;
	if(argnum == 1){
		$(".jqzoom img").attr("src",$(obj).attr("src"));
		$(".jqzoom img").attr("jqimg",$(obj).attr("bimg"));
	}else{
		$(".jqzoom img").attr("jqimg",$(".items li img").eq(0).attr("bimg"));
    	$(".jqzoom img").attr("src",$(".items li img").eq(0).attr("src"));
	}
}
//==================图文详细页函数=====================