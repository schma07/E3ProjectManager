newFunction();

function newFunction() {
    $(function() {
        $.fn.editableform.buttons = '<button type="submit" class="btn btn-success editable-submit btn-sm waves-effect waves-light"><i class="mdi mdi-check"></i></button><button type="button" class="btn btn-danger editable-cancel btn-sm waves-effect waves-light"><i class="mdi mdi-close"></i></button>', $("#inline-username").editable({ type: "text", pk: 1, name: "username", title: "Enter username", mode: "inline", inputclass: "form-control-sm" }), $("#inline-firstname").editable({
            validate: function(e) {
                if ("" == $.trim(e))
                    return "This field is required";
            }, mode: "inline", inputclass: "form-control-sm"
        }), $("#inline-sex").editable({ prepend: "not selected", mode: "inline", inputclass: "form-control-sm", source: [{ value: 1, text: "Male" }, { value: 2, text: "Female" }], display: function(t, e) { var n = $.grep(e, function(e) { return e.value == t; }); n.length ? $(this).text(n[0].text).css("color", { "": "#98a6ad", 1: "#5fbeaa", 2: "#5d9cec" }[t]) : $(this).empty(); } }), $("#inline-status").editable({ mode: "inline", inputclass: "form-control-sm" }), $("#inline-group").editable({ showbuttons: !1, mode: "inline", inputclass: "form-control-sm" }), $("#inline-dob").editable({ mode: "inline", inputclass: "form-control-sm" }), $("#inline-comments").editable({ showbuttons: "bottom", mode: "inline", inputclass: "form-control-sm" });
    });
}
