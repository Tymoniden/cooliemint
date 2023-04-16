using Cooliemint.Api.Server.Services;
using Cooliemint.Api.Server.Services.Converter;
using Cooliemint.Api.Server.Services.Storage;

namespace Cooliemint.Api.Server
{
    public static class CoolieMintServiceExtension
    {
        public static void RegisterCooliemintServices(this IServiceCollection services)
        {
            RegisterBasicServices(services);
            RegisterStorageServices(services);
            RegisterSerializerServices(services);
        }

        private static void RegisterSerializerServices(IServiceCollection services)
        {
            services.AddSingleton<IJsonSerializeService, JsonSerializeService>();
            services.AddSingleton<IJsonProviderService, JsonProviderService>();
        }

        public static void RegisterStorageServices(IServiceCollection services)
        {
            services.AddSingleton<IFileSystemService, FileSystemService>();
        }

        public static void RegisterBasicServices(IServiceCollection services)
        {
            services.AddSingleton<ISettingsProvider, SettingsProvider>();
        }
    }
}
