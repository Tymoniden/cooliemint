using CoolieMint.WindowsClient.Services.Container;
using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CoolieMint.WindowsClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            var container = new Container();
            ContainerService.InitializeContainer(container);
            ContainerService.Verify();
        }
    }
}
