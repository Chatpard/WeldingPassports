$(function () {
    $(function () {
        $('input.reset').on("click", function () {
            var mySelect = $(this).parent().prevAll("select");
            mySelect.val("");
            mySelect.trigger("change");
        });
    })
})