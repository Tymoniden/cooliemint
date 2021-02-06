namespace WebControlCenter.Services
{
    public interface IConverterService
    {
        T ConvertValue<T>(object value);
    }
}