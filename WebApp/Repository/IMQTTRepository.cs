using System;
using WebControlCenter.CommandAdapter;

namespace WebControlCenter.Repository
{
    public interface IMqttRepository
    {
        void PublishMessage(string topic, string payload, bool retain = false);

        void Initialize();
    }
}