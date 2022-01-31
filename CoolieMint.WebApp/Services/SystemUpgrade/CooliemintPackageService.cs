using CoolieMint.WebApp.Services.FileSystem;
using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using WebControlCenter.Services.Log;

namespace CoolieMint.WebApp.Services.SystemUpgrade
{
    public class CooliemintPackageService : ICooliemintPackageService
    {
        private readonly IDirectoryProvider _directoryProvider;
        private readonly ICompressionService _compressionService;
        private readonly ILogService _logService;

        public CooliemintPackageService(IDirectoryProvider directoryProvider, ICompressionService compressionService, ILogService logService)
        {
            _directoryProvider = directoryProvider ?? throw new ArgumentNullException(nameof(directoryProvider));
            _compressionService = compressionService ?? throw new ArgumentNullException(nameof(compressionService));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public bool TryInspectVersion(byte[] package, out Version version)
        {
            version = null;
            try
            {
                using (var memoryStream = new MemoryStream(package))
                {
                    var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read);
                    var entry = archive.GetEntry("CoolieMint.WebApp.dll");

                    var targetFile = Path.Combine(Path.GetTempPath(), entry.Name);
                    if (File.Exists(targetFile))
                    {
                        File.Delete(targetFile);
                    }

                    entry.ExtractToFile(targetFile);
                    var assemblyName = AssemblyName.GetAssemblyName(targetFile);

                    if (assemblyName.Version != null)
                    {
                        version = assemblyName.Version;
                        return true;
                    }
                }
            }
            catch (Exception exception)
            {
                _logService.LogException(exception, "Something went wrong extracting the version of package");
            }

            return false;
        }

        public void SavePackage(byte[] content)
        {
            if (TryInspectVersion(content, out Version version))
            {
                var destinationFolder = _directoryProvider.RelativeContentRoot("..", $"CoolieMint_{version}");

                using (var memoryStream = new MemoryStream(content))
                {
                    _compressionService.DecompressFile(memoryStream, destinationFolder);
                }

                return;
            }

            throw new ArgumentNullException(nameof(content));
        }
    }
}
