namespace WebControlCenter.CommandAdapter.Temperature
{
    public class TemperatureInitializationArgument
    {
        public string Identifier { get; set; }

        public string TopicPrefix { get; set; } = "/";
    }
}