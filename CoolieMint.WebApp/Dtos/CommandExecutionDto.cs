using System;

namespace CoolieMint.WebApp.Dtos
{
    public class CommandExecutionDto
    {
        public CommandDto Command { get; set; }
        public DateTime? ExecutionTimestamp { get; set; }

        public string Delay { get; set; }
    }
}
