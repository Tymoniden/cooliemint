namespace CoolieMint.WebApp.Automation.Conditions.Temperature
{
    public interface ITemperatureCondition
    {
        public decimal Temperature { get; set; }
        public string SensorIdentifier { get; set; }
    }
}
