using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationStarter.Services
{
    public interface IProcessService 
    {
        bool IsProcessStarted(string path);

        void StartProcess(string path, CancellationToken cancellationToken);
    }

    public class ProcessService : IProcessService
    {
        readonly IFileSystemService _fileSystemService;

        public ProcessService(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService ?? throw new ArgumentNullException(nameof(fileSystemService));
        }

        public bool IsProcessStarted(string path)
        {
            foreach(var process in Process.GetProcesses().OrderByDescending(p => p.Id))
            {
                try
                {
                    if (process.MainModule.FileName == path)
                    {
                        return true;
                    }
                }
                catch { }
            }

            return false;
        }

        public void StartProcess(string path, CancellationToken cancellationToken)
        {
            Console.WriteLine("starting process");

            var process = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true,
                LoadUserProfile = true
            };
            Task.Run(() => 
            {
                var p = Process.Start(process);
                p.WaitForExit();
            }, cancellationToken);
        }
    }
}
