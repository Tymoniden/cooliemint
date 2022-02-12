using System;

namespace CoolieMint.WebApp.Automation.Conditions.Time
{
    public class TimeLaterCondition : ICondition, ITimeCondition
    {
        public ConditionType ConditionType { get; set; } = ConditionType.Time;
        public TimeSpan Time { get; set; }
        public bool IsInverted { get; set; }
    }
}
