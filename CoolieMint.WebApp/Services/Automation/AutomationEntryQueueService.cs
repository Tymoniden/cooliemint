using System;
using System.Collections.Generic;
using System.Linq;

namespace CoolieMint.WebApp.Services.Automation
{
    public class AutomationEntryQueueService : IAutomationEntryQueueService
    {
        List<AutomationEntry> entries = new List<AutomationEntry>();

        public AutomationEntry PopAutomationEntry()
        {
            var entry = entries.FirstOrDefault(e => e.NextExecutionTime == null || e.NextExecutionTime < DateTime.Now);
            if (entry != null)
            {
                entries.Remove(entry);
            }

            return entry;
        }

        public void PushAutomationEntry(AutomationEntry automationEntry)
        {
            lock (entries)
            {
                entries.Add(automationEntry);
            }
        }
    }
}
