function PrepareFormOnsubmit() {
    $("#ProcessID").removeAttr("disabled");
    $("#RegistrationTypeID").removeAttr("disabled");
    $("#ExpiryDate").removeAttr("readonly");
    $("#HasPassed").removeAttr("disabled");
    $("#RevokedByCompanyContactID").removeAttr("disabled");
    $("#RevokeDate").removeAttr("readonly");
    $("#RevokeComment").removeAttr("disabled");
}