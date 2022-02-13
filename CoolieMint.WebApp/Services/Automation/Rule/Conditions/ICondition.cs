namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions
{
    public interface ICondition
    {
        public ConditionType ConditionType { get; set; }
        public bool IsInverted { get; set; }
    }
}
