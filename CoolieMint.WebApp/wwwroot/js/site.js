if (typeof (ControlCenter) == "undefined") {
    console.log("initialize control center");
    ControlCenter = new Object();
    ControlCenter.Site = true;
    ControlCenter.AutoUpdateControls = true;
    ControlCenter._registeredScripts = [];
    ControlCenter.AdapterSetup = "";
}

ControlCenter.IsScriptRegistered = function(name) {
    return ControlCenter._registeredScripts.indexOf(name) != -1;
}

ControlCenter.RegisterScript = function(name) {
    if (!ControlCenter.IsScriptRegistered(name)) {
        ControlCenter._registeredScripts.push(name);
    }
}

$(function () {
    $(".footer ul li a").click(function () {
        $($(this).parent()).addClass("clicked");
    });
});