import * as ProcessID from "./ProcessID.js"
import * as ExpiryDate from "./ExpiryDate.js"
import * as HasPassed from "./HasPassed.js"

window.OnChangeRegistrationTypeID = () => {

    ProcessID.ToggleDisable(
        $("#ProcessID"),
        $("#ProcessID-error"),
        $("#ProcessIDClearButton"),
        $("#PEPassportID"),
        $("#RegistrationTypeID")
    );
    ExpiryDate.GetMaxExpiryDate(
        $("#ExpiryDate"),
        $("#RevokeDate"),
        $("#ExamDate"),
        $("#PEPassportID"),
        $("#ProcessID"),
        $("#RegistrationTypeID")
    );
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
        $("#ExpiryDate"),
        $("#RevokedByCompanyContactID")
    );
}