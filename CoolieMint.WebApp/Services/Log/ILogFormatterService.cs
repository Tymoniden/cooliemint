using System;

namespace WebControlCenter.Services.Log
{
    public interface ILogFormatterService
    {
        string FormatMessage(LogSeverity severity, string message, DateTime datetime);
        string GetSeverityIndex(LogSeverity severity);
    }
}