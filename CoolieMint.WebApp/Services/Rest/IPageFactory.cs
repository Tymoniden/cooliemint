using System.Collections.Generic;
using WebControlCenter.Models;

namespace WebControlCenter.Services.Rest
{
    public interface IPageFactory
    {
        Page CreatePage(UiConfigurationCategory uiConfigurationCategory);
        List<Page> CreatePages(UiConfigurationCategory[] uiConfigurationCategories);
    }
}
