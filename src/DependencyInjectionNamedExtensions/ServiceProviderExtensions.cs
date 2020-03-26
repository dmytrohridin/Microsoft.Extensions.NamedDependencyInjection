using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DependencyInjectionNamedExtensions
{
    public static class ServiceProviderExtensions
    {
        public static object GetService<TKey>(this IServiceProvider serviceProvider, Type serviceType, TKey key)
        {
            var envelope = serviceProvider
                .GetServices<INamedServiceEnvelope<TKey>>()
                .Where(s => s.Key.Equals(key))
                .FirstOrDefault(e => e.ServiceType == serviceType);
            var service = envelope?.ImplementationFactory(serviceProvider);
            return service;
        }

        public static T GetService<T, TKey>(this IServiceProvider serviceProvider, TKey key)
        {
            var service = serviceProvider.GetService(typeof(T), key);
            return (T) service;
        }
     
        public static T GetService<T, TKey>(this IServiceProvider serviceProvider, Func<TKey, bool> keyComparer)
        {
            var envelope = serviceProvider
                .GetServices<INamedServiceEnvelope<TKey>>()
                .Where(s => keyComparer(s.Key))
                .FirstOrDefault(e => e.ServiceType == typeof(T));

            if (envelope == null)
            {
                return default;
            }

            return (T) envelope.ImplementationFactory(serviceProvider);
        }
    }
}
