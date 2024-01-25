$(document).ready(function() {
$("input.PhoneMask").mask("(999) 999-9999");
$("input.ZipCodeMask").mask("99999");
$("input.DateMask").mask("99/99/9999");
});
 
    ///IntegerNumber Only Accept
$(document).ready(function() {
 $("input.NumericOnly").keydown(function (e) { 
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
    });
    
    ///decimalNumber Only Accept
    $(document).ready(function() {
    $("input.decimalOnly").keydown(function(e){
    var key = e.which;

    // backspace, tab, left arrow, up arrow, right arrow, down arrow, delete, numpad decimal pt, period, enter
    if (key != 8 && key != 9 && key != 37 && key != 38 && key != 39 && key != 40 && key != 46 && key != 110 && key != 190 && key != 13){
        if (key < 48){
            e.preventDefault();
        }
        else if (key > 57 && key < 96){
            e.preventDefault();
        }
        else if (key > 105) {
            e.preventDefault();
        }
    }
});
});
