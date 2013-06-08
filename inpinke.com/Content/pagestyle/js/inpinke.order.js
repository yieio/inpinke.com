$(function () {
    $("div.consignee_item").bind("click", function () {
        var id = $(this).attr("id"), idInfo = id.replace("consignee_item_", "");
        $(this).addClass("selected").siblings("div.consignee_item").removeClass("selected");
        $("#address_radio_" + idInfo).attr("checked", "checked");
    })

    $("input.num_input").keyup(function () {  //keyup事件处理 
        $(this).val($(this).val().replace(/\D|^0/g, ''));
    }).bind("paste", function () {  //CTR+V事件处理 
        $(this).val($(this).val().replace(/\D|^0/g, ''));
    }).css("ime-mode", "disabled");

    $("input.num_input").change(function () {
        var num = $(this).val();
        if (num <= 0) {
            $(this).val(1);
            alert("印品购买数量不能少于1件");
            num = 1;
        }
        var id = $(this).attr("id"),
            idInfo = id.split('_'),
            totalObj = $("#total_price_" + idInfo[1]),
            singlePrice = $("#single_price_" + idInfo[1]).val(),
            totalPrice = singlePrice * num;
        totalObj.text(totalPrice.toFixed(2));
    })

    $("#deliveryMethod").change(function () {

    })

    //$("#ConsigneeForm").submit(function () )
})

//删除收货人
function DeleteAddress(aid) {
    if (confirm("确定删除该收货人信息吗？")) {
        var theObj = $("#consignee_item_" + aid);
        theObj.remove();
        $.ajax({
            url: "/my/AjaxDeleteAddress",
            data: { id: aid },
            dataType: 'json',
            type: 'get',
            success: function (r) {
                if (!r.success) {
                    alert(r.msg);
                }
            }
        })
    }
}
//设置默认收货人
function SetDefaultAddress(aid) {
    var theObj = $("#consignee_item_" + aid);
    if (theObj.hasClass("default_css")) {
        return;
    }
    theObj.addClass("default_css").siblings("tr").removeClass("default_css").find("td.con_address").attr("title", "点击设为默认收货地址");
    theObj.find("td.con_address").attr("title", "默认收地址")
    $.ajax({
        url: "/my/AjaxSetDefaultAddress",
        data: { id: aid },
        dataType: 'json',
        type: 'get',
        success: function (r) {
            if (!r.success) {
                alert(r.msg);
            }
        }
    })
}

function InAddressPageAfterSubmit() {
    $.ajax({
        url: $("#ConsigneeForm").attr("action"),
        data: { Consignee: $("#Consignee").val(), Mobile: $("#Mobile").val(), ProvID: $("#ProvID").val(), CityID: $("#CityID").val(), AreaID: $("#AreaID").val(), Address: $("#Address").val(), AddressID: $("#AddressID").val() },
        type: 'post',
        dataType: 'json',
        cache: false,
        success: function (r) {
            if (r.success) {
                var oldObj = $("#consignee_item_" + r.id), wrapper = $("#consigneeItemWrapper"),
                     newItem = '<tr id="consignee_item_' + r.id + '">'
                                          +'<td title="点击设置为默认收货人" onclick="SetDefaultAddress('+r.id+')">' + r.provname + '&nbsp;' + r.cityname + '&nbsp;'
                                          + r.areaname + '&nbsp;' + r.address + '</td>'
                                          + '<td>' + r.consignee + '</td><td>' + r.mobile + '</td><td><a href="javascript:ModifyAddress(' + r.id + ')">[修改]</a>&nbsp;<a href="javascript:DeleteAddress(' + r.id + ')">[删除] </a></td></tr>';
                if (oldObj.length > 0) {
                    oldObj.replaceWith(newItem);
                } else {
                    wrapper.append($(newItem));
                }

                $("#ViewBagMsg").text("收货人信息保存成功");
            } else {
                $("#ViewBagMsg").text(r.message);
            }
        }
    })
    return false;
}

function InOrderPageAfterSubmit() {
    $.ajax({
        url: $("#ConsigneeForm").attr("action"),
        data: { Consignee: $("#Consignee").val(), Mobile: $("#Mobile").val(), ProvID: $("#ProvID").val(), CityID: $("#CityID").val(), AreaID: $("#AreaID").val(), Address: $("#Address").val(), AddressID: $("#AddressID").val() },
        type: 'post',
        dataType: 'json',
        cache: false,
        success: function (r) {
            if (r.success) {
                var oldObj = $("#consignee_item_" + r.id), wrapper = $("#consigneeItemWrapper"),
                    newItem = '<div class="consignee_item selected" id="consignee_item_' + r.id + '"> '
                                      + '<input type="radio" name="AddressID" value="r.id" style="width: auto;" checked="true"  id="address_radio_' + r.id + '"/>'
                                      + '<span>' + r.provname + '&nbsp;' + r.cityname + '&nbsp;' + r.areaname + '&nbsp;' + r.address + '</span><span> ' + r.consignee + '</span>'
                                      + '<span> ' + r.mobile + '</span> <a href="javascript:ModifyAddress(' + r.id + ')"> [修改] </a></div>';
                if (oldObj.length > 0) {
                    oldObj.replaceWith(newItem);
                } else {
                    wrapper.append($(newItem));
                }
                $("#consignee_item_" + r.id).bind("click", function () {
                    var id = $(this).attr("id"), idInfo = id.replace("consignee_item_", "");
                    $(this).addClass("selected").siblings("div.consignee_item").removeClass("selected");
                    $("#address_radio_" + idInfo).attr("checked", "checked");
                }).trigger("click");
                $("#ViewBagMsg").text("收货人信息保存成功");
            } else {
                $("#ViewBagMsg").text(r.message);
            }
        }
    })
    return false;
}

function ModifyAddress(aid) {
    var pHeight = 350, pWidth = 600,
        wHeight = $(window).height(), wWidth = $(window).width(),
        left = (wWidth - pWidth) / 2, top = (wHeight - pHeight) / 2;
    $.blockUI({ message: $("#AddressForm"), css: { border: '#ccc solid 7px', width: pWidth, backgroundColor: '#fff', height: pHeight, left: left, top: top }, overlayCSS: { backgroundColor: "#555"} });
    $("#ViewBagMsg").text("");
    $("#boxTitle").text("编辑收货人");
    $.ajax({
        url: "/order/ajaxGetAddress",
        data: { addressid: aid },
        type: 'get',
        dataType: 'json',
        success: function (r) {
            if (r.success) {
                $("#Consignee").val(r.consignee);
                $("#Mobile").val(r.mobile);
                $("#Address").val(r.address);
                $("#AddressID").val(r.id);
                Scripts.pca.Doajax('ajax.do?act=province', 'post', 'ProvID', "province", r.provid);
                Scripts.pca.Doajax('ajax.do?act=city&id=' + escape(r.provid), 'post', 'CityID', "city", r.cityid);
                Scripts.pca.Doajax('ajax.do?act=area&id=' + escape(r.cityid), 'post', 'AreaID', "area", r.areaid);
            } else {
            }
        },
        error: function (e) { }
    })
}

function AddConsignee() {
    var pHeight = 350, pWidth = 600,
        wHeight = $(window).height(), wWidth = $(window).width(),
        left = (wWidth - pWidth) / 2, top = (wHeight - pHeight) / 2;
    $.blockUI({ message: $("#AddressForm"), css: { border: '#ccc solid 7px', width: pWidth, backgroundColor: '#fff', height: pHeight, left: left, top: top }, overlayCSS: { backgroundColor: "#555"} });
    $("#AddressID").val(0);
    $("#Consignee").val("");
    $("#Mobile").val("");
    $("#Address").val("");
    $("#ViewBagMsg").text("");
    $("#boxTitle").text("添加收货人");
}

function ShowPayNotice() {
    var pHeight = 300, pWidth = 550,
        wHeight = $(window).height(), wWidth = $(window).width(),
        left = (wWidth - pWidth) / 2, top = (wHeight - pHeight) / 2;
    $.blockUI({ message: $("#AddressForm"), css: { border: '#ccc solid 7px', width: pWidth, backgroundColor: '#fff', height: pHeight, left: left, top: top }, overlayCSS: { backgroundColor: "#555"} });
    $("#boxTitle").text("支付提醒");
}

function CloseBox() {
    $.unblockUI();
}