using System;
using WebControlCenter.CommandAdapter.Enums;

namespace CoolieMint.WebApp.Services.Storage
{
    public class StateEntryMapper : IStateEntryMapper
    {
        private readonly IStateEntryFactory _stateEntryFactory;

        public StateEntryMapper(IStateEntryFactory stateEntryFactory)
        {
            _stateEntryFactory = stateEntryFactory ?? throw new ArgumentNullException(nameof(stateEntryFactory));
        }

        public StateEntry Map(string identifier, WeatherStatus weatherStatus)
        {
            return _stateEntryFactory.CreateStateEntry(identifier, new WeatherState
            {
                Temperature = weatherStatus.Temperature,
                Humidity = weatherStatus.Humidity,
                Preassure = weatherStatus.Pressure
            });
        }

        public StateEntry Map(string identifier, PowerSocketState powerSocketState)
        {
            return _stateEntryFactory.CreateStateEntry(identifier, new SwitchState
            {
                IsOn = powerSocketState == PowerSocketState.On
            });
        }
    }
}
