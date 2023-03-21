import * as PEPassportID from "./PEPassportID.js"
import * as RegistrationTypeID from "./RegistrationTypeID.js"

window.OnChangeProcessID = () => {
    PEPassportID.ToggleDisable(
        $("#PEPassportID"),
        $("#PEPassportIDClearButton"),
        $("#ProcessID")
    );
    RegistrationTypeID.SetRegistrationTypeIDSelectList(
        $("#RegistrationTypeID"),
        $("#RegistrationTypeID-error"),
        $("#ExamDate"),
        $("#PEPassportID"),
        $("#CompanyID"),
        $("#ProcessID")
    );
    RegistrationTypeID.ToggleDisable(
        $("#RegistrationTypeID"),
        $("#RegistrationTypeIDClearButton"),
        $("#ProcessID"),
        $("#HasPassed")
    );
}