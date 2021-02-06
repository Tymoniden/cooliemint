using System;

namespace WebControlCenter.Services.Log.Sink
{
    public interface IlogSink
    {
        void Log(LogSeverity severity, string message, DateTime datetime);

        void LogException(Exception exception, DateTime dateTime);

        void LogException(Exception exception, string message, DateTime dateTime);
    }
}
