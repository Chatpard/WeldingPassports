function OnClickClear(select2Clear) {
    select2Clear.val(null);
    select2Clear.trigger("change");
}