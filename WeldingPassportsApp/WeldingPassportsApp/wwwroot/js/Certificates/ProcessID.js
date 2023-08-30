//todo: implement ajax ProcessSelectList
export function ToggleDisable(processID, processIDError, processIDClearButton, pePassportID, registrationTypeID) {
    if(pePassportID.val() == null) {
        processID.prop("disabled", true);
        processIDClearButton.prop("disabled", true);
        processIDError.remove();
    }
    else {
        if (registrationTypeID.val() == null) {
            processID.removeAttr("disabled");
            processIDClearButton.removeAttr("disabled");
        }
        else {
            processID.prop("disabled", true);
            processIDClearButton.prop("disabled", true);
            processIDError.remove();
        }
    }
}

export function GetProcessIDSelectList(processID, processIDError, examinationEncryptedID, pePassportID, registrationID = null) {
    console.log("GetProcessIDSelectList: ",
        "ExaminationEncryptedID = " + examinationEncryptedID.val(),
        "pePassportID = " + pePassportID.val(),
        "registrationID = " + registrationID.val()
    );
    if (examinationEncryptedID.val() != null && pePassportID.val() != null) {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/../../api/CertificatesApi/GetProcessNames",
            data: {
                examinationEncryptedID: examinationEncryptedID.val(),
                pePassportID: pePassportID.val(),
                registrationID: registrationID == null ? null : registrationID.val()
            },
            success: function (response) {
                processID.find("option").remove();
                var chooseOption = new Option("Choose Process", "");
                chooseOption.disabled = true;
                chooseOption.selected = true;
                if (response.processNamesSelectList.length != 0) {
                    chooseOption.hidden = true;
                }
                processID.append(chooseOption);
                for (var i = 0; i < response.processNamesSelectList.length; i++) {
                    processID.append(new Option(response.processNamesSelectList[i].text, response.processNamesSelectList[i].value));
                }
                if (response.processNamesSelectList.length == 1) {
                    processIDError.remove();
                    processID.val(response.processNamesSelectList[0].value);
                }
                processID.trigger("onchange");
            },
            error: function (jqXHR, testStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    };
};