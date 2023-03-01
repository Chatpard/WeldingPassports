export function ToggleDisable(hasPassed, currentCertificateHasPassedClearButton, expiryDate, revokedBy) {
    if (expiryDate.val() == "") {
        hasPassed.prop("disabled", true);
        currentCertificateHasPassedClearButton.prop("disabled", true);
    }
    else {
        if (revokedBy.val() == null) {
            hasPassed.removeAttr("disabled");
            currentCertificateHasPassedClearButton.removeAttr("disabled");
        }
        else {
            hasPassed.prop("disabled", true);
            currentCertificateHasPassedClearButton.prop("disabled", true);
        }
    }
}