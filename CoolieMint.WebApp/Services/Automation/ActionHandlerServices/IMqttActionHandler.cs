﻿using WebControlCenter.Automation;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices
{
    public interface IMqttActionHandler
    {
        void HandleAction(MqttAction mqttAction);
    }
}