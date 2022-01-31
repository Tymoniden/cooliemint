using System;
using System.Collections.Generic;
using WebControlCenter.Models;

namespace WebControlCenter.Services.Rest
{
    public class PageFactory : IPageFactory
    {
        private readonly IControlFactory _controlFactory;

        public PageFactory(IControlFactory controlFactory)
        {
            _controlFactory = controlFactory ?? throw new ArgumentNullException(nameof(controlFactory));
        }

        public List<Page> CreatePages(UiConfigurationCategory[] uiConfigurationCategories)
        {
            var pages = new List<Page>();
            foreach (var category in uiConfigurationCategories)
            {
                pages.Add(CreatePage(category));
            }

            return pages;
        }

        public Page CreatePage(UiConfigurationCategory uiConfigurationCategory)
        {
            return new Page
            {
                Id = uiConfigurationCategory.Id,
                Name = uiConfigurationCategory.Name,
                Icon = uiConfigurationCategory.Symbol,
                StateId = Guid.Empty,
                Controls = _controlFactory.CreateControls(uiConfigurationCategory)
            };
        }
    }
}
