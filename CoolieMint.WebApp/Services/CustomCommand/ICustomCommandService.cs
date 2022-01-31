using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public interface ICustomCommandService
    {
        Task<bool> ExecuteCommand(long commandId, CancellationToken cancellationToken);
        Command GetCommand(long id);
        List<Command> GetCommands();
        void RegisterCommand(Command command);
        void PersistChanges();
    }
}