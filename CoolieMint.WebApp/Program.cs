using CoolieMint.WebApp.Services.Automation;
using CoolieMint.WebApp.Services.CustomCommand;
using CoolieMint.WebApp.Services.Mqtt;
using CoolieMint.WebApp.Services.Notification.Pushover;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WebControlCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch(OperationCanceledException)
            {
                Environment.Exit(0);
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseUrls("http://*:7777")
                        .UseKestrel(options =>
                        {
                            options.Limits.MaxRequestBodySize = 100000000;
                        })
                        .UseStartup<Startup>();
                })            
                .ConfigureServices(services =>
                {
                    services.AddHttpClient<PushoverHttpClient>();
                })
                .ConfigureServices(services =>
                {
                    services.AddHostedService<CommandExecutionManager>();
                    services.AddHostedService<MqttConnectionProviderService>();
                    services.AddHostedService<AutomationService>();
                    services.AddHostedService<AutomationRulesWorker>();
                });
    }
}