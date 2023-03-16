namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.Date
{
    public class WeekDayCondition : ICondition
    {
        public ConditionType ConditionType { get; set; } = ConditionType.WeekDay;
        public bool IsInverted { get; set; }

        public WeekDay WeekDays { get; set; }
    }
}
