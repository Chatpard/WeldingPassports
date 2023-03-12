import * as ProcessID from "./ProcessID.js"

window.OnChangePEPassportID = () => {
    ProcessID.GetProcessIDSelectList(
        $("#ProcessID"),
        $("#ExaminationEncryptedID"),
        $("#PEPassportID"),
        $("#RegistrationID")
    );
    ProcessID.ToggleDisable(
        $("#ProcessID"),
        $("#ProcessIDClearButton"),
        $("#PEPassportID"),
        $("#RegistrationTypeID")
    );
}