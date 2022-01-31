using System.Collections.Generic;
using WebControlCenter.Models;

namespace WebControlCenter.Services.Rest
{
    public interface IControlFactory
    {
        Models.Control CreateControl(IControlModel controlModel);
        List<Models.Control> CreateControls(UiConfigurationCategory uiConfigurationCategory);
    }
}
