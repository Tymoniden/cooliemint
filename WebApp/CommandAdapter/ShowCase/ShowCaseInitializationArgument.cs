using WebControlCenter.Repository;

namespace WebControlCenter.CommandAdapter.ShowCase
{
    public class ShowCaseInitializationArgument
    {
        public string Identifier { get; set; }

        public IMessageBroker MessageBroker { get; set; }

        public int Columns { get; set; }

        public int Rows { get; set; }

        public int LightStartIndex { get; set; }

        public string TopicPrefix { get; set; } = string.Empty;
    }
}
