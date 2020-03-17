using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DependecyInjectionNamedExtensions
{
    public static class ServiceProviderExtensions
    {
        public static object GetService<TKey>(this IServiceProvider serviceProvider, TKey key)
        {
            var services = serviceProvider.GetServices<INamedServiceEnvelope<TKey>>();
            var envelope = services.FirstOrDefault(s => s.Key.Equals(key));
            var service = envelope?.ImplementationFactory(serviceProvider);
            return service;
        }
        
        public static T GetService<T, TKey>(this IServiceProvider serviceProvider, TKey key)
        {
            return (T) serviceProvider.GetService<TKey>(key);
        }
        
        public static T GetService<T, TKey>(this IServiceProvider serviceProvider, Func<TKey, bool> keyComparer)
        {
            var services = serviceProvider.GetServices<INamedServiceEnvelope<TKey>>();
            var envelope = services.FirstOrDefault(s => keyComparer(s.Key));
            if (envelope == null)
            {
                return default;
            }

            var service = envelope.ImplementationFactory(serviceProvider);
            return (T) service;
        }
    }
}
