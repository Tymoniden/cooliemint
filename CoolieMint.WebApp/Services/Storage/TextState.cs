using System;

namespace CoolieMint.WebApp.Services.Storage
{
    public class TextState : IStateEntryValue
    {
        public string Text { get; set; }

        public bool HasChanged(IStateEntryValue value)
        {
            if(value is TextState textState)
            {
                return Text.Equals(textState.Text);
            }

            throw new ArgumentException(nameof(value));
        }
    }
}
