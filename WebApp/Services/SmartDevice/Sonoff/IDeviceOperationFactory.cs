using System.Collections.Generic;

namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public interface IDeviceOperationFactory
    {
        IEnumerable<DeviceOperation> CreateAllDeviceOperations(DeviceArgument deviceArgument);
        DeviceOperation CreateDeviceOperation(DeviceArgument deviceArgument, OperationType type);
    }
}