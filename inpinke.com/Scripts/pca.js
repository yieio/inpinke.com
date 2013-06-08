/*******************************************************************************
* function doajax(url,type,id,other){ $.ajax({ url: url, type: type, error:
* function(){ alert('Error loading XML document'); }, success: function(xml){ //
* do something with xml var _select = document.getElementById(id); var
* length=_select.options.length; $(_select).empty();// _select.options.add(new
* Option('---请选择---','0')); $(xml).find(id).each(function(){ var item_text =
* $.trim($(this).text()); var item_id = $.trim($(this).attr('id'));
* //alert("item_id:"+item_id+" defualt:"+other); //$('
* <li></li>
* ').html(item_text).appendTo('ol'); var s=" "; if(item_id==other){
* s="selected"; } var o="<option value='"+item_id+"' "+s+">"+item_text+"</option>"
* $(o).appendTo("#"+id) // _select.options.add(new Option(item_text,item_id));
* }); } }); }
******************************************************************************/
//解决命名冲突
if (typeof (Scripts) == "undefined") { Scripts = {}; };
Scripts.pca =
    {
        //GetCityArea:function(url,

        Doajax: function (url, type, id,region, other, callback) {
            // do something with xml
            var _select = document.getElementById(id);
            var length = _select.options.length;
            $(_select).empty(); //
            //_select.options.add(new Option('---请选择---', '0'));
            var o = "<option value='0' class='br'>---请选择---</option>"
            $(o).appendTo(_select);
            re = /(id=)([0-9]+)/; // 获取id
            var id_tmp = url.match(re);
            var parentId;
            if (id_tmp != undefined && id_tmp.length > 0) {//只查找省的时候，url中有不存在“id”的情况
                parentId = id_tmp[2]; //父id
            }
            $.ajax({
                url: '/Content/data/p_c_a.xml',
                dataType: 'xml',
                error: function (xml) {
                    alert('error:' + xml);
                },
                success: function (xml) {
                    //alert("加载数据成功");
                    var parentName = ""; // 所要查找行政单位的父行政级别
                    if (region == "area") {// 查找县区
                        parentName = "city";
                    } else if (region == "city") {// 查找 市
                        parentName = "province";
                    }
                    //alert("以行政单位:"+parentName+"   为父单位，查询");
                    if (region != "province") {
                        //////////////////////不查询省begin
                        queryNoProvince(xml, id, parentName, parentId,region, other);
                        ////////////////不查询省end
                    } else {//////////查询省
                        //alert("查找 省");
                        var row = 0;
                        $(xml).find(region).each(function () {// 在父行政级别下查找子行政区域
                            item_text = $(this).attr('name');
                            item_id = $(this).attr('id');
                            //alert(item_text+"----"+item_id);
                            var s = "  ";
                            if (other != undefined && item_id == other) {// 默认选择
                                s = "selected";
                            }

                            var br = " ";
                            row++;
                            if (row % 5 == 0) {
                                br = " class='br' ";
                            }
                            //alert("区域："+id+" 当前id："+item_id+"  默认id:"+other);
                            var o = "<option value='" + item_id + "' " + s + br + ">" + item_text
								        + "</option>"
                            $(o).appendTo("#" + id);

                        });
                    }
                    if (callback != null) {
                        callback();
                    }
                }
            });
        }

    }
/*
* 
*/
function queryNoProvince(xml, id, parentName, parentId,region, other) {
    //debugger;
    $(xml).find(parentName).each(function () {
        var item_id = $(this).attr('id');
        var item_text;
        //alert("查询到"+parentName+"   ："+item_id+"   需要："+parentId);
        if (parentId == item_id) {// 当前的行政区域名字 与 所要查找的行政单位的父行政区域匹配
            var row = 0;
            $(this).find(region).each(function () {// 在父行政级别下查找子行政区域
                if (region == "area") {// 县区
                    item_text = $(this).text();
                } else {// 市
                    item_text = $(this).attr('name');
                }
                item_id = $(this).attr('id');
                var s = "  ";
                if (other != undefined && item_id == other) {// 默认选择
                    s = "selected";
                }

                var br = " ";
                row++;
                if (row % 5 == 0) {
                    br = " class='br' ";
                }

                //alert("区域："+id+" 当前id："+item_id+"  默认id:"+other);
                var o = "<option value='" + item_id + "' " + s + br + ">" + item_text
												+ "</option>"
                $(o).appendTo("#" + id)
            });
            return;
            // _select.options.add(new
            // Option(item_text,item_id));
        }
    });
}
