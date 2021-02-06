using System;

namespace WebControlCenter.CommandAdapter.Enums
{
    public class PowerSocketStatus : IAdapterStatusMessage
    {
        public PowerSocketState State { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}