using System;
using System.Collections.Generic;

namespace WebControlCenter.Repository
{
    public interface IMqttValueProvider
    {
        MqttValueContract GetContract(string topic);
        List<MqttValueContract> GetAllContracts();
        T GetValue<T>(string topic);
        void SetValue(MqttValueContract value);
        void SetValue(string topic, MqttValueContract value);

        List<MqttValueContract> GetContracts(string[] topics, DateTime? timestamp = null);
    }
}