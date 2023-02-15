$(document).ready(function SetTrainingCenterSelectSingle() {
    if ($("#TrainingCenterID option").length == 2) {
        $("#TrainingCenterID").val("1").change();
    }
})