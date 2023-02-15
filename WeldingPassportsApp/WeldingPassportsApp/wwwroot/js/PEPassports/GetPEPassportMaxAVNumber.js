$(function () {
    $(function () {
        $("#TrainingCenterID").on("change", function () {
            $.getJSON("/../../api/PEPassportsApi/GetMaxAVNumber", { trainingCenterID: Number($(this).val()) }, function () {
                console.log("succes");
            }).done(function (data) {
                $("#AVNumber").val(data);
            });
        });
    });
});