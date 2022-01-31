using System;
using System.Collections.Generic;
using WebControlCenter.Services.Log.Sink;

namespace WebControlCenter.Services.Log
{
    public interface ILogService
    {
        void Log(LogSeverity severity, string message);
        void LogDebug(string message);
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
        void LogException(Exception exception);
        void LogException(Exception exception, string message);
        void RegisterSink(IlogSink logSink);
        void RegisterSinks(List<IlogSink> logSinks);
    }
}