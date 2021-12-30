using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoolieMint.WindowsClient.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        Dictionary<string, object> _values = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;

        protected T GetValue<T>([CallerMemberName] string callerMemberName = null)
        {
            if (callerMemberName is null)
            {
                throw new ArgumentNullException(nameof(callerMemberName));
            }

            if (!_values.ContainsKey(callerMemberName))
            {
                return default(T);
            }

            return (T)_values[callerMemberName];
        }

        protected void SetValue<T>(T value, [CallerMemberName] string callerMemberName = null)
        {
            if(callerMemberName == null)
            {
                throw new ArgumentNullException(nameof(callerMemberName));
            }

            lock (_values)
            {
                if(_values.ContainsKey(callerMemberName) && _values[callerMemberName]?.Equals(value) == true)
                {
                    return;
                }

                _values[callerMemberName] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(callerMemberName));
            }
        }
    }
}
