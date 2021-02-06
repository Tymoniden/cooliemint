using System;

namespace WebControlCenter.CommandAdapter.Enums
{
    public class TemperatureStatus: IAdapterStatusMessage
    {
        public double Temperature { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}