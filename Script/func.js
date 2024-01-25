function showDialog(id) {
    $('#' + id).dialog("open");
    return false;
}

function closeDialog(id) {
    $('#' + id).dialog("close");
    return false;
}
function CreateModalPopUp(divID, widthModal, heightModal, titleModal) {
    $(divID).dialog({
        autoOpen: false,
        draggable: true,
        modal: true,
        resizable:false,
        title: titleModal,
        width: widthModal,
        height: heightModal,
        open: function(type, data) {
            $(this).parent().appendTo("form");
        }
    });    
}

function clear_form_elements(ele) {

    $(ele).find(':input').each(function() {
        switch (this.type) {
            case 'password':
            case 'select-multiple':
            case 'select-one':
            case 'text':
            case 'textarea':
                $(this).val('');
                break;
            case 'checkbox':
            case 'radio':
                this.checked = false;
        }
    });
}

function Generate3DigitCode(Control) {
    var str = '' + Control.value;
    while (str.length < 3) {
        str = '0' + str;
    }
    Control.value = str;
    //return str;
}

function Generate2DigitCode(Control) {
    var str = '' + Control.value;
    while (str.length < 2) {
        str = '0' + str;
    }
    Control.value = str;
    //return str;
}

function Generate4DigitCode(Control) {
    var str = '' + Control.value;
    while (str.length < 4) {
        str = '0' + str;
    }
    Control.value = str;
    //return str;
}

function IsNumber(evt) {
    var CharCode = (evt.which) ? evt.which : evt.keyCode
    if (CharCode > 31 && (CharCode < 48 || CharCode > 57))
        return false;
    return true;
}

function getFormattedDate(date) {
    var day = date.getDate().toString();
    var month = (date.getMonth() + 1).toString();
    var year = date.getFullYear().toString();
    return day + '/' + month + '/' + year;
}

function NumericTest(elem) {
    var text = $(elem).val();
    if (!isNaN(text)) {
        if (parseFloat(text) > 0)
            return true;
        else
            return false;
    }
    else
    { return false; }
}
function CheckNum(elem) {
    var text = $(elem).val();
    if (!isNaN(text)) {
        if (parseFloat(text) > 0) {
            if ($("[id$=txtRequestAmount]").val() >= text)
                return true;
            else
                return false;
        }
        else
            return false;
    }
    else
    { return false; }
}

function DateTimePicker() {
    $(".DateTimePicker").datepicker({ dateFormat: "dd/mm/yy" });
}
function SetCurrentDate() {
    var currentDate = new Date();
    $(".DateTimePicker").datepicker("setDate", currentDate);
}
function ShowAddRemove(remove) {
    //var btnadd = "#" + add;
    var btnRemove = "#" + remove;
    //$(btnadd).show();
    $(btnRemove).show();
}
function HideAddRemove(remove) {
    //var btnadd = "#" + add;
    var btnRemove = "#" + remove;
    //$(btnadd).hide();
    $(btnRemove).hide();
}
function divide(divFrom, divTo) {
    var result = divTo / divFrom;
    return result;
}

function delrow(rowindex) {
    var gridID = document.getElementById("<%= GridTrans.ClientID %>");
    gridID.deleteRow(rowindex + 1);
    gridID.deleteRow(gridID.rows.lenght - 1);
    return true;
}
function runEffect(divId) {
    var selectedEffect = 'blind';
    var options = {};
    options = { percent: 0 };
    options = { to: { width: 200, height: 60} };
    $(divId).toggle(selectedEffect, options, 500);
    return false;
}
function process(date) {
    var parts = date.split("/");
    return new Date(parts[2], parts[1] - 1, parts[0]);
}
