namespace CoolieMint.WebApp.Services.Storage
{
    public interface IStateEntryFactory
    {
        StateEntry CreateStateEntry(string identifier, IStateEntryValue value);
    }
}