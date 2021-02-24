using System;
using System.Collections.Generic;
using WebControlCenter.Models;

namespace WebControlCenter.Services.Rest
{
    public class ControlFactory : IControlFactory
    {
        private readonly IControlModelService _controlModelService;

        public ControlFactory(IControlModelService controlModelService)
        {
            _controlModelService = controlModelService ?? throw new ArgumentNullException(nameof(controlModelService));
        }

        public List<Control> CreateControls(UiConfigurationCategory uiConfigurationCategory)
        {
            var controls = new List<Control>();
            foreach (var controlModel in uiConfigurationCategory.ControlModels)
            {
                var convertedControlModel = _controlModelService.Convert(controlModel);
                if (convertedControlModel == null)
                {
                    continue;
                }

                var control = CreateControl(convertedControlModel);
                controls.Add(control);
            }
            return controls;
        }

        public Control CreateControl(IControlModel controlModel)
        {
            return new Control
            {
                Id = controlModel.Id,
                Name = controlModel.Title,
                StateId = Guid.Empty
            };
        }
    }
}
