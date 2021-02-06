using System;
using WebControlCenter.Services.Log.Sink;

namespace WebControlCenter.Services.Log
{
    public class LogMessageFactory : ILogMessageFactory
    {
        public LogMessage CreateLogMessage(LogSeverity severity, string message, DateTime dateTime)
        {
            return new LogMessage()
            {
                Severity = severity,
                Message = message,
                Timestamp = dateTime
            };
        }

        public LogMessage CreateLogMessage(LogSeverity severity, Exception exception, DateTime dateTime)
        {
            return new LogMessage()
            {
                Severity = severity,
                Exception = exception,
                Timestamp = dateTime
            };
        }
    }
}
