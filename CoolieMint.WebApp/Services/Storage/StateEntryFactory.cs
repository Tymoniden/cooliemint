namespace CoolieMint.WebApp.Services.Storage
{
    public class StateEntryFactory : IStateEntryFactory
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public StateEntryFactory(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new System.ArgumentNullException(nameof(dateTimeProvider));
        }

        public StateEntry CreateStateEntry(string identifier, IStateEntryValue value)
        {
            return new StateEntry
            {
                Identifier = identifier,
                Value = value,
                Start = _dateTimeProvider.UtcNow()
            };
        }
    }
}
