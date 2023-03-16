namespace CoolieMint.WebApp.Services.FileSystem
{
    public class FileNameProvider : IFileNameProvider
    {
        readonly IDirectoryProvider _directoryProvider;
        readonly IFileSystemService _fileSystemService;

        public FileNameProvider(IDirectoryProvider directoryProvider, IFileSystemService fileSystemService)
        {
            _directoryProvider = directoryProvider ?? throw new System.ArgumentNullException(nameof(directoryProvider));
            _fileSystemService = fileSystemService ?? throw new System.ArgumentNullException(nameof(fileSystemService));
        }

        public string GetCustomCommandFile() => _fileSystemService.CombinePath(_directoryProvider.GetConfiguration(), FileNames.CustomCommand);
        public string GetCustomCommandLegacyFile() => _fileSystemService.CombinePath(_directoryProvider.GetConfiguration(), FileNames.CustomCommandsFile);

        public string GetMigrationFile() => _fileSystemService.CombinePath(_directoryProvider.GetContentRoot(), FileNames.Migration);

        public string GetScenesConfigFile() => _fileSystemService.CombinePath(_directoryProvider.GetConfiguration(), FileNames.ScenesConfig);
    }
}
