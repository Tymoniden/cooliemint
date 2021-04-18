ControlCenter.GetStatus = function() {
    this.Log("status @" + LASTUPDATE, "debug");

    $.ajax({
        url: "/api/v1/Status/?ticks=" + LASTUPDATE + "&adapterSetupId=" + ControlCenter.AdapterSetup,
        type: "GET",
        cache: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        processData: false,
        success: function(contract) {
            ControlCenter.HandleStatusReturn(contract);

            if (ControlCenter.AutoUpdateControls) {
                ControlCenter.Log("auto update status", "info");
                ControlCenter.GetStatus();
            } else {
                ControlCenter.Log("no update status", "info");
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            ControlCenter.GetStatus();
        }
    });
};

ControlCenter.SendControlMessage = function (adapter, id, payload) {
    ControlCenter.Log("Sending control message" , "info");

    $.ajax({
        url: "/api/v1/SendControlMessage",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ Adapter: adapter, Id : id, Payload: JSON.stringify(payload) }),
        
        error: function (jqXHR, textStatus, errorThrown) {
            ControlCenter.Log("run into error: " + errorThrown + "; Status: " + textStatus, "error");
        }
    });
}

ControlCenter.HandleStatusReturn = function (contract) {
    if (contract.context == "success") {
        LASTUPDATE = contract.timestamp;
        this.Log("contract timestamp:" + LASTUPDATE, "debug");

        jQuery.each(contract.result,
            function (index, value) {
                jQuery("body").trigger(value.adapterType, value);
            });
    }

    if ((contract.context == "Exception" && contract.value != "timeout") && contract.context != "success") {
        this.Log("contract context is unknown: " + contract.context, "debug");
        return;
    }
}