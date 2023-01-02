$(function () {
    $(function () {
        $('input.reset').on("click", function () {
            $(this).parent().prevAll("select").val("");
        });
    });
});