using System.Collections.Generic;
using WebControlCenter.Models;

namespace WebControlCenter.Services.Rest
{
    public interface IControlFactory
    {
        Control CreateControl(IControlModel controlModel);
        List<Control> CreateControls(UiConfigurationCategory uiConfigurationCategory);
    }
}
