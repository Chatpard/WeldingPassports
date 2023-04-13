export function ToggleDisable(companyID, companyIDCreateButton, companyIDClearButton, companyContactID) {
    if (companyContactID.val() == null) {
        companyID.removeAttr("disabled");
        companyIDCreateButton.removeAttr("hidden");
        companyIDClearButton.removeAttr("disabled");
    }
    else {
        companyID.prop("disabled", true);
        companyIDCreateButton.prop("hidden", true);
        companyIDClearButton.prop("disabled", true);
    }
}