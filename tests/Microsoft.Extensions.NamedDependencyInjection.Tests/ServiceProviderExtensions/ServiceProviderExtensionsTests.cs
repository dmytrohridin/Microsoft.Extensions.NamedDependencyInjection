using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Microsoft.Extensions.NamedDependencyInjection.Tests.ServiceProviderExtensions
{
    [Trait("Category", "ServiceProviderExtensions")]
    public class ServiceProviderExtensionsTests : IDisposable
    {
        private readonly IServiceProvider serviceProvider;

        public ServiceProviderExtensionsTests()
        {
            var services = new ServiceCollection();

            services
                .AddScoped<IDummyService, DummyServiceA, string>(DummyServiceConstants.DummyServiceAKey);
            services
                .AddScoped<IDummyService, DummyServiceB, string>(DummyServiceConstants.DummyServiceBKey);

            services
                .AddScoped<IAnotherDummyService, AnotherDummyServiceA, string>(DummyServiceConstants.DummyServiceAKey);
            services
                .AddScoped<IAnotherDummyService, AnotherDummyServiceB, string>(DummyServiceConstants.DummyServiceBKey);

            serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void GetServiceByServiceTypeReturnValidService()
        {
            var serviceA = serviceProvider.GetService(typeof(IDummyService), DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService(typeof(IDummyService), DummyServiceConstants.DummyServiceBKey);

            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
        }

        [Fact]
        public void GetServiceByInvalidServiceTypeReturnNull()
        {
            var serviceA = serviceProvider.GetService(typeof(DummyServiceA), DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService(typeof(DummyServiceB), DummyServiceConstants.DummyServiceBKey);

            Assert.Null(serviceA);
            Assert.Null(serviceB);
        }

        [Fact]
        public void GetServiceByServiceTypeByInvalidKeyReturnNull()
        {
            var serviceA = serviceProvider.GetService(typeof(IDummyService), "non_existing_key_a");
            var serviceB = serviceProvider.GetService(typeof(IDummyService), "non_existing_key_b");

            Assert.Null(serviceA);
            Assert.Null(serviceB);
        }

        [Fact]
        public void GetServiceByGenericTypeReturnValidService()
        {
            var serviceA = serviceProvider.GetService<IDummyService, string>(DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService<IDummyService, string>(DummyServiceConstants.DummyServiceBKey);

            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
        }

        [Fact]
        public void GetServiceByInvalidGenericTypeReturnNull()
        {
            var serviceA = serviceProvider.GetService<DummyServiceA, string>(DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService<DummyServiceB, string>(DummyServiceConstants.DummyServiceBKey);

            Assert.Null(serviceA);
            Assert.Null(serviceB);
        }

        [Fact]
        public void GetServiceByGenericTypeByInvalidKeyReturnNull()
        {
            var serviceA = serviceProvider.GetService<IDummyService, string>("non_existing_key_a");
            var serviceB = serviceProvider.GetService<IDummyService, string>("non_existing_key_b");

            Assert.Null(serviceA);
            Assert.Null(serviceB);
        }

        [Fact]
        public void GetServiceByGenericTypeKeyComparerReturnValidService()
        {
            var serviceA = serviceProvider.GetService<IDummyService, string>(
                key => key == DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService<IDummyService, string>(
                key => key == DummyServiceConstants.DummyServiceBKey);

            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
        }

        [Fact]
        public void GetServiceByInvalidGenericTypeKeyComparerReturnNull()
        {
            var serviceA = serviceProvider.GetService<DummyServiceA, string>(
                key => key == DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService<DummyServiceB, string>(
                key => key == DummyServiceConstants.DummyServiceBKey);

            Assert.Null(serviceA);
            Assert.Null(serviceB);
        }

        [Fact]
        public void GetServiceByGenericTypeByInvalidKeyComparerReturnNull()
        {
            var serviceA = serviceProvider.GetService<IDummyService, string>(key => key == "non_existing_key_a");
            var serviceB = serviceProvider.GetService<IDummyService, string>(key => key == "non_existing_key_b");

            Assert.Null(serviceA);
            Assert.Null(serviceB);
        }

        [Fact]
        public void GetDifferentServicesWithSameKeysByServiceTypeReturnValidServices()
        {
            var serviceA = serviceProvider.GetService(typeof(IDummyService), DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService(typeof(IDummyService), DummyServiceConstants.DummyServiceBKey);

            var anotherServiceA = serviceProvider.GetService(typeof(IAnotherDummyService), DummyServiceConstants.DummyServiceAKey);
            var anotherServiceB = serviceProvider.GetService(typeof(IAnotherDummyService), DummyServiceConstants.DummyServiceBKey);

            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
            Assert.NotNull(anotherServiceA as AnotherDummyServiceA);
            Assert.NotNull(anotherServiceB as AnotherDummyServiceB);
        }

        [Fact]
        public void GetDifferentServicesWithSameKeysByGenericTypeReturnValidServices()
        {
            var serviceA = serviceProvider.GetService<IDummyService, string>(DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService<IDummyService, string>(DummyServiceConstants.DummyServiceBKey);

            var anotherServiceA = serviceProvider.GetService<IAnotherDummyService, string>(DummyServiceConstants.DummyServiceAKey);
            var anotherServiceB = serviceProvider.GetService<IAnotherDummyService, string>(DummyServiceConstants.DummyServiceBKey);

            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
            Assert.NotNull(anotherServiceA as AnotherDummyServiceA);
            Assert.NotNull(anotherServiceB as AnotherDummyServiceB);
        }

        [Fact]
        public void GetDifferentServicesWithSameKeysByGenericTypeKeyComparerReturnValidServices()
        {
            var serviceA = serviceProvider.GetService<IDummyService, string>(
                key => key ==DummyServiceConstants.DummyServiceAKey);
            var serviceB = serviceProvider.GetService<IDummyService, string>(
                key => key == DummyServiceConstants.DummyServiceBKey);

            var anotherServiceA = serviceProvider.GetService<IAnotherDummyService, string>(
                key => key == DummyServiceConstants.DummyServiceAKey);
            var anotherServiceB = serviceProvider.GetService<IAnotherDummyService, string>(
                key => key == DummyServiceConstants.DummyServiceBKey);

            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
            Assert.NotNull(anotherServiceA as AnotherDummyServiceA);
            Assert.NotNull(anotherServiceB as AnotherDummyServiceB);
        }

        public void Dispose() => 
            (serviceProvider as ServiceProvider)?.Dispose();
    }
}
