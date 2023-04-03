export function ToggleDisable(companyContactID, companyContactIDCreateButton, companyContactIDClearButton, companyID) {
    if(companyID.val() == null) {
        companyContactID.prop("disabled", true);
        companyContactIDCreateButton.prop("hidden", true);
        companyContactIDClearButton.prop("disabled", true);
    }
    else {
        companyContactID.removeAttr("disabled");
        companyContactIDCreateButton.removeAttr("hidden");
        companyContactIDClearButton.removeAttr("disabled");
    }
}

export function GetCompanyContactIDSelectList(companyContactID, companyID) {
    console.log("GetCompanyContactIDSelectList: ",
        "companyContactID = " + companyContactID.val(),
        "companyID = " + companyID.val()
    );
    if (companyID.val() != null) {
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/../../api/CompanyContactsApi/GetCompanyContactsFromCompany",
            data: {
                companyID: companyID.val()
            },
            success: function (response) {
                var textOptionEmpty = companyContactID.find('option')[0].innerHTML;
                companyContactID.find("option").remove();
                var chooseOption = new Option(textOptionEmpty, "");
                chooseOption.disabled = true;
                chooseOption.selected = true;
                if (response.length != 0) {
                    chooseOption.hidden = true;
                }
                companyContactID.append(chooseOption);
                for (var i = 0; i < response.length; i++) {
                    companyContactID.append(new Option(response[i].text, response[i].value));
                }
                if (response.length == 1) {
                    companyContactID.val(response[0].value);
                }
            },
            error: function (jqXHR, testStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    };
};