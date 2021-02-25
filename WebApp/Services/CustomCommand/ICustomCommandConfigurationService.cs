using System.Collections.Generic;
using WebControlCenter.CustomCommand;

namespace WebControlCenter.Services.CustomCommand
{
    public interface ICustomCommandConfigurationService
    {
        List<Command> ReadCustomCommands();
        void ReloadConfiguration();
    }
}