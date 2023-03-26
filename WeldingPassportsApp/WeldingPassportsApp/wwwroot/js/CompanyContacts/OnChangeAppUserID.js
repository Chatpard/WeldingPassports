import * as AppRoleID from "./AppRoleID.js"

window.OnChangeAppUserID = () => {
    AppRoleID.ToggleDisable(
        $("#AppRoleID"),
        $("#AppRoleIDClearButton"),
        $("#AppUserID")
    )
}