using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace DependecyInjectionNamedExtensions.Tests
{
    [Trait("Category", "ServiceCollection")]
    public class ServiceCollectionExtensionsScopedTests : ServiceCollectionTestBase
    {
        [Fact]
        public void AddScoped_ServiceType_ImplementationType_StringKey_Test()
        {
            services.AddScoped(typeof(IDummyService), typeof(DummyService), nameof(DummyService));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyService));
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddScoped_ServiceType_ImplementationFactory_StringKey_Test()
        {
            services.AddScoped(typeof(IDummyService), (serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddScoped_GenericServiceFactory_StringKey_Test()
        {
            services.AddScoped<IDummyService, string>((serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddScoped_GenericService_GenericImplementation_StringKey_Test()
        {
            services.AddScoped<IDummyService, DummyService, string>(nameof(DummyService));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyService));
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddScoped_GenericImplementationFactory_StringKey_Test()
        {
            services.AddScoped<IDummyService, DummyService, string>((serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }
    }
}
