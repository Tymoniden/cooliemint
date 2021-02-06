namespace WebControlCenter.Services.Log
{
    public interface ILogFileService
    {
        string GetLogFileName();
        string GetLogs();
    }
}