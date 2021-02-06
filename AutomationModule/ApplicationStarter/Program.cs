using ApplicationStarter.Services;
using ApplicationStarter.Services.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationStarter
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = DependencyInjectionService.SetupContainer();
            var settings = container.GetInstance<IConfigurationService>().GetSettings();
            DependencyInjectionService.ConfigureLoggingService(container.GetInstance<ILoggingService>(), container.GetInstance<IFileSystemService>(), settings);

            var cts = new CancellationTokenSource();

            //Task.Run( () =>
            //{
            //    container.GetInstance<IProcessWatcherService>().StartWatchingProcesses(settings.ObservedApplications, cts.Token);
            //}, cts.Token);

            Task.Run(async () =>
            {
                await container.GetInstance<IUpdateService>().CheckLatestVersion();
            });


            cts.Cancel();
            cts.Dispose();

            Console.WriteLine("press any key to close");
            Console.ReadKey();
        }
    }
}
