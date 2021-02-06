namespace WebControlCenter.CommandAdapter.Temperature
{
    public class WeatherAdapterInitializationArgument
    {
        public string Identifier { get; set; }

        public string TopicPrefix { get; set; } = "/";
    }
}