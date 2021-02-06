using ApplicationStarter.Services.Logging.LogSinks;
using System.Collections.Generic;

namespace ApplicationStarter.Services.Logging
{
    public class LoggingService : ILoggingService
    {
        List<ILogSink> _logSink = new List<ILogSink>();

        public void AddLogSink(ILogSink logSink) => _logSink.Add(logSink);

        public void LogMessage(string message, LogSeverity severity)
        {
            foreach (var sink in _logSink)
            {
                sink.Log(message, severity);
            }
        }

        public void LogError(string message)
        {
            LogMessage(message, LogSeverity.Error);
        }

        public void LogWarning(string message)
        {
            LogMessage(message, LogSeverity.Warning);
        }

        public void LogInfo(string message)
        {
            LogMessage(message, LogSeverity.Info);
        }

        public void LogVerbose(string message)
        {
            LogMessage(message, LogSeverity.Verbose);
        }
    }
}
