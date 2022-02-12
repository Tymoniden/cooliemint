using System;
using System.Collections.Generic;

namespace WebControlCenter.Automation
{
    public class Rule
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public DateTime? NextExecution { get; set; }
        public ConditionContainer Condition { get; set; }
        public List<IAutomationAction> OnTrue { get; set; } = new List<IAutomationAction>();
        public List<IAutomationAction> OnFalse { get; set; } = new List<IAutomationAction>();
    }
}
