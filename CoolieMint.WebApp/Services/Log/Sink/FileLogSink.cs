using CoolieMint.WebApp.Services.FileSystem;
using System;

namespace WebControlCenter.Services.Log.Sink
{
    public class FileLogSink : IlogSink
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly ILogFormatterService _logFormatterService;
        private readonly ILogFileService _logFileService;

        public FileLogSink(IFileSystemService fileSystemService, ILogFormatterService logFormatterService, ILogFileService logFileService)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _logFormatterService = logFormatterService ?? throw new ArgumentNullException(nameof(logFormatterService));
            _logFileService = logFileService ?? throw new ArgumentNullException(nameof(logFileService));
        }

        public void Log(LogSeverity severity, string message, DateTime datetime) => LogMessageToFile(_logFormatterService.FormatMessage(severity, message, datetime));

        public void LogException(Exception exception, DateTime dateTime) => LogMessageToFile(_logFormatterService.FormatMessage(LogSeverity.Error, exception.ToString(), dateTime));

        public void LogException(Exception exception, string message, DateTime dateTime) => LogMessageToFile(_logFormatterService.FormatMessage(LogSeverity.Error, $"{message}{Environment.NewLine}{exception}", dateTime));

        void LogMessageToFile(string message)
        {
            _fileSystemService.AppendAllText(_logFileService.GetLogFileName(), $"{message}{Environment.NewLine}");
        }
    }
}
