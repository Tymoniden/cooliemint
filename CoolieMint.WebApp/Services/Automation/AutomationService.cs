using CoolieMint.WebApp.Services.Automation.ActionHandlerServices;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.Automation
{
    public class AutomationService : IHostedService
    {
        private readonly IAutomationEntryQueueService _automationEntryQueueService;
        private readonly IActionMapperService _actionMapperService;
        Queue<AutomationEntry> queue = new Queue<AutomationEntry>();
        
        public AutomationService(IAutomationEntryQueueService automationEntryQueueService, IActionMapperService actionMapperService)
        {
            _automationEntryQueueService = automationEntryQueueService ?? throw new ArgumentNullException(nameof(automationEntryQueueService));
            _actionMapperService = actionMapperService ?? throw new ArgumentNullException(nameof(actionMapperService));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => ExecuteQueue(cancellationToken), cancellationToken);
            Task.Run(() => FillQueue(cancellationToken), cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // persist action
            return Task.CompletedTask;
        }

        void ExecuteQueue(CancellationToken cancellationToken)
        {
            SpinWait spin = new SpinWait();
            while (!cancellationToken.IsCancellationRequested)
            {
                if (queue.Count > 0)
                {
                    AutomationEntry entry;
                    lock (queue)
                    {
                        entry = queue.Dequeue();
                    }

                    _actionMapperService.HandleAction(entry.Action);
                }

                spin.SpinOnce();
            }
        }

        void FillQueue(CancellationToken cancellationToken)
        {
            SpinWait spin = new SpinWait();
            while (!cancellationToken.IsCancellationRequested)
            {
                var entry = _automationEntryQueueService.PopAutomationEntry();
                if (entry != null)
                {
                    lock (queue)
                    {
                        queue.Enqueue(new AutomationEntry
                        {
                            Action = entry.Action,
                            NextExecutionTime = entry.NextExecutionTime
                        });
                    }
                }
                spin.SpinOnce();
            }
        }
    }
}
