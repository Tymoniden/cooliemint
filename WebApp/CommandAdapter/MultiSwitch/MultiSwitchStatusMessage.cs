using System;

namespace WebControlCenter.CommandAdapter.MultiSwitch
{
    public class MultiSwitchStatusMessage : IAdapterStatusMessage
    {
        public DateTime TimeStamp { get; set; }

        public object StateMapping { get; set; }
    }
}
