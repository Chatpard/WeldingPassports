import * as ProcessID from "./ProcessID.js"

window.OnChangePEPassportID = () => {
    ProcessID.ToggleDisable(
        $("#ProcessID"),
        $("#ProcessIDClearButton"),
        $("#PEPassportID"),
        $("#RegistrationTypeID")
    );
}