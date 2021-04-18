namespace WebControlCenter.Services.SmartDevice.Sonoff
{
    public class DeviceArgumentFactory : IDeviceArgumentFactory
    {
        private const string DefaultCommandPrefix = "cmnd";
        private const string DefaultTelePrefix = "tele";
        private const string DefaultStatPrefix = "stat";

        public DeviceArgument CreateDeviceArgument(string identifier)
        {
            return CreateDeviceArgument(identifier, DefaultCommandPrefix);
        }

        public DeviceArgument CreateDeviceArgument(string identifier, string commandPrefix)
        {
            return CreateDeviceArgument(identifier, commandPrefix, DefaultTelePrefix, DefaultStatPrefix);
        }

        public DeviceArgument CreateDeviceArgument(string identifier, string commandPrefix, string telePrefix, string statPrefix)
        {
            return new DeviceArgument
            {
                Identifier = identifier,
                CommandPrefix = commandPrefix,
                TelePrefix = telePrefix,
                StatPrefix = statPrefix
            };
        }

        public DeviceArgument CreateDeviceArgument(MqttAdapterEntry mqttAdapterEntry)
        {
            var convertedArguments = mqttAdapterEntry.Arguments.ToObject<DeviceArgument>();
            if (string.IsNullOrWhiteSpace(convertedArguments.CommandPrefix))
            {
                convertedArguments.CommandPrefix = DefaultCommandPrefix;
            }

            if (string.IsNullOrWhiteSpace(convertedArguments.TelePrefix))
            {
                convertedArguments.TelePrefix = DefaultTelePrefix;
            }

            if (string.IsNullOrWhiteSpace(convertedArguments.StatPrefix))
            {
                convertedArguments.StatPrefix = DefaultStatPrefix;
            }

            return convertedArguments;
        }
    }
}
