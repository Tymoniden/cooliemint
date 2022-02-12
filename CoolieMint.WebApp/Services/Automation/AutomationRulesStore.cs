using System.Collections.Generic;
using System.Linq;
using WebControlCenter.Automation;

namespace CoolieMint.WebApp.Services.Automation
{
    public sealed class AutomationRulesStore : IAutomationRulesStore
    {
        readonly List<Rule> _rules = new();
        private readonly IDateTimeProvider _dateTimeProvider;

        public AutomationRulesStore(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new System.ArgumentNullException(nameof(dateTimeProvider));
        }

        public void AddRule(Rule rule, bool replace = true)
        {
            var existingRule = _rules.FirstOrDefault(r => r.Id == rule.Id);
            if (existingRule != null)
            {
                if (replace)
                {
                    _rules.Remove(existingRule);
                }
                else
                {
                    return;
                }
            }

            _rules.Add(rule);
        }

        public List<Rule> GetRules() => _rules.Where(rule => rule.NextExecution == null || rule.NextExecution < _dateTimeProvider.Now()).ToList();
    }
}
