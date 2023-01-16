function ActionEditHref2ID(SelectID) {
    $("#" + SelectID + "Edit").attr("href", $("#" + SelectID + "Edit").attr("href").replace("Edit/0?", "Edit/" + $("#"+SelectID).val() + "?"));
}

function ActionDisableEdit(SelectID) {
    if ($("#" + SelectID).val() == null) {
        $("#" + SelectID + "Edit").addClass("disabled");
    }
    else {
        $("#" + SelectID + "Edit").removeClass("disabled");
    };
}