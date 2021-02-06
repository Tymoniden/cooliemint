using System;
using System.Collections.Generic;
using System.Linq;

namespace WebControlCenter.Repository
{
    public class HistoryProvider
    {
        private List<HistoryEntry> _history = new List<HistoryEntry>();
        public void AddHistoryEntry(HistoryEntry entry)
        {
            _history.Add(entry);
        }

        public List<HistoryEntry> GetHistorySince(Guid guid)
        {
            var refEntry = _history.FirstOrDefault(entry => entry.Guid == guid);
            return GetHistorySince(refEntry.EntryTimeStamp);
        }

        public List<HistoryEntry> GetHistorySince(DateTime timestamp)
        {
            return _history.Where(entry => entry.EntryTimeStamp > timestamp)
                .OrderByDescending(h => h.EntryTimeStamp)
                .ToList();
        }
    }

    public class HistoryEntry
    {
        public Guid Guid { get; } = new Guid();
        public string Key { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }
        public DateTime EntryTimeStamp { get; set; }
    }
}
