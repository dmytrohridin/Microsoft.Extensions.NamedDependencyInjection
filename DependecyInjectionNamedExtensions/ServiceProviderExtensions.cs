using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DependecyInjectionNamedExtensions
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T, TKey>(this IServiceProvider serviceProvider, TKey key)
        {
            var services = serviceProvider.GetServices<INamedServiceEnvelope<TKey>>();
            var envelope = services.FirstOrDefault(s => s.Key.Equals(key));
            if (envelope == null)
            {
                return default;
            }
            
            var service = envelope.ImplementationFactory(serviceProvider);
            return (T)service;
        }
    }
}
