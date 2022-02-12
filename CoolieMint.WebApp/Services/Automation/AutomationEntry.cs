using System;
using WebControlCenter.Automation;

namespace CoolieMint.WebApp.Services.Automation
{
    public class AutomationEntry
    {
        public DateTime? NextExecutionTime { get; set; }
        public IAutomationAction Action { get; set; }
    }
}
