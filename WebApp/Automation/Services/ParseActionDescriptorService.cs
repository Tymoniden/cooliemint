using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace WebControlCenter.Automation.Services
{
    public class ParseActionDescriptorService : IParseActionDescriptorService
    {
        public List<IActionDescriptor> ParseActionDescriptors(JArray array)
        {
            var actionDescriptors = new List<IActionDescriptor>();
            foreach (var jObect in array)
            {
                actionDescriptors.Add(ParseActionDescriptor((JObject)jObect));
            }

            return actionDescriptors;
        }

        public IActionDescriptor ParseActionDescriptor(JObject jObject)
        {
            var typeString = jObject["Type"].ToString();
            var type = Enum.Parse(typeof(ActionDescriptorType), typeString);
            switch (type)
            {
                case ActionDescriptorType.MQTT:
                    return MqttActionDescriptorFromJObject(jObject);
                case ActionDescriptorType.ValueStore:
                    return ValueStoreActionDescriptorFromJObject(jObject);
                default:
                    throw new NotSupportedException();
            }
        }

        // TODO auslagern?
        public IActionDescriptor ValueStoreActionDescriptorFromJObject(JObject jObject)
        {
            var actionDescriptor = new ValueStoreActionDescriptor();
            actionDescriptor.Key = jObject["Key"].ToString();
            actionDescriptor.Value = jObject["Value"].ToString();

            return actionDescriptor;
        }

        // TODO auslagern?
        public IActionDescriptor MqttActionDescriptorFromJObject(JObject jObject)
        {
            var actionDescriptor = new MqttActionDescriptor();
            actionDescriptor.Topic = jObject["Topic"].ToString();
            actionDescriptor.Body = jObject["Body"].ToString();
            actionDescriptor.IsRetained = Convert.ToBoolean(jObject["IsRetained"].ToString());

            return actionDescriptor;
        }
    }
}
