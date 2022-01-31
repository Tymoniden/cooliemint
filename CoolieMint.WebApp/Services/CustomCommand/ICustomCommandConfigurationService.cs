using CoolieMint.WebApp.Services.CustomCommand;
using System.Collections.Generic;

namespace WebControlCenter.Services.CustomCommand
{
    public interface ICustomCommandConfigurationService
    {
        List<Command> ReadCustomCommands();
        void ReloadConfiguration();
    }
}