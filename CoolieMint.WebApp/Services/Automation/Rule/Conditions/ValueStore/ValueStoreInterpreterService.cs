using CoolieMint.WebApp.Services.Storage;
using System;

namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.ValueStore
{
    public class ValueStoreInterpreterService : IValueStoreInterpreterService
    {
        private readonly ISystemStateCache _systemStateCache;

        public ValueStoreInterpreterService(ISystemStateCache systemStateCache)
        {
            _systemStateCache = systemStateCache ?? throw new ArgumentNullException(nameof(systemStateCache));
        }

        public bool IsTrue(ICondition condition)
        {
            if (condition is ValueStoreCondition valueStoreCondition)
            {
                var stateEntryValue = _systemStateCache.Get(valueStoreCondition.Key);

                if (stateEntryValue == null)
                {
                    return false;
                }

                return stateEntryValue.Value.Equals(valueStoreCondition.Value);
            }

            throw new ArgumentException(nameof(condition));
        }
    }
}
