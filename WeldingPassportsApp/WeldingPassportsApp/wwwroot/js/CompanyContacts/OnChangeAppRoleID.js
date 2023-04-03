import * as AppUserID from "./AppUserID.js"

window.OnChangeAppRoleID = () => {
    AppUserID.ToggleDisable(
        $("#AppUserID"),
        $("#AppUserIDClearButton"),
        $("#AppRoleID")
    )
}