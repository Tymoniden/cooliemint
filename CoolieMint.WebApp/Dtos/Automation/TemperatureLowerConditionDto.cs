using CoolieMint.WebApp.Services.Automation.Rule.Conditions;

namespace CoolieMint.WebApp.Dtos.Automation
{
    public class TemperatureLowerConditionDto : IConditionDto
    {
        public ConditionDtoType ConditionType { get; set; }
        public bool IsInverted { get; set; }
    }
}
