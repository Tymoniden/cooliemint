using MQTTnet;
using System.Collections.Generic;
using WebControlCenter.Repository;

namespace WebControlCenter.Services.Storage
{
    public interface IMqttMessageCacheProvider
    {
        MqttValueContract[] GetIncomingMessages();
        void StoreIncomingMessage(MqttValueContract messageContract);

        public void StoreOutgoiningMessage(MqttValueContract messageContract);

        public void StoreOutgoiningMessage(MqttApplicationMessage message);

        public MqttValueContract[] GetOutgoiningMessages();

        public List<MqttValueContract> GetMessages();
    }
}