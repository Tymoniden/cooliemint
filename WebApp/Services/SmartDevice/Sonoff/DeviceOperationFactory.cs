using System;
using System.Collections.Generic;

namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public class DeviceOperationFactory : IDeviceOperationFactory
    {
        private readonly IMqttInteractionService _mqttInteractionService;
        private readonly IBrokerMessageFactory _brokerMessageFactory;

        public DeviceOperationFactory(IMqttInteractionService mqttInteractionService, IBrokerMessageFactory brokerMessageFactory)
        {
            _mqttInteractionService = mqttInteractionService ?? throw new ArgumentNullException(nameof(mqttInteractionService));
            _brokerMessageFactory = brokerMessageFactory ?? throw new ArgumentNullException(nameof(brokerMessageFactory));
        }

        public IEnumerable<DeviceOperation> CreateAllDeviceOperations(DeviceArgument deviceArgument)
        {
            yield return CreateDeviceOperation(deviceArgument, OperationType.On);
            yield return CreateDeviceOperation(deviceArgument, OperationType.Off);
            yield return CreateDeviceOperation(deviceArgument, OperationType.Toggle);
        }

        public DeviceOperation CreateDeviceOperation(DeviceArgument deviceArgument, OperationType type)
        {
            var brokerMessage = _brokerMessageFactory.CreateBrokerMessage(deviceArgument, type);

            return new DeviceOperation()
            {
                Name = $"Sonoff:{deviceArgument.Identifier}:{type}",
                DisplayName = type.ToString(),
                Action = () =>
                {
                    _mqttInteractionService.SendMessage(brokerMessage);
                }
            };
        }
    }
}
