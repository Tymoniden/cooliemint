using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WebControlCenter.Automation.Services
{
    public interface IParseRuleService
    {
        List<Rule> ParseRules(JArray array);
    }
}
