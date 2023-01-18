function ActionDisableEdit(EditBtnID) {
    var myEditBtn = $("#" + EditBtnID);
    var mySelect = myEditBtn.parent().parent().prevAll("select")
    if (mySelect.val() == null) {
        myEditBtn.addClass("disabled");
    }
    else {
        myEditBtn.removeClass("disabled");
    };
}