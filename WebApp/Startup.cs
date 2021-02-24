using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Diagnostics;
using WebControlCenter.CommandAdapter;
using WebControlCenter.Database.Repository;
using WebControlCenter.Database.Services;
using WebControlCenter.Repository;
using WebControlCenter.Services;
using WebControlCenter.Services.Database;
using WebControlCenter.Services.FileSystem;
using WebControlCenter.Services.Log;
using WebControlCenter.Services.Log.Sink;
using WebControlCenter.Services.Mqtt;
using WebControlCenter.Services.Rest;
using WebControlCenter.Services.Setting;
using WebControlCenter.Services.Storage;
using WebControlCenter.Services.System;

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
            services.AddSingleton<IConnectionProvider, ConnectionProvider>();

            services.AddSingleton<IMqttRepository, MqttRepository>();
            services.AddSingleton<IMqttValueProvider, MqttValueProvider>();
            services.AddSingleton<IMessageBroker, MessageBroker>();
            services.AddSingleton<IMqttCommandAdapter, MqttCommandAdapter>();
            services.AddSingleton<IAdapterService, AdapterService>();
            services.AddSingleton<IAdapterSettingService, AdapterSettingService>();
            services.AddSingleton<IAdapterFactory, AdapterFactory>();
            services.AddSingleton<IFileSystemService, FileSystemService>();
            services.AddSingleton<IUiConfigurationService, UiConfigurationService>();
            services.AddSingleton<IMqttAdapterFactory, MqttAdapterFactory>();
            services.AddSingleton<IMqttAdapterService, MqttAdapterService>();
            services.AddSingleton<ISettingsProvider, SettingsProvider>();
            services.AddSingleton<ISettingsService, SettingsService>();

            services.AddSingleton<IMqttMessageCacheProvider, MqttMessageCacheProvider>();

            services.AddSingleton<IMqttAdapterDbService, MqttAdapterDbService>();
            services.AddSingleton<IRepositoryService, RepositoryService>();
            services.AddSingleton<IStateRepository, StateRepository>();
            services.AddSingleton<IControllerStateService, ControllerStateService>();

            services.AddSingleton<IEntityFactory, EntityFactory>();
            services.AddSingleton<IModelFactory, ModelFactory>();
            services.AddSingleton<IEntityRepository, EntityRepository>();

            services.AddSingleton<INotificationService, NotificationService>();

            // REST API services
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IPageFactory, PageFactory>();
            services.AddSingleton<IControlFactory, ControlFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            ILogService logService,
            ILogMessageFactory logMessageFactory,
            IConnectionProvider connectionProvider,
            IMqttRepository repository, 
            IMqttCommandAdapter commandAdapter, 
            IAdapterSettingService adapterSettingService, 
            IUiConfigurationService uiConfigurationService,
            INotificationService notificationService,
            ISettingsService settingsService,
            IFileSystemService fileSystemService,
            ILogFormatterService logFormatterService,
            ILogFileService logFileService,
            ISettingsProvider settingsProvider)
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

            logService.Log(LogSeverity.Info, "Logging started.");

            var repo = new DatabaseRepository();
            repo.InitializeDatabase();

            commandAdapter.Initialize();
            connectionProvider.InitializeConnection();
            repository.Initialize();
            adapterSettingService.Initialize();
            uiConfigurationService.ReadAllConfigurationFiles();
        }
    }
}
