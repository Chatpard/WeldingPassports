import * as ProcessID from "./ProcessID.js"
import * as ExpiryDate from "./ExpiryDate.js"
import * as HasPassed from "./HasPassed.js"

window.OnChangeRegistrationTypeID = () => {

    ProcessID.ToggleDisable(
        $("#ProcessID"),
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
        $("#ExpiryDateClearButton"),
        $("#ExpiryDate-error"),
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