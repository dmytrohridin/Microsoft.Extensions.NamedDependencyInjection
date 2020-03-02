using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependecyInjectionNamedExtensions.Tests
{
    public abstract class ServiceCollectionTestBase
    {
        protected readonly ServiceCollection services;
        protected ServiceCollectionTestBase()
        {
            services = new ServiceCollection();
        }

        protected static void ExecuteAssertion(ServiceDescriptor descriptor, ServiceLifetime lifetime)
        {
            Assert.NotNull(descriptor);
            Assert.Equal(lifetime, descriptor.Lifetime);
        }
    }
}
