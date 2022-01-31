namespace WebControlCenter.Repository
{
    public class MessageBrokerMessageArgument
    {
        public string Topic { get; set; }
        public object Payload { get; set; }
        public bool IsRetained { get; set; }
    }
}