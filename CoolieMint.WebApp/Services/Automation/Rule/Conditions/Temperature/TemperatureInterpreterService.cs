using CoolieMint.WebApp.Services.Storage;
using System;

namespace CoolieMint.WebApp.Services.Automation.Rule.Conditions.Temperature
{
    public class TemperatureInterpreterService : ITemperatureInterpreterService
    {
        private readonly ISystemStateCache _systemStateCache;

        public TemperatureInterpreterService(ISystemStateCache systemStateCache)
        {
            _systemStateCache = systemStateCache ?? throw new ArgumentNullException(nameof(systemStateCache));
        }

        public bool IsTrue(ICondition condition)
        {
            if (condition is ITemperatureCondition temperatureCondition)
            {
                var state = _systemStateCache.Get(temperatureCondition.SensorIdentifier);
                if (state == null)
                {
                    return false;
                }

                if (state.Value is WeatherState temperatureWeather)
                {
                    var temperature = temperatureWeather.Temperature;
                    if (condition is TemperatureHigherCondition)
                    {
                        return temperature > temperatureCondition.Temperature;
                    }

                    if (condition is TemperatureLowerCondition)
                    {
                        return temperature < temperatureCondition.Temperature;
                    }

                    return false;
                }
            }

            throw new ArgumentException(nameof(condition));
        }
    }
}
