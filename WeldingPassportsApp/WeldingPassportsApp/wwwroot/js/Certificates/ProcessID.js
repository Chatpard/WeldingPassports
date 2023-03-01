export function ToggleDisable(processID, processIDClearButton, pePassportID, registrationTypeID) {
    if(pePassportID.val() == null) {
        processID.prop("disabled", true);
        processIDClearButton.prop("disabled", true);
    }
    else {
        if (registrationTypeID.val() == null) {
            processID.removeAttr("disabled");
            processIDClearButton.removeAttr("disabled");
        }
        else {
            processID.prop("disabled", true);
            processIDClearButton.prop("disabled", true);
        }
    }
}