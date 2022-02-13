namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.Time
{
    public interface ITimeInterpreterService
    {
        bool IsTrue(ICondition condition);
    }
}