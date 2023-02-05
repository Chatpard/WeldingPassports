$(function () {
    $(function () {
        $("#TrainingCenterID").on("change", function () {
            $.getJSON("https://localhost:44315/api/WeldersApi/GetWeldersNotFromTrainingCenterId", { id: Number($(this).val()) }, function () {
                console.log("succes");
            }).done(function (data) {
                $("#PEWelderID").find("option").remove();
                $("#PEWelderID").append(new Option("Choose PE Welder", "", true));
                $("#PEWelderID").find("option").prop("disabled", true);
                if (data.length > 0) {
                    $("#PEWelderID").find("option").prop("hidden", true);
                }
                for (i = 0; i < data.length; i++) {
                    $("#PEWelderID").append(new Option(data[i].text, data[i].value));
                }
            })
        })
    })
})