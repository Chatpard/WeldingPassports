$(function () {
    $(function () {
        $("#CompanyID").on("change", function () {
            $.getJSON("/../../api/TrainingCentersApi/GetLetterByTrainingCenterId", { companyID: Number($(this).val()) }, function () { })
                .done(function (data) {
                    if (data != "") {
                        $("#Letter")[0].value = data;
                    }
                });
        });
    });
});