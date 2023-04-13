export function ToggleDisable(companyID, companyIDCreateButton, companyIDClearButton, companyIDActionButton, companyContactID) {
    if (companyContactID.val() == null) {
        companyID.removeAttr("disabled");
        companyIDClearButton.removeAttr("disabled");
        companyIDCreateButton.removeAttr("hidden");
        companyIDActionButton.removeAttr("disabled");
    }
    else {
        companyID.prop("disabled", true);
        companyIDClearButton.prop("disabled", true);
        companyIDCreateButton.prop("hidden", true);
        companyIDActionButton.prop("disabled", true); 
    }
}