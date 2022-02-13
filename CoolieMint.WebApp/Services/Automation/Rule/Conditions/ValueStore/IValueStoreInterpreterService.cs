namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.ValueStore
{
    public interface IValueStoreInterpreterService
    {
        bool IsTrue(ICondition condition);
    }
}