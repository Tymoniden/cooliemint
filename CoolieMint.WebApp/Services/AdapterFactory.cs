using System;
using Microsoft.Extensions.DependencyInjection;
using WebControlCenter.CommandAdapter;
using WebControlCenter.CommandAdapter.Temperature;
using WebControlCenter.CommandAdapter.Weather;
using WebControlCenter.Repository;

namespace WebControlCenter.Services
{
    public interface IAdapterFactory
    {
        IMqttAdapter CreateMqttAdapter(Type adapterType);
    }

    public class AdapterFactory : IAdapterFactory
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IJsonSerializerService _jsonSerializerService;
        private readonly IEncodingService _endEncodingService;

        public AdapterFactory(IMessageBroker messageBroker, IJsonSerializerService jsonSerializerService, IEncodingService endEncodingService)
        {
            _messageBroker = messageBroker;
            _jsonSerializerService = jsonSerializerService;
            _endEncodingService = endEncodingService;
        }

        public IMqttAdapter CreateMqttAdapter(Type adapterType)
        {
            if(adapterType == typeof(WeatherAdapter))
            {
                return new WeatherAdapter( _jsonSerializerService, _endEncodingService);
            }

            throw new NotImplementedException();
        }
    }
}