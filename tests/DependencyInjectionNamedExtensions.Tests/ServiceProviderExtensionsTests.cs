using Xunit;

namespace DependencyInjectionNamedExtensions.Tests
{
    public class ServiceProviderExtensionsTests : ServiceProviderExtensionsTestBase
    {
        [Theory]
        [InlineData(DummyServiceConstants.DummyServiceAKey, DummyServiceConstants.DummyServiceBKey)]
        public void GetService_Object_Test(string key1, string key2)
        {
            RegisterServices(key1, key2);
            var serviceA = serviceProvider.GetService(typeof(IDummyService), key1);
            var serviceB = serviceProvider.GetService(typeof(IDummyService), key2);

            Assert.NotNull(serviceA);
            Assert.NotNull(serviceB);
            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
        }

        [Theory]
        [InlineData(DummyServiceConstants.DummyServiceAKey, DummyServiceConstants.DummyServiceBKey)]
        public void GetService_Generic_Test(string key1, string key2)
        {
            RegisterServices(key1, key2);
            var serviceA = serviceProvider.GetService<IDummyService, string>(key1);
            var serviceB = serviceProvider.GetService<IDummyService, string>(key2);

            Assert.NotNull(serviceA);
            Assert.NotNull(serviceB);
            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
        }
        
        [Theory]
        [InlineData(DummyServiceConstants.DummyServiceAKey, DummyServiceConstants.DummyServiceBKey)]
        public void GetService_Generic_Comparer_Test(string key1, string key2)
        {
            RegisterServices(key1, key2);
            var serviceA = serviceProvider.GetService<IDummyService, string>(key => key == key1);
            var serviceB = serviceProvider.GetService<IDummyService, string>(key => key == key2);

            Assert.NotNull(serviceA);
            Assert.NotNull(serviceB);
            Assert.NotNull(serviceA as DummyServiceA);
            Assert.NotNull(serviceB as DummyServiceB);
        }
    }
}
