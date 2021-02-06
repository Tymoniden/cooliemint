using System;

namespace WebControlCenter.Services
{
    public class ConverterService : IConverterService
    {
        public T ConvertValue<T>(object value)
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
