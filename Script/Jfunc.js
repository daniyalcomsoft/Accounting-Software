
function showStatusMsg(MsgType, Msg) {
    if (MsgType == "1") {
        $("#StausMsg").removeClass().addClass("msg msg-ok");
        $("#StausMsg").html("<p>" + Msg + "</p>");
    }
    else if (MsgType == "2") {
        $("#StausMsg").removeClass().addClass("msg msg-error");
        $("#StausMsg").html("<p>" + Msg + "</p>");
    }
    else if (MsgType == "3") {
        $("#StausMsg").removeClass().addClass("msg msg-warn");
        $("#StausMsg").html("<p>" + Msg + "</p>");
    }
    $("#StausMsg").show("fade");
    setTimeout("hideStatusMsg()", 3000);
}

function hideStatusMsg() {
    $("#StausMsg").hide('fade');
}
