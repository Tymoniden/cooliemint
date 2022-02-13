using CoolieMint.WebApp.Dtos.Automation;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.Temperature;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.Time;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.ValueStore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoolieMint.WebApp.Services.Automation.Factories
{
    public class AutomationConditionFactory : IAutomationConditionFactory
    {
        public ConditionContainerDto CreateConditionContainerDto(ConditionContainer conditionContainer)
        {
            return new ConditionContainerDto
            {
                ChainType = CreateChainTypeDto(conditionContainer.ChainType),
                Conditions = conditionContainer.Conditions.Select(condition => CreateCondition(condition)).ToList()
            };
        }

        public ChainTypeDto CreateChainTypeDto(ChainType chainType)
        {
            switch (chainType)
            {
                case ChainType.AND:
                    return ChainTypeDto.AND;
                case ChainType.OR:
                    return ChainTypeDto.OR;
                default:
                    throw new ArgumentException(nameof(chainType));
            }
        }

        public IConditionDto CreateCondition(ICondition condition)
        {
            if (condition is null)
            {
                throw new ArgumentNullException(nameof(condition));
            }

            if (condition is ValueStoreCondition valueStoreCondition)
            {
                return CreateValueStoreConditionDto(valueStoreCondition);
            }

            if (condition is TimeEarlierCondition timeEarlierCondition)
            {
                return CreateTimeEarlierConditionDto(timeEarlierCondition);
            }

            if (condition is TimeLaterCondition timeLaterCondition)
            {
                return CreateTimeLaterConditionDto(timeLaterCondition);
            }

            if (condition is TemperatureHigherCondition temperatureHigherCondition)
            {
                return CreateTemperatureHigherConditionDto(temperatureHigherCondition);
            }

            if (condition is TemperatureLowerCondition temperatureLowerCondition)
            {
                return CreateTemperatureLowerConditionDto(temperatureLowerCondition);
            }

            throw new ArgumentException(nameof(condition));

        }

        public ConditionDtoType CreateConditionDtoType(ConditionType conditionType)
        {
            switch (conditionType)
            {
                case ConditionType.ValueStore:
                    return ConditionDtoType.ValueStore;
                case ConditionType.Time:
                    return ConditionDtoType.Time;
                case ConditionType.Date:
                    return ConditionDtoType.Date;
                case ConditionType.Weekday:
                    return ConditionDtoType.Weekday;
                case ConditionType.Temperature:
                    return ConditionDtoType.Temperature;
                default:
                    throw new ArgumentOutOfRangeException(nameof(conditionType));
            }
        }

        public TemperatureLowerConditionDto CreateTemperatureLowerConditionDto(TemperatureLowerCondition temperaturLowerCondition)
        {
            return new TemperatureLowerConditionDto
            {
                ConditionType = CreateConditionDtoType(temperaturLowerCondition.ConditionType),
                IsInverted = temperaturLowerCondition.IsInverted
            };
        }

        public TemperatureHigherConditionDto CreateTemperatureHigherConditionDto(TemperatureHigherCondition temperatureHigherCondition)
        {
            return new TemperatureHigherConditionDto
            {
                ConditionType = CreateConditionDtoType(temperatureHigherCondition.ConditionType),
                IsInverted = temperatureHigherCondition.IsInverted
            };
        }

        public TimeEarlierConditionDto CreateTimeEarlierConditionDto(TimeEarlierCondition timeEarlierCondition)
        {
            return new TimeEarlierConditionDto
            {
                ConditionType = CreateConditionDtoType(timeEarlierCondition.ConditionType),
                IsInverted = timeEarlierCondition.IsInverted
            };
        }

        public TimeLaterConditionDto CreateTimeLaterConditionDto(TimeLaterCondition timeLaterCondition)
        {
            return new TimeLaterConditionDto
            {
                ConditionType = CreateConditionDtoType(timeLaterCondition.ConditionType),
                IsInverted = timeLaterCondition.IsInverted
            };
        }

        public ValueStoreConditionDto CreateValueStoreConditionDto(ValueStoreCondition valueStoreCondition)
        {
            return new ValueStoreConditionDto
            {
                ConditionType = CreateConditionDtoType(valueStoreCondition.ConditionType),
                IsInverted = valueStoreCondition.IsInverted
            };
        }
    }
}
