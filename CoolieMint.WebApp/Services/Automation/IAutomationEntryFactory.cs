using CoolieMint.WebApp.Services.Automation.Rule.Action;
using System;

namespace CoolieMint.WebApp.Services.Automation
{
    public interface IAutomationEntryFactory
    {
        AutomationEntry CreateAutomationEntry(IAutomationAction automationAction, DateTime? nextExecutionTime = null);
    }
}