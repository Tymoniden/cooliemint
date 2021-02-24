using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebControlCenter.Services;
using WebControlCenter.Services.Log;

namespace WebControlCenter.Controllers
{
    [Route("[controller]/v1")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IAdapterService _adapterService;
        private readonly IAdapterSettingService _adapterSettingService;
        private readonly ILogService _logService;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public ApiController(IAdapterService adapterService, IAdapterSettingService adapterSettingService, ILogService logService, IHostApplicationLifetime hostApplicationLifetime)
        {
            _adapterService = adapterService;
            _adapterSettingService = adapterSettingService;
            _logService = logService;
            _hostApplicationLifetime = hostApplicationLifetime;
        }

        [HttpPost("SendControlMessage", Name = "SendControlMessage")]
        public bool SendControlMessage([FromBody] CommandAdapterMessage message)
        {
            _adapterService.SendMessage(message);

            return true;
        }

        [HttpGet("Status", Name = "GetStatus")]
        public async Task<JsonResult> GetStatus(long ticks, string adapterSetupId)
        {
            var timestamp = new DateTime(ticks);
            var adapters = _adapterSettingService.GetAdapters(adapterSetupId);
            var tokenSource = new CancellationTokenSource(new TimeSpan(0, 1, 0));

            var contracts = await Task.Run(() =>
                {
                    while (!tokenSource.IsCancellationRequested)
                    {
                        var messages = _adapterService.GetUpdates(timestamp, adapters);
                        if (messages.Any())
                        {
                            return messages;
                        }

                        Task.Delay(50);
                    }

                    return new List<AdapterStatusMessage>();

                }, tokenSource.Token
            );

            if (tokenSource.IsCancellationRequested)
            {
                return new JsonResult(new { context = "Exception", value = "timeout" });
            }

            var returnValue = new
            {
                context = "success",
                timestamp = contracts.Any() 
                    ? contracts.OrderByDescending(contract => contract.Timestamp).First().Timestamp.Ticks.ToString() 
                    : timestamp.Ticks.ToString(), 
                result = contracts.ToArray()
            };

            return new JsonResult(returnValue);
        }

        [HttpGet(nameof(Shutdown))]
        public void Shutdown()
        {
            _hostApplicationLifetime.StopApplication();
        }
    }

    public class CommandAdapterMessage
    {
        public string Adapter { get; set; }

        public string Id { get; set; }

        // JSON
        public string Payload { get; set; }
    }
}