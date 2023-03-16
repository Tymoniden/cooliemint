using CoolieMint.WebApp.Services.Automation.Rule.Action;
using CoolieMint.WebApp.Services.Storage;

namespace CoolieMint.WebApp.Services.Automation.ActionHandlerServices
{
    public class ValueStoreActionHandler : IValueStoreActionHandler
    {
        private readonly ISystemStateCache _systemStateCache;
        private readonly IStateEntryFactory _stateEntryFactory;

        public ValueStoreActionHandler(ISystemStateCache systemStateCache, IStateEntryFactory stateEntryFactory)
        {
            _systemStateCache = systemStateCache ?? throw new System.ArgumentNullException(nameof(systemStateCache));
            _stateEntryFactory = stateEntryFactory ?? throw new System.ArgumentNullException(nameof(stateEntryFactory));
        }

        public void HandleAction(ValueStoreAction valueStoreAction)
        {
            var stateEntry = _stateEntryFactory.CreateStateEntry(valueStoreAction.Key, new TextState { Text = valueStoreAction.Value });

            _systemStateCache.AddStateEntry(stateEntry);
        }
    }
}
