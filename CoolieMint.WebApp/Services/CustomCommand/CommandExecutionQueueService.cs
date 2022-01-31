using CoolieMint.WebApp.Dtos;
using System.Collections.Generic;
using System.Linq;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public class CommandExecutionQueueService : ICommandExecutionQueueService
    {
        List<CommandExecutionDto> _queue = new List<CommandExecutionDto>();
        private readonly IDateTimeProvider _dateTimeProvider;

        public CommandExecutionQueueService(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider ?? throw new global::System.ArgumentNullException(nameof(dateTimeProvider));
        }

        public void Enqueue(CommandExecutionDto commandExecutionDto)
        {
            lock (_queue)
            {
                _queue.Add(commandExecutionDto);
            }
        }

        public CommandExecutionDto Dequeue()
        {
            var now = _dateTimeProvider.UtcNow();
            CommandExecutionDto commandExecutionDto;
            lock (_queue)
            {
                commandExecutionDto = _queue.FirstOrDefault(c => c.ExecutionTimestamp == null || c.ExecutionTimestamp <= now);

                if (commandExecutionDto != null)
                {
                    _queue.Remove(commandExecutionDto);
                }
            }

            return commandExecutionDto;
        }

        public bool HasExecutableItem()
        {
            var now = _dateTimeProvider.UtcNow();
            return _queue.Any(c => c.ExecutionTimestamp == null || c.ExecutionTimestamp <= now);
        }
    }
}
