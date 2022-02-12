namespace CoolieMint.WebApp.Automation.Conditions.ValueStore
{
    public interface IValueStoreInterpreterService
    {
        bool IsTrue(ICondition condition);
    }
}