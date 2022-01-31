using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace WebControlCenter.Repository
{
    public class MqttValueProvider : IMqttValueProvider
    {
        private static readonly ConcurrentDictionary<string, MqttValueContract> _values = new ConcurrentDictionary<string, MqttValueContract>();

        public MqttValueContract GetContract(string topic) => !_values.ContainsKey(topic) ? _values[topic] : null;
        public List<MqttValueContract> GetAllContracts() => _values.Select(entry => entry.Value).ToList();

        public T GetValue<T>(string topic)
        {
            if (!_values.ContainsKey(topic))
            {
                return default;
            }

            return (T)Convert.ChangeType(_values[topic].Payload, typeof(T));
        }

        public void SetValue(MqttValueContract value)
        {
            _values[value.Topic] = value;
        }

        public void SetValue(string topic, MqttValueContract value)
        {
            _values[topic] = value;
        }

        public List<MqttValueContract> GetContracts(string[] topics, DateTime? timestamp = null)
        {
            var values = _values.Where(entry => topics.Contains(entry.Key)).ToList();
            if (timestamp != null)
            {
                var refTimestamp = new DateTime(timestamp.Value.Year, timestamp.Value.Month, timestamp.Value.Day, timestamp.Value.Hour, timestamp.Value.Minute, timestamp.Value.Second, timestamp.Value.Millisecond);

                values = values.Where(entry => entry.Value.TimeStamp > refTimestamp).ToList();
            }
            return values.Select(entry => entry.Value).ToList();
        }
    }
}
