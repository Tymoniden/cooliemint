using WebControlCenter.Automation;

namespace CoolieMint.WebApp.Services.Automation
{
    public interface IAutomationRulesConditionValidator
    {
        bool CanExecute(ConditionContainer conditionContainer);
    }
}