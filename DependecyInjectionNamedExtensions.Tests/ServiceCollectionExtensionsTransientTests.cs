using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace DependecyInjectionNamedExtensions.Tests
{
    [Trait("Category", "ServiceCollection")]
    public class ServiceCollectionExtensionsTransientTests : ServiceCollectionTestBase
    {
        [Fact]
        public void AddTransient_GenericImplementationFactory_StringKey_Test()
        {
            services.AddTransient<IDummyService, DummyService, string>((serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }

        [Fact]
        public void AddTransient_GenericServiceFactory_StringKey_Test()
        {
            services.AddTransient<IDummyService, string>((serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }

        [Fact]
        public void AddTransient_GenericService_GenericImplementation_StringKey_Test()
        {
            services.AddTransient<IDummyService, DummyService, string>(nameof(DummyService));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyService));
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }

        [Fact]
        public void AddTransient_ServiceType_ImplementationFactory_StringKey_Test()
        {
            services.AddTransient(typeof(IDummyService), (serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }

        [Fact]
        public void AddTransient_ServiceType_ImplementationType_StringKey_Test()
        {
            services.AddTransient(typeof(IDummyService), typeof(DummyService), nameof(DummyService));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyService));
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }
    }
}
