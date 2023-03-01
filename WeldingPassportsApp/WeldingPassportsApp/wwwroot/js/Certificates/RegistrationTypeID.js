export function ToggleDisable(registrationTypeID, registrationTypeIDClearButton, processID, hasPassed) {
    if (typeof hasPassed.val() === "undefined") {
        hasPassed = $("<select></select>");
        hasPassed.val("1");
    }
    if (processID.val() == null) {
        registrationTypeID.prop("disabled", true);
        registrationTypeIDClearButton.prop("disabled", true);
    }
    else {
        if (hasPassed.val() == "" || hasPassed.val() == null) {
            registrationTypeID.removeAttr("disabled");
            registrationTypeIDClearButton.removeAttr("disabled");
        }
        else {
            registrationTypeID.prop("disabled", true);
            registrationTypeIDClearButton.prop("disabled", true);
        }
    }
}

export function SetRegistrationTypeIDSelectList(registrationTypeID, examDate, pePassportID, companyID, processID) {
    console.log("SetRegistrationTypesIDSelectList :",
        "registrationTypeID = " + registrationTypeID.val(),
        "examDate = " + examDate.val(),
        "pePassportID = " + pePassportID.val(),
        "processID = " + processID.val(),
    );
    if (pePassportID.val() != null && processID != null && examDate != null) {

        $.getJSON("/../../api/CertificatesApi/GetRegistrationTypesFromPEPassport", {
            pePassportID: pePassportID.val(),
            processID: processID.val(),
            examDate: examDate.val()
        }, function () { }
        ).done(function (data) {
            if (data.companyID != null) {
                companyID.val(data.companyID);
            }
            registrationTypeID.find("option").remove();
            var chooseOption = new Option("Choose Registration Type", "");
            chooseOption.disabled = true;
            chooseOption.selected = true;
            if (data.registrationsSelectList.length != 0) {
                chooseOption.hidden = true;
            }
            registrationTypeID.append(chooseOption);
            console.log("registrationTypeID = " + registrationTypeID.val());
            for (var i = 0; i < data.registrationsSelectList.length; i++) {
                registrationTypeID.append(new Option(data.registrationsSelectList[i].text, data.registrationsSelectList[i].value));
            }
            registrationTypeID.val(data.registrationsSelectList.length);
            registrationTypeID.trigger("onchange");
        });

    }
}