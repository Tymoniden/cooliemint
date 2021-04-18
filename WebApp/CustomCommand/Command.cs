using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebControlCenter.CustomCommand
{
    public class Command
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CallingIdentifier { get; set; }

        public List<ControllerAction> Actions { get; set; }
    }

    public class ControllerAction
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int Order { get; set; }
    }

    public class CommandCondition
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Reference { get; set; }

        public string Value { get; set; }

        public string Operation { get; set; }
    }
}
