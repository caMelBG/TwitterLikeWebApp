var skip = 0;
var pageSize = 3;
var id = $(".user-info input[type=hidden]").val();
var url = "/User/GetTweetsByUserId";

loadUrl(url, skip, pageSize);
function loadUrl(url, skip, pageSize) {
    $.get(url, { id: id, skip: skip, pageSize: pageSize }, function (resultHtml) {
        $("#content-container").html(resultHtml);
    });
}

$("#btn-tweets").on("click", function () {
    skip = 0;
    pageSize = 3;
    url = "/User/GetTweetsByUserId";
    loadUrl(url, skip, pageSize);
});

$("#btn-favorites").on("click", function () {
    skip = 0;
    pageSize = 3;
    url = "/User/GetFavoritesByUserId";
    loadUrl(url, skip, pageSize);
});

$("#btn-followers").on("click", function () {
    skip = 0;
    pageSize = 7;
    url = "/User/GetFollowersByUserId";
    loadUrl(url, skip, pageSize);
});

$("#btn-following").on("click", function () {
    skip = 0;
    pageSize = 7;
    url = "/User/GetFollowingByUserId";
    loadUrl(url, skip, pageSize);
});

$("#btn-view-more").on("click", function () {
    skip++;
    $.get(url, { id: id, skip: skip, pageSize: pageSize }, function (resultHtml) {
        var currHtml = $("#content-container").html();
        $("#content-container").html(currHtml + resultHtml);
    });
});

$("#btn-edit-profile").on("click", function () {
    window.location.href = "/Account/Edit";
});
