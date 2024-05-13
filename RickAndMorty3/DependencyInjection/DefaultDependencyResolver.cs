using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace RickAndMorty3.DependencyInjection
{
    public class DefaultDependencyResolver : IDependencyResolver
    {
        protected IServiceProvider ServiceProvider;

        public DefaultDependencyResolver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IDependencyScope BeginScope()
        {
            // This is important to ensure each request gets its own scope
            return new DefaultDependencyResolver(ServiceProvider.CreateScope().ServiceProvider);
        }

        public object GetService(Type serviceType)
        {
            return ServiceProvider.GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return ServiceProvider.GetServices(serviceType);
        }

        public void Dispose()
        {
            // If ServiceProvider is IDisposable, dispose it if necessary
            (ServiceProvider as IDisposable)?.Dispose();
        }
    }
}