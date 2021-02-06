using MQTTnet;
using System;
using System.Collections.Generic;
using System.Linq;
using WebControlCenter.Repository;

namespace WebControlCenter.Services.Storage
{
    public class MqttMessageCacheProvider : IMqttMessageCacheProvider
    {
        int _limit = 100;
        Queue<MqttValueContract> _outgoingCache = new Queue<MqttValueContract>();
        Queue<MqttValueContract> _incomingCache = new Queue<MqttValueContract>();

        public void StoreIncomingMessage(MqttValueContract messageContract)
        {
            if (_incomingCache.Count == _limit)
            {
                _incomingCache.Dequeue();
            }

            _incomingCache.Enqueue(messageContract);
        }

        public void StoreOutgoiningMessage(MqttApplicationMessage message)
        {
            if (_outgoingCache.Count == _limit)
            {
                _outgoingCache.Dequeue();
            }

            // TODO: Converter?
            _outgoingCache.Enqueue(new MqttValueContract{
                Payload = message.Payload,
                Topic = message.Topic,
                TimeStamp = DateTime.UtcNow
            });
        }

        public void StoreOutgoiningMessage(MqttValueContract messageContract)
        {
            if (_outgoingCache.Count == _limit)
            {
                _outgoingCache.Dequeue();
            }

            _outgoingCache.Enqueue(messageContract);
        }

        public MqttValueContract[] GetIncomingMessages()
        {
            return _incomingCache.ToArray();
        }

        public MqttValueContract[] GetOutgoiningMessages()
        {
            return _outgoingCache.ToArray();
        }

        public List<MqttValueContract> GetMessages()
        {
            var allMessages = new List<MqttValueContract>();
            foreach(var message in _incomingCache)
            {
                allMessages.Add(new MqttValueContract
                {
                    Payload = message,
                    Topic = $"incoming: {message.Topic}",
                    TimeStamp = message.TimeStamp
                });
            }

            foreach (var message in _outgoingCache)
            {
                allMessages.Add(new MqttValueContract
                {
                    Payload = message,
                    Topic = $"outgoing: {message.Topic}",
                    TimeStamp = message.TimeStamp
                });
            }

            return allMessages.OrderBy(m => m.TimeStamp).ToList();
        }
    }
}
