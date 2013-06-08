function checkall(form, prefix, checkall) {
    // debugger;
    var checkall = checkall ? checkall : '_list_chkall';
    var hidChk = document.getElementById("hidChkValue");
    if (hidChk != null && hidChk != undefined)
        hidChk.value = "";
    for (var i = 0; i < form.elements.length; i++) {
        var e = form.elements[i];
        if (e.name && e.name != checkall && (!prefix || (prefix && e.name.match(prefix)))) {
            e.checked = form.elements[checkall].checked;
            if (hidChk != null && hidChk != undefined)
                hidChk.value += e.value + "," + (e.checked ? "1" : "0") + "|";
        }
    }
}