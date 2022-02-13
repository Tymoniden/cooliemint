using System;
using System.Collections.Generic;
using System.Linq;

namespace CoolieMint.WebApp.Services.Storage
{
    public class SystemStateCache : ISystemStateCache
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        List<StateEntry> _stateEntries = new();

        public SystemStateCache(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
        }

        public void AddStateEntry(StateEntry stateEntry)
        {
            lock (_stateEntries)
            {
                var existingEntry = _stateEntries.FirstOrDefault(s => s.Identifier == stateEntry.Identifier && s.End == null);
                if (existingEntry != null)
                {
                    if (!existingEntry.Value.HasChanged(stateEntry.Value))
                    {
                        return;
                    }

                    existingEntry.End = _dateTimeProvider.UtcNow();
                    _stateEntries.Add(stateEntry);
                }
                else
                {
                    _stateEntries.Add(stateEntry);
                }
            }
        }

        public StateEntry Get(string index)
        {
            StateEntry entry;
            lock (_stateEntries)
            {
                // TODO is that such a good idea?
                entry = _stateEntries.FirstOrDefault(entry => entry.Identifier == index && entry.End == null);
            }

            return entry;
        }

        public List<StateEntry> GetAll()
        {
            return _stateEntries;
        }

        public void CleanUp()
        {
            for (int i = _stateEntries.Count - 1; i >= 0; i--)
            {
                var stateEntry = _stateEntries[i];
                if (stateEntry.End != null && stateEntry.PersistTimestamp != null)
                {
                    _stateEntries.Remove(stateEntry);
                }
            }
        }
    }
}
