﻿using WebControlCenter.Automation;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices
{
    public interface IActionMapperService
    {
        void HandleAction(IAutomationAction action);
    }
}