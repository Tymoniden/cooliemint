using System;

namespace CoolieMint.WebApp.Services.Storage
{
    public class StateEntry
    {
        public string Identifier { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public IStateEntryValue Value { get; set; }
        public DateTime? PersistTimestamp { get; set; }
    }
}
