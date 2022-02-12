using System;

namespace CoolieMint.WebApp.Services.Storage
{
    public sealed class SwitchState : IStateEntryValue
    {
        public bool IsOn { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is bool state)
            {
                return state == IsOn;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool HasChanged(IStateEntryValue value)
        {
            if(value is SwitchState switchState)
            {
                return switchState.IsOn != IsOn;
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
