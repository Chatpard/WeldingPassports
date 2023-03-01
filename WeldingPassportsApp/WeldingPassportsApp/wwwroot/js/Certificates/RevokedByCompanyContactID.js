export function ToggleDisable(revokedByCompanyContactID, revokedByCompanyContactIDClearButton, hasPassed) {
    if (hasPassed.val() == "True") {
        revokedByCompanyContactID.removeAttr("disabled");
        revokedByCompanyContactIDClearButton.removeAttr("disabled");
    }
    else {
        revokedByCompanyContactID.prop("disabled", true);
        revokedByCompanyContactIDClearButton.prop("disabled", true);
    }
}