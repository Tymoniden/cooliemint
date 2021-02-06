using System;
using System.Diagnostics;

namespace WebControlCenter.Services.Log.Sink
{
    public class DebugLogSink : IlogSink
    {
        public void Log(LogSeverity severity, string message, DateTime datetime)
        {
            WriteOutput(FormatMessage(severity, message, DateTime.UtcNow));
        }

        public void LogException(Exception exception, DateTime datetime)
        {
            WriteOutput(FormatMessage(LogSeverity.Error, exception.ToString(), DateTime.UtcNow));
        }

        public void LogException(Exception exception,string message, DateTime datetime)
        {
            WriteOutput(FormatMessage(LogSeverity.Error, message, DateTime.UtcNow));
            WriteOutput(FormatMessage(LogSeverity.Error, exception.ToString(), DateTime.UtcNow));
        }

        void WriteOutput(string message)
        {
            Debug.WriteLine(message);
        }


        string FormatMessage(LogSeverity severity, string message, DateTime datetime)
        {
            var severityIndex = GetSeverityIndex(severity);

            return $"[{datetime.ToString("yyyy-MM-dd HH:mm:ss")}] {severityIndex} {message}";
        }

        string GetSeverityIndex(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Debug:
                    return "D";

                case LogSeverity.Info:
                    return "I";

                case LogSeverity.Warning:
                    return "W";

                case LogSeverity.Error:
                    return "E";

                default:
                    throw new ArgumentException($"case for argument {nameof(severity)} not known");
            }
        }
    }
}
