using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace DependencyInjectionNamedExtensions.Tests
{
    [Trait("Category", "ServiceCollection")]
    public class ServiceCollectionExtensionsSingletonTests : ServiceCollectionTestBase
    {
        [Fact]
        public void AddSingleton_GenericImplementationFactory_StringKey_Test()
        {
            services.AddSingleton<IDummyService, DummyServiceA, string>((serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_GenericServiceFactory_StringKey_Test()
        {
            services.AddSingleton<IDummyService, string>((serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_GenericService_GenericImplementation_StringKey_Test()
        {
            services.AddSingleton<IDummyService, DummyServiceA, string>(nameof(DummyServiceA));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyServiceA));
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_ServiceType_ImplementationFactory_StringKey_Test()
        {
            services.AddSingleton(typeof(IDummyService), (serviceProvider) => new DummyServiceA(), nameof(DummyServiceA));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyServiceA != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_ServiceType_ImplementationType_StringKey_Test()
        {
            services.AddSingleton(typeof(IDummyService), typeof(DummyServiceA), nameof(DummyServiceA));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyServiceA));
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_ServiceImplementation_StringKey_Test()
        {
            IDummyService service = new DummyServiceA();
            services.AddSingleton(service, nameof(DummyServiceA));

            var descriptor = services.FirstOrDefault(x => x.ImplementationInstance?.GetType() == typeof(DummyServiceA));
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_ServiceType_ServiceImplementation_StringKey_Test()
        {
            DummyServiceA service = new DummyServiceA();
            services.AddSingleton(typeof(IDummyService), service, nameof(DummyServiceA));

            var descriptor = services.FirstOrDefault(x => x.ImplementationInstance?.GetType() == typeof(DummyServiceA));
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }
    }
}
