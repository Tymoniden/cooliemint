using System;

namespace WebControlCenter.CommandAdapter.Enums
{
    public class WeatherStatus : IAdapterStatusMessage
    {
        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double Pressure { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}