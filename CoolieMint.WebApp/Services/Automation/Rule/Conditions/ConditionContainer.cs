using System.Collections.Generic;

namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions
{
    public class ConditionContainer
    {
        public ChainType ChainType { get; set; }
        public List<ICondition> Conditions { get; set; } = new();
        public List<ConditionContainer> ChildConditions { get; set; } = new();
    }
}
