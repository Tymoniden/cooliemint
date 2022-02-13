using CoolieMint.WebApp.Services.Automation.Rule.Action;
using System.Collections.Generic;

namespace CoolieMint.WebApp.Dtos.Automation
{
    public class SceneDto
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ConditionContainerDto Condition { get; set; }
        public List<IAutomationActionDto> OnTrue { get; set; } = new List<IAutomationActionDto>();
        public List<IAutomationActionDto> OnFalse { get; set; } = new List<IAutomationActionDto>();
    }
}
