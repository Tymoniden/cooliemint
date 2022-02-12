using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace WebControlCenter.Automation
{
    class Automation
    {

        //public ConditionContainer ParseCondition(JObject jObject)
        //{
        //    var condition = new ConditionContainer();
        //    condition.ChainType = (ChainType)Enum.Parse(typeof(ChainType), jObject["ChainType"].ToString());

        //    foreach (var parameter in (JArray)jObject["Parameters"])
        //    {
        //        condition.Parameters.Add(ParseParameter((JObject)parameter));
        //    }

        //    return condition;
        //}

        //public IConditionParameter ParseParameter(JObject jObject)
        //{
        //    var parameterType = Enum.Parse(typeof(ConditionParameterType), jObject["Type"].ToString());
        //    switch (parameterType)
        //    {
        //        case ConditionParameterType.MQTT:
        //            return MQTTConditionFromJObject(jObject);
        //        case ConditionParameterType.ValueStore:
        //            return ValueStoreConditionFromJObject(jObject);
        //        case ConditionParameterType.Timestamp:
        //            return TimestampConditionFromJObject(jObject);
        //        default:
        //            throw new NotSupportedException();
        //    }
        //}

        //public MqttCondition MQTTConditionFromJObject(JObject jObject)
        //{
        //    var condition = new MqttCondition();
        //    condition.Comparer = (MqttComparer)Enum.Parse(typeof(MqttComparer), jObject["Comparer"].ToString());
        //    condition.Topic = jObject["Topic"]?.ToString();
        //    condition.Body = jObject["Body"]?.ToString();

        //    return condition;
        //}

        //public ValueStoreCondition ValueStoreConditionFromJObject(JObject jObject)
        //{
        //    var condition = new ValueStoreCondition();

        //    condition.Key = jObject["Key"].ToString();
        //    condition.Operation = jObject["Operation"].ToString();
        //    condition.Operator = jObject["Operator"].ToString();

        //    return condition;
        //}

        //public TimestampCondition TimestampConditionFromJObject(JObject jObject)
        //{
        //    var condition = new TimestampCondition();

        //    var dateTimeType = jObject["DateTimeType"].ToString();
        //    condition.Type = (TimestampConditionType)Enum.Parse(typeof(TimestampConditionType), dateTimeType);
        //    condition.IncludedDays = ParseDays((JArray)jObject["IncludedDays"]);
        //    condition.ExcludedDays = ParseDays((JArray)jObject["ExcludedDays"]);
        //    condition.Value = jObject["Value"].ToString();

        //    return condition;
        //}

        //public List<TimestampConditionDay> ParseDays(JArray jArray)
        //{
        //    var days = new List<TimestampConditionDay>();
        //    foreach (var dayToken in jArray)
        //    {
        //        days.Add(Enum.Parse<TimestampConditionDay>(dayToken.ToString(), true));
        //    }

        //    return days;
        //}
    }
}
