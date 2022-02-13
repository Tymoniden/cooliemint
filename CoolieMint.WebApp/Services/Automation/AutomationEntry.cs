using CoolieMint.WebApp.Services.Automation.Rule.Action;
using System;

namespace CoolieMint.WebApp.Services.Automation
{
    public class AutomationEntry
    {
        public DateTime? NextExecutionTime { get; set; }
        public IAutomationAction Action { get; set; }
    }
}
