using CoolieMint.WebApp.Services.Notification.Pushover;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CoolieMint.WebApp.Services.CustomCommand
{
    public sealed class CommandExecutionManager : ICommandExecutionManager, IHostedService, IDisposable
    {
        private Timer _timer = null;
        private readonly ICommandExecutionQueueService _commandExecutionQueueService;
        private readonly ICustomCommandService _customCommandService;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public CommandExecutionManager(ICommandExecutionQueueService commandExecutionQueueService, ICustomCommandService customCommandService)
        {
            _commandExecutionQueueService = commandExecutionQueueService ?? throw new ArgumentNullException(nameof(commandExecutionQueueService));
            _customCommandService = customCommandService ?? throw new ArgumentNullException(nameof(customCommandService));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(20));
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _timer?.Change(Timeout.Infinite, 0);
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
            _timer?.Dispose();
        }

        private void DoWork(object state)
        {
            if (!_commandExecutionQueueService.HasExecutableItem())
            {
                return;
            }

            var commandExecutionDto = _commandExecutionQueueService.Dequeue();
            
            Task.Run(async () => await _customCommandService.ExecuteCommand(commandExecutionDto.Command.Id, _cancellationTokenSource.Token), _cancellationTokenSource.Token);
        }
    }
}
