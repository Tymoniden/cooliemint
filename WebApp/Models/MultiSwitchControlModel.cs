using System;
using WebControlCenter.CommandAdapter.MultiSwitch;

namespace WebControlCenter.Models
{
    public class MultiSwitchControlModel : IControlModel
    {
        public MultiSwitchControlModel()
        {
        }

        public MultiSwitchControlModel(MultiSwitchInitializationArgument arguments)
        {
            Identifier = arguments.Identifier;
            NumSwitches = arguments.NumSwitches;
            StartIndex = arguments.StartIndex;
        }

        public string Identifier { get; set; }

        public int StartIndex { get; set; }

        public int NumSwitches { get; set; }

        public string Title { get; set; }

        public string Adapter { get; set; } = "Mqtt:MultiSwitchAdapter";

        public ControlAlignment Alignment { get; set; }

        public ControlSize Size { get; set; } = ControlSize.Full;
    }
}
