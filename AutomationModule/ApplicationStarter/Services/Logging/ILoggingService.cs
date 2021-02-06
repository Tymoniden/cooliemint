using ApplicationStarter.Services.Logging.LogSinks;

namespace ApplicationStarter.Services.Logging
{
    public interface ILoggingService
    {
        void AddLogSink(ILogSink logSink);
        void LogError(string message);
        void LogInfo(string message);
        void LogMessage(string message, LogSeverity severity);
        void LogVerbose(string message);
        void LogWarning(string message);
    }
}