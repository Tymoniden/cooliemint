using CoolieMint.WebApp.Automation.Conditions;
using System.Collections.Generic;

namespace WebControlCenter.Automation
{
    public class ConditionContainer
    {
        public ChainType ChainType { get; set; }
        public List<ICondition> Conditions { get; set; } = new ();
        public List<ConditionContainer> ChildConditions { get; set; } = new();
    }
}
