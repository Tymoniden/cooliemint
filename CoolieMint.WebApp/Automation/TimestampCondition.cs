using System.Collections.Generic;

namespace WebControlCenter.Automation
{
    public class TimestampCondition : IConditionParameter
    {
        public TimestampConditionType Type { get; set; }
        public List<TimestampConditionDay> IncludedDays { get; set; }
        public List<TimestampConditionDay> ExcludedDays { get; set; }
        public string Value { get; set; }
    }

}
