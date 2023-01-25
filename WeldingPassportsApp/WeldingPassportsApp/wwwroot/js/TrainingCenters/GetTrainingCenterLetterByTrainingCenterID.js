$(function () {
    $(function () {
        $("#CompanyID").on("change", function () {
            $.getJSON("https://localhost:44315/api/TrainingCentersApi/GetLetterByTrainingCenterId", { companyID: Number($(this).val()) }, function () { })
                .done(function (data) {
                    if (data != "") {
                        $("#Letter")[0].value = data;
                    }
                });
        });
    });
});