export function ToggleDisable(appUserID, appUserIDClearButton, appRoleID) {
    if (appRoleID.val() != null) {
        appUserID.prop("disabled", true);
        appUserIDClearButton.prop("disabled", true);
    }
    else {
        appUserID.removeAttr("disabled");
        appUserIDClearButton.removeAttr("disabled");
    }
}