using WebControlCenter.CommandAdapter.Enums;

namespace CoolieMint.WebApp.Services.Storage
{
    public interface IStateEntryMapper
    {
        StateEntry Map(string identifier, WeatherStatus weatherStatus);
        StateEntry Map(string identifier, PowerSocketState powerSocketState);
    }
}