using CoolieMint.WebApp.Services.Automation.Rule.Action;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices
{
    public interface IValueStoreActionHandler
    {
        void HandleAction(ValueStoreAction valueStoreAction);
    }
}