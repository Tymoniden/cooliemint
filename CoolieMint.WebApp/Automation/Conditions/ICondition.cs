namespace CoolieMint.WebApp.Automation.Conditions
{
    public interface ICondition
    {
        public ConditionType ConditionType { get; set; }
        public bool IsInverted { get; set; }
    }
}
