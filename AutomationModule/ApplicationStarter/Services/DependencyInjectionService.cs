using ApplicationStarter.Services.Logging;
using ApplicationStarter.Services.Logging.LogSinks;
using SimpleInjector;
using System.Collections.Generic;

namespace ApplicationStarter.Services
{
    class DependencyInjectionService
    {
        public static Container SetupContainer()
        {
            var container = new Container();
            container.RegisterSingleton<ILoggingService, LoggingService>();
            container.RegisterSingleton<IEncodingService, EncodingService>();
            container.RegisterSingleton<IJsonSerializerService, JsonSerializerService>();
            container.RegisterSingleton<ISerializerService, SerializerService>();
            container.RegisterSingleton<IFileSystemService, FileSystemService>();
            container.RegisterSingleton<IConfigurationService, ConfigurationService>();

            container.RegisterSingleton<IZipService, ZipService>();
            container.RegisterSingleton<ICompressionService, CompressionService>();
            container.RegisterSingleton<IHttpService, HttpService>();
            container.RegisterSingleton<IGitHubService, GitHubService>();
            container.RegisterSingleton<IProcessService, ProcessService>();
            container.RegisterSingleton<IProcessWatcherService, ProcessWatcherService>();

            container.RegisterSingleton<IVersionService, VersionService>();
            container.RegisterSingleton<IUpdateService, UpdateService>();

            

            container.Verify();

            return container;
        }

        public static void ConfigureLoggingService(ILoggingService loggingService, IFileSystemService fileSystemService, Settings settings)
        {
            if (loggingService is null) { throw new System.ArgumentNullException(nameof(loggingService)); }
            if (fileSystemService is null) { throw new System.ArgumentNullException(nameof(fileSystemService)); }

            loggingService.AddLogSink(new FileSink(fileSystemService, settings.LogPath));
        }
    }
}
