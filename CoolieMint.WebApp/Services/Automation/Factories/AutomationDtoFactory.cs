using CoolieMint.WebApp.Dtos.Automation;
using System.Linq;

namespace CoolieMint.WebApp.Services.Automation.Factories
{
    public class AutomationDtoFactory : IAutomationDtoFactory
    {
        private readonly IAutomationConditionFactory _automationConditionFactory;
        private readonly IAutomationActionFactory _automationActionFactory;

        public AutomationDtoFactory(IAutomationConditionFactory automationConditionFactory, IAutomationActionFactory automationActionFactory)
        {
            _automationConditionFactory = automationConditionFactory ?? throw new System.ArgumentNullException(nameof(automationConditionFactory));
            _automationActionFactory = automationActionFactory ?? throw new System.ArgumentNullException(nameof(automationActionFactory));
        }

        public SceneDto CreateSceneDto(Scene scene)
        {
            return new SceneDto
            {
                Id = scene.Id,
                DisplayName = scene.DisplayName,
                Description = scene.Description,
                Condition = _automationConditionFactory.CreateConditionContainerDto(scene.Condition),
                OnTrue = scene.OnTrue.Select(action => _automationActionFactory.CreateAutomationActionDto(action)).ToList(),
                OnFalse = scene.OnFalse.Select(action => _automationActionFactory.CreateAutomationActionDto(action)).ToList()
            };
        }
    }
}
