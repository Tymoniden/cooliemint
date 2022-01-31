using System.Collections.Generic;

namespace CoolieMint.WebApp.Dtos
{
    public class CommandDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ActionDto> Actions { get; set; } = new List<ActionDto>();
    }
}
