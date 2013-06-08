//预设版式
var _pagestyles = [{
    imgContainer: [{ width: 420, height: 420, left: 0, top: 0, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 360, height: 15, left: 30, top: 375, css: 'page_txt_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 1, align: 'left'}],
    pageItemCss: 'page_item',
    isSkip: false,
    pageNum: 0,
    styleid: 0,
    outPageNum: '0_0'

}, {
    imgContainer: [{ width: 380, height: 380, left: 20, top: 20, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 360, height: 15, left: 30, top: 375, css: 'page_txt_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 1, align: 'left'}],
    pageItemCss: 'page_item',
    isSkip: false,
    pageNum: 0,
    styleid: 1,
    outPageNum: '0_0'
}, {
    imgContainer: [{ width: 420, height: 300, left: 0, top: 0, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 360, height: 15, left: 30, top: 375, css: 'page_txt_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 1, align: 'left'}],
    pageItemCss: 'page_item',
    isSkip: false,
    pageNum: 0,
    styleid: 2,
    outPageNum: '0_0'
}, {
    imgContainer: [{ width: 420, height: 300, left: 0, top: 120, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 360, height: 15, left: 30, top: 25, css: 'page_txt_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 1, align: 'left'}],
    pageItemCss: 'page_item',
    isSkip: false,
    pageNum: 0,
    styleid: 3,
    outPageNum: '0_0'
}, {
    imgContainer: [{ width: 300, height: 420, left: 120, top: 0, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 360, height: 15, left: 30, top: 375, css: 'page_txt_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 1, align: 'left'}],
    pageItemCss: 'page_item',
    isSkip: false,
    pageNum: 0,
    styleid: 4,
    outPageNum: '0_0'
}, {
    imgContainer: [{ width: 300, height: 420, left: 0, top: 0, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 360, height: 15, left: 30, top: 375, css: 'page_txt_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 1, align: 'left'}],
    pageItemCss: 'page_item',
    isSkip: false,
    pageNum: 0,
    styleid: 5,
    outPageNum: '0_0'
}, {
    imgContainer: [],
    txtContainer: [{ width: 360, height: 260, left: 30, top: 80, css: 'page_txt_container mutiline_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 8, align: 'left'}],
    pageItemCss: 'page_item',
    isSkip: false,
    pageNum: 0,
    styleid: 6,
    outPageNum: '0_0'
}, {
    imgContainer: [],
    txtContainer: [{ width: 360, height: 360, left: 30, top: 30, css: 'page_txt_container mutiline_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 6, align: 'left'}],
    pageItemCss: 'page_item',
    isSkip: false,
    pageNum: 0,
    styleid: 7,
    outPageNum: '0_0'
}, {
    imgContainer: [{ width: 840, height: 420, left: 0, top: 0, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [],
    pageItemCss: 'page_item skip_page_item',
    isSkip: true,
    pageNum: 0,
    styleid: 8,
    outPageNum: '2_3'
}, {
    imgContainer: [{ width: 800, height: 380, left: 20, top: 20, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [],
    pageItemCss: 'page_item skip_page_item',
    isSkip: true,
    pageNum: 0,
    styleid: 9,
    outPageNum: '2_3'
}, {
    imgContainer: [{ width: 610, height: 380, left: 210, top: 20, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 180, height: 380, left: 20, top: 20, css: 'page_txt_container mutiline_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 12, align: 'left'}],
    pageItemCss: 'page_item skip_page_item',
    isSkip: true,
    pageNum: 0,
    styleid: 10,
    outPageNum: '2_3'
}, {
    imgContainer: [{ width: 610, height: 380, left: 20, top: 20, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 180, height: 380, left: 640, top: 20, css: 'page_txt_container mutiline_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 12, align: 'left'}],
    pageItemCss: 'page_item skip_page_item',
    isSkip: true,
    pageNum: 0,
    styleid: 11,
    outPageNum: '2_3'
}, {
    imgContainer: [{ width: 630, height: 420, left: 0, top: 0, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 190, height: 400, left: 640, top: 10, css: 'page_txt_container mutiline_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 12, align: 'left'}],
    pageItemCss: 'page_item skip_page_item',
    isSkip: true,
    pageNum: 0,
    styleid: 12,
    outPageNum: '2_3'
}, {
    imgContainer: [{ width: 630, height: 420, left: 210, top: 0, css: 'page_img_container', conid: 'img_1'}],
    txtContainer: [{ width: 190, height: 400, left: 10, top: 10, css: 'page_txt_container mutiline_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 12, align: 'left'}],
    pageItemCss: 'page_item skip_page_item',
    isSkip: true,
    pageNum: 0,
    styleid: 13,
    outPageNum: '2_3'
}]

var _editorData = {
    bookID: 1,
    userID: 1,
    //书本名称
    bookName: '',
    //书本序言
    bookBrief: '',
    //书本作者
    bookAuthor: '',
    //当前编辑页
    currEditorPNum: 0,
    //当前编辑的两个页面
    outPNum: '-1_0',
    //封面页
    coverPNum: 0,
    //封底
    bottomPNum: -1,
    //封面折页
    firstFlodPNum: -2,
    //封底折页
    lastFlodPNum: -3,
    //正在编辑的txt
    editorTextarea: {},
    //总页数
    totalCount: 44,
    //最少页数
    minCount: 44,
    //可用来扣减的页数
    changeCount: 0,

    setCurrEidtorPNum: function (p, op) {
        this.currEditorPNum = p;
        if (op != undefined) {
            this.outPNum = op;
        }
    }
}

/*
图片面板
*/
function ImageView(options) {
    var config = {

        //每次获取照片数
        count: 500,
        //跳过个数
        skip: 0,
        //每页个数
        pageCount: 9,
        dotImg: '/content/pagestyle/images/dot_spirt.png',
        loadingImg: '/content/pagestyle/editor_img/loader_32x32.gif',
        slideSelector: 'a.bind_slide',
        imgViewList: 'imageViewList',
        container: 'imgViewPanel',
        itemWidth: 81,
        imgWidth: 80,
        imgHeight: 80,
        //图片起始目录
        imgPath: '/UserFile/',
        //图片扩展名
        plusName: '_thumb.jpg'
    }
    $.extend(config, options || {});

    var _imgViewListObj = $("#" + config.imgViewList);
    /*
    获取书本图片
    */
    function getBookImage() {
        $.ajax({
            url: '/FileUpload/AjaxGetBookImage',
            data: { userid: _editorData.userID, bookid: _editorData.bookID, count: 500, skip: 0 },
            type: 'get',
            dataType: 'json',
            cache: false,
            success: function (r) {
                if (r.success) {
                    var len = r.images.length, images = r.images, items = [];
                    if (len > 0) {
                        for (var i = 0; i < len; i++) {
                            var usedIcon = images[i].usednum > 0 ? '<div class="img_used_icon" id="imgusedicon_' + images[i].id + '"></div>' : '';
                            items.push('<li class="need_load" style="position:relative;"><img class="need_load_img draggable_img" imageid="' + images[i].id + '" src="' + config.dotImg + '" alt="' + config.imgPath + images[i].name + config.plusName + '" />' + usedIcon + '</li>');
                        }
                        _imgViewListObj.prepend(items.join(''));
                        len = (_imgViewListObj.find('li')).length;
                        if (len > config.pageCount) {
                            _imgViewListObj.width(len * config.itemWidth);
                        }
                        loadItemImg(0, config.pageCount);
                    }
                }
            },
            error: function () { }
        })
    };
    /*
    初始化imgview控件
    */
    function _init() {
        //加载书本图片
        getBookImage();

        var slideBtns = $("#" + config.container + " " + config.slideSelector)
        slideBtns.bind("click", function (e) {
            imageViewSlide(e, config.pageCount - 1, config.itemWidth);
        })
        slideBtns.bind("mouseover", function (e) {
            imageViewSlide(e, config.pageCount - 1, config.itemWidth);
        })
        slideBtns.bind("mouseout", function (e) {
            imageViewStopSlide(config.imgViewList);
        })
    }
    /*
    添加图片
    */
    function _addItemImg(imgid, imgname) {
        var item = $('<li class="need_load"><img class="need_load_img draggable_img" imageid="' + imgid + '" src="' + config.dotImg + '" alt="' + config.imgPath + imgname + config.plusName + '" alt="" /></li>');
        _imgViewListObj.prepend(item);
        bindEvent2Img(item);
    }

    var t;
    /*
    绑定图片事件
    */
    function bindEvent2Img(item) {
        if (item.hasClass("need_load")) {
            item.block({ message: '<img style="width:32px; height:32px;" src="' + config.loadingImg + '" alt="" />', css: { border: 'none', backgroundColor: 'none' }, overlayCSS: { backgroundColor: '#efefef'} });
            item.removeClass("need_load");
        }
        var theImg = item.find('img.need_load_img');
        if (theImg.length > 0) {
            theImg[0].onload = function () {
                var _this = $(this);
                _this.parent('li').unblock();
                _this.parent('li').bind('mouseover', function () {
                    var delIcon = $(this).find("a.delete_img_icon");
                    if (delIcon.length > 0) {
                        delIcon.show();
                    } else {
                        var imgID = $(this).find("img").attr("imageid");
                        $('<a href="javascript:DeleteImgViewItem(' + imgID + ');" title="删除" class="delete_img_icon"></a>').appendTo($(this));
                    }
                }).bind('mouseout', function () {
                    var delIcon = $(this).find("a.delete_img_icon");
                    if (delIcon.length > 0) {
                        delIcon.hide();
                    }
                })


                _this.unbind('mouseover').bind('mouseover', function (e) {
                    showPreview(e);
                });
                _this.unbind('mouseout').bind('mouseout', function (e) {
                    clearTimeout(t);
                    hidePreview(e);
                });
                _this.draggable({ helper: 'clone', appendTo: 'body' }); //设置拖拽
            }
            theImg[0].src = theImg.attr("alt");
        }
    }
    /*
    //延迟加载图片
    */
    function loadItemImg(sIndex, count) {
        var wrapperObj = _imgViewListObj, items = wrapperObj.find('li'), stopIndex = sIndex + count;
        for (sIndex; sIndex <= stopIndex; sIndex++) {
            var item = items.eq(sIndex);
            bindEvent2Img(item);
        }
    }
    /*
    //清除预览
    */
    function hidePreview(e) {
        var wrapperObj = $("div.preview_tip");
        if (wrapperObj.length > 0) {
            wrapperObj.stop().hide();
        }
    }
    /*
    加载预览
    */
    function showPreview(e) {
        var targetObj = $(e.target), imageid = targetObj.attr("imageid"),
        wrapperObj = $("div.preview_tip");
        if (wrapperObj.length <= 0) {
            wrapperObj = $('<div class="preview_tip" style="position:absolute; z-index:110;"></div>');
            $("div.editor_wrapper").append(wrapperObj[0]);
            wrapperObj.hide();
        } else {
            var previewImg = wrapperObj.find('img[imageid=' + imageid + ']');
            if (previewImg.length > 0) {
                wrapperObj.find('img').hide();
                var width = previewImg.width();
                wrapperObj.width(width).height(previewImg.height());
                var left = targetObj.offset().left, top = targetObj.offset().top;
                if (left + width > 960) {
                    left = left - width + config.imgWidth;
                }
                wrapperObj.animate({ left: left, top: top + config.imgHeight }, 150).unblock();
                previewImg.show();
                wrapperObj.show();
                return;
            }
        }
        wrapperObj.show();
        wrapperObj.block({ message: '<img style="width:32px; height:32px;" src="' + config.loadingImg + '" alt="" />', css: { border: 'none', backgroundColor: 'none' }, overlayCSS: { backgroundColor: '#efefef'} });
        var left = targetObj.offset().left, top = targetObj.offset().top, wrapperWidth = wrapperObj.width();
        if (left + wrapperWidth > 960) {
            left = left - wrapperWidth + config.imgWidth;
        }
        wrapperObj.animate({ left: left, top: top + config.imgHeight }, 150);
        t = setTimeout(function () {
            var img = new Image();
            img.onload = function () {
                var width = this.width / 3, height = this.height / 3;
                wrapperObj.width(width).height(height);
                wrapperObj.find("img").hide();
                wrapperObj.find('img[imageid=' + imageid + ']').remove();
                $(this).width(width).height(height).attr("imageid", imageid).appendTo(wrapperObj);
                wrapperObj.unblock();
            };
            img.src = targetObj.attr('src').replace(config.plusName, '_edit.jpg');
        }, 1000);
    }
    /*
    //图片滚动
    */
    function imageViewSlide(e, count, itemWidth) {
        var wrapperObj = _imgViewListObj, items = wrapperObj.find("li"),
    len = items.length, photoViewWidth = len * itemWidth,
    left = wrapperObj.position().left, startIndex = Math.floor(-left / itemWidth),
    nextItem = items.eq(startIndex + count), ismover = e.handleObj.type != 'click' || false,
    direction = !($(e.target).hasClass('slide_l'));
        if (len > config.pageCount) {
            wrapperObj.width(photoViewWidth);
        }
        if (direction) {
            if (ismover) {
                if (items.eq(-count).length > 0) {
                    var endLeft = -items.eq(-count).position().left;
                    if (endLeft < left) {
                        wrapperObj.stop(true, false).delay(1200).animate({ left: endLeft }, (endLeft - left) * (-5));
                        if (wrapperObj.find("li.need_load").length > 0) {
                            loadItemImg(startIndex + count, len - count - startIndex);
                        }
                    }
                }
            } else {
                if (nextItem.length > 0) {
                    left = -nextItem.position().left;
                    wrapperObj.stop(true, false).animate({ left: left }, 350);
                    loadItemImg(startIndex + count, count); //加载图片                   
                }
            }
        } else {
            if (ismover) {
                var endLeft = 0;
                if (left < endLeft) {
                    wrapperObj.stop(true, false).delay(1200).animate({ left: endLeft }, (endLeft - left) * 5);
                }
            } else {
                nextItem = wrapperObj.find("li").eq(startIndex > count ? startIndex - count : 0);
                left = -nextItem.position().left;
                wrapperObj.stop(true, false).animate({ left: left }, 350);
            }
        }
    }
    /*
    停止滚动
    */
    function imageViewStopSlide() {
        var itemWidth = config.itemWidth, wrapperObj = _imgViewListObj;
        wrapperObj.stop();
        var left = wrapperObj.position().left, stopIndex = Math.floor(-left / itemWidth), items = wrapperObj.find("li"),
    endLeft = items.eq(stopIndex).position().left;
        wrapperObj.stop(true, false).animate({ left: -endLeft }, 150);
    }

    /*
    公有方法
    */
    this.init = _init;
    this.addItemImg = _addItemImg;
}

/*
图片上传面板
*/
function ImageUploader(options) {
    var config = { container: "uploadPanel", queue: "uploadQueue", selectBtn: "uploadSelectFile", clearBtn: "clearUploadQueue", uploadBtn: "uploadFiles", addImgBtn: "addImageBtn", closeSelector: 'a.panel_close', showUploadPanel: "#ShowUploadPanelBtn" };
    $.extend(config, options);

    var uploadPanel = $("#" + config.container),
    queueObj = $('#' + config.queue),
    uploadBtnObj = $("#" + config.uploadBtn),
    clearBtnObj = $("#" + config.clearBtn),
    selectBtnObj = $("#" + config.selectBtn),
     _imgViewObj = new ImageView({ userid: _editorData.userID, bookid: _editorData.bookID, imgPath: '/UserFile/' + _editorData.userID + '/' });

    var uploader = new plupload.Uploader({
        runtimes: 'flash',
        browse_button: config.selectBtn,
        container: config.container,
        max_file_size: '10mb',
        url: '/FileUpload/UploadFile',
        multipart_params: { bookid: _editorData.bookID, userid: _editorData.userID },
        flash_swf_url: '/Content/pagestyle/js/plupload/plupload.flash.swf',
        filters: [{ title: "Image files", extensions: "jpg,jpeg,png"}]
    });

    function removeFile(id) {
        var up = uploader;
        var tr = $('#' + id);
        tr.fadeOut(300, function () {
            tr.remove();
        });

        for (var i = 0, len = up.files.length; i < len; i++) {
            if (up.files[i].id == id) {
                up.removeFile(up.files[i]);
                break;
            }
        }
    }

    /*
    初始化上传的plupload
    */
    function initPluploader() {
        uploader.bind('Init', function (up, params) {
            uploadBtnObj.hide();
            clearBtnObj.hide();
            selectBtnObj.bind("click", resetUploaderPosition);
        });
        uploader.bind('FilesAdded', function (up, files) {
            var items = [];
            for (var i in files) {
                items.push('<tr id="' + files[i].id + '"><td>' + files[i].name + '</td><td> ' + plupload.formatSize(files[i].size) + '</td> <td><span class="percent">0%</span><span class="info"></span></td><td class="action_btn"><a href="javascript:;" class="clear_file" id="clear_' + files[i].id + '">[取消]</a></td></tr>');
            }
            queueObj.append(items.join(''));
            queueObj.find("a.clear_file").bind('click', function () {
                var id = $(this).attr('id'), idInfo = id.split('_');
                if (idInfo.length == 2) {
                    removeFile(idInfo[1]);
                }
            })
            uploadBtnObj.show();
            clearBtnObj.show();
        });

        uploader.bind('FilesRemoved', function (up, files) {
            var isUploading = false;
            for (var i = 0, len = files.length; i < len; i++) {
                if (files[i].status == plupload.UPLOADING) {
                    up.stop();
                    up.start();
                    isUploading = true;
                }
            }
            if (up.files.length <= 0) {
                uploadBtnObj.hide();
                clearBtnObj.hide();
            } else {
                var isQueued = false;
                for (var i = 0, len = up.files.length; i < len; i++) {
                    if (up.files[i].status == plupload.QUEUED) {
                        isQueued = true;
                        break;
                    }
                }
                if (isQueued && !isUploading) {
                    uploadBtnObj.show();
                } else {
                    uploadBtnObj.hide();
                }
            }
        });

        uploader.bind('FilesRemoved', function (up, files) {

        })

        //文件上传成功
        uploader.bind('FileUploaded', function (up, file, r) {
            var response = parseObj(r.response), spanInfo = $("#" + file.id).find('span.info'), tdBtn = $("#" + file.id).find('td.action_btn');
            $("#" + file.id).find('span.info').text('-上传完成，正在处理...');
            if (response.success) {
                spanInfo.css("color", "#009900").text('-上传成功');
                tdBtn.find('a').text('[清除]');
                var imgObj = response.image;
                _imgViewObj.addItemImg(imgObj.id, imgObj.name);
            } else {
                spanInfo.css("color", "#aa0000").text('-上传失败');
            }
        })

        uploader.bind("UploadFile", function (up, file) {

        })

        uploader.bind('UploadProgress', function (up, file) {
            $("#" + file.id).find('span.percent').text(file.percent + "%");

        });

        uploader.bind('UploadComplete', function (up, file) {
            uploadPanel.find("div.upload_panel_btn").unblock();
            uploadBtnObj.hide();
        })

        uploader.init();
    }
    /*
    用来把字符串转成josn
    */
    function parseObj(strData) {
        return (new Function("return " + strData))();
    }
    /*
    重置uploaderflash的位置
    */
    function resetUploaderPosition() {
        var uploaderPostion = { left: selectBtnObj.offset().left - uploadPanel.offset().left, top: selectBtnObj.offset().top - uploadPanel.offset().top };
        $("#" + uploader.id + "_flash_container").css({ left: uploaderPostion.left + "px", top: uploaderPostion.top + "px" })
            .width(selectBtnObj.outerWidth()).height(selectBtnObj.outerHeight());
    }
    /*
    初始化上传面板
    */
    function _init() {
        uploadPanel.find(config.closeSelector).bind("click", function () {
            uploadPanel.css("z-index", 101);
            uploadPanel.css("visibility", "hidden");
        });
        $("#" + config.addImgBtn).bind("click", function () {
            $("div.upload_panel:visible").css("z-index", 101);
            uploadPanel.css("z-index", 102);
            uploadPanel.css("visibility", "visible");
            resetUploaderPosition();
        })

        uploadBtnObj.bind('click', function () {
            var btnPanel = uploadPanel.find("div.upload_panel_btn");
            if (uploader.files.length > 0) {
                uploader.start();
                uploadBtnObj.hide();
                var msg = $('<div style="padding:10px;">正在上传，请稍后...[<span class="hide_panel" style="cursor:pointer;">后台上传</span>]&nbsp;&nbsp[<span class="hide_block" style="cursor:pointer;">重新选择</span>]</div>');
                msg.find('span.hide_panel').bind('click', function () {
                    uploadPanel.css("visibility", "hidden");
                })
                msg.find('span.hide_block').bind('click', function () {
                    btnPanel.unblock();
                })
                btnPanel.block({ message: msg[0], baseZ: 100000 });
            }
        });

        $('#' + config.clearBtn).bind('click', function () {
            $("#" + config.queue).empty();
            for (var k = 0, len = uploader.files.length; k < len; k++) {
                uploader.removeFile(uploader.files[0]);
            }
        })

        $(config.showUploadPanel).bind('click', function () {
            $("div.upload_panel:visible").css("z-index", 101);
            uploadPanel.css("z-index", 102);
            uploadPanel.css("visibility", "visible");
            resetUploaderPosition();
        })

        //初始化plupload
        initPluploader();
    }

    /*
    公有方法
    */
    this.init = _init;
}
/*
清空页面图片
*/
function ClearPageImg(cImgID) {
    var idInfo = cImgID.split("|"), conObj = $("#bookPageWrapper div[pagenum=" + idInfo[0] + "]").find("div.page_img_container[conid=" + idInfo[1] + "]");
    if (conObj.length > 0) {
        conObj.empty();
        _editorData.setCurrEidtorPNum(idInfo[0]);
        setPageEditAttr(idInfo[0]);
    }
}
/*
删除imgview图片
*/
function DeleteImgViewItem(imgID) {
    var imgItem = $("#imageViewList li img[imageid=" + imgID + "]");
    if (imgItem.length > 0) {
        //imgItem.parent('li').remove();
        $.ajax({
            url: '/FileUpload/AjaxDeleteBookImage',
            data: { userid: _editorData.userID, bookid: _editorData.bookID, imageid: imgID },
            type: 'get',
            dataType: 'json',
            success: function (r) {
                if (r.success) {
                    $("#imageViewList li img[imageid=" + r.imageid + "]").parent('li').remove();
                }
            }
        })
    }
}

//获取16进制颜色值
function getHexColor(rgb) {
    if (rgb.indexOf('#') > -1) {
        return rgb;
    }
    var rgbInfo = (rgb.toLowerCase().replace('rgb(', '').replace(')', '').replace(/ /g, '')).split(',');
    var r = rgbInfo[0], g = rgbInfo[1], b = rgbInfo[2], rle = new Array(r, g, b), hexcode = "#";
    for (x = 0; x < 3; x++) {
        if (rle[x] < 0 || rle[x] > 255) {
            return rgb;
        }
        var n = rle[x];
        var c = "0123456789abcdef";
        var b = "";
        var a = n % 16;
        b = c.substr(a, 1);
        a = (n - a) / 16;
        hexcode += c.substr(a, 1) + b;
    }
    return hexcode;
}
//获取RGB颜色值
function getRGBColor(hex) {
    var a = hex;
    if (a.substr(0, 1) == "#") {
        a = a.substring(1);
    }
    if (a.length != 6) {
        return hex;
    }
    a = a.toLowerCase();
    var b = new Array();
    for (x = 0; x < 3; x++) {
        b[0] = a.substr(x * 2, 2);
        b[3] = "0123456789abcdef";
        b[1] = b[0].substr(0, 1);
        b[2] = b[0].substr(1, 1);
        b[20 + x] = b[3].indexOf(b[1]) * 16 + b[3].indexOf(b[2]);
    }
    return 'rgb(' + b[20] + ', ' + b[21] + ', ' + b[22] + ')'
}
//设置页面编辑状态
//给页面添加 isedit 属性
function setPageEditAttr(pnum) {
    $('div.book_page div[pagenum=' + pnum + ']').attr("isedit", true);
}

function startTime() {
    //add a zero in front of numbers which<10 
    function checkTime(i) {
        if (i < 10) {
            i = "0" + i
        }
        return i
    }
    var today = new Date()
    var h = today.getHours()
    var m = today.getMinutes()
    var s = today.getSeconds()
    //add a zero in front of numbers which<10 
    h = checkTime(h)
    m = checkTime(m)
    s = checkTime(s)
    document.getElementById('timeClock').innerHTML = h + ":" + m + ":" + s
    t = setTimeout('startTime()', 100)
}


//显示提示信息
function showEditMsg(msg) {
    var noticeCon = $("#noticeMsgCon"), msgCon = $("#noticeMsgCon>div");
    //today = new Date(), now = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds()
    msgCon.css("position", "absolute");
    msgCon.animate({ top: -33 }, 400, function () {
        noticeCon.empty().append("<div>" + msg + "</div>");

        //        $("#noticeMsgCon>div").delay(3000).animate({ top: -33 }, 400, function () {
        //            noticeCon.empty().append("<div>" + now + "-" + msg + "</div>");
        //        });
    })

}
//获取左右边
function getEditorSide(pnum) {
    return pnum == 0 ? 'r' : pnum == -1 ? 'l' : pnum == -3 ? 'l' : pnum % 2 == 0 ? 'l' : 'r';
}
//重置编辑工具
function resetEditorTool(pnum) {
    if (getEditorSide(pnum) == 'l') {
        $('div.book_panel').addClass('curr_edit_l');
        $('div.editor_tool').addClass('curr_edit_l');
        $('#pageStylePanel').addClass('curr_edit_l');
        $('#bgColorPanel').addClass('curr_edit_l');
    } else {
        $('div.book_panel').removeClass('curr_edit_l');
        $('div.editor_tool').removeClass('curr_edit_l');
        $('#pageStylePanel').removeClass('curr_edit_l');
        $('#bgColorPanel').removeClass('curr_edit_l');
    }
    if (pnum == 1 || pnum == _editorData.totalCount) {
        $("#pageStylePanel").css("width", "420px").find("div.skip_pagestyle_item").hide();
    } else {
        $("#pageStylePanel").css("width", "820px").find("div.skip_pagestyle_item").show();
    }

    var pObj = $("#bookPageWrapper div[pagenum=" + pnum + "]"), styleID = pObj.attr("styleid"),
    bgColor = getHexColor(pObj.css("background-color")).replace("#", ""),
    blockC = { message: '<img style="width:24px; height:22px;" src="/content/pagestyle/images/dot_spirt.png" class="pagestyle_item_checked" alt="" />', css: { border: 'none', backgroundColor: 'none', width: '24', height: '22' }, overlayCSS: { backgroundColor: '#efefef'} },
    isVisible = $("#pageStylePanel").is(":visible");
    if (!isVisible) {
        $("#pageStylePanel").show();
    }
    $("#pageStylePanel a[styleid=" + styleID + "]").parent('div.pagestyle_item').block(blockC).siblings().unblock();
    if (!isVisible) {
        $("#pageStylePanel").hide();
    }
    $("#bgColorPanel a.color_" + bgColor.toUpperCase()).parent('div.bgcolor_item').addClass("selected").siblings().removeClass("selected");
    var pnumT = (parseInt(pnum) < 10 && parseInt(pnum) > 0) ? "0" + pnum : pnum;
    $("#panNumTxt").text(pnumT);
}

/*
页面
*/
function BookPage(options) {
    var config = {
        //图片容器筛选器
        imgConSelector: '.page_img_container',
        acceptSelector: 'img.draggable_img',
        loadingImg: '/content/pagestyle/editor_img/loader_32x32.gif',
        loadBlockConfig: { message: '<img style="width:32px; height:32px;" src="/content/pagestyle/editor_img/loader_32x32.gif" alt="" />', css: { border: 'none', backgroundColor: 'none' }, overlayCSS: { backgroundColor: '#efefef' }, timeout: 180000 },
        saveBookBtn: '#saveBookBtn',
        savePageDataBtn: '#SavePageDataBtn'
    };

    $.extend(config, options || {});
    var _pageStyle = new PageStyle();
    //设置页面版式
    function _setPageStyle(styleOpt) {
        _pageStyle.setOptions(styleOpt);
    }

    //格式化输入
    function formatHtmlChar(str) {
        return str.replace(/\\/g, '\\\\').replace(/"/g, '\\"').replace(/&quot;/g, '\\"');
    }

    //获取页面内容
    function _getPageDataJson(pageObj) {
        var images = $(pageObj).find("img.page_img");
        var imageJson = "", textJson = "";
        //收集页面图片
        if (images.length > 0) {
            for (var i = 0, imgLength = images.length; i < imgLength; i++) {
                var imgObj = images.eq(i), conObj = imgObj.parent("div.page_img_container"),
                  imageid = (typeof imgObj.attr("imageid") == "undefined") ? 0 : imgObj.attr("imageid"),
                imagesrc = imgObj.attr("src");
                imagesrc = imagesrc.indexOf("http://") > -1 ? imagesrc.substring(imagesrc.toLowerCase().indexOf("/userfile")) : imagesrc;
                imageJson += '{"conid":"' + conObj.attr("conid") + '", "width":' + imgObj.width() + ', "height":' + imgObj.height() + ',"src": "' + imagesrc + '","x":' + imgObj.position().left + ',"y":' + imgObj.position().top + ',"imageid":' + imageid + ',"conx":' + conObj.position().left + ',"cony":' + conObj.position().top + ',"conwidth":' + conObj.width() + ',"conheight":' + conObj.height() + ',"orgwidth":' + imgObj.attr("orgwidth") + ',"orgheight":' + imgObj.attr("orgheight") + '}';
                if (i != imgLength - 1) {
                    imageJson += ",";
                }
            }
        }
        //收集页面文字
        var texts = $(pageObj).find("textarea");
        if (texts.length > 0) {
            for (var j = 0, textLength = texts.length; j < textLength; j++) {
                var txtObj = texts.eq(j), conObj = txtObj.parent('div.page_txt_container'), color = getHexColor(txtObj.css("color")),
                textStr = txtObj.val(), textStr = formatHtmlChar(textStr);
                textJson += '{"conid":"' + conObj.attr("conid") + '", content:"' + textStr + '","color":"' + color + '","fontsize":' + txtObj.css("font-size").replace("px", "") + ',"textalign":"' + txtObj.css("text-align") + '","conx":' + conObj.position().left + ',"cony":' + conObj.position().top + ',"conwidth":' + conObj.width() + ',"conheight":' + conObj.height() + ',"issingle":"' + txtObj.attr("issingle") + '"}'
                if (j != textLength - 1) {
                    textJson += ",";
                }
            }
        }
        var bgColor = getHexColor(pageObj.css("background-color")),
        isSkip = pageObj.hasClass('skip_page_item'),
        isFinish = true;

        return '{"isfinish":"' + isFinish + '","isskip":"' + isSkip + '","pagenum":' + pageObj.attr("pagenum") + ',"opagenum":"' + pageObj.parent("div.book_page").attr("pagenum") + '","styleid": ' + pageObj.attr("styleid") + ',"side":"' + pageObj.attr("side") + '","bgcolor":"' + bgColor + '", "image":[' + imageJson + '],"text":[' + textJson + '] }';
    }

    var pageImgs = [], pageTxts = [];
    //提取页面内容
    function _extraPageData(opnum, pnum) {
        var pageObj = $('div.book_page[pagenum=' + opnum + ']');
        if (pnum != undefined) {
            pageObj = $('div.book_page div[pagenum=' + pnum + ']');
        }
        var images = $(pageObj).find("img.page_img"), texts = $(pageObj).find("textarea");
        if (images.length > 0) {
            pageImgs = images;
        }
        if (texts.length > 0) {
            pageTxts = texts;
        }
    }
    //设置页面内容到对应容器
    function renderPageData(pdata) {
        var pCss = pdata.isskip == 'true' ? "page_item skip_page_item" : "page_item book_page_" + getEditorSide(pdata.pnum);
        _setPageStyle($.extend(true, {}, _pagestyles[pdata.styleid], { pageItemCss: pCss, pageNum: pdata.pnum, outPageNum: pdata.opnum }));
        if (pdata.pnum == _editorData.coverPNum) {
            var maintop = 320, left, subtop = 360, subleft;
            if (pdata.styleid == 3) {
                maintop = 30;
                subtop = 70;
            }
            if (pdata.styleid != 6 && pdata.styleid != 7) {
                var styleOpts = $.extend(true, {}, _pagestyles[pdata.styleid], { pageItemCss: pCss, outPageNum: pdata.opnum, pageNum: pdata.pnum, txtContainer: [{ width: 360, height: 32, css: 'page_txt_container cover_title', txt: '点击输入主标题', left: null, top: maintop, conid: 'txt_1', rows: '1' }, { width: 360, height: 18, css: 'page_txt_container cover_subtitle', txt: '点击输入副标题', conid: 'txt_2', rows: '1', top: subtop}] });
                _setPageStyle(styleOpts);
            }
        }
        _pageStyle.render();
        var pageObj = $('div.book_page div[pagenum=' + pdata.pnum + ']'),
        imgs = pdata.image,
        txts = pdata.text;
        pageObj.css("background-color", pdata.bgcolor);
        if (imgs) {
            var imgItems = [];
            if (Object.prototype.toString.apply(imgs) === '[object Array]') {
                imgItems = imgs;
            } else {
                imgItems.push(imgs);
            }
            for (var i = 0, len = imgItems.length; i < len; i++) {
                var content = pageObj.find('div.page_img_container[conid=' + imgItems[i].conid + ']');
                if (content.length > 0) {
                    _setPageImg(content, $('<img src="' + imgItems[i].src + '" imageid="' + imgItems[i].imageid + '"  />'), imgItems[i]);
                }
            }
        }
        if (txts) {
            var txtItems = [];
            if (Object.prototype.toString.apply(txts) === '[object Array]') {
                txtItems = txts;
            } else {
                txtItems.push(txts);
            }
            for (var k = 0, len = txtItems.length; k < len; k++) {
                var content = pageObj.find('div.page_txt_container[conid=' + txtItems[k].conid + '] textarea') || [];
                if (content.length > 0) {
                    content.val(txtItems[k].content).css({ "text-align": txtItems[k].textalign, color: txtItems[k].color });
                }
            }
        }
    }
    //顺序填充页面内容
    function _setPageData(pnum) {
        var pageObj = $('div.book_page div[pagenum=' + pnum + ']');
        if (pageImgs.length > 0) {
            var imgCons = pageObj.find('div.page_img_container') || [];
            if (imgCons.length > 0) {
                for (var i = 0, len = imgCons.length; i < len; i++) {
                    if (pageImgs[i] != undefined) {
                        _setPageImg(imgCons.eq(i), pageImgs.eq(i));
                    }
                }
            }
        }
        if (pageTxts.length > 0) {
            var txtCons = pageObj.find('div.page_txt_container textarea') || [];
            if (txtCons.length > 0) {
                for (var j = 0, len = txtCons.length; j < len; j++) {
                    if (pageTxts[j] != undefined && txtCons.eq(j).attr('issingle') == pageTxts.eq(j).attr('issingle')) {
                        txtCons.eq(j).val(pageTxts.eq(j).val());
                    }
                }
            }
        }
    }
    //设置页面图片
    function _setPageImg(content, imgdata, posdata) {
        var editImage = new Image(), axis, imageWidth = content.innerWidth(), imageHeight = content.innerHeight();
        content.block(config.loadBlockConfig);
        editImage.onload = function () {
            var widthScale = editImage.width / imageWidth, heightScale = editImage.height / imageHeight;
            $(editImage).attr("orgwidth", editImage.width).attr("orgheight", editImage.height);

            if (widthScale > heightScale) {
                editImage.height = imageHeight;
                axis = 'x'
                editImage.width = editImage.width / heightScale;
            } else {
                editImage.width = imageWidth;
                axis = 'y';
                editImage.height = editImage.height / widthScale;
            };
            content.empty().append(editImage).unblock;

            var x1, y1, x2, y2;
            if (axis == 'x') { y1 = 0; y2 = 0; x1 = content.offset().left + (content.width() - this.width); x2 = content.offset().left; }
            else { x1 = 0; x2 = 0; y1 = content.offset().top + (content.height() - this.height); y2 = content.offset().top; }
            content.children("img").draggable({ axis: axis, start: function (event, ui) {
                var pnum = $(this).parent('div.page_img_container').parent('div.page_item').attr('pagenum');
                setPageEditAttr(pnum);
            }, containment: [x1, y1, x2, y2], stop: function (event, ui) { }
            });
        }
        editImage.src = imgdata.attr("src").replace("_thumb", "_edit");
        $(editImage).attr("imageid", imgdata.attr("imageid")).addClass("page_img");
        if (posdata != undefined) {
            $(editImage).css({ "left": posdata.x + "px", "top": posdata.y + "px" });
        }
    }
    //更换页面版式
    function _changePageStyle(pnum, styleOpts) {
        var pageObj = $('div.book_page div[pagenum=' + pnum + ']');
        if (pageObj.length < 0) {
            return;
        }
        if (styleOpts.isSkip) {
            _extraPageData(styleOpts.outPageNum);
        } else {
            _extraPageData(styleOpts.outPageNum, pnum);
        }
        _setPageStyle(styleOpts);
        _pageStyle.render();
        _setPageData(pnum);
        setPageEditAttr(pnum);
    }
    //保存页面内容
    function _savePageData(opnum) {
        var opInfo = opnum.split('_');
        var pageObj = $('div.book_page div[pagenum=' + opInfo[0] + ']'), pageObj1 = $('div.book_page div[pagenum=' + opInfo[1] + ']'), pageJson = '{\"pdatas\":[';
        if (pageObj.length > 0 && pageObj.attr("isedit") === "true") {
            pageJson += _getPageDataJson(pageObj) + ",";
        }
        if (pageObj1.length > 0 && pageObj1.attr("isedit") === "true") {
            pageJson += _getPageDataJson(pageObj1);
        }
        pageJson += ']}';
        if (pageJson != '' && (pageObj.attr("isedit") === "true" || pageObj1.attr("isedit") === "true")) {
            $.ajax({
                url: '/intime/SavePageData',
                dataType: 'json',
                data: { userid: _editorData.userID, bookid: _editorData.bookID, pdata: pageJson },
                type: 'post',
                cache: false,
                success: function (r) {
                    if (r.success) {
                        var pviews = r.pviews, noneused = r.noneusedimg, usedimg = r.usedimg, usedHtml = '<div class="img_used_icon"></div>';
                        if (pviews != undefined && pviews.length > 0) {
                            for (var i = 0, len = pviews.length; i < len; i++) {
                                var skipCss = pviews[i].isskip ? "skip" : "";
                                $("#pview_" + pviews[i].pnum).css("background-image", "url(" + pviews[i].src + "?t=" + Math.random() + ")").addClass(skipCss);
                                $("#mpview_" + pviews[i].pnum).css("background-image", "url(" + pviews[i].src.replace('pview', 'pthumb') + "?t=" + Math.random() + ")").addClass(skipCss);
                                $("div.book_page div[pagenum=" + pviews[i].pnum + "]").attr("isedit", false);
                                showEditMsg('<span style="color:#191;">页面内容自动保存成功</span>');
                                if (pviews[i].istxtover) {
                                    showEditMsg('<span style="color:#a00;">注意：文字内容过长被截断，请查看预览。</span>');
                                }

                                if (pviews[i].pnum == _editorData.coverPNum) {
                                    if (pviews[i].isskip) {
                                        $("#pview_-1").hide();
                                    } else {
                                        $("#pview_" + pviews[i].pnum).removeClass("skip");
                                        $("#pview_-1").show();
                                    }
                                }
                            }
                        }
                        if (noneused != undefined && noneused.length > 0) {
                            for (var i = 0, len = noneused.length; i < len; i++) {
                                $("#imgusedicon_" + noneused[i]).remove();
                            }
                        }
                        if (usedimg != undefined && usedimg.length > 0) {
                            for (var i = 0, len = usedimg.length; i < len; i++) {
                                var _usedHtml = $(usedHtml).attr('id', 'imgusedicon_' + usedimg[i]);
                                $("#imageViewList").find('li img[imageid=' + usedimg[i] + ']').parent('li').append(_usedHtml);
                            }
                        }
                    } else {
                        showEditMsg('<span style="color:#a00;">' + r.msg + '</span>');
                    }
                },
                error: function (r) {
                    showEditMsg('<span style="color:#a00;">页面内容自动保存失败,请尝试手动保存上一页编辑的内容或刷新页面重试</span>');
                }
            })
        }
    }
    //加载页面内容
    function _loadPageData(opnum) {
        $.ajax({
            url: '/intime/GetPageData',
            dataType: 'json',
            data: { userid: _editorData.userID, bookid: _editorData.bookID, opnum: opnum },
            type: 'post',
            cache: false,
            success: function (r) {
                if (r.length > 0) {
                    for (var i = 0, len = r.length; i < len; i++) {
                        renderPageData(r[i].layout);
                    }
                    resetEditorTool(_editorData.currEditorPNum);
                }
            },
            error: function (r) { }
        })
    }

    function _init() {
        _pageStyle.render();
        $(config.saveBookBtn).unbind('click').bind("click", function () {
            _savePageData(_editorData.outPNum);
        })
        $(config.savePageDataBtn).unbind('click').bind("click", function () {
            _savePageData(_editorData.outPNum);
        })
    }

    this.init = _init;
    this.setPageStyle = _setPageStyle;
    this.changePageStyle = _changePageStyle;
    this.savePageData = _savePageData;
    this.loadPageData = _loadPageData;
}

/*
页面预览面板
*/
function PageView(options) {
    var config = {
        pageCount: 5,

        itemWidth: 158,
        bookPageContainer: '#bookPageWrapper',
        container: '#pageViewPanel',
        pageViewList: '#pageViewList',
        slideSelector: '.bind_slide',
        pviewItemSelector: '.pview_item',
        addPageItemBtn: 'div.addpage_item'
    };

    var _pageViewListObj = $(config.pageViewList);
    /*
    //预览页滚动
    */
    function pageViewSlide(e, count, itemWidth) {
        var wrapperObj = _pageViewListObj, items = wrapperObj.find("li"),
    len = items.length, photoViewWidth = len * itemWidth,
    left = wrapperObj.position().left, startIndex = Math.floor(-left / itemWidth),
    nextItem = items.eq(startIndex + count), ismover = e.handleObj.type != 'click' || false,
    direction = !($(e.target).hasClass('slide_l'));

        wrapperObj.width(photoViewWidth);
        if (direction) {
            if (ismover) {
                var endLeft = -items.eq(-count).position().left;
                if (endLeft < left) {
                    wrapperObj.stop(true, false).delay(1200).animate({ left: endLeft }, (endLeft - left) * (-5));
                    if (wrapperObj.find("li.need_load").length > 0) {
                        //loadItemImg(startIndex + count, len - count - startIndex);
                    }
                }
            } else {
                if (nextItem.length > 0) {
                    left = -nextItem.position().left;
                    wrapperObj.stop(true, false).animate({ left: left }, 350);
                    //loadItemImg(startIndex + count, count); //加载图片                   
                }
            }
        } else {
            if (ismover) {
                var endLeft = 0;
                if (left < endLeft) {
                    wrapperObj.stop(true, false).delay(1200).animate({ left: endLeft }, (endLeft - left) * 5);
                }
            } else {
                nextItem = wrapperObj.find("li").eq(startIndex > count ? startIndex - count : 0);
                left = -nextItem.position().left;
                wrapperObj.stop(true, false).animate({ left: left }, 350);
            }
        }
    }
    /*
    停止滚动
    */
    function pageViewStopSlide() {
        var itemWidth = config.itemWidth, wrapperObj = _pageViewListObj;
        wrapperObj.stop();
        var left = wrapperObj.position().left, stopIndex = Math.floor(-left / itemWidth), items = wrapperObj.find("li"),
    endLeft = items.eq(stopIndex).position().left;
        wrapperObj.stop(true, false).animate({ left: -endLeft }, 150);
    }
    /*
    显示PageView
    */
    function render() {
        var items = [], len = _editorData.totalCount;
        for (var i = -1; i <= len; i++) {
            var tipName = i == -1 ? '封底' : i == 0 ? '封面' : i < 10 ? '0' + i : i, className = i == -1 ? 'class="selected"' : '', pagenum = 'pagenum="' + i + '_' + (i + 1) + '"';
            if (i == 1) {
                tipName = '折页';
                items.push('<li pagenum="-2_1"><div  class="pview_item" id="pview_-2" pagenum="-2"><span class="pnum_tip">' + tipName + '</span></div>');
                tipName = '01'
                items.push('<div class="pview_item" id="pview_' + i + '" pagenum="' + i + '"><span class="pnum_tip">' + tipName + '</span></div></li>');
                continue;
            }
            if (i == len) {
                items.push('<li pagenum="' + i + '_-3"><div class="pview_item" id="pview_' + i + '" pagenum="' + i + '"><span class="pnum_tip">' + tipName + '</span></div>');
                tipName = '封底折页';
                items.push('<div class="pview_item" id="pview_-3" pagenum="-3"><span class="pnum_tip">' + tipName + '</span></div></li>');
                break;
            }

            items.push('<li ' + className + ' ' + pagenum + '><div  class="pview_item" id="pview_' + i + '" pagenum="' + i + '"><span class="pnum_tip">' + tipName + '</span></div>');
            i++
            tipName = i == -1 ? '封底' : i == 0 ? '封面' : i < 10 ? '0' + i : i;
            items.push('<div class="pview_item" id="pview_' + i + '" pagenum="' + i + '"><span class="pnum_tip">' + tipName + '</span></div></li>');
        }
        items.push('<li><div  class="e_bg addpage_item"></div></li>');
        _pageViewListObj.append(items.join(''));

    }
    /*
    获取pageview的图片
    */
    function getPageViewImgs() {
        $.ajax({
            url: '/intime/GetPageView',
            dataType: 'json',
            data: { userid: _editorData.userID, bookid: _editorData.bookID },
            type: 'get',
            cache: false,
            success: function (r) {
                if (r.success) {
                    var pviews = r.pviews;
                    if (pviews != undefined && pviews.length > 0) {
                        for (var i = 0, len = pviews.length; i < len; i++) {
                            var skipCss = pviews[i].isskip ? "skip" : "";
                            $("#pview_" + pviews[i].pnum).css("background-image", "url(" + pviews[i].src + "?t=" + Math.random() + ")").addClass(skipCss);
                            if (pviews[i].isskip && pviews[i].pnum == _editorData.coverPNum) {
                                $("#pview_" + _editorData.bottomPNum).hide();
                            }
                        }
                    }
                }
            },
            error: function (r) { }
        })
    }
    /*
    选中项时调整位置
    */
    function pageViewSelectSlide(clkObj) {
        var items = _pageViewListObj.find('li'), index = items.index(clkObj.parent('li')), sIndex = index >= 3 ? (index - 2) : 0,
        sItem = items.eq(sIndex), left = sItem.position().left;
        _pageViewListObj.width(items.length * config.itemWidth);
        _pageViewListObj.stop(true, false).animate({ left: -left }, 350);
    }
    /*
    点击选中项时改变当前编辑的页
    */
    function changeEditPage(clkObj) {
        var pagenum = clkObj.attr('pagenum'), pageWrapperObj = $(config.bookPageContainer), clkObjParent = clkObj.parent('li'),
        outPageNum = clkObjParent.attr('pagenum'), theBookPage = pageWrapperObj.find('div.book_page[pagenum=' + outPageNum + ']');
        if (outPageNum == _editorData.outPNum && theBookPage.find('div.page_item').length <= 0) {
            return false;
        }
        var pagenumInfo = outPageNum.split('_');
        if (theBookPage.length > 0) {
            theBookPage.show().siblings('div.book_page').hide();
        } else {
            pageWrapperObj.find('div.book_page').hide();
            var bp = new BookPage();
            if (parseInt(pagenumInfo[0]) > _editorData.coverPNum) {
                bp.setPageStyle($.extend(true, {}, _pagestyles[0], { pageItemCss: 'page_item book_page_l', outPageNum: outPageNum, pageNum: pagenumInfo[0], txtContainer: [{ align: 'left'}] }));
                bp.init();
            }
            if (parseInt(pagenumInfo[1]) > _editorData.coverPNum) {
                bp.setPageStyle($.extend(true, {}, _pagestyles[0], { pageItemCss: 'page_item book_page_r', outPageNum: outPageNum, pageNum: pagenumInfo[1], txtContainer: [{ align: 'right'}] }));
                bp.init();
            }
            bp.loadPageData(outPageNum);
        }
        var isPlusPage = pagenum < _editorData.coverPNum ? true : false;
        if (isPlusPage) {
            pagenum = parseInt(pagenumInfo[0]) == pagenum ? parseInt(pagenumInfo[1]) : parseInt(pagenumInfo[0]);
        }
        if (pagenum >= _editorData.coverPNum) {
            _editorData.setCurrEidtorPNum(pagenum, outPageNum);
            resetEditorTool(pagenum);
        }
    }
    /*
    绑定点击事件
    */
    function bindEvent2Item() {
        _pageViewListObj.find('li div.pview_item').unbind('click').bind('click', function (e) {
            var _this = $(this);
            $(this).parent('li').addClass('selected').siblings().removeClass('selected');
            pageViewSelectSlide(_this);
            var bp = new BookPage();
            bp.savePageData(_editorData.outPNum);
            changeEditPage(_this); //更换当前编辑页
        })
    }

    this.bindEvent2Items = bindEvent2Item();
    /*
    添加页面
    */
    function addPageViewItem() {
        var nextpnum = _editorData.totalCount + 1; opnum = _editorData.totalCount + "_" + nextpnum,
                lastItem = $("#pview_" + _editorData.totalCount),
                theDelItem = $("#pageViewListClone").find("li[pagenum=" + opnum + "]");
        var addItem = '<li pagenum="' + opnum + '" style=""><div class="pview_item" id="pview_' + _editorData.totalCount + '" pagenum="' + _editorData.totalCount + '" style="' + lastItem.attr("style") + '"><span class="pnum_tip">' + _editorData.totalCount + '</span></div><div class="pview_item" id="pview_' + nextpnum + '" pagenum="' + nextpnum + '" ><span class="pnum_tip">' + nextpnum + '</span></div></li><li pagenum="' + (_editorData.totalCount + 2) + '_-3" class="page_item_disabled"><div class="pview_item" id="pview_' + (_editorData.totalCount + 2) + '" pagenum="' + (_editorData.totalCount + 2) + '" ><span class="pnum_tip">' + (_editorData.totalCount + 2) + '</span></div><div class="pview_item" id="pview_-3" pagenum="-3"><span class="pnum_tip">封底折页</span></div></li>';

        _pageViewListObj.find("li[pagenum=" + _editorData.totalCount + "_-3]").replaceWith(addItem);
        var lastPage = $("#bookPageWrapper div.book_page[pagenum=" + _editorData.totalCount + "_-3]");
        if (lastPage.length > 0) {
            lastPage.remove();
            $('#pview_' + (parseInt(_editorData.totalCount) - 1)).trigger('click');
        }
        $(config.PageViewList).find("li[pagenum=" + _editorData.totalCount + "_-3]").replaceWith(addItem);
        _editorData.totalCount = _editorData.totalCount + 2;

        $.ajax({
            url: '/intime/addbookpage',
            type: 'get',
            dataType: 'json',
            data: { userid: _editorData.userID, bookid: _editorData.bookID, pagecount: _editorData.totalCount },
            success: function (r) {

            }
        })
        bindEvent2Item();
        _pageViewListObj.width(_pageViewListObj.width() + config.itemWidth * 2);
    }

    /*
    初始化
    */
    function _init() {
        render(); //显示
        getPageViewImgs(); //获取预览页图片
        bindEvent2Item(); //绑定事件
        var slideBtns = $(config.container + " " + config.slideSelector)
        slideBtns.bind("click", function (e) {
            pageViewSlide(e, config.pageCount - 1, config.itemWidth);
        })
        slideBtns.bind("mouseover", function (e) {
            pageViewSlide(e, config.pageCount - 1, config.itemWidth);
        })
        slideBtns.bind("mouseout", function (e) {
            pageViewStopSlide(config.imgViewList);
        })
        $(config.addPageItemBtn).bind("click", function (e) {
            addPageViewItem();
        })
    }

    this.init = _init;
}
/*
页面版式
*/
function PageStyle(options) {
    var config = {
        imgContainer: [{ width: 420, height: 420, left: 0, top: 0, css: 'page_img_container', conid: 'img_1'}],
        txtContainer: [{ width: 400, height: 15, left: 10, top: 395, css: 'page_txt_container', txt: '点击输入', conid: 'txt_1', cols: 20, rows: 1}],
        isSkip: false,
        pageCss: 'book_page clearfix',
        pageItemCss: 'page_item',
        pageContainer: '#bookPageWrapper',
        pageNum: 0,
        outPageNum: '0_0',
        acceptSelector: 'img.draggable_img',
        loadBlockConfig: { message: '<img style="width:32px; height:32px;" src="/content/pagestyle/editor_img/loader_32x32.gif" alt="" />', css: { border: 'none', backgroundColor: 'none' }, overlayCSS: { backgroundColor: '#efefef' }, timeout: 180000 }
    };
    $.extend(config, options || {});
    _pageWrapperObj = $(config.pageContainer);

    //获取默认版式
    function getDefaultPageStyle(opts) {
        var styleOpts = $.extend(true, {}, _pagestyles[0], opts), theBookPage;
        theBookPage = $('<div class="' + styleOpts.pageItemCss + '" pagenum="' + styleOpts.pageNum + '" styleid="' + styleOpts.styleid + '" side="' + getEditorSide(styleOpts.pageNum) + '"></div>');
        //图片容器
        for (var i = 0, len = styleOpts.imgContainer.length; i < len; i++) {
            var imgConObj = styleOpts.imgContainer[i];
            theBookPage.append($('<div class="' + imgConObj.css + '" style="width:' + imgConObj.width + 'px;height:' + imgConObj.height + 'px;left:' + imgConObj.left + 'px;top:' + imgConObj.top + 'px" conid="' + imgConObj.conid + '"></div>'));
        };
        //文字容器
        for (var k = 0, klen = styleOpts.txtContainer.length; k < klen; k++) {
            var txtConObj = styleOpts.txtContainer[k];

            theBookPage.append($('<div class="' + txtConObj.css + '" style="width:' + txtConObj.width + 'px;height:' + txtConObj.height + 'px;left:' + txtConObj.left + 'px;top:' + txtConObj.top + 'px" conid="' + txtConObj.conid + '"><textarea issingle="false" cols="' + txtConObj.cols + '" rows="' + txtConObj.rows + '" style="width:' + txtConObj.width + 'px;height:' + txtConObj.height + 'px;" title="' + txtConObj.txt + '"></textarea></div>'));

        };
        return theBookPage;
    }
    /*
    显示
    */
    function _render() {
        var bookPageObj = _pageWrapperObj.find('div.book_page[pagenum="' + config.outPageNum + '"]'), theBookPage, siblingBookPage;

        theBookPage = $('<div class="' + config.pageItemCss + '" pagenum="' + config.pageNum + '" styleid="' + config.styleid + '" side="' + getEditorSide(config.pageNum) + '"></div>');
        //图片容器
        for (var i = 0, len = config.imgContainer.length; i < len; i++) {
            var imgConObj = config.imgContainer[i];
            theBookPage.append($('<div class="' + imgConObj.css + '" style="width:' + imgConObj.width + 'px;height:' + imgConObj.height + 'px;left:' + imgConObj.left + 'px;top:' + imgConObj.top + 'px" conid="' + imgConObj.conid + '"></div>'));
        };
        //文字容器
        for (var k = 0, klen = config.txtContainer.length; k < klen; k++) {
            var txtConObj = config.txtContainer[k];

            theBookPage.append($('<div class="' + txtConObj.css + '" style="width:' + txtConObj.width + 'px;height:' + txtConObj.height + 'px;left:' + txtConObj.left + 'px;top:' + txtConObj.top + 'px" conid="' + txtConObj.conid + '"><textarea   issingle="' + (txtConObj.rows == "1") + '" cols="' + txtConObj.cols + '" rows="' + txtConObj.rows + '" style="width:' + txtConObj.width + 'px;height:' + txtConObj.height + 'px; text-align:' + txtConObj.align + '" title="' + txtConObj.txt + '"></textarea></div>'));

        };
        if (bookPageObj.length > 0) {
            var oldPage = bookPageObj.find('div[pagenum="' + config.pageNum + '"]');
            if (config.isSkip) {
                bookPageObj.empty().append(theBookPage);
            } else if (oldPage.length > 0) {
                oldPage.replaceWith(theBookPage);
            } else {
                bookPageObj.append(theBookPage);
            }
        } else {
            bookPageObj = $('<div class="' + config.pageCss + '"  pagenum="' + config.outPageNum + '"></div>').append(theBookPage);
            _pageWrapperObj.append(bookPageObj);
        }
        bindDroppable(bookPageObj.find('div.page_img_container'));
        bindClearEvent(bookPageObj.find('div.page_img_container')); //鼠标悬停时显示清除图片按钮
        bindTxtEditor(bookPageObj.find('div.page_txt_container textarea'));
        bindChangeEditorPage(bookPageObj.find('div.page_item'));
    }
    /*
    点击切换编辑页
    */
    function bindChangeEditorPage(pageItemObj) {
        pageItemObj.bind('click', function () {
            var p = $(this).attr('pagenum'); // op = $(this).parent('div.book_page').attr('pagenum');
            _editorData.setCurrEidtorPNum(p);
            if (p > _editorData.coverPNum) {
                resetEditorTool(p);
            }
        })

        pageItemObj.bind('mouseover', function () {
            pageItemObj.find("div.page_txt_container").addClass("txt_active");
        }).bind('mouseout', function () {
            pageItemObj.find("div.page_txt_container").removeClass("txt_active");
        })
    }
    /*
    绑定拖拽事件
    */
    function bindDroppable(imgContainerObj) {
        imgContainerObj.droppable({
            accept: config.acceptSelector,
            activeClass: 'ui-state-hover',
            drop: function (event, ui) {
                var editImage = new Image(), axis, content = $(this), imageWidth = content.innerWidth(), imageHeight = content.innerHeight();
                content.block(config.loadBlockConfig);
                editImage.onload = function () {
                    var widthScale = editImage.width / imageWidth, heightScale = editImage.height / imageHeight;
                    $(editImage).attr("orgwidth", editImage.width).attr("orgheight", editImage.height);
                    if (widthScale > heightScale) {
                        editImage.height = imageHeight;
                        axis = 'x'
                        editImage.width = editImage.width / heightScale;
                    } else {
                        editImage.width = imageWidth;
                        axis = 'y';
                        editImage.height = editImage.height / widthScale;
                    };
                    content.empty().append(editImage).unblock;
                    var x1, y1, x2, y2;
                    if (axis == 'x') { y1 = 0; y2 = 0; x1 = content.offset().left + (content.width() - this.width); x2 = content.offset().left; }
                    else { x1 = 0; x2 = 0; y1 = content.offset().top + (content.height() - this.height); y2 = content.offset().top; }
                    content.children("img").draggable({ axis: axis, start: function (event, ui) {
                        var pnum = $(this).parent('div.page_img_container').parent('div.page_item').attr('pagenum');
                        setPageEditAttr(pnum);
                    }, containment: [x1, y1, x2, y2], stop: function (event, ui) { }
                    });
                    setPageEditAttr(_editorData.currEditorPNum);
                }
                editImage.src = ui.draggable.attr("src").replace("_thumb", "_edit");
                $(editImage).attr("imageid", ui.draggable.attr("imageid")).addClass("page_img");
                //选中编辑当前页
                var pDiv = $(this).parent('div.page_item');
                if (pDiv.length > 0) {
                    var p = pDiv.attr('pagenum');
                    _editorData.setCurrEidtorPNum(p);
                    resetEditorTool(p);
                }
            }
        });
    }
    /*
    绑定鼠标移入显示清空图片按钮
    */
    function bindClearEvent(imgContainerObj) {
        imgContainerObj.bind("mouseover", function () {
            var imgEditorTool = $(this).find("div.imgeditor_tool"), cImgID = $(this).parent("div").attr("pagenum") + "|" + $(this).attr("conid"), conImg = $(this).find("img");
            if (conImg.length > 0) {
                if (imgEditorTool.length > 0) {
                    imgEditorTool.show();
                } else {
                    imgContainerObj = $('<div class="imgeditor_tool"><a href="javascript:ClearPageImg(\'' + cImgID + '\');" class="tool_item clear_img" title="清除图片">X</a></div>');
                    imgContainerObj.appendTo($(this));
                }
            }
        })

        imgContainerObj.bind("mouseout", function () {
            var imgEditorTool = $(this).find("div.imgeditor_tool");
            if (imgEditorTool.length > 0) {
                imgEditorTool.hide();
            }
        })
    }
    /*
    绑定点击输入事件
    */
    function bindTxtEditor(txtContainerObj) {
        txtContainerObj.bind('click', function (e) {
            $(this).addClass('focus');
            var txtEditorTool = new TxtEditorTool({ editTextarea: $(this) });
            txtEditorTool.show();
            //选中编辑当前页
            var pDiv = $(this).parent().parent('div.page_item');
            if (pDiv.length > 0) {
                var p = pDiv.attr('pagenum');
                _editorData.setCurrEidtorPNum(p);
                resetEditorTool(p);

            }
            setPageEditAttr(_editorData.currEditorPNum);
            e.stopPropagation();
        })
        txtContainerObj.bind('blur', function () {
            $(this).removeClass('focus');
        })
        txtContainerObj.bind('focus', function () {
            $(this).removeClass('focus');
        })
        txtContainerObj.bind('change', function () {
            setPageEditAttr(_editorData.currEditorPNum);
        })

    }
    /*
    更换页面参数
    */
    function _setOptions(options) {
        $.extend(config, options || {});
    }

    this.setOptions = _setOptions;
    this.render = _render;
}

/*
文字编辑工具
*/
function TxtEditorTool(options) {
    var config = {
        editTextarea: {},
        container: '#txtEditorTool',
        alignItemSelector: '.txt_align',
        alignLeftBtn: '#txtAlignLeft',
        alignRightBtn: '#txtAlignRight',
        alignMidBtn: '#txtAlignMid',
        colorItemSelector: '.txt_color',
        colorWhite: '#txtColorWhite',
        colorBlack: '#txtColorBlack',
        canInputCount:'#canInputCount',
        alreadyInputCount: '#alreadyInputCount'
    }
    $.extend(true, config, options || {});

    function setActiveAlign(theObj) {
        theObj.parent('li' + config.alignItemSelector).addClass('active').siblings('li' + config.alignItemSelector).removeClass('active');
        config.editTextarea.focus();

    }

    function setSelectedColor(theObj) {
        theObj.parent('li' + config.colorItemSelector).addClass('selected').siblings('li' + config.colorItemSelector).removeClass('selected');
        config.editTextarea.focus();

    }

    var _toolObj = $(config.container), alignLeftBtn = $(config.alignLeftBtn), alignMidBtn = $(config.alignMidBtn)
    alignRightBtn = $(config.alignRightBtn), colorWhite = $(config.colorWhite), colorBlack = $(config.colorBlack);
    function _init() {
        _toolObj.find('li a').unbind('click');
        var align = config.editTextarea.css('text-align') || 'left', color = config.editTextarea.css('color') || '#000000';
        color = getHexColor(color);
        switch (align) {
            case 'left': setActiveAlign(alignLeftBtn);
                break;
            case 'right': setActiveAlign(alignRightBtn);
                break;
            case 'center': setActiveAlign(alignMidBtn);
                break;
            default: setActiveAlign(alignLeftBtn);
                break;
        }
        switch (color) {
            case '#ffffff': setSelectedColor(colorWhite);
                break;
            case '#000000': setSelectedColor(colorBlack);
                break;
            default: setSelectedColor(colorBlack);
                break;
        }

        var maxLength = config.editTextarea.attr("cols") || '0';


        alignLeftBtn.bind('click', function (e) {
            config.editTextarea.css('text-align', 'left');
            setActiveAlign($(this));
            e.stopPropagation();
        })
        alignMidBtn.bind('click', function (e) {
            config.editTextarea.css('text-align', 'center');
            setActiveAlign($(this));
            e.stopPropagation();
        })
        alignRightBtn.bind('click', function (e) {
            config.editTextarea.css('text-align', 'right');
            setActiveAlign($(this));
            e.stopPropagation();
        })
        colorWhite.bind('click', function (e) {
            config.editTextarea.css('color', '#ffffff');
            setSelectedColor($(this));
            e.stopPropagation();
        })
        colorBlack.bind('click', function (e) {
            config.editTextarea.css('color', '#000000');
            setSelectedColor($(this));
            e.stopPropagation();
        })

        $("#leftWrapper").bind('click', function () {
            _toolObj.hide();
        })
    }

    function _show(nOptions) {
        _setOptions(nOptions);
        _init();
        var txtAreaObj = $(config.editTextarea);
        _toolObj.css({ left: txtAreaObj.offset().left + 'px', top: (txtAreaObj.offset().top - _toolObj.outerHeight()) + 'px' }).show();
    }

    function _setOptions(nOptions) {
        $.extend(true, config, options || {});
    }

    this.show = _show;
    this.hide = function () {
        _toolObj.hide();
    }
}

/*
编辑工具类，版式，文字颜色，背景色
*/
function EditorTool(options) {
    config = {
        dotImg: '/content/pagestyle/images/dot_spirt.png',
        pagestyleBtn: '#pageStyleBtn',
        pagestylePanel: '#pageStylePanel',
        pagestylePanelClose: '#pageStylePanelClose',
        checkBlockConfig: { message: '<img style="width:24px; height:22px;" src="/content/pagestyle/images/dot_spirt.png" class="pagestyle_item_checked" alt="" />', css: { border: 'none', backgroundColor: 'none', width: '24', height: '22' }, overlayCSS: { backgroundColor: '#efefef'} }
    }
    $.extend(config, options || {});

    var _pageStylePanelObj = $(config.pagestylePanel);
    function render() {
        var items = [], startLeft = 0, startTop = -40, itemWidth = 29, longItemWidth = 59, width = 30, skipCss = "";
        for (var i = 0; i < 14; i++) {
            var left = startLeft - (i * itemWidth), top = startTop;
            if (i >= 8) {
                width = longItemWidth;
                left = left - (i - 8) * itemWidth;
                skipCss = "skip_pagestyle_item";
            }

            items.push('<div class="pagestyle_item ' + skipCss + '"  style="background-position:' + left + 'px ' + top + 'px; width:' + width + 'px"><a href="javascript:;" style="width:' + width + 'px" styleid="' + i + '"></a></div>');
        }
        _pageStylePanelObj.append(items.join(''));
        _pageStylePanelObj.find("div.pagestyle_item a").bind('click', function () {
            $(this).parent('div.pagestyle_item').block(config.checkBlockConfig).siblings().unblock();
            var styleid = $(this).attr('styleid'),
            side = getEditorSide(_editorData.currEditorPNum),
            align = side == 'l' ? 'left' : 'right',
            isSkip = _pagestyles[styleid].isSkip,
            pCss = isSkip ? "page_item skip_page_item" : "page_item book_page_" + side,
            styleOpts = $.extend(true, {}, _pagestyles[styleid], { outPageNum: _editorData.outPNum, pageNum: _editorData.currEditorPNum, pageItemCss: pCss, txtContainer: [{ align: align}] });

            var pInfo = _editorData.outPNum.split('_'), isNeedSibling = false,
            siblingStyle = $.extend(true, {}, _pagestyles[0], { outPageNum: _editorData.outPNum, pageNum: pInfo[1], pageItemCss: "page_item book_page_r", txtContainer: [{ align: 'right'}] });
            if (isSkip && parseInt(pInfo[0]) >= _editorData.coverPNum) {
                _editorData.setCurrEidtorPNum(pInfo[0]);
                styleOpts = $.extend(true, {}, _pagestyles[styleid], { outPageNum: _editorData.outPNum, pageNum: _editorData.currEditorPNum, pageItemCss: pCss });
                $("#pview_" + pInfo[0]).addClass('skip');
                resetEditorTool(pInfo[0]);
            } else {
                $("#pview_" + pInfo[0]).removeClass('skip');
                var oldPageObj = $('div.book_page div[pagenum=' + pInfo[0] + ']');
                if (oldPageObj.hasClass('skip_page_item')) {
                    var sbp = new BookPage();
                    sbp.changePageStyle(pInfo[1], siblingStyle);
                }
            }
            //如果是封面
            if (_editorData.currEditorPNum == _editorData.coverPNum || _editorData.currEditorPNum == _editorData.bottomPNum) {
                var maintop = 320, mainleft, subtop = 360, subleft;
                if (styleid == 3) {
                    maintop = 30;
                    subtop = 70;
                }
                if (styleid != 6 && styleid != 7) {
                    styleOpts = $.extend(true, {}, _pagestyles[styleid], { pageItemCss: pCss, outPageNum: _editorData.outPNum, pageNum: _editorData.currEditorPNum, txtContainer: [{ width: 360, height: 32, css: 'page_txt_container cover_title', txt: '点击输入主标题', left: null, top: maintop, conid: 'txt_1', rows: '1' }, { width: 360, height: 20, css: 'page_txt_container cover_subtitle', txt: '点击输入副标题', conid: 'txt_2', rows: '1', top: subtop}] });
                }
            }
            var bp = new BookPage();
            bp.changePageStyle(_editorData.currEditorPNum, styleOpts);
        })
    }

    function _init() {
        $(config.pagestyleBtn).bind('click', function () {
            resetEditorTool(_editorData.currEditorPNum);
            _pageStylePanelObj.fadeToggle();
        })
        $(config.pagestylePanelClose).bind('click', function () {
            _pageStylePanelObj.fadeOut();
        })
        render();
    }

    this.init = _init;
}
/*
大图预览页面
*/
function MPageView(options) {
    var config = {
        mpviewPanel: "#mpviewPanel",
        mpviewList: "#mpviewList",
        prevPage: "#prevPage",
        nextPage: "#nextPage",
        editPage: "#editPage",
        closePanel: "#closePanel",
        currViewPNum: 0,
        currViewOPNum: "-1_0",
        switch2ViewBtn: "#switch2ViewBtn",
        showPageViewBtn: "#ShowPageViewBtn"
    };
    $.extend(true, config, options);

    var _mpViewListObj = $(config.mpviewList),
     _mpViewPanel = $(config.mpviewPanel);

    function render() {
        var items = [], len = config.totalCount;
        for (var i = -1; i <= len; i++) {
            var tipName = i == -1 ? '封底' : i == 0 ? '封面' : i < 10 ? '0' + i : i, className = i == -1 ? 'class="selected"' : '', pagenum = 'pagenum="' + i + '_' + (i + 1) + '"';
            if (i == 1) {
                tipName = '折页';
                items.push('<li pagenum="-2_1"><div  class="pview_item" id="mpview_-2" pagenum="-2"><span class="pnum_tip">' + tipName + '</span></div>');
                tipName = '01'
                items.push('<div class="pview_item" id="mpview_' + i + '" pagenum="' + i + '"><span class="pnum_tip">' + tipName + '</span></div></li>');
                continue;
            }
            if (i == len) {
                items.push('<li pagenum="' + i + '_-3"><div class="pview_item" id="mpview_' + i + '" pagenum="' + i + '"><span class="pnum_tip">' + tipName + '</span></div>');
                tipName = '封底折页';
                items.push('<div class="pview_item" id="mpview_-3" pagenum="-3"><span class="pnum_tip">' + tipName + '</span></div></li>');
                break;
            }
            items.push('<li ' + className + ' ' + pagenum + '><div  class="pview_item" id="mpview_' + i + '" pagenum="' + i + '"><span class="pnum_tip">' + tipName + '</span></div>');
            i++
            tipName = i == -1 ? '封底' : i == 0 ? '封面' : i < 10 ? '0' + i : i;
            items.push('<div class="pview_item" id="mpview_' + i + '" pagenum="' + i + '"><span class="pnum_tip">' + tipName + '</span></div></li>');
        }

        _mpViewListObj.append(items.join('')).addClass("render");
    }

    function _show() {
        var isrender = _mpViewListObj.hasClass("render"),
        wHeight = $(window).height(), wWidth = $(window).width(),
        pHeight = 490, pWidth = 850,
        left = (wWidth - pWidth) / 2, top = (wHeight - pHeight) / 2;

        if (isrender) {
            _mpViewListObj.find('li[pagenum=' + _editorData.outPNum + ']').show().siblings("li").hide();
        } else {
            render();
            _mpViewListObj.find('li[pagenum=' + _editorData.outPNum + ']').show().siblings("li").hide();
            getPageViewImgs();
        }
        $.blockUI({ message: _mpViewPanel[0], css: { border: 'none', width: '840', backgroundColor: 'none', height: '420', left: left, top: top }, overlayCSS: { backgroundColor: "#555"} });
        config.currViewOPNum = _editorData.outPNum;
        config.currViewPNum = _editorData.currEditorPNum;
    }

    /*
    获取pageview的图片
    */
    function getPageViewImgs() {
        $.ajax({
            url: '/intime/GetPageView',
            dataType: 'json',
            data: { userid: _editorData.userID, bookid: _editorData.bookID },
            type: 'get',
            cache: false,
            success: function (r) {
                if (r.success) {
                    var pviews = r.pviews;
                    if (pviews != undefined && pviews.length > 0) {
                        for (var i = 0, len = pviews.length; i < len; i++) {
                            var skipCss = pviews[i].isskip ? "skip" : "";
                            $("#mpview_" + pviews[i].pnum).css("background-image", "url(" + pviews[i].src.replace('pview', 'pthumb') + "?t=" + Math.random() + ")").addClass(skipCss);
                            if (pviews[i].isskip && pviews[i].pnum == _editorData.coverPNum) {
                                $("#mpview_" + _editorData.bottomPNum).hide();
                            }
                        }
                    }
                }
            },
            error: function (r) { }
        })
    }

    function _init() {
        $(config.switch2ViewBtn).bind("click", _show);
        $(config.showPageViewBtn).bind("click", _show);
        $(config.nextPage).bind("click", function () {
            var opnumInfo = config.currViewOPNum.split('_'), showOpnum = '-1_0';
            if (parseInt(opnumInfo[0]) == _editorData.bottomPNum) {
                showOpnum = '-2_1';
            } else if (parseInt(opnumInfo[0]) == _editorData.firstFlodPNum) {
                showOpnum = '2_3';

            } else if (parseInt(opnumInfo[0]) == _editorData.totalCount - 2) {
                showOpnum = _editorData.totalCount + '_-3';

            } else if (parseInt(opnumInfo[0]) == _editorData.totalCount) {
                showOpnum = _editorData.totalCount + '_-3';

            } else {
                showOpnum = (parseInt(opnumInfo[0]) + 2) + "_" + (parseInt(opnumInfo[1]) + 2);
            }

            if (config.currViewOPNum != showOpnum) {
                config.currViewOPNum = showOpnum;
                config.currViewPNum = showOpnum.split("_")[0];
                _mpViewListObj.find('li[pagenum=' + showOpnum + ']').show().siblings("li").hide();
            }
        });
        $(config.prevPage).bind("click", function () {
            var opnumInfo = config.currViewOPNum.split('_'), showOpnum = '-1_0';
            if (parseInt(opnumInfo[0]) == _editorData.bottomPNum) {
                showOpnum = '-1_0';
            } else if (parseInt(opnumInfo[0]) == _editorData.firstFlodPNum) {
                showOpnum = '-1_0';
            } else if (parseInt(opnumInfo[0]) == _editorData.coverPNum + 2) {
                showOpnum = '-2_1';
            } else if (parseInt(opnumInfo[0]) == _editorData.totalCount) {
                showOpnum = (_editorData.totalCount - 2) + "_" + (_editorData.totalCount - 1);
            } else {
                showOpnum = (parseInt(opnumInfo[0]) - 2) + "_" + (parseInt(opnumInfo[1]) - 2);
            }
            if (config.currViewOPNum != showOpnum) {
                config.currViewOPNum = showOpnum;
                config.currViewPNum = showOpnum.split("_")[0];
                _mpViewListObj.find('li[pagenum=' + showOpnum + ']').show().siblings("li").hide();
            }
        });
        $(config.closePanel).bind("click", function () {
            $.unblockUI();
        });
        $(config.editPage).bind("click", function () {
            $('#pview_' + config.currViewPNum).trigger('click');
            $.unblockUI();
        });
    }

    this.init = _init;
    //打开预览面板
    this.show = _show;
}
/*
背景色设置面板
*/
function BgColorPanel(options) {
    var config = {
        bgColorPanel: "#bgColorPanel",
        bgColorItem: "div.bgcolor_item",
        bgColor: "a.bgcolor",
        bgColorBtn: "#bgColorBtn",
        panelCloseBtn: "a.panel_close"
    };
    $.extend(true, config, options);

    var _bgColorPanelObj = $(config.bgColorPanel);
    function _init() {
        _bgColorPanelObj.hide();
        _bgColorPanelObj.find(config.panelCloseBtn).bind("click", function () {
            _bgColorPanelObj.hide();
        })
        $(config.bgColorBtn).bind("click", function () {
            _bgColorPanelObj.fadeToggle();
        })
        $(config.bgColorPanel + " " + config.bgColor).bind("click", function () {
            $(this).parent(config.bgColorItem).addClass("selected").siblings(config.bgColorItem).removeClass("selected");
            $("#bookPageWrapper div[pagenum=" + _editorData.currEditorPNum + "]").css("background-color", $(this).css("background-color"));
            setPageEditAttr(_editorData.currEditorPNum);
        });
    }

    this.init = _init;
}
/*
书本预设面板
*/
function BookSetPanel(options) {
    var config = {
        BookSetPanel: "#bookSetPanel",
        PanelCloseBtn: "a.panel_close",
        FormSaveBtn: "#saveBookSetBtn",
        BookName: "#BookName",
        BookAuthor: "#BookAuthor",
        BookBrief: "#BookBrief",
        BookPrintFont: "#BookPrintFont",
        ShowPanelBtn: "#ShowBookSetPanelBtn",
        SaveNotice: "#saveBookSetNotice",
        BookID: "#BookID",
        UserID: "#UserID",
        PrintBookBtn: "#PrintBookBtn"
    };
    $.extend(true, config, options);

    var _bookSetPanelObj = $(config.BookSetPanel);

    function _saveBookSet() {
        var bookname = $(config.BookName).val(),
        bookauthor = $(config.BookAuthor).val(),
        bookfont = $(config.BookPrintFont).val(),
        bookbrief = $(config.BookBrief).val();
        $.ajax({
            url: '/intime/savebookset',
            data: { bookname: bookname, bookauthor: bookauthor, bookbrief: bookbrief, bookid: _editorData.bookID, userid: _editorData.userID, bookfont: bookfont },
            dataType: 'json',
            type: 'post',
            success: function (r) {
                $(config.SaveNotice).text(r.msg);
            }
        });
        return false;
    }

    function _intBookSetInfo() {
        $(config.BookName).val(_editorData.bookName);
        $(config.BookAuthor).val(_editorData.bookAuthor);
        $(config.BookBrief).val(_editorData.bookBrief);
    }

    function _initEditorData() {
        _editorData.bookName = $(config.BookName).val();
        _editorData.bookAuthor = $(config.BookAuthor).val();
        _editorData.bookBrief = $(config.BookBrief).val();
        _editorData.bookID = $(config.BookID).val();
        _editorData.userID = $(config.UserID).val();
    }

    function _order2Print() {
        $.ajax({
            url: '/intime/checkbookdone',
            type: 'get',
            cache: false,
            async: false,
            dataType: 'json',
            data: { userid: _editorData.userID, bookid: _editorData.bookID },
            success: function (r) {
                if (r.success) {
                    window.location = "/order/addbook2cart?bookid=" + _editorData.bookID;
                } else {
                    if (confirm(r.msg)) {
                        window.location = "/order/addbook2cart?bookid=" + _editorData.bookID;
                    }
                }
            }
        })
    }

    function _init() {
        _bookSetPanelObj.find(config.PanelCloseBtn).bind("click", function () {
            _bookSetPanelObj.css("z-index", 101);
            _bookSetPanelObj.fadeOut();
        })

        $(config.ShowPanelBtn).bind("click", function () {
            $("div.upload_panel:visible").css("z-index", 101);
            _bookSetPanelObj.css("z-index", 102);
            _bookSetPanelObj.fadeIn();
        })

        $(config.FormSaveBtn).bind("click", function () {
            _saveBookSet();
        })

        $(config.PrintBookBtn).bind("click", function () {
            _order2Print();
        })
        $("#order2Print").bind("click", function () {
            _order2Print();
        })
        //初始化
        _initEditorData();
    }

    this.init = _init;
}

/*
删除
*/
function DeletePageViewItem(opnum) {
    if (_editorData.changeCount <= 0) {
        alert("已经是最少页数了，不能再删啦！");
        return false;
    }

    $("#pageViewListClone").find('li[pagenum=' + opnum + ']').addClass("page_item_del");
    _editorData.changeCount -= 2;
}

/*
书本页面管理面板
*/
function PageSetPanel(options) {
    var config = {
        PageSetPanel: "#pageSetPanel",
        PanelCloseBtn: "a.panel_close",
        FormSaveBtn: "#savePageSetBtn",
        SaveNotice: "#savePageSetNotice",
        ShowPanelBtn: "#ShowPageSetPanelBtn",
        PageViewList: "#pageViewList",
        PageSetItem: "#pageSetItem"
    };
    $.extend(true, config, options);

    var _pageSetPanelObj = $(config.PageSetPanel);
    //保存页面顺序的调整
    function _savePageSet() {
        var liItems = $(config.PageSetItem).find("ul li").not(".page_item_del"),
        delLiItems = $(config.PageSetItem).find("ul li.page_item_del"),
        delItems = delLiItems.find("div.pview_item"), delLen = delItems.length, delnums = [],
        items = liItems.find("div.pview_item"), len = items.length, pnums = [];
        for (var i = 0; i < len; i++) {
            pnums.push(items.eq(i).attr('pagenum'));
        }
        for (var j = 0; j < delLen; j++) {
            delnums.push(delItems.eq(j).attr('pagenum'));
        }

        var numorder = pnums.join(','), delpnum = delnums.join(',');
        $.ajax({
            url: '/intime/SetPageNumOrder',
            type: 'get',
            dataType: 'json',
            data: { bookid: _editorData.bookID, userid: _editorData.userID, numorder: numorder, delpnum: delpnum },
            cache: false,
            success: function (r) {
                if (r.success) {
                    window.location = "/intime/editor?bookid=" + _editorData.bookID;
                } else {
                    alert(r.msg);
                }
            }
        })

    }

    function _intPageSetInfo() {

    }

    function _initEditorData() {

    }

    function _bindSortable(obj) {
        var items = obj.find('li'), len = items.length;
        items.addClass('page_item_li');
        items.eq(0).addClass('page_item_disabled').removeClass('page_item_li');
        items.eq(1).addClass('page_item_disabled').removeClass('page_item_li');
        items.eq(len - 1).addClass('page_item_disabled').removeClass('page_item_li');
        items.eq(len - 2).addClass('page_item_disabled').removeClass('page_item_li');
        obj.sortable({
            items: "li:not(.page_item_disabled)"
        });
        obj.disableSelection();

        items.not('.page_item_disabled').bind('mouseover', function () {
            var delIcon = $(this).find("a.delete_img_icon");
            if (delIcon.length > 0) {
                delIcon.show();
            } else {
                var imgID = $(this).attr("pagenum");
                $('<a href="javascript:DeletePageViewItem(\'' + imgID + '\');" title="删除" class="delete_img_icon"></a>').appendTo($(this));
            }
        }).bind('mouseout', function () {
            var delIcon = $(this).find("a.delete_img_icon");
            if (delIcon.length > 0) {
                delIcon.hide();
            }
        })
    }



    function _init() {
        _pageSetPanelObj.find(config.PanelCloseBtn).bind("click", function () {
            _pageSetPanelObj.css("z-index", 101);
            _pageSetPanelObj.fadeOut();
            $(config.PageSetItem).empty();
        })

        $(config.ShowPanelBtn).bind("click", function () {
            var pageItemListObj = $(config.PageViewList).clone();
            $(config.PageSetItem).empty().append((pageItemListObj = $(config.PageViewList).clone()));
            pageItemListObj.css({ width: "100%", left: 0 });
            pageItemListObj.attr('id', 'pageViewListClone');
            pageItemListObj.find('div.addpage_item').remove();
            _bindSortable(pageItemListObj);

            $("div.upload_panel:visible").css("z-index", 101);
            _pageSetPanelObj.css("z-index", 102);
            _pageSetPanelObj.fadeIn();
            _editorData.changeCount = _editorData.totalCount - _editorData.minCount;
        })

        $(config.FormSaveBtn).bind("click", function () {
            _savePageSet();
        })
        //初始化
        _initEditorData();
    }

    this.init = _init;
}
 