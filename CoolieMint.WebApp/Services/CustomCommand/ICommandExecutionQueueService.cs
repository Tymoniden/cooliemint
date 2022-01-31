using CoolieMint.WebApp.Dtos;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public interface ICommandExecutionQueueService
    {
        CommandExecutionDto Dequeue();
        void Enqueue(CommandExecutionDto commandExecutionDto);
        bool HasExecutableItem();
    }
}