using System;
using System.IO;

namespace ApplicationStarter.Services.Logging.LogSinks
{
    public class FileSink : ILogSink
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly string _logDirectory;

        public FileSink(IFileSystemService fileSystemService, string logDirectory)
        {
            _fileSystemService = fileSystemService ?? throw new System.ArgumentNullException(nameof(fileSystemService));
            _logDirectory = logDirectory;
            _fileSystemService.CreateDirectory(logDirectory);
        }

        public void Log(string message, LogSeverity severity)
        {
            var logFile = Path.Combine(_logDirectory, $"{DateTime.UtcNow.ToString("d")}.txt");
            var logMessage = GenerateLogMessage(message, severity);
            _fileSystemService.SaveString(logMessage, logFile);
        }

        string GenerateLogMessage(string message, LogSeverity severity)
        {
            return $"[{DateTime.UtcNow.ToString("G")}] {AbbreviateLogSeverity(severity)} {message}";
        }

        string AbbreviateLogSeverity(LogSeverity severity)
        {
            switch (severity)
            {
                case LogSeverity.Error:
                    return "E";
                case LogSeverity.Warning:
                    return "W";
                case LogSeverity.Info:
                    return "I";
                case LogSeverity.Verbose:
                    return "V";
                default:
                    throw new NotSupportedException(nameof(severity));
            }
        }
    }
}
