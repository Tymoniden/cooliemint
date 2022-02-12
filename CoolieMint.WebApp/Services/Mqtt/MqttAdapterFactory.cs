using CoolieMint.WebApp.Services.Storage;
using Newtonsoft.Json.Linq;
using System;
using WebControlCenter.CommandAdapter;
using WebControlCenter.CommandAdapter.MultiSwitch;
using WebControlCenter.CommandAdapter.ShowCase;
using WebControlCenter.CommandAdapter.Sonoff;
using WebControlCenter.CommandAdapter.Temperature;
using WebControlCenter.CommandAdapter.Weather;
using WebControlCenter.Database.Services;
using WebControlCenter.Repository;
using WebControlCenter.Services;

namespace CoolieMint.WebApp.Services.Mqtt
{
    public class MqttAdapterFactory : IMqttAdapterFactory
    {
        readonly IMessageBroker _messageBroker;
        readonly IJsonSerializerService _jsonSerializerService;
        readonly IEncodingService _encodingService;
        readonly IMqttAdapterDbService _mqttAdapterDbService;
        readonly IControllerStateService _controllerStateService;
        readonly IModelFactory _modelFactory;
        readonly IStateEntryMapper _stateEntryMapper;
        readonly ISystemStateCache _systemStateCache;

        public MqttAdapterFactory(
            IMessageBroker messageBroker,
            IJsonSerializerService jsonSerializerService,
            IEncodingService encodingService,
            IMqttAdapterDbService mqttAdapterDbService,
            IControllerStateService controllerStateService,
            IModelFactory modelFactory,
            IStateEntryMapper stateEntryMapper,
            ISystemStateCache systemStateCache)
        {
            _messageBroker = messageBroker ?? throw new ArgumentNullException(nameof(messageBroker));
            _jsonSerializerService = jsonSerializerService ?? throw new ArgumentNullException(nameof(jsonSerializerService));
            _encodingService = encodingService ?? throw new ArgumentNullException(nameof(encodingService));
            _mqttAdapterDbService = mqttAdapterDbService ?? throw new ArgumentNullException(nameof(mqttAdapterDbService));
            _controllerStateService = controllerStateService ?? throw new ArgumentNullException(nameof(controllerStateService));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            _stateEntryMapper = stateEntryMapper ?? throw new ArgumentNullException(nameof(stateEntryMapper));
            _systemStateCache = systemStateCache ?? throw new ArgumentNullException(nameof(systemStateCache));
        }

        public IMqttAdapter CreateMqttAdapter(MqttAdapterEntry mqttAdapterEntry)
        {

            switch (mqttAdapterEntry.Type)
            {
                case MqttAdapterTypes.Sonoff:
                    return CreateSonoffAdapter(mqttAdapterEntry.Arguments);
                case MqttAdapterTypes.Weather:
                    return CreateWeatherAdapter(mqttAdapterEntry.Arguments);
                case MqttAdapterTypes.Multiswitch:
                    return CreateMultiSwitchAdapter(mqttAdapterEntry.Arguments);
                case MqttAdapterTypes.Showcase:
                    return CreateShowCaseAdapter(mqttAdapterEntry.Arguments);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        SonoffAdapter CreateSonoffAdapter(JObject arguments)
            => new SonoffAdapter(arguments.ToObject<SonoffInitializationArgument>(), _messageBroker, _mqttAdapterDbService, _controllerStateService, _modelFactory);

        WeatherAdapter CreateWeatherAdapter(JObject arguments) =>
            new WeatherAdapter(arguments.ToObject<WeatherAdapterInitializationArgument>(),
                _jsonSerializerService,
                _encodingService,
                _stateEntryMapper,
                _systemStateCache);

        MultiSwitchAdapter CreateMultiSwitchAdapter(JObject arguments) =>
            new MultiSwitchAdapter(arguments.ToObject<MultiSwitchInitializationArgument>(),
                _messageBroker,
                _stateEntryMapper,
                _systemStateCache);

        ShowCaseAdapter CreateShowCaseAdapter(JObject arguments) =>
            new ShowCaseAdapter(arguments.ToObject<ShowCaseInitializationArgument>(),
                _messageBroker);
    }
}