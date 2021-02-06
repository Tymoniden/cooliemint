using System.Collections.Generic;

namespace WebControlCenter.Automation
{
    public class Condition
    {
        public ChainType ChainType { get; set; }
        public List<IConditionParameter> Parameters { get; set; } = new List<IConditionParameter>();
    }

}
