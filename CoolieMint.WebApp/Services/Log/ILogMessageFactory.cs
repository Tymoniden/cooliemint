using System;
using WebControlCenter.Services.Log.Sink;

namespace WebControlCenter.Services.Log
{
    public interface ILogMessageFactory
    {
        LogMessage CreateLogMessage(LogSeverity severity, Exception exception, DateTime datetime);
        LogMessage CreateLogMessage(LogSeverity severity, string message, DateTime datetime);
    }
}