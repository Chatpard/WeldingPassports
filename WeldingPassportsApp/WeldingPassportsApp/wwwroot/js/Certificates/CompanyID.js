export function GetCompanyID(companyID, companyIDError, pePassportID) {
    console.log("GetCompanyID",
        "companyID = ", companyID.val(),
        "pePassportID = ", pePassportID.val()
    );
    if (pePassportID.val() != null) {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/../../api/CertificatesApi/GetCompanyID",
            data: {
                pePassportID: pePassportID.val()
            },
            success: function (response) {
                if (response != null && Number.isInteger(response)) {
                    companyIDError.remove();
                    companyID.val(response);
                }
            },
            error: function (jqXHR, testStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    };
};