using System.Collections.Generic;

namespace WebControlCenter.Services.SmartDevice
{
    public interface IDeviceOperationService
    {
        IEnumerable<DeviceOperation> GetOperations(MqttAdapterEntry adapterEntry);
    }

    public interface IDeviceOperationServiceProvider : IDeviceOperationService
    {
        void RegisterOperationService(IDeviceOperationService deviceOperationService);
    }
}