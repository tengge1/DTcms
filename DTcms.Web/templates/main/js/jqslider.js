$.fn.jqslider = function() {
    var fun = function(parentObj){
        var picTotal = parentObj.find('.list-box li').length; //总共几张
        var currentIndex = 0; // 当前要显示的索引
        var toDisplayPicNumber = 1; // 定时器下一次执行用到的索引
        var timer = null; // 定时器对象
        var hideIndex = 0; // 要隐藏的索引
        var iTime = 6000; // 间隔时间

        //插入HTML代码
        var speedHtml = '<div class="speed-box"><div class="speed-btn">';
        for(var i=0; i<picTotal; i++){
            speedHtml += '<a href="javascript:;"></a>';
        }
        speedHtml += '</div></div>';
        $(speedHtml).appendTo(parentObj); //插入进度按钮
        $('<a class="prev-btn" href="javascript:;"></a><a class="next-btn" href="javascript:;"></a>').appendTo(parentObj); //插入左右按钮
        parentObj.find('.list-box li').eq(0).css({'opacity':1,'z-index':2});
        parentObj.find('.speed-btn a').eq(0).addClass('selected');
        
        //移上移下显示隐藏
        parentObj.hover(function(){
        	clearTimeout(timer);
            parentObj.find('.prev-btn').show();
            parentObj.find('.next-btn').show();
        },function(){
        	parentObj.find('.prev-btn').hide();
            parentObj.find('.next-btn').hide();
        	timer = setTimeout(PicNumClick, iTime);
        	//离开从新赋值
        	toDisplayPicNumber = (currentIndex + 1 == picTotal ? 0 : currentIndex + 1);
        });
        //下一个
        parentObj.find('.next-btn').click(function(){
        	currentIndex = (currentIndex + 1 == picTotal ? 0 : currentIndex + 1); 
        	DisplayPic();
        });
        //上一个
        parentObj.find('.prev-btn').click(function(){
        	currentIndex = (currentIndex - 1 < 0 ? picTotal-1 : currentIndex -1); 
        	DisplayPic();
        });
        //中间按钮
        parentObj.find('.speed-btn a').click(function(){
        	currentIndex = $(this).index();
        	DisplayPic();
        });
        
        //图片切换效果
        function DisplayPic() {
            clearTimeout(timer);
			if(currentIndex == hideIndex){return;}
            parentObj.find('.list-box li').eq(currentIndex).css({'opacity':1,'z-index':2});
            parentObj.find('.list-box li').eq(hideIndex).css({'z-index':5}).stop(true,true).animate({'opacity':0}, 300 , function(){
            	hideIndex = currentIndex;
            	$(this).css({'z-index':1});
            });
            parentObj.find('.speed-btn a').eq(currentIndex).addClass('selected').siblings().removeClass('selected');
        }
        //循环调用自身
        function PicNumClick() {
            parentObj.find(".speed-btn a").eq(toDisplayPicNumber).trigger("click");
            toDisplayPicNumber = (toDisplayPicNumber + 1) % picTotal;
            timer = setTimeout(PicNumClick,iTime);
        }
        setTimeout(PicNumClick, iTime);
	};
	return $(this).each(function() {
		fun($(this));						 
	});
}