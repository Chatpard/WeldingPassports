import * as CompanyID from "./CompanyID.js"

window.OnChangeCompanyContactID = () => {
    CompanyID.ToggleDisable(
        $("#CompanyID"),
        $("#CompanyIDCreateButton"),
        $("#CompanyIDClearButton"),
        $("#CompanyIDActionButton"), 
        $("#CompanyContactID")
    );
}