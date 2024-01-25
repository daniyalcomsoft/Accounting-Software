/*
	jQuery Form Validators v0.3.5
	Website: http://validator.codeplex.com/
	License: http://validator.codeplex.com/license
	Examples:
    <input type='text' validate="group" require="please enter a value" />
	<input type='text' validate="group" email="invalid email" />
    <input type='text' validate="group" regular="must be less than 6 chars long" validExpress=".{6,}" />
	<input type='text' validate="group" regular="must be less than 6 chars long" invalidExpress=".{,6}" />
    <input type='text' validate="group" compare="password mismatch" compareTo="button1" />
    <input type='text' validate="group" custom="must be less than 6 chars long" customFn="this.length < 6" />
    <input type='text' validate="group" invalid="username cannot be used" invalidVal="username" />
*/

var validate;

(function($) {

    validate = function(group) {
        var marker = true;
        $("*[validate=" + group + "]").each(function(i, elm) {
            if (check(elm)) {
                $(elm).highlight();
                if (marker)
                    $(elm).find(":input").andSelf().focus();
                marker = false;
            }
            else
                $(elm).unhighlight();
        });
        return marker;
    }

    function revalidate() {
        if (!check(this))
            $(this).unhighlight();
        else
            $(this).highlight();
    }

    function check(elm) {
        var jelm = $(elm);

        var listsize = jelm.find("input:radio, input:checkbox").size();
        if (jelm.attr("disabled") || listsize > 0 && listsize == jelm.find("input:radio:disabled, input:checkbox:disabled").size())
            return "";

        //if empty value only perform required validation
        if (($.trim(jelm.val()) == "" || $.trim(jelm.val()) == null) && jelm.find("input:radio:checked, input:checkbox:checked").size() == 0)
            return jelm.attr("require") ? "require" : "";

        if (jelm.attr("regular") && jelm.attr("validExpress") && !new RegExp(jelm.attr("validExpress"), "m").test(jelm.val()))
            return "regular";

        if (jelm.attr("regular") && jelm.attr("invalidExpress") && new RegExp(jelm.attr("invalidExpress"), "m").test(jelm.val()))
            return "regular";

        if (jelm.attr("compare") && $("#" + jelm.attr("compareTo")).val() != jelm.val())
            return "compare";

        if (jelm.attr("custom") && !new Function(jelm.attr("customFn")).call(elm))
            return "custom";

        if (jelm.attr("invalid") && jelm.val() == jelm.attr("invalidVal"))
            return "invalid";

        if (validators != undefined) {
            for (var name in validators)
                if (jelm.attr(name) && !validators[name].call(elm))
                return name;
        }
    }

    function showAlert() {
        var ctrl = $(this);

        //		var top = ctrl.offset().top + ctrl.height() + 4;
        //		var left = ctrl.offset().left + Math.max(ctrl.width() - 260, 0);

        var top = ctrl.offset().top + ctrl.height() - 30;
        var left = ctrl.offset().left; //+Math.max(ctrl.width(), 0);

        ctrl.parents().each(function() {
            if ($(this).css("position") != "static" && (!$.browser.mozilla || $(this).css("display") != "table")) {
                var offset = $(this).offset();
                top -= offset.top;
                left -= offset.left+1;
                return false;
            }
        });
        ctrl.parent().children(".alertbox").remove();
        ctrl.parent().append("<div class='alertbox' style='top:" + top + "px; left:" + left + "px;'><div style='width:" + (ctrl.width() - 2) + "px;'>" + ctrl.attr(check(this)) + "</div></div>");
    }

    function hideAlert() {
        $(this).parent().children(".alertbox").remove();
    }

    $.fn.highlight = function() { this.addClass("highlight").focus(showAlert).blur(hideAlert).change(revalidate); return this; }
    $.fn.unhighlight = function() { this.removeClass("highlight").unbind("focus", showAlert).unbind("blur", hideAlert).parent().children(".alertbox").remove(); return this; }

})(jQuery);

var validators = {
	"email": function() { return new RegExp("[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?", "m").test(this.value); }
};
