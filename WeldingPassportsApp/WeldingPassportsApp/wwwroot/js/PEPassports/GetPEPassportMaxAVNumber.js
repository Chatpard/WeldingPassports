$(function () {
    $(function () {
        $("#TrainingCenterID").on("change", function () {
            $.getJSON("https://localhost:44315/Api/PEPassportsApi/GetMaxAVNumber", { trainingCenterID: Number($(this).val()) }, function () {
                console.log("succes");
            }).done(function (data) {
                $("#AVNumber").val(data);
            });
        });
    });
});