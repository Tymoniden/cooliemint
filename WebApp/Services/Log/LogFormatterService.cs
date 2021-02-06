using System;

namespace WebControlCenter.Services.Log
{
    public class LogFormatterService : ILogFormatterService
    {
        public string FormatMessage(LogSeverity severity, string message, DateTime datetime)
        {
            var severityIndex = GetSeverityIndex(severity);

            return $"[{datetime.ToString("yyyy-MM-dd HH:mm:ss")}] {severityIndex} {message}";
        }

        public string GetSeverityIndex(LogSeverity severity)
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
