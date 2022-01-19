$(function () {
    $(function () {
        $("#CompanyID").on("change", function () {
            $.getJSON("https://localhost:44315/Api/CompanyContactsApi/GetCompanyContactsFromCompany", { CompanyID: Number($(this).val()) }, function () {
                console.log("succes");
            }).done(function (data) {
                $("#CompanyContactID").find("option").remove();
                $("#CompanyContactID").append(new Option("Choose a Contact", ""));
                for (i = 0; i < data.length; i++) {
                    $("#CompanyContactID").append(new Option(data[i].text, data[i].value));
                }
            });
        });
    });
});