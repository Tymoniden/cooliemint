namespace ApplicationStarter.Services.Logging.LogSinks
{
    public interface ILogSink 
    {
        void Log(string message, LogSeverity severity);
    }
}
