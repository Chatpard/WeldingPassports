$(document).ready(function SetExamCenterSelectSingle() {
    if ($("#ExamCenterID option").length == 2) {
        $("#ExamCenterID").val("1").change();
    }
})