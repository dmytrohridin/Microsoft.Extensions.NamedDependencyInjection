using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.Extensions.NamedDependencyInjection.Tests
{
    [Trait("Category", "ServiceCollection")]
    public class ServiceCollectionExtensionsScopedTests : ServiceCollectionTestBase
    {
        [Fact]
        public void AddScoped_ServiceType_ImplementationType_StringKey_Test()
        {
            services.AddScoped(typeof(IDummyService), typeof(DummyServiceA), nameof(DummyServiceA));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyServiceA));
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddScoped_ServiceType_ImplementationFactory_StringKey_Test()
        {
            services.AddScoped(typeof(IDummyService), (serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddScoped_GenericServiceFactory_StringKey_Test()
        {
            services.AddScoped<IDummyService, string>((serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddScoped_GenericService_GenericImplementation_StringKey_Test()
        {
            services.AddScoped<IDummyService, DummyServiceA, string>(nameof(DummyServiceA));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyServiceA));
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }

        [Fact]
        public void AddScoped_GenericImplementationFactory_StringKey_Test()
        {
            services.AddScoped<IDummyService, DummyServiceA, string>((serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Scoped);
        }
    }
}
