using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace WebControlCenter.Automation.Services
{
    public class ParseRuleService : IParseRuleService
    {
        private readonly IParseConditionService _parseConditionService;
        private readonly IParseActionDescriptorService _actionDescriptorService;

        public ParseRuleService(IParseConditionService parseConditionService, IParseActionDescriptorService actionDescriptorService)
        {
            _parseConditionService = parseConditionService;
            _actionDescriptorService = actionDescriptorService;
        }

        public List<Rule> ParseRules(JArray array)
        {
            var rules = new List<Rule>();

            foreach (JObject ruleEntry in array)
            {
                rules.Add(ParseRule(ruleEntry));
            }

            return rules;
        }

        public Rule ParseRule(JObject jObject)
        {
            var rule = new Rule();
            rule.Name = jObject["Name"].ToString();

            //rule.Condition = _parseConditionService.ParseCondition((JObject)jObject["Condition"]);
            rule.OnTrue = _actionDescriptorService.ParseActionDescriptors((JArray)jObject["OnTrue"]);
            rule.OnFalse = _actionDescriptorService.ParseActionDescriptors((JArray)jObject["OnFalse"]);
            return rule;
        }
    }
}
