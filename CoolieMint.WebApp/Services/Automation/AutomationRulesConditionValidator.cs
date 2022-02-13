using CoolieMint.WebApp.Services.Automation.Rule.Conditions;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.Temperature;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.Time;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.ValueStore;
using System;

namespace CoolieMint.WebApp.Services.Automation
{
    public class AutomationRulesConditionValidator : IAutomationRulesConditionValidator
    {
        private readonly ITimeInterpreterService _timeInterpreterService;
        private readonly IValueStoreInterpreterService _valueStoreInterpreterService;
        private readonly ITemperatureInterpreterService _temperatureInterpreterService;

        public AutomationRulesConditionValidator(ITimeInterpreterService timeInterpreterService, 
            IValueStoreInterpreterService valueStoreInterpreterService,
            ITemperatureInterpreterService temperatureInterpreterService)
        {
            _timeInterpreterService = timeInterpreterService ?? throw new ArgumentNullException(nameof(timeInterpreterService));
            _valueStoreInterpreterService = valueStoreInterpreterService ?? throw new ArgumentNullException(nameof(valueStoreInterpreterService));
            _temperatureInterpreterService = temperatureInterpreterService ?? throw new ArgumentNullException(nameof(temperatureInterpreterService));
        }

        public bool CanExecute(ConditionContainer conditionContainer)
        {
            foreach (var condition in conditionContainer.Conditions)
            {
                if (!IsMet(condition))
                {
                    return false;
                }
            }

            return true;
        }

        bool IsMet(ICondition condition)
        {
            switch (condition.ConditionType)
            {
                case ConditionType.ValueStore:
                    return _valueStoreInterpreterService.IsTrue(condition);
                case ConditionType.Time:
                    return _timeInterpreterService.IsTrue(condition);
                case ConditionType.Date:
                    return false;
                case ConditionType.Weekday:
                    return false;
                case ConditionType.Temperature:
                    return _temperatureInterpreterService.IsTrue(condition);
                default:
                    throw new ArgumentException(nameof(condition));
            }
        }


    }
}
