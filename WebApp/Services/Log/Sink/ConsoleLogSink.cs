using System;

namespace WebControlCenter.Services.Log.Sink
{
    public class ConsoleLogSink : IlogSink
    {
        public void Log(LogSeverity severity, string message, DateTime datetime)
        {
            WriteOutput(severity, FormatMessage(severity, message, datetime));
        }

        public void LogException(Exception exception, DateTime datetime)
        {
            WriteOutput(LogSeverity.Error, FormatMessage(LogSeverity.Error, exception.Message, datetime));
        }

        public void LogException(Exception exception, string message, DateTime datetime)
        {
            WriteOutput(LogSeverity.Error, FormatMessage(LogSeverity.Error, message, datetime));
            WriteOutput(LogSeverity.Error, FormatMessage(LogSeverity.Error, exception.Message, datetime));
        }

        void WriteOutput(LogSeverity severity, string message)
        {
            var foreGround = Console.ForegroundColor;

            Console.ForegroundColor = GetSeverityColor(severity);
            Console.WriteLine(message);
            Console.ForegroundColor = foreGround;
        }

        ConsoleColor GetSeverityColor(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Debug:
                    return ConsoleColor.DarkGray;

                case LogSeverity.Info:
                    return ConsoleColor.Gray;
                    
                case LogSeverity.Warning:
                    return ConsoleColor.DarkYellow;

                case LogSeverity.Error:
                    return ConsoleColor.Red;

                default:
                    throw new ArgumentException($"case for argument {nameof(severity)} not known");
            }
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
