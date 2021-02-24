using System;
using System.Collections.Generic;
using System.Linq;

namespace WebControlCenter.Services.SmartDevice
{
    public class DeviceOperationProvider : IDeviceOperationProvider
    {
        List<DeviceOperation> _deviceOperations = new List<DeviceOperation>();

        public List<DeviceOperation> GetDeviceOperations => _deviceOperations;

        public DeviceOperation GetOperation(string operationName) => _deviceOperations.FirstOrDefault(op => op.Name == operationName);

        public void RegisterDeviceOperation(DeviceOperation deviceOperation)
        {
            if (deviceOperation is null)
            {
                throw new ArgumentNullException(nameof(deviceOperation));
            }

            _deviceOperations.Add(deviceOperation);
        }

        public void RegisterDeviceOperations(IEnumerable<DeviceOperation> deviceOperations)
        {
            foreach (var deviceOperation in deviceOperations)
            {
                RegisterDeviceOperation(deviceOperation);
            }
        }
    }
}
