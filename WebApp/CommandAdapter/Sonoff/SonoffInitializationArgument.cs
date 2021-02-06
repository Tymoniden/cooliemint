using WebControlCenter.Repository;

namespace WebControlCenter.CommandAdapter.Sonoff
{
    public class SonoffInitializationArgument
    {
        private const string DefaultCommandPrefix = "cmnd";
        private const string DefaultTelePrefix = "tele";
        private const string DefaultStatPrefix = "stat";

        public string Identifier { get; set; }

        public string CommandPrefix { get; set; } = DefaultCommandPrefix;

        public string TelePrefix { get; set; } = DefaultTelePrefix;

        public string StatPrefix { get; set; } = DefaultStatPrefix;

        public IMessageBroker MessageBroker { get; set; }
    }
}