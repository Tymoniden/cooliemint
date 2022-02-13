namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.ValueStore
{
    public sealed class ValueStoreCondition : ICondition
    {
        public ConditionType ConditionType { get; set; } = ConditionType.ValueStore;
        public bool IsInverted { get; set; }
        public string Key { get; set; }
        public object Value { get; set; }
    }
}
