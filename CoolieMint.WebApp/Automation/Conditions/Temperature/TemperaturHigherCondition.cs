namespace CoolieMint.WebApp.Automation.Conditions.Temperature
{
    public class TemperaturHigherCondition : ICondition, ITemperatureCondition
    {
        public ConditionType ConditionType { get; set; } = ConditionType.Temperature;
        public bool IsInverted { get; set; }
        public decimal Temperature { get; set; }
        public string SensorIdentifier { get; set; }
    }
}
