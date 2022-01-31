using System;

namespace WebControlCenter.Services.SmartDevice
{
    public class DeviceOperation
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Action Action { get; set; }

        public void Execute() => Action.Invoke();
    }
}
