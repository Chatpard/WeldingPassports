import * as RegistrationTypeID from "./RegistrationTypeID.js"
import * as HasPassed from "./HasPassed.js"

window.OnChangeExpiryDate = () => {
    RegistrationTypeID.ToggleDisable(
        $("#RegistrationTypeID"),
        $("#RegistrationTypeIDClearButton"),
        $("#ProcessID"),
        $("#HasPassed")
    );
    HasPassed.ToggleDisable(
        $("#HasPassed"),
        $("#HasPassedClearButton"),
        $("#RegistrationTypeID"),
        $("#RevokedByCompanyContactID")
    );
}