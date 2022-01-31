$("body").on("control:update",
    function (event, payload) {
        if (typeof (payload.context) != "undefined" && payload.context.startsWith("light")) {
            var startIndex = payload.context.lastIndexOf("/");
            var id = payload.context.substring(startIndex + 1, payload.context.length);
            SetLightValue(id, payload.value);
        }
    });

function ChangeLightValue(element) {
    if (!$(element).hasClass("light-box")) { return; }

    var id = $(element).attr("data-id");

    //$.ajax({
    //    url: "api/light/flip/" + id,
    //    cache: false,
    //    dataType: "text",
    //    success: UpdateLightBox(element)
    //});
}

$(".light-box").click(function () {
    ChangeLightValue(this);
});