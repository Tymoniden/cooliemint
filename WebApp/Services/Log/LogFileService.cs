using System;
using System.Linq;
using System.Text;
using WebControlCenter.Services.FileSystem;

namespace WebControlCenter.Services.Log
{
    public class LogFileService : ILogFileService
    {
        private readonly IFileSystemService _fileSystemService;
        private readonly ISettingsService _settingsService;
        private readonly IFolderService _folderService;
        private string _logDirectory = string.Empty;
        private FolderContentStrategy _folderContentStrategyBySize;
        private FolderContentStrategy _folderContentStrategyByCount;
        private string _lastLogFile = string.Empty;

        public LogFileService(IFileSystemService fileSystemService, ISettingsService settingsService, IFolderService folderService)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _settingsService = settingsService ?? throw new ArgumentNullException(nameof(settingsService));
            _folderService = folderService ?? throw new ArgumentNullException(nameof(folderService));
            _logDirectory = _settingsService.GetValue<string>("LogDirectory");

            if (!_fileSystemService.DirectoryExists(_logDirectory))
            {
                try
                {
                    _fileSystemService.CreateDirectory(_logDirectory);
                }
                catch (Exception e)
                {
                    throw new ArgumentException($"Could not create logpath from LogDirectory setting: \"{_logDirectory}\"", e);
                }
            }

            _folderContentStrategyBySize = new FolderContentStrategy
            {
                Path = _logDirectory,
                DetectionStrategy = DetectionStrategy.BySize,
                CleanUpStrategy = CleanUpStrategy.ByDateAsc,
                Size = 1024 * 1024 * 20
            };

            _folderContentStrategyByCount = new FolderContentStrategy
            {
                Path = _logDirectory,
                DetectionStrategy = DetectionStrategy.ByCount,
                CleanUpStrategy = CleanUpStrategy.ByDateAsc,
                Count = 10
            };
        }

        public string GetLogFileName()
        {
            var logfile = _fileSystemService.CombinePath(_logDirectory, $"{DateTime.UtcNow.ToString("yyyy-MM-dd")}.txt");

            if(_lastLogFile != logfile)
            {
                CheckLogFolder();
            }

            _lastLogFile = logfile;
            return logfile;
        }

        public string GetLogs()
        {
            if (!_fileSystemService.DirectoryExists(_logDirectory))
            {
                return string.Empty;
            }

            var files = _fileSystemService.GetFilesInFolder(_logDirectory).OrderBy(f => f.Name);
            var logContent = new StringBuilder();

            foreach(var file in files)
            {
                logContent.Append(_fileSystemService.ReadFileAsString(file.FullName));
            }

            return logContent.ToString();
        }

        void CheckLogFolder()
        {
            // dont' delete single file even though it's possible to exceed the maximum size of the log folder
            if (_fileSystemService.ReadFilesInFolder(_logDirectory).Length == 1)
            {
                return;
            }

            _folderService.EnsureFolderContent(_folderContentStrategyBySize);
            _folderService.EnsureFolderContent(_folderContentStrategyByCount);
        }
    }
}
