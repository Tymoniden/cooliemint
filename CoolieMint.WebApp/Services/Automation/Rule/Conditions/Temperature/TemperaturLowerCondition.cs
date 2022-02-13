namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.Temperature
{
    public class TemperaturLowerCondition : ICondition, ITemperatureCondition
    {
        public ConditionType ConditionType { get; set; } = ConditionType.Temperature;
        public bool IsInverted { get; set; }
        public decimal Temperature { get; set; }
        public string SensorIdentifier { get; set; }
    }
}
