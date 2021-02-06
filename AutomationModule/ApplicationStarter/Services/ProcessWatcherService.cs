using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading;

namespace ApplicationStarter.Services
{
    public class ProcessWatcherService : IProcessWatcherService
    {
        private readonly IProcessService _processService;
        private List<string> _filenames;

        public ProcessWatcherService(IProcessService processService)
        {
            _processService = processService ?? throw new ArgumentNullException(nameof(processService));
        }
        public void StartWatchingProcesses(List<string> filenames, CancellationToken cancellationToken)
        {
            _filenames = filenames;
            while (true)
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Process watcher stopped!");
                        break;
                    }
                    WatchProcess(cancellationToken);
                    Thread.Sleep(5000);
                }
                catch(OperationCanceledException)
                {
                    Console.WriteLine("Process watcher stopped!");
                    break;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Exception happened");
                    Console.WriteLine(e.ToString());
                    break;
                }
            }
        }

        void WatchProcess(CancellationToken cancellationToken)
        {
            foreach (var filename in _filenames)
            {
                Console.Write($"Process {filename} was ");

                if (!_processService.IsProcessStarted(filename))
                {
                    Console.WriteLine("not started");
                    _processService.StartProcess(filename, cancellationToken);
                }
                else
                {
                    Console.WriteLine("started");
                }
            }
        }
    }
}
