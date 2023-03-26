export function ToggleDisable(appRoleID, appRoleIDClearButton, appUserID) {
    if (appUserID.val() == null) {
        appRoleID.prop("disabled", true);
        appRoleID.val(null);
        appRoleIDClearButton.prop("disabled", true);
    }
    else {
        appRoleID.removeAttr("disabled");
        appRoleIDClearButton.removeAttr("disabled");
    }
}