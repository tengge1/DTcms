// 管理内页脚本
/*====================================
 *基于JQuery 1.10.2以上主框架
 *DTcms管理界面
 *作者：一些事情
====================================*/
//页面加载完成时执行
$(function () {
    initContentTab(); //初始化TAB
    $(".toolbar").ruleLayoutToolbar();
    $(".imglist").ruleLayoutImgList();
    $(".content-tab").ruleLayoutTab();
    $(".tab-content").ruleLayoutContent();
    //$(".table-container").ruleLayoutTable();
    $(".page-footer").ruleLayoutFooter();
    //窗口尺寸改变时
    $(window).resize(function () {
        //延迟执行,防止多次触发
        setTimeout(function () {
            $("#floatHead").children("div").width($("#floatHead").width());
            $(".toolbar").ruleLayoutToolbar();
            $("#floatHead").height($("#floatHead").children("div").outerHeight());
            $(".imglist").ruleLayoutImgList();
            $(".content-tab").ruleLayoutTab();
            $(".tab-content").ruleLayoutContent();
            //$(".table-container").ruleLayoutTable();
            $(".page-footer").ruleLayoutFooter();
        }, 200);
    });
});

//工具栏响应式
$.fn.ruleLayoutToolbar = function() {
	var fun = function(parentObj){
		//先移除事件和样式
		parentObj.removeClass("mini");
		parentObj.removeClass("list");
		parentObj.find(".l-list").css("display","");
		parentObj.find(".menu-btn").unbind("click");
		//声明变量
		var rightObj = parentObj.find(".r-list");
		var objWidth = parentObj.width();
		var rightWidth = 0;
		var iconWidth = 0;
		var menuWidth = 0;
		//计算宽度
		parentObj.find(".icon-list li").each(function() {
			iconWidth += $(this).width();
		});
		parentObj.find(".menu-list").children().each(function() {
			menuWidth += $(this).width();
		});
		if(rightObj.length > 0){
			rightWidth = rightObj.width();
		}
		
		//判断及设置相应的样式和事件
		if((iconWidth+rightWidth)<objWidth && menuWidth<objWidth && (iconWidth+menuWidth+rightWidth)>objWidth){
			parentObj.addClass("list");
		}else if((iconWidth+rightWidth)>objWidth || menuWidth>objWidth || (iconWidth+menuWidth+rightWidth)>objWidth){
			parentObj.addClass("mini");
			var listObj = parentObj.find(".l-list");
			parentObj.find(".menu-btn").click(function(e){
				e.stopPropagation();
				if(listObj.is(":hidden")){
					listObj.show();
				}else{
					listObj.hide();
				}
			});
			listObj.click(function(e) {
				if(parentObj.hasClass("mini")){
					e.stopPropagation(); 
				}
			}); 
			$(document).click(function(e){
				if(parentObj.hasClass("mini")){
					listObj.hide();
				}
			});
		}
	};
	return $(this).each(function() {
		fun($(this));						 
	});
}

//图文列表排列响应式
$.fn.ruleLayoutImgList = function() {
	var fun = function(parentObj){
		var divWidth = parentObj.width();
		var liSpace = parseFloat(parentObj.find("ul li").css("margin-left"));
		var rankCount = Math.floor((divWidth+liSpace)/235);
		var liWidth = ((divWidth+liSpace)/rankCount) - liSpace;
		parentObj.find("ul li").width(liWidth);
	};
	return $(this).each(function() {
		fun($(this));						 
	});
}

//内容页TAB表头响应式
$.fn.ruleLayoutTab = function () {
    var fun = function (parentObj) {
        parentObj.removeClass("mini"); //计算前先清除一下样式
        tabWidth = parentObj.width();
        liWidth = 0;
        parentObj.find("ul li").each(function () {
            liWidth += $(this).outerWidth();
        });
        if (liWidth > tabWidth) {
            parentObj.addClass("mini");
        } else {
            parentObj.removeClass("mini");
        }
    };
    return $(this).each(function () {
        fun($(this));
    });
}

//内容页TAB内容响应式
$.fn.ruleLayoutContent = function () {
    var fun = function (parentObj) {
        parentObj.removeClass("mini"); //计算前先清除一下样式
        var contentWidth = $("body").width() - parentObj.find("dl dt").eq(0).outerWidth();
        var dlMaxWidth = 0;
        parentObj.find("dl dd").children().each(function () {
            if ($(this).outerWidth() > dlMaxWidth) {
                dlMaxWidth = $(this).outerWidth();
            }
        });
        parentObj.find("dl dd table").each(function () {
            if (parseFloat($(this).css("min-width")) > dlMaxWidth) {
                dlMaxWidth = parseFloat($(this).css("min-width"));
            }
        });
        if (dlMaxWidth > contentWidth) {
            parentObj.addClass("mini");
        } else {
            parentObj.removeClass("mini");
        }
    };
    return $(this).each(function () {
        fun($(this));
    });
}

//表格处理事件
$.fn.ruleLayoutTable = function () {
    var fun = function (parentObj) {
        var tableWidth = parentObj.children("table").outerWidth();
        var minWidth = parseFloat(parentObj.children("table").css("min-width"));
        if (minWidth > tableWidth) {
            parentObj.children("table").width(minWidth);
        } else {
            parentObj.children("table").css("width", "");
        }
    };
    return $(this).each(function () {
        fun($(this));
    });
}

//页面底部按钮事件
$.fn.ruleLayoutFooter = function() {
	var fun = function(parentObj){
		var winHeight = $(window).height();
		var docHeight = $(document).height();
		if(docHeight > winHeight){
			parentObj.find(".btn-wrap").css("position", "fixed");
		}else{
			parentObj.find(".btn-wrap").css("position", "static");
		}
	};
	return $(this).each(function() {
		fun($(this));						 
	});
}

//初始化Tab事件
function initContentTab() {
    var parentObj = $(".content-tab");
    var tabObj = $('<div class="tab-title"><span>' + parentObj.find("ul li a.selected").text()
        + '</span><i class="iconfont icon-arrow-down"></i></div>');
    parentObj.children().children("ul").before(tabObj);
    parentObj.find("ul li a").click(function () {
        var tabNum = $(this).parent().index("li")
        //设置点击后的切换样式
        $(this).parent().parent().find("li a").removeClass("selected");
        $(this).addClass("selected");
        tabObj.children("span").text($(this).text());
        //根据参数决定显示内容
        $(".tab-content").hide();
        $(".tab-content").eq(tabNum).show();
        $(".page-footer").ruleLayoutFooter();
    });
}

//初始化Tree目录结构
function initCategoryHtml(parentObj, layNum){
    $(parentObj).find('li.layer-'+layNum).each(function(i){
        var liObj = $(this);
        var nextNum = layNum + 1;
        if(liObj.next('.layer-' + nextNum).length > 0){
            initCategoryHtml(parentObj, nextNum);
            var newObj = $('<ul></ul>').appendTo(liObj);
            moveCategoryHtml(liObj, newObj, nextNum);
        }
    });
}
function moveCategoryHtml(liObj, newObj, nextNum){
    if(liObj.next('.layer-' + nextNum).length > 0){
        liObj.next('.layer-' + nextNum).appendTo(newObj);
        moveCategoryHtml(liObj, newObj, nextNum);
    }
}
//初始化Tree目录事件
$.fn.initCategoryTree = function(isOpen) {
    var fCategoryTree = function(parentObj){
        //遍历所有的UL
        parentObj.find("ul").each(function (i) {
            //遍历UL第一层LI
            $(this).children("li").each(function () {
                var liObj = $(this);
                //判断是否有子菜单和设置距左距离
				var parentIconLenght = liObj.parent().parent().children(".tbody").children(".index").children(".icon").length; //父节点的左距离
                var indexObj = liObj.children(".tbody").children(".index"); //需要树型的目录列
                //设置左距离
                if(parentIconLenght == 0){
                    parentIconLenght = 1;
                }
                for (var n = 0; n <= parentIconLenght; n++) { //注意<=
                    $('<i class="icon"></i>').prependTo(indexObj); //插入到index前面
                }
                //设置按钮和图标
                indexObj.children(".icon").last().addClass("iconfont icon-folder"); //设置最后一个图标
                //如果有下级菜单
                if (liObj.children("ul").length > 0) {
                    //如果要求全部展开
                    if(isOpen){
                        indexObj.children(".icon").eq(-2).addClass("expandable iconfont icon-open"); //设置图标展开状态
                    }else{
                        indexObj.children(".icon").eq(-2).addClass("expandable iconfont icon-close"); //设置图标闭合状态
                        liObj.children("ul").hide(); //隐藏下级的UL
                    }
					//绑定单击事件
					indexObj.children(".expandable").click(function () {
                        //如果菜单已展开则闭合
                        if($(this).hasClass("icon-open")){
                            //设置自身的右图标为+号
                            $(this).removeClass("icon-open");
                            $(this).addClass("icon-close");
                            //隐藏自身父节点的UL子菜单
                            $(this).parent().parent().parent().children("ul").slideUp(300);
                        }else{
                            //设置自身的右图标为-号
							$(this).removeClass("icon-close");
							$(this).addClass("icon-open");
							//显示自身父节点的UL子菜单
							$(this).parent().parent().parent().children("ul").slideDown(300);
                        }
                    });
                }else{
                    indexObj.children(".icon").eq(-2).addClass("iconfont icon-csac");
                }
            });
            //显示第一个UL
            if (i == 0) {
                $(this).show();
                //展开第一个菜单
                if ($(this).children("li").first().children("ul").length > 0) {
					$(this).children("li").first().children(".tbody").children(".index").children(".expandable").removeClass("icon-close");
					$(this).children("li").first().children(".tbody").children(".index").children(".expandable").addClass("icon-open");
					$(this).children("li").first().children("ul").show();
				}
            }
        });
    };
	return $(this).each(function() {
		fCategoryTree($(this));						 
	});
}