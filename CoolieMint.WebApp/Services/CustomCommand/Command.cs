using System.Collections.Generic;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public class Command
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CallingIdentifier { get; set; }

        public List<ControllerAction> Actions { get; set; }
    }
}
