using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebControlCenter.Automation.Services
{
    public class ParseConfigurationService : IParseConfigurationService
    {
        private readonly IParseRuleService _ruleService;

        public ParseConfigurationService(IParseRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        public List<Rule> ParseConfiguration(string content)
        {
            var rules = JsonConvert.DeserializeObject<JArray>(content);
            return _ruleService.ParseRules(rules);
        }
    }
}
