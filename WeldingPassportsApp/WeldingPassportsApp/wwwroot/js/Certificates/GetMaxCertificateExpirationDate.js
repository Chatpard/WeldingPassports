function MaxCertificateExpirationDate(pePassportID, processID, examDate) {
    console.log("MaxCertificateExpirationDate: pePassportID = " + pePassportID + ", processID = " + processID, "examDate = " + examDate);
    if (pePassportID != null && processID != null && examDate != null) {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/../../Api/CertificatesApi/MaxCertificateExpirationDate",
            data: { pePassportID: pePassportID, processID: processID, examDateString: examDate },
            success: function (response) {
                console.log("MaxCertificateExpirationDate: " + response);
                try {
                    if (response != null) {
                        let d = new Date(response);
                        $("#ExpiryDate").val(d.getFullYear() + "-" + (d.getMonth() + 1).toString().padStart(2, '0') + "-" + d.getDate().toString().padStart(2, '0'));
                    }
                }
                catch (e) {
                    console.error(e)
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }
}