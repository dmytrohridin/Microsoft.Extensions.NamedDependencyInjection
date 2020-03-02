using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace DependecyInjectionNamedExtensions.Tests
{
    [Trait("Category", "ServiceCollection")]
    public class ServiceCollectionExtensionsSingletonTests : ServiceCollectionTestBase
    {
        [Fact]
        public void AddSingleton_GenericImplementationFactory_StringKey_Test()
        {
            services.AddSingleton<IDummyService, DummyService, string>((serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_GenericServiceFactory_StringKey_Test()
        {
            services.AddSingleton<IDummyService, string>((serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_GenericService_GenericImplementation_StringKey_Test()
        {
            services.AddSingleton<IDummyService, DummyService, string>(nameof(DummyService));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyService));
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_ServiceType_ImplementationFactory_StringKey_Test()
        {
            services.AddSingleton(typeof(IDummyService), (serviceProvider) => new DummyService(), nameof(DummyService));

            using var provider = services.BuildServiceProvider();
            var descriptor = services.FirstOrDefault(x => x.ImplementationFactory(provider) as DummyService != null);
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_ServiceType_ImplementationType_StringKey_Test()
        {
            services.AddSingleton(typeof(IDummyService), typeof(DummyService), nameof(DummyService));

            var descriptor = services.FirstOrDefault(x => x.ImplementationType == typeof(DummyService));
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_ServiceImplementation_StringKey_Test()
        {
            IDummyService service = new DummyService();
            services.AddSingleton(service, nameof(DummyService));

            var descriptor = services.FirstOrDefault(x => x.ImplementationInstance?.GetType() == typeof(DummyService));
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }

        [Fact]
        public void AddSingleton_ServiceType_ServiceImplementation_StringKey_Test()
        {
            DummyService service = new DummyService();
            services.AddSingleton(typeof(IDummyService), service, nameof(DummyService));

            var descriptor = services.FirstOrDefault(x => x.ImplementationInstance?.GetType() == typeof(DummyService));
            ExecuteAssertion(descriptor, ServiceLifetime.Singleton);
        }
    }
}
