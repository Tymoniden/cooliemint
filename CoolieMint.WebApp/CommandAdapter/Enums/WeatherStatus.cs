using System;

namespace WebControlCenter.CommandAdapter.Enums
{
    public class WeatherStatus : IAdapterStatusMessage
    {
        public decimal Temperature { get; set; }

        public int Humidity { get; set; }

        public int Pressure { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}