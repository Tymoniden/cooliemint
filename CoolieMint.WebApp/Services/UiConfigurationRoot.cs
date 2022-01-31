using System;

namespace WebControlCenter.Services
{
    public class UiConfigurationRoot
    {
        public string Name { get; set; }
        public string Symbol { get; set; }
        public Guid? Id { get; set; }
        public UiConfigurationCategory[] Categories { get; set; }
    }
}