$(".btn-edit").on("click", function (event) {
    var target = $(event.target);
    var id = target.parent().children("input[type=hidden]").val();
    var url = "/Tweet/Edit" + "/" + id;
    document.location.href = url;
});

$(".btn-post-new-tweet").on("click", function (event) {
    var url = "/Tweet/Create";
    document.location.href = url;
});

$(".btn-user-info").on("click", function (event) {
    var target = $(event.target);
    var id = target.parent().children("input[type=hidden]").val();
    var url = "/User/Details" + "/" + id;
    document.location.href = url;
});

$(".btn-favorite").on("click", function (event) {
    var target = $(event.target);
    var id = target.parent().children("input[type=hidden]").val();
    var url = "/Tweet/Favorite";
    $.post(url, { id: id }, function () {
        markAsSelected(target, "Favorited");
    });
});

$(".btn-retweet").on("click", function (event) {
    var target = $(event.target);
    var id = target.parent().children("input[type=hidden]").val();
    var url = "/Tweet/ReTweet";
    $.post(url, { id: id }, function () {
        markAsSelected(target, "Retweeted");
    });
});

function markAsSelected(target, value) {
    target.css("border", "none");
    target.css("color", "black");
    target.css("background-color", "beige");
    target.val(value);
}
