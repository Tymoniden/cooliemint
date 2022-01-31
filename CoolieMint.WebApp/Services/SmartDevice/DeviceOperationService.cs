using System;
using System.Collections.Generic;

namespace WebControlCenter.Services.SmartDevice
{
    public class DeviceOperationService : IDeviceOperationServiceProvider
    {
        List<IDeviceOperationService> _registeredOperationServices = new List<IDeviceOperationService>();

        public IEnumerable<DeviceOperation> GetOperations(MqttAdapterEntry adapterEntry)
        {
            foreach(var service in _registeredOperationServices)
            {
                foreach(var operation in service.GetOperations(adapterEntry))
                {
                    yield return operation;
                }
            }
        }

        public void RegisterOperationService(IDeviceOperationService deviceOperationService)
        {
            if (deviceOperationService is null)
            {
                throw new ArgumentNullException(nameof(deviceOperationService));
            }

            _registeredOperationServices.Add(deviceOperationService);
        }
    }
}
