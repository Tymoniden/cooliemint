using CoolieMint.WebApp.Services;
using CoolieMint.WebApp.Services.Automation;
using CoolieMint.WebApp.Services.Automation.ActionHandlerServices;
using CoolieMint.WebApp.Services.Automation.Rule.Action;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.Temperature;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.Time;
using CoolieMint.WebApp.Services.Automation.Rule.Conditions.ValueStore;
using CoolieMint.WebApp.Services.CustomCommand;
using CoolieMint.WebApp.Services.FileSystem;
using CoolieMint.WebApp.Services.Http;
using CoolieMint.WebApp.Services.Mqtt;
using CoolieMint.WebApp.Services.Notification.Pushover;
using CoolieMint.WebApp.Services.Storage;
using CoolieMint.WebApp.Services.SystemUpgrade;
using CoolieMint.WebApp.Services.SystemUpgrade.Migration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Database.Repository;
using WebControlCenter.Database.Services;
using WebControlCenter.Repository;
using WebControlCenter.Services;
using WebControlCenter.Services.CustomCommand;
using WebControlCenter.Services.Database;
using WebControlCenter.Services.FileSystem;
using WebControlCenter.Services.Log;
using WebControlCenter.Services.Log.Sink;
using WebControlCenter.Services.Rest;
using WebControlCenter.Services.Setting;
using WebControlCenter.Services.SmartDevice;
using WebControlCenter.Services.SmartDevice.Sonoff;
using WebControlCenter.Services.Storage;

namespace WebControlCenter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSingleton<ISystemInteractionService, SystemInteractionService>();

            services.AddSingleton<IFolderService, FolderService>();
            services.AddSingleton<ILogFileService, LogFileService>();

            services.AddSingleton<ILogMessageFactory, LogMessageFactory>();
            services.AddSingleton<ILogFormatterService, LogFormatterService>();
            services.AddSingleton<ILogService, LogService>();

            services.AddSingleton<IJsonSerializerService, JsonSerializerService>();
            services.AddSingleton<IEncodingService, EncodingService>();
            services.AddSingleton<IConverterService, ConverterService>();
            services.AddSingleton<IControlModelService, ControlModelService>();

            services.AddSingleton<IMqttRepository, MqttRepository>();
            services.AddSingleton<IMqttValueProvider, MqttValueProvider>();
            services.AddSingleton<IMessageBroker, MessageBroker>();
            services.AddSingleton<IMessageBrokerMessageArgumentFactory, MessageBrokerMessageArgumentFactory>();

            services.AddSingleton<IMqttCommandAdapter, MqttCommandAdapter>();
            services.AddSingleton<IAdapterService, AdapterService>();
            services.AddSingleton<IAdapterSettingService, AdapterSettingService>();
            services.AddSingleton<IFileSystemService, FileSystemService>();
            services.AddSingleton<ICompressionService, CompressionService>();
            services.AddSingleton<IDirectoryProvider, DirectoryProvider>();
            services.AddSingleton<IUiConfigurationService, UiConfigurationService>();
            services.AddSingleton<IMqttAdapterFactory, MqttAdapterFactory>();
            services.AddSingleton<IMqttAdapterService, MqttAdapterService>();
            services.AddSingleton<ISettingsProvider, SettingsProvider>();
            services.AddSingleton<ISettingsService, SettingsService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICommandExecutionQueueService, CommandExecutionQueueService>();
            services.AddSingleton<ICommandExecutionManager, CommandExecutionManager>();

            // State cache
            services.AddSingleton<ISystemStateCache, SystemStateCache>();
            services.AddSingleton<IStateEntryFactory, StateEntryFactory>();
            services.AddSingleton<IStateEntryMapper, StateEntryMapper>();

            // Automation
            services.AddSingleton<IAutomationEntryQueueService, AutomationEntryQueueService>();
            services.AddSingleton<IAutomationRulesStore, AutomationRulesStore>();
            services.AddSingleton<IAutomationRulesConditionValidator, AutomationRulesConditionValidator>();
            services.AddSingleton<IAutomationEntryFactory, AutomationEntryFactory>();
            services.AddSingleton<IMqttActionHandler, MqttActionHandler>();
            services.AddSingleton<IActionMapperService, ActionMapperService>();
            services.AddSingleton<ITimeInterpreterService, TimeInterpreterService>();
            services.AddSingleton<IValueStoreInterpreterService, ValueStoreInterpreterService>();
            services.AddSingleton<ITemperatureInterpreterService, TemperatureInterpreterService>();

            // Migration services
            services.AddSingleton<IConfigurationMigrationService, ConfigurationMigrationService>();

            // Mqtt
            services.AddSingleton<IMqttFactory, MqttFactory>();
            services.AddSingleton<MqttClientOptionsBuilder>();
            services.AddSingleton<IMqttMessageCacheProvider, MqttMessageCacheProvider>();
            services.AddSingleton<IMqttSettingsProvider, MqttSettingsProvider>();
            services.AddSingleton<IMqttConnectionProvider, MqttConnectionProvider>();
            services.AddSingleton<IMqttClientInteractionService, MqttClientInteractionService>();

            services.AddSingleton<IMqttAdapterDbService, MqttAdapterDbService>();
            services.AddSingleton<IRepositoryService, RepositoryService>();
            services.AddSingleton<IStateRepository, StateRepository>();
            services.AddSingleton<IControllerStateService, ControllerStateService>();

            services.AddSingleton<IEntityFactory, EntityFactory>();
            services.AddSingleton<IModelFactory, ModelFactory>();
            services.AddSingleton<IEntityRepository, EntityRepository>();

            services.AddSingleton<ICooliemintPackageService, CooliemintPackageService>();
            services.AddSingleton<INotificationService, NotificationService>();

            // REST API services
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IPageFactory, PageFactory>();
            services.AddSingleton<IControlFactory, ControlFactory>();

            // Custom commands
            services.AddSingleton<ICustomCommandService, CustomCommandService>();
            services.AddSingleton<ICommandDtoFactory, CommandDtoFactory>();
            services.AddSingleton<ICommandExecutionService, CommandExecutionService>();
            services.AddSingleton<ICommandFactory, CommandFactory>();
            services.AddSingleton<ICustomCommandConfigurationService, CustomCommandConfigurationService>();

            // Http
            services.AddSingleton<IHttpContentFactory, HttpContentFactory>();

            //Pushover notifications
            services.AddSingleton<IPushoverHttpContentFactory, PushoverHttpContentFactory>();
            services.AddSingleton<IPushoverHttpRequestFactory, PushoverHttpRequestFactory>();
            services.AddSingleton<IPushoverService, PushoverService>();

            RegisterSystemSpecifics(services);
        }

        public void RegisterSystemSpecifics(IServiceCollection services)
        {
            services.AddSingleton<IDeviceOperationServiceProvider, DeviceOperationService>();
            services.AddSingleton<ISonoffDeviceOperationService, SonoffDeviceOperationService>();
            services.AddSingleton<IBrokerMessageFactory, BrokerMessageFactory>();
            services.AddSingleton<IDeviceArgumentFactory, DeviceArgumentFactory>();
            services.AddSingleton<IDeviceOperationFactory, DeviceOperationFactory>();
            services.AddSingleton<IMqttInteractionService, MqttInteractionService>();

            services.AddSingleton<IDeviceOperationProvider, DeviceOperationProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ILogService logService,
            ILogMessageFactory logMessageFactory,
            IMqttRepository repository,
            IMqttCommandAdapter commandAdapter,
            IAdapterSettingService adapterSettingService,
            IUiConfigurationService uiConfigurationService,
            INotificationService notificationService,
            ISettingsService settingsService,
            IFileSystemService fileSystemService,
            ILogFormatterService logFormatterService,
            ILogFileService logFileService,
            ISettingsProvider settingsProvider,
            ICustomCommandConfigurationService customCommandConfigurationService,
            IHostApplicationLifetime hostApplicationLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            settingsProvider.ReadSettings();

            logService.RegisterSinks(new List<IlogSink>
            {
                new ConsoleLogSink(),
                new MemoryLogSink(logMessageFactory, notificationService),
                new FileLogSink(fileSystemService, logFormatterService, logFileService)
            });

            if (Debugger.IsAttached)
            {
                logService.RegisterSink(new DebugLogSink());
            }

            logService.Log(LogSeverity.Info, "[Startup] Logging started.");

            var migrationService = app.ApplicationServices.GetService<IConfigurationMigrationService>();

            logService.Log(LogSeverity.Info, "[Startup] Starting migration.");
            if (migrationService.Migrate().GetAwaiter().GetResult())
            {
                logService.Log(LogSeverity.Info, "[Startup] Restarting app due to migration.");
                hostApplicationLifetime.StopApplication();
            }

            var repo = new DatabaseRepository();
            repo.InitializeDatabase();

            commandAdapter.Initialize();
            repository.Initialize();
            adapterSettingService.Initialize();
            uiConfigurationService.ReadAllConfigurationFiles();

            ConfigureSystemSpecifics(app.ApplicationServices);
            customCommandConfigurationService.ReloadConfiguration();
            ServiceLocatorService.Instance.RegisterContainer(app.ApplicationServices);

            // Dummy automation
            app.ApplicationServices.GetService<IAutomationRulesStore>().AddScene(new Scene
            {
                Id = 0,
                DisplayName = "Heizung an 7:00-18:00",
                Condition = new ConditionContainer
                {
                    ChainType = ChainType.AND,
                    Conditions = new List<ICondition>
                    {
                        new TimeLaterCondition
                        {
                            Time = new TimeSpan(07,00,00),
                        },
                        new TimeEarlierCondition
                        {
                            Time = new TimeSpan(18,00,00),
                        },
                        new ValueStoreCondition
                        {
                            Key = "Mqtt:MultiSwitchAdapter:1/5",
                            Value = false
                        },
                        new TemperaturLowerCondition
                        {
                            SensorIdentifier = "Mqtt:WeatherAdapter:1",
                            Temperature = 20.3M
                        }
                    }
                },
                OnTrue = new List<IAutomationAction>
                {
                    new MqttAction
                    {
                        Topic = "switch/1/5",
                        Payload = "1"
                    }
                }
            });

            app.ApplicationServices.GetService<IAutomationRulesStore>().AddScene(new Scene
            {
                Id = 1,
                DisplayName = "Heizung aus 7:00-18:00",
                Condition = new ConditionContainer
                {
                    ChainType = ChainType.AND,
                    Conditions = new List<ICondition>
                    {
                        new TimeLaterCondition
                        {
                            Time = new TimeSpan(07,00,00),
                        },
                        new TimeEarlierCondition
                        {
                            Time = new TimeSpan(18,00,00),
                        },
                        new ValueStoreCondition
                        {
                            Key = "Mqtt:MultiSwitchAdapter:1/5",
                            Value = true
                        },
                        new TemperaturHigherCondition
                        {
                            SensorIdentifier = "Mqtt:WeatherAdapter:1",
                            Temperature = 20.4M
                        }
                    }
                },
                OnTrue = new List<IAutomationAction>
                {
                    new MqttAction
                    {
                        Topic = "switch/1/5",
                        Payload = "0"
                    }
                }
            });
        }

        public void ConfigureSystemSpecifics(IServiceProvider serviceProvider)
        {
            var deviceOperationServiceProvider = serviceProvider.GetService<IDeviceOperationServiceProvider>();
            var sonoffDeviceOperationService = serviceProvider.GetService<ISonoffDeviceOperationService>();

            deviceOperationServiceProvider.RegisterOperationService(sonoffDeviceOperationService);
            var deviceOperationProvider = serviceProvider.GetService<IDeviceOperationProvider>();

            foreach (var adapterEntry in serviceProvider.GetService<IMqttAdapterService>().GetAdapterEntries())
            {
                deviceOperationProvider.RegisterDeviceOperations(deviceOperationServiceProvider.GetOperations(adapterEntry));
            }
        }
    }
}
