using System.Collections.Generic;

namespace WebControlCenter.Services.SmartDevice
{
    public interface IDeviceOperationProvider
    {
        DeviceOperation GetOperation(string operationName);
        void RegisterDeviceOperation(DeviceOperation deviceOperation);

        void RegisterDeviceOperations(IEnumerable<DeviceOperation> deviceOperation);
    }
}