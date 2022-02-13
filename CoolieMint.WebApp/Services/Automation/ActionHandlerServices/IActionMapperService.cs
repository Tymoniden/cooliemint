using CoolieMint.WebApp.Services.Automation.Rule.Action;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices
{
    public interface IActionMapperService
    {
        void HandleAction(IAutomationAction action);
    }
}