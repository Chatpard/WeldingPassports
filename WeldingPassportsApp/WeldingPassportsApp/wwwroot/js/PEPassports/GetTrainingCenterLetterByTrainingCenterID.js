function GetTrainingCenterLetterByTrainingCenterID(select) {
    $.getJSON("/api/TrainingCentersApi/GetLetterByTrainingCenterID", { trainingCenterID: Number($(select).val()) }, function () { })
        .done(function (data) {
            if (data != "") {
                $("#Letter").val(data);
                $("#LetterSpan").text(data);
            }
            else {
                $("#Letter").val("�");
                $("#LetterSpan").text("�");
            }
        });
};
