using CoolieMint.WebApp.Dtos.Automation;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions;

namespace CoolieMint.WebApp.Services.Automation.Factories
{
    public interface IAutomationConditionFactory
    {
        ConditionContainerDto CreateConditionContainerDto(ConditionContainer conditionContainer);
    }
}