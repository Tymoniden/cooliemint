﻿using CoolieMint.WebApp.Services.Automation.Rule.Conditions;

namespace CoolieMint.WebApp.Dtos.Automation
{
    public class ValueStoreConditionDto : IConditionDto
    {
        public ConditionDtoType ConditionType { get; set; }
        public bool IsInverted { get; set; }
    }
}