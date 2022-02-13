using CoolieMint.WebApp.Services.Automation.Rule.Conditions;
using System.Collections.Generic;

namespace CoolieMint.WebApp.Dtos.Automation
{
    public class ConditionContainerDto
    {
        public ChainTypeDto ChainType { get; set; }
        public List<IConditionDto> Conditions { get; set; } = new();
    }
}
