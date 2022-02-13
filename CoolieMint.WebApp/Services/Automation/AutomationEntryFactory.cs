using CoolieMint.WebApp.Services.Automation.Rule.Action;
using System;

namespace CoolieMint.WebApp.Services.Automation
{
    public class AutomationEntryFactory : IAutomationEntryFactory
    {
        public AutomationEntry CreateAutomationEntry(IAutomationAction automationAction, DateTime? nextExecutionTime = null)
        {
            return new AutomationEntry
            {
                Action = automationAction,
                NextExecutionTime = nextExecutionTime
            };
        }
    }
}
