$(document).ready(SetRegistrationTypeSelectSingle())

function SetRegistrationTypeSelectSingle() {
    if ($("#RegistrationTypeID option").length == 2) {
        $("#RegistrationTypeID").val("1").change();
    }
}