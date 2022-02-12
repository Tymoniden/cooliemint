using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using WebControlCenter.Services.Log;

namespace CoolieMint.WebApp.Services.Automation
{
    public class AutomationRulesWorker : IHostedService
    {
        private readonly IAutomationRulesStore _automationRulesStore;
        private readonly IAutomationEntryQueueService _automationEntryQueueService;
        private readonly IAutomationRulesConditionValidator _automationRulesConditionValidator;
        private readonly IAutomationEntryFactory _automationEntryFactory;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly ILogService _logService;

        public AutomationRulesWorker(IAutomationRulesStore automationRulesStore, 
            IAutomationEntryQueueService automationEntryQueueService, 
            IAutomationRulesConditionValidator automationRulesConditionValidator,
            IAutomationEntryFactory automationEntryFactory,
            IDateTimeProvider dateTimeProvider,
            ILogService logService)
        {
            _automationRulesStore = automationRulesStore ?? throw new System.ArgumentNullException(nameof(automationRulesStore));
            _automationEntryQueueService = automationEntryQueueService ?? throw new System.ArgumentNullException(nameof(automationEntryQueueService));
            _automationRulesConditionValidator = automationRulesConditionValidator ?? throw new System.ArgumentNullException(nameof(automationRulesConditionValidator));
            _automationEntryFactory = automationEntryFactory ?? throw new System.ArgumentNullException(nameof(automationEntryFactory));
            _dateTimeProvider = dateTimeProvider ?? throw new System.ArgumentNullException(nameof(dateTimeProvider));
            _logService = logService ?? throw new System.ArgumentNullException(nameof(logService));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => DoWork(cancellationToken), cancellationToken);

            return Task.CompletedTask;
        }

        void DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var rule in _automationRulesStore.GetRules())
                {
                    var wasExecuted = false;
                    if (_automationRulesConditionValidator.CanExecute(rule.Condition))
                    {
                        _logService.LogInfo($"Executing Rule: {rule.DisplayName}");
                        foreach(var action in rule.OnTrue)
                        {
                            _automationEntryQueueService.PushAutomationEntry(_automationEntryFactory.CreateAutomationEntry(action));
                            wasExecuted = true;
                        }
                    }
                    else
                    {
                        foreach(var action in rule.OnFalse)
                        {
                            _automationEntryQueueService.PushAutomationEntry(_automationEntryFactory.CreateAutomationEntry(action));
                            wasExecuted = true;
                        }
                    }

                    if (wasExecuted)
                    {
                        rule.NextExecution = _dateTimeProvider.Now().AddSeconds(10);
                    }
                }

                Thread.Sleep(10);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
