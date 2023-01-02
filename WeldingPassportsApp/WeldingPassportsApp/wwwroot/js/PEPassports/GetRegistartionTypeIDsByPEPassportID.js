$(function () {
    $(function () {
        $("#PEPassportID").on("change", function () {
            $.getJSON("https://localhost:44315/Api/CertificatesApi/GetRegistrationTypesFromPEPassport", { PEPassportID: Number($(this).val()) }, function () {
                console.log("succes");
            }).done(function (data) {
                $("#CompanyID").val(data.companyID);
                $("#ProcessID").val(data.processID);
                $("#RegistrationTypeID").find("option").remove();
                $("#RegistrationTypeID").append(new Option("Choose Registration Type", ""));
                for (i = 0; i < data.registrationsSelectList.length; i++) {
                    $("#RegistrationTypeID").append(new Option(data.registrationsSelectList[i].text, data.registrationsSelectList[i].value));
                }
            });
        });
    });
});