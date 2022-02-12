using System.Collections.Generic;
using WebControlCenter.Automation;

namespace CoolieMint.WebApp.Services.Automation
{
    public interface IAutomationRulesStore
    {
        void AddRule(Rule rule, bool replace = true);
        List<Rule> GetRules();
    }
}