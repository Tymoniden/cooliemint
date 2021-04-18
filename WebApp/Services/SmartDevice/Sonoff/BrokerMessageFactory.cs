using System;

namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public class BrokerMessageFactory : IBrokerMessageFactory
    {
        public SonoffBrokerMessage CreateBrokerMessage(DeviceArgument deviceArgument, OperationType operationType)
        {
            if (deviceArgument is null)
            {
                throw new ArgumentNullException(nameof(deviceArgument));
            }

            return new SonoffBrokerMessage
            {
                Identifier = deviceArgument.Identifier,
                CommandPrefix = deviceArgument.CommandPrefix,
                Value = operationType.ToString()
            };
        }
    }
}
