using CoolieMint.WebApp.Services.FileSystem;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebControlCenter.Services.Log;

namespace CoolieMint.WebApp.Services.SystemUpgrade.Migration
{
    public class ConfigurationMigrationService : IConfigurationMigrationService
    {
        readonly IDirectoryProvider _directoryProvider;
        readonly IFileSystemService _fileSystemService;
        readonly IFileNameProvider _fileNameProvider;
        readonly ILogService _logService;

        public ConfigurationMigrationService(IDirectoryProvider directoryProvider, IFileSystemService fileSystemService, IFileNameProvider fileNameProvider, ILogService logService)
        {
            _directoryProvider = directoryProvider ?? throw new ArgumentNullException(nameof(directoryProvider));
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
            _fileNameProvider = fileNameProvider ?? throw new ArgumentNullException(nameof(fileNameProvider));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public Task<bool> Migrate()
        {
            try
            {
                if (!IsMigrationNecessary())
                {
                    _logService.LogInfo("[ConfigurationMigrationService] No migration is needed.");
                    return Task.FromResult(false);
                }

                if (!TryGetPredecessorFolder(out var predecessorFolder))
                {
                    _logService.LogInfo("[ConfigurationMigrationService] Could not find predecessor folder.");
                    WriteMigrationFile(predecessorFolder);
                    return Task.FromResult(false);
                }

                _logService.LogInfo("[ConfigurationMigrationService] Migrating configuration folder.");
                if (!MigrateConfigurationFolder(predecessorFolder, _directoryProvider.GetContentRoot()))
                {
                    return Task.FromResult(false);
                }

                _logService.LogInfo("[ConfigurationMigrationService] Migrating settings file.");
                if (!MigrateSettingsFile(predecessorFolder, _directoryProvider.GetContentRoot()))
                {
                    return Task.FromResult(false);
                }

                _logService.LogInfo("[ConfigurationMigrationService] Writing migration file.");
                WriteMigrationFile(predecessorFolder);
            }
            catch (Exception ex)
            {
                _logService.LogException(ex, "[ConfigurationMigrationService] Error during migration process.");
            }

            return Task.FromResult(true);
        }

        private void WriteMigrationFile(string predecessorFolder)
        {
            var path = _fileNameProvider.GetMigrationFile();
            _fileSystemService.WriteAllText(path, predecessorFolder);
            
        }

        private bool IsMigrationNecessary()
        {
            var path = _fileNameProvider.GetMigrationFile();
            if (_fileSystemService.FileExists(path))
            {
                return false;
            }

            return true;
        }

        bool TryGetPredecessorFolder(out string predecessorFolder)
        {
            predecessorFolder = null;
            var currentApplicationFolder = _directoryProvider.GetContentRoot();
            var parentFolder = _directoryProvider.GetParentSystemFolder();

            var directories = Directory
                .GetDirectories(parentFolder)
                .OrderByDescending(d => d)
                .Select(d => new DirectoryInfo(d))
                .Where(d => d.Name.StartsWith("CoolieMint"));

            // The parent folder only contains the current version.
            if (directories.Count() < 2)
            {
                return false;
            }

            // something went wrong. The folder structure is not as expected, therefor we don't continue.
            if (directories.First().FullName != currentApplicationFolder)
            {
                return false;
            }

            predecessorFolder = directories.Skip(1).First().FullName;
            return true;
        }

        bool MigrateConfigurationFolder(string predecessorApplicationFolder, string currentApplicationFolder)
        {
            var predecessorConfigurationFolder = _fileSystemService.CombinePath(predecessorApplicationFolder, FileNames.ConfigurationFolder);
            var currentConfigurationFolder = _fileSystemService.CombinePath(currentApplicationFolder, FileNames.ConfigurationFolder);

            if (_fileSystemService.TryCopyFolder(predecessorConfigurationFolder, currentConfigurationFolder))
            {
                return true;
            }

            return false;
        }

        bool MigrateSettingsFile(string predecessorApplicationFolder, string currentApplicationFolder)
        {
            var sourceSettingsFile = _fileSystemService.CombinePath(predecessorApplicationFolder, FileNames.SettingsFiles);
            var destinationSettingsFile = _fileSystemService.CombinePath(currentApplicationFolder, FileNames.SettingsFiles);

            try
            {
                _fileSystemService.CopyFile(sourceSettingsFile, destinationSettingsFile, true);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
    }
}
