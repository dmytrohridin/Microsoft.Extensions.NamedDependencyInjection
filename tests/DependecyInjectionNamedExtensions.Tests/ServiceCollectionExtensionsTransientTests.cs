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
            services.AddTransient<IDummyService, DummyServiceA, string>((serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }

        [Fact]
        public void AddTransient_GenericServiceFactory_StringKey_Test()
        {
            services.AddTransient<IDummyService, string>((serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }

        [Fact]
        public void AddTransient_GenericService_GenericImplementation_StringKey_Test()
        {
            services.AddTransient<IDummyService, DummyServiceA, string>(nameof(DummyServiceA));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyServiceA));
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }

        [Fact]
        public void AddTransient_ServiceType_ImplementationFactory_StringKey_Test()
        {
            services.AddTransient(typeof(IDummyService), (serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }

        [Fact]
        public void AddTransient_ServiceType_ImplementationType_StringKey_Test()
        {
            services.AddTransient(typeof(IDummyService), typeof(DummyServiceA), nameof(DummyServiceA));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyServiceA));
            ExecuteAssertion(descriptor, ServiceLifetime.Transient);
        }
    }
}
