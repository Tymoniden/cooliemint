using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json.Linq;
using WebControlCenter.Models;

namespace WebControlCenter.Services
{
    public interface IControlModelService
    {
        IControlModel Convert(JObject jObject);
    }

    public class ControlModelService : IControlModelService
    {
        public IControlModel Convert(JObject jObject)
        {
            var property = jObject.Property("Type");
            if(property.HasValues){
                switch(property.Value.ToString()){
                    case "ShowCase":
                        return (ShowCaseControlModel) jObject.ToObject(typeof(ShowCaseControlModel));
                    case "Sonoff":
                        return (SonoffControlModel) jObject.ToObject(typeof(SonoffControlModel));
                    case "MultiSwitch":
                        return (MultiSwitchControlModel) jObject.ToObject(typeof(MultiSwitchControlModel));
                    case "Temperature":
                        return (TemperatureControlModel) jObject.ToObject(typeof(TemperatureControlModel));
                    case "Weather":
                        return (WeatherControlModel) jObject.ToObject(typeof(WeatherControlModel));
                }
            }

            return null;
        }
    }
}