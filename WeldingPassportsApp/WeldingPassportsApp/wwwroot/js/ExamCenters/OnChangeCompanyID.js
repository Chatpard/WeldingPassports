import * as CompanyContactID from "./CompanyContactID.js"

window.OnChangeCompanyID = () => {
    CompanyContactID.GetCompanyContactIDSelectList(
        $("#CompanyContactID"),
        $("#CompanyID")
    );
    CompanyContactID.ToggleDisable(
        $("#CompanyContactID"),
        $("#CompanyContactIDCreateButton"),
        $("#CompanyContactIDClearButton"),
        $("#CompanyID")
    );
}