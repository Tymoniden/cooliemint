namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.Temperature
{
    public interface ITemperatureInterpreterService
    {
        bool IsTrue(ICondition condition);
    }
}