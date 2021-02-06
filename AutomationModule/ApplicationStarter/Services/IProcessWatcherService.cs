using System.Collections.Generic;
using System.Threading;

namespace ApplicationStarter.Services
{
    public interface IProcessWatcherService
    {
        void StartWatchingProcesses(List<string> filenames, CancellationToken cancellationToken);
    }
}