using CoolieMint.WindowsClient.Services.CoolieMint;
using CoolieMint.WindowsClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolieMint.WindowsClient.Services.Container
{
    public static class ContainerService
    {
        static SimpleInjector.Container _container;

        public static void InitializeContainer(SimpleInjector.Container container)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;

            container.RegisterSingleton<IFileSystemService, FileSystemService>();
            container.RegisterSingleton<IUploadService, UploadService>();
            container.RegisterSingleton<IPackageService, PackageService>();
            container.Register<MainViewModel>();
        }

        public static TService GetInstance<TService>() where TService : class => _container.GetInstance<TService>();

        public static void Verify()
        {
            if (_container is null)
            {
                throw new ArgumentNullException(nameof(_container));
            }

            if (Debugger.IsAttached)
            {
                _container.Verify();
            }
        }
    }
}
