import * as HasPassed from "./HasPassed.js"
import * as RevokeDate from "./RevokeDate.js"
import * as RevokeComment from "./RevokeComment.js"

window.OnChangeRevokedBy = () => {
    HasPassed.ToggleDisable(
        $("#HasPassed"),
        $("#HasPassedClearButton"),
        $("#RegistrationTypeID"),
        $("#RevokedByCompanyContactID")
    );
    RevokeDate.ToggleDisable(
        $("#RevokeDate"),
        $("#RevokeDate-error"),
        $("#RevokedByCompanyContactID")
    );
    RevokeComment.ToggleDisable(
        $("#RevokeComment"),
        $("#RevokeComment-error"),
        $("#RevokedByCompanyContactID")
    );
}