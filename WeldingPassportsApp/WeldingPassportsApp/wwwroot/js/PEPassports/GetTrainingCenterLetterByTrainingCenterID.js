$(function () {
    $(function () {
        $("#TrainingCenterID").on("change", function () {
            $.getJSON("https://localhost:44315/api/TrainingCentersApi/GetLetterByTrainingCenterId", { id: Number($(this).val()) }, function () { })
                .done(function (data) {
                    if (data != "") {
                        $("#Letter")[0].innerHTML = data;
                    }
                    else {
                        $("#Letter")[0].innerHTML = "�";
                    }
                });
        });
    });
});