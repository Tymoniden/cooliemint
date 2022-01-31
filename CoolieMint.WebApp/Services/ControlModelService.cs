using Newtonsoft.Json.Linq;
using System;
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
            IControlModel controlModel = null;
            var property = jObject.Property("Type");
            if(property.HasValues){
                
                switch(property.Value.ToString()){
                    case "ShowCase":
                        controlModel = (ShowCaseControlModel) jObject.ToObject(typeof(ShowCaseControlModel));
                        break;
                    case "Sonoff":
                        controlModel = (SonoffControlModel) jObject.ToObject(typeof(SonoffControlModel));
                        break;
                    case "MultiSwitch":
                        controlModel = (MultiSwitchControlModel) jObject.ToObject(typeof(MultiSwitchControlModel));
                        break;
                    case "Temperature":
                        controlModel = (TemperatureControlModel) jObject.ToObject(typeof(TemperatureControlModel));
                        break;
                    case "Weather":
                        controlModel = (WeatherControlModel) jObject.ToObject(typeof(WeatherControlModel));
                        break;
                }

                controlModel.Id = controlModel.Id == Guid.Empty ? Guid.NewGuid() : controlModel.Id;
            }

            return controlModel;
        }
    }
}