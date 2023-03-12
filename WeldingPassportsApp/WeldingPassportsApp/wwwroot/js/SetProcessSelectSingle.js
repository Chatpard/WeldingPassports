$(document).ready(SetProcessSelectSingle())

function SetProcessSelectSingle() {
    if ($("#ProcessID option").length == 2) {
        $("#ProcessID").val("1").change();
    }
}