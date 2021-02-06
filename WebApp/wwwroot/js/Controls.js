var scriptsLoaded = 0;
var scriptsToLoad = [
    "/js/Controls/Light.js",
];

$.getScript("/js/Controls/Helpers.js", function () {
    $.getScript("/js/Controls/Base.js", function () {
        for (var i = 0; i < scriptsToLoad.length; i++) {
            $.getScript(scriptsToLoad[i], function () { scriptsLoaded++; });
        }

        StartControlListener();
    });
});

function StartControlListener() {
    if (scriptsLoaded == scriptsToLoad.length) {
        ControlCenter.GetStatus();
    } else {
        setTimeout(StartControlListener, 1000);
    }
}