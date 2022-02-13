using System.Collections.Generic;

namespace CoolieMint.WebApp.Services.Storage
{
    public interface ISystemStateCache
    {
        void AddStateEntry(StateEntry stateEntry);
        void CleanUp();
        StateEntry Get(string index);
        List<StateEntry> GetAll();
    }
}