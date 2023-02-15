$(function () {
    $(function () {
        $("#CompanyID").on("change", function () {
            $.getJSON("/../../api/CompanyContactsApi/GetCompanyContactsFromCompany", { CompanyID: Number($(this).val()) }, function () {
                console.log("succes");
            }).done(function (data) {
                $("#CompanyContactID").find("option").remove();
                var chooseOption = new Option("Choose a Contact", "");
                chooseOption.disabled = true;
                chooseOption.selected = true;
                if (data.length != 0) {
                    chooseOption.hidden = true;
                }
                $("#CompanyContactID").append(chooseOption);
                for (i = 0; i < data.length; i++) {
                    $("#CompanyContactID").append(new Option(data[i].text, data[i].value));
                }
            });
        });
    });
});