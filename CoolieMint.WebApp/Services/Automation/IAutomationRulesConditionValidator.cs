using CoolieMint.WebApp.Services.Automation.Rule.Conditions;

namespace CoolieMint.WebApp.Services.Automation
{
    public interface IAutomationRulesConditionValidator
    {
        bool CanExecute(ConditionContainer conditionContainer);
    }
}