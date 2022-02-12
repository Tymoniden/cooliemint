namespace CoolieMint.WebApp.Services.Storage
{
    public interface IStateEntryValue
    {
        bool HasChanged(IStateEntryValue value);
    }
}
