import * as CompanyID from "./CompanyID.js"
import * as ProcessID from "./ProcessID.js"

window.OnChangePEPassportID = () => {
    CompanyID.GetCompanyID(
        $("#CompanyID"),
        $("#CompanyID-error"),
        $("#PEPassportID")
    );
    ProcessID.GetProcessIDSelectList(
        $("#ProcessID"),
        $("#ProcessID-error"),
        $("#ExaminationEncryptedID"),
        $("#PEPassportID"),
        $("#RegistrationID")
    );
    ProcessID.ToggleDisable(
        $("#ProcessID"),
        $("#ProcessID-error"),
        $("#ProcessIDClearButton"),
        $("#PEPassportID"),
        $("#RegistrationTypeID")
    );
}