using System;
using WebControlCenter.Automation;

namespace CoolieMint.WebApp.Services.Automation
{
    public interface IAutomationEntryFactory
    {
        AutomationEntry CreateAutomationEntry(IAutomationAction automationAction, DateTime? nextExecutionTime = null);
    }
}