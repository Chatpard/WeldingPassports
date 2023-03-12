export function ToggleDisable(expiryDate, expiryDateClearButton, expiryDateError, registrationTypeID, hasPassed) {
    if (typeof hasPassed.val() === "undefined") {
        hasPassed = $("<select></select>");
    }
    if (registrationTypeID.val() == null) {
        expiryDate.val(null);
        expiryDate.prop("readonly", true);
        expiryDateClearButton.prop("disabled", true);
    }
    else {
        if (hasPassed.val() == "" || hasPassed.val() == null) {
            expiryDate.removeAttr("readonly");
            expiryDateClearButton.removeAttr("disabled");
        }
        else {
            expiryDate.prop("readonly", true);
            expiryDateClearButton.prop("disabled", true);
        }
    }
    expiryDateError.remove();
}

export function GetMaxExpiryDate(expiryDate, revokeDate, examDate, pePassportID, processID, registrationTypeID) {
    console.log("MaxCertificateExpirationDate: pePassportID = " + pePassportID.val() + ", processID = " + processID.val(), "examDate = " + examDate.val(), "registrationTypeID = " + registrationTypeID.val());
    if (examDate.val() != null && pePassportID.val() != null && processID.val() != null && registrationTypeID.val()) {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/../../api/CertificatesApi/GetMaxCertificateExpirationDate",
            data: {
                examDateString: examDate.val(),
                pePassportID: pePassportID.val(),
                processID: processID.val(),
                registrationTypeID: registrationTypeID.val()
            },
            success: function (response) {
                try {
                    if (response != null) {

                        expiryDate.val(response.expiryDate);
                        expiryDate.attr("min", response.minExpiryDate);
                        expiryDate.attr("max", response.maxExpiryDate);

                        expiryDate.rules("add", {
                            messages: {
                                min: response.expiryDateErrorMessage,
                                max: response.expiryDateErrorMessage
                            }
                        });
                        expiryDate.trigger("change");

                        revokeDate.attr("min", response.minExpiryDate);
                        revokeDate.attr("max", response.maxExpiryDate);

                        revokeDate.rules("add", {
                            messages: {
                                min: response.revokeDateErrorMessage,
                                max: response.revokeDateErrorMessage
                            }
                        });
                    }
                }
                catch (e) {
                    console.error("GetMaxExpiryDate error");
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }
}
