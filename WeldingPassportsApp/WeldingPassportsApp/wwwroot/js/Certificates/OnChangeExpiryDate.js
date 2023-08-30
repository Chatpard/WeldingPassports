import * as RegistrationTypeID from "./RegistrationTypeID.js"
import * as ExpiryDate from "./ExpiryDate.js"
import * as HasPassed from "./HasPassed.js"

window.OnChangeExpiryDate = (UserCanEdit) => {
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

    HasPassed.ToggleDisable(
        $("#HasPassed"),
        $("#HasPassedClearButton"),
        $("#RegistrationTypeID"),
        $("#RevokedByCompanyContactID")
    );
}