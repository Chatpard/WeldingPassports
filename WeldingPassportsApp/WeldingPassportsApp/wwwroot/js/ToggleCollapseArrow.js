$(".collapse").on("hide.bs.collapse", function () {
    $('a[href="#'+$(this).attr("id")+'"]').html("▼");
})

$(".collapse").on("show.bs.collapse", function () {
    $('a[href="#'+$(this).attr("id")+'"]').html("▲");
})