export function ToggleDisable(pePassportID, pePassportIDClear, processID) {
    if (processID.val() == null) {
        pePassportID.removeAttr("disabled");
        pePassportIDClear.removeAttr("disabled");
    }
    else {
        pePassportID.prop("disabled", true);
        pePassportIDClear.prop("disabled", true);
    }
}