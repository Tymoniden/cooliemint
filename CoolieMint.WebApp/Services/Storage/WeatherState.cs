using System;

namespace CoolieMint.WebApp.Services.Storage
{
    public class WeatherState : IStateEntryValue
    {
        public decimal Temperature { get; set; }
        public int Humidity { get; set; }
        public int Preassure { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is WeatherState weatherState)
            {
                return weatherState.Temperature == Temperature &&
                    weatherState.Humidity == Humidity &&
                    weatherState.Preassure == Preassure;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool HasChanged(IStateEntryValue value)
        {
            if(value is WeatherState weatherState)
            {
                return weatherState.Temperature != Temperature ||
                    weatherState.Humidity != Humidity ||
                    weatherState.Preassure != Preassure;
            }

            throw new ArgumentException($"Type mismatch. Expected type {typeof(WeatherState)} got {value.GetType()}",nameof(value));
        }
    }
}
