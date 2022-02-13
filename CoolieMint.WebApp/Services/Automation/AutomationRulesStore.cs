using System.Collections.Generic;
using System.Linq;

namespace CoolieMint.WebApp.Services.Automation
{
    // TODO rename to AutomationScenesStore or rename Scene
    public sealed class AutomationRulesStore : IAutomationRulesStore
    {
        readonly List<Scene> _scenes = new();
        private readonly IDateTimeProvider _dateTimeProvider;

        public AutomationRulesStore(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new System.ArgumentNullException(nameof(dateTimeProvider));
        }

        public void AddScene(Scene rule, bool replace = true)
        {
            var existingRule = _scenes.FirstOrDefault(r => r.Id == rule.Id);
            if (existingRule != null)
            {
                if (replace)
                {
                    _scenes.Remove(existingRule);
                }
                else
                {
                    return;
                }
            }

            _scenes.Add(rule);
        }

        public List<Scene> GetScenes() => _scenes.Where(rule => rule.NextExecution == null || rule.NextExecution < _dateTimeProvider.Now()).ToList();
    }
}
