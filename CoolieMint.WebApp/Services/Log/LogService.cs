using System;
using System.Collections.Generic;
using WebControlCenter.Services.Log.Sink;

namespace WebControlCenter.Services.Log
{
    public class LogService : ILogService
    {
        List<IlogSink> _sinks = new List<IlogSink>();

        public void Log(LogSeverity severity, string message)
        {
            foreach (var sink in _sinks)
            {
                sink.Log(severity, message, DateTime.UtcNow);
            }
        }

        public void LogDebug(string message)
        {
            foreach (var sink in _sinks)
            {
                sink.Log(LogSeverity.Debug, message, DateTime.UtcNow);
            }
        }

        public void LogInfo(string message)
        {
            foreach (var sink in _sinks)
            {
                sink.Log(LogSeverity.Info, message, DateTime.UtcNow);
            }
        }

        public void LogWarning(string message)
        {
            foreach (var sink in _sinks)
            {
                sink.Log(LogSeverity.Warning, message, DateTime.UtcNow);
            }
        }

        public void LogError(string message)
        {
            foreach (var sink in _sinks)
            {
                sink.Log(LogSeverity.Error, message, DateTime.UtcNow);
            }
        }

        public void LogException(Exception exception)
        {
            foreach (var sink in _sinks)
            {
                sink.LogException(exception, DateTime.UtcNow);
            }
        }

        public void LogException(Exception exception, string message)
        {
            foreach (var sink in _sinks)
            {
                sink.LogException(exception, message, DateTime.UtcNow);
            }
        }

        public void RegisterSink(IlogSink logSink)
        {
            _sinks.Add(logSink);
        }

        public void RegisterSinks(List<IlogSink> logSinks)
        {
            _sinks.AddRange(logSinks);
        }
    }
}
