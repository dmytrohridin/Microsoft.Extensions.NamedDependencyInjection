using Microsoft.Extensions.DependencyInjection;
using System;

namespace DependencyInjectionNamedExtensions.Tests
{
    public abstract class ServiceProviderExtensionsTestBase : IDisposable
    {
        private readonly ServiceCollection services;
        protected IServiceProvider serviceProvider;
        
        protected ServiceProviderExtensionsTestBase()
        {
            services = new ServiceCollection();
        }

        protected void RegisterServices<T1, T2>(T1 key1, T2 key2)
        {
            services.AddScoped<IDummyService, DummyServiceA, T1>(key1);
            services.AddScoped<IDummyService, DummyServiceB, T2>(key2);
            serviceProvider = BuildServiceProvider();
        }
        
        private IServiceProvider BuildServiceProvider()
        {
            return services.BuildServiceProvider();
        }

        public void Dispose()
        {
            var casted = serviceProvider as ServiceProvider;
            casted?.Dispose();
        }
    }
}
