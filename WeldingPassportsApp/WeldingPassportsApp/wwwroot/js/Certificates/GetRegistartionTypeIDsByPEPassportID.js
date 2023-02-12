
function GetRegistrationTypesFromPEPassport(pePassportID, processID, examDate) {
    $.getJSON("/../../Api/CertificatesApi/GetRegistrationTypesFromPEPassport", { pePassportID: pePassportID, processID: processID, examDate: examDate }, function () {
    }).done(function (data) {
        $("#CompanyID").val(data.companyID);
        $("#RegistrationTypeID").find("option").remove();
        var chooseOption = new Option("Choose Registration Type", "");
        chooseOption.disabled = true;
        chooseOption.selected = true;
        if (data.registrationsSelectList.length != 0) {
            chooseOption.hidden = true;
        }
        $("#RegistrationTypeID").append(chooseOption);
        for (i = 0; i < data.registrationsSelectList.length; i++) {
            $("#RegistrationTypeID").append(new Option(data.registrationsSelectList[i].text, data.registrationsSelectList[i].value));
        }
        SetRegistrationTypeSelectSingle();

        $("#ProcessID").val(data.processID);
        GetMaxCertificateExpirationDate(pePassportID, data.processID, $("#RegistrationTypeID").val(), examDate);
    });
}

function SetRegistrationTypeSelectSingle() {
    if ($("#RegistrationTypeID option").length == 2) {
        $("#RegistrationTypeID").val("1").change();
    }
}