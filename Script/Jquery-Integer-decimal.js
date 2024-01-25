///IntegerNumber Only Accept
$(document).ready(function() {
    NumericOnly();
    DecimalOnly();
    //ddlStateCityValidation();
    //ddlval();
});

function NumericOnly() {
    $("input.NumericOnly").keydown(function(e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) { // if shift, ctrl or alt keys held down 
            e.preventDefault();         // Prevent character input 
        } else {
            var n = e.keyCode;
            if (!((n == 8)              // backspace 
            || (n == 46)                // delete 
            || (n >= 35 && n <= 40)     // arrow keys/home/end 
            || (n >= 48 && n <= 57)     // numbers on keyboard 
            || (n >= 96 && n <= 105))   // number on keypad 
            ) {
                e.preventDefault();     // Prevent character input 
            }
        }
    });
}

///decimalNumber Only Accept
function DecimalOnly() {
    ///decimalNumber Only Accept
    $(document).ready(function() {
        $("input.decimalOnly").keydown(function(e) {
            var key = e.which;
            // backspace, tab, left arrow, up arrow, right arrow, down arrow, delete, numpad decimal pt, period, enter
            if (key != 8 && key != 9 && key != 37 && key != 38 && key != 39 && key != 40 && key != 46 && key != 110 && key != 190 && key != 13) {
                if (key < 48) {
                    e.preventDefault();
                }
                else if (key > 57 && key < 96) {
                    e.preventDefault();
                }
                else if (key > 105) {
                    e.preventDefault();
                }
            }
        });
    });
}

/// css in /// <reference path="../css/Custom.css" />
//DropdownValidation
//City and State Only
function ddlStateCityValidation() {
    //$("input.ddlstbtn").click(function() {
        var ddlState = $("select.ddlState option:selected").text();
        var ddlCity = $("select.ddlCity option:selected").text();
        var ddlGender = $("select.ddlGender option:selected").text();
        var ddlUserType = $("select.ddlUserType option:selected").text();
        var ddlCustomer = $("select.ddlCustomer option:selected").text();

        if (ddlGender == "--Please Select--") {
            $(".ddlGender").addClass("ddlcss");
            return false;
        }
        else {
            $(".ddlGender").removeClass("ddlcss");
        }
        if (ddlUserType == "--Please Select--") {
            $(".ddlUserType").addClass("ddlcss");
            return false;
        }
        else {
            $(".ddlUserType").removeClass("ddlcss");
        }
        if (ddlState == "--Please Select--") {
            $(".ddlState").addClass("ddlcss");
            return false;
        }
        else {
            $(".ddlState").removeClass("ddlcss");
        }
        if (ddlCity == "--Please Select--") {
            $(".ddlCity").addClass("ddlcss");
            return false;
        }
        else {
            $(".ddlCity").removeClass("ddlcss");
        }
        if (ddlCustomer == "--Please Select--") {
            $(".ddlCustomer").addClass("ddlcss");
            return false;
        }
        else {
            $(".ddlCustomer").removeClass("ddlcss");
        }
       
       
   // });
}
// all ddl
function ddlval() {
    //$("input.ddlbtnn").click(function() {
        var ddl = $("select.ddl option:selected").text();
        if (ddl == "--Please Select--") {
            $(".ddl").addClass("ddlcss");
            return false;
        }
        else {
            $(".ddl").removeClass("ddlcss");
        }
    //});
}
