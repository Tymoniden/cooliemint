using System;
using System.Collections.Generic;
using WebControlCenter.Services.Database;

namespace WebControlCenter.Services.Log.Sink
{
    public class MemoryLogSink : IlogSink
    {
        private readonly ILogMessageFactory _logMessageFactory;
        private readonly INotificationService _notificationService;
        Queue<LogMessage> _messages = new Queue<LogMessage>();
        int _maxNumMessages = 100;

        public MemoryLogSink(ILogMessageFactory logMessageFactory, INotificationService notificationService)
        {
            _logMessageFactory = logMessageFactory ?? throw new ArgumentNullException(nameof(logMessageFactory));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
        }

        public void Log(LogSeverity severity, string message, DateTime datetime)
        {
            AddNewMessage(_logMessageFactory.CreateLogMessage(severity, message, datetime));
        }

        public void LogException(Exception exception, DateTime datetime)
        {
            AddNewMessage(_logMessageFactory.CreateLogMessage(LogSeverity.Error, exception, datetime));
            _notificationService.AddNotification(exception, datetime);
        }

        public void LogException(Exception exception,string message, DateTime datetime)
        {
            var logMessage = _logMessageFactory.CreateLogMessage(LogSeverity.Error, exception, datetime);
            logMessage.Message = message;
            AddNewMessage(logMessage);
            _notificationService.AddNotification(exception, message, datetime);
        }

        public LogMessage[] GetLogMessages() => _messages.ToArray();

        void AddNewMessage(LogMessage message)
        {
            if(message.Severity != LogSeverity.Error && message.Severity != LogSeverity.Warning)
            {
                return;
            }

            if (_messages.Count > _maxNumMessages)
            {
                _messages.Dequeue();
            }

            _messages.Enqueue(message);
        }
    }
}
