using System;
using System.Collections.Generic;

namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public class SonoffDeviceOperationService : ISonoffDeviceOperationService
    {
        private readonly IDeviceOperationFactory _deviceOperationFactory;
        private readonly IDeviceArgumentFactory _deviceArgumentFactory;

        public SonoffDeviceOperationService(IDeviceOperationFactory deviceOperationFactory, IDeviceArgumentFactory deviceArgumentFactory)
        {
            _deviceOperationFactory = deviceOperationFactory ?? throw new ArgumentNullException(nameof(deviceOperationFactory));
            _deviceArgumentFactory = deviceArgumentFactory ?? throw new ArgumentNullException(nameof(deviceArgumentFactory));
        }

        public IEnumerable<DeviceOperation> GetOperations(MqttAdapterEntry adapterEntry)
        {
            if(adapterEntry.Type != MqttAdapterTypes.Sonoff)
            {
                yield break;
            }

            var deviceArguments = _deviceArgumentFactory.CreateDeviceArgument(adapterEntry);

            foreach (var operation in _deviceOperationFactory.CreateAllDeviceOperations(deviceArguments))
            {
                yield return operation;
            }
        }
    }
}
