import * as RegistrationTypeID from "./RegistrationTypeID.js"
import * as ExpiryDate from "./ExpiryDate.js"
import * as Revoke from "./Revoke.js"
import * as RevokedByCompanyContactID from "./RevokedByCompanyContactID.js"

window.OnChangeHasPassedID = (UserCanEdit) => {
    if (UserCanEdit) {
        RegistrationTypeID.ToggleDisable(
            $("#RegistrationTypeID"),
            $("#RegistrationTypeIDClearButton"),
            $("#ProcessID"),
            $("#HasPassed")
        );
    }
    ExpiryDate.ToggleDisable(
        $("#ExpiryDate"),
        $("#ExpiryDate-error"),
        $("#ExpiryDateClearButton"),
        $("#RegistrationTypeID"),
        $("#HasPassed")
    );
    Revoke.ToggleDisable(
        $("#Revoke"),
        $("#HasPassed")
    );
    RevokedByCompanyContactID.ToggleDisable(
        $("#RevokedByCompanyContactID"),
        $("#RevokedByCompanyContactIDClearButton"),
        $("#HasPassed")
    );
}