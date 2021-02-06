ControlCenter.Log = function (message, flag) {
    switch (flag) {
        case "info":
            ControlCenter.LogCallbacks.Info(message);
            break;
        case "debug":
            ControlCenter.LogCallbacks.Debug(message);
            break;
        case "error":
            ControlCenter.LogCallbacks.Error(message);
            break;
        case "critical":
            ControlCenter.LogCallbacks.Critical(message);
            break;
    }
}

ControlCenter.LogCallbacks = {
    Info: function (message) { /*$("footer div.container").text(message);*/ },
    Debug: function (message) { console.log(message); },
    Error: function (message) { console.log(message); },
    Critical: function (message) { console.log(message); }
};