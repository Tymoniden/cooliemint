using System;
using Microsoft.Extensions.DependencyInjection;

namespace CoolieMint.WebApp.Services
{
    public class ServiceLocatorService
    {
        private static IServiceProvider _container;
        private static ServiceLocatorService _instance;

        public void RegisterContainer(IServiceProvider container)
        {
            _container = container;
        }

        public static ServiceLocatorService Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new ServiceLocatorService();
                }

                return _instance;
            }
        }

        public TService GetInstance<TService>() => _container.GetService<TService>();
    }
}
