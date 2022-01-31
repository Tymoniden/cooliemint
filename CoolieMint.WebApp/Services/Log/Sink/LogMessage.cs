using System;

namespace WebControlCenter.Services.Log.Sink
{
    public class LogMessage
    {
        public LogSeverity Severity { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
