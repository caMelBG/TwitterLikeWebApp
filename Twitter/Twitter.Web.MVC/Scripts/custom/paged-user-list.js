$(".btn-follow").on("click", function (event) {
    var target = $(event.target);
    var id = target.parent().parent().children("input[type=hidden]").val();
    var url = "/User/Follow";
    console.log(url + id);
    $.get(url, { id: id }, function (result) {
        target.parent().html(result);
    });
});

$(".btn-user-info").on("click", function (event) {
    var target = $(event.target);
    var id = target.parent().children("input[type=hidden]").val();
    var url = "/User/Details" + "/" + id;
    document.location.href = url;
});