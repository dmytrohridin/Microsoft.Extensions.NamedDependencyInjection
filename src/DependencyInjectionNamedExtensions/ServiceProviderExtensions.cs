using DependencyInjectionNamedExtensions.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DependencyInjectionNamedExtensions
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Gets the service object of the specified type by specified key of type TKey.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="serviceProvider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <param name="key">A key on which the dependency is registered</param>
        /// <returns>A service object of type serviceType that registered by specified key.
        /// If there are no service object or key - null will be returned</returns>
        public static object GetService<TKey>(this IServiceProvider serviceProvider, Type serviceType, TKey key)
        {
            var envelope = serviceProvider
                .GetServices<INamedServiceEnvelope<TKey>>()
                .Where(s => s.Key.Equals(key))
                .FirstOrDefault(e => e.ServiceType == serviceType);
            var service = envelope?.ImplementationFactory(serviceProvider);
            return service;
        }

        /// <summary>
        /// Gets the service of type T by specified key of type TKey.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="serviceProvider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="key">A key on which the dependency is registered</param>
        /// <returns>A service object of type T that registered by specified key.
        /// If there are no service or key - default value will be returned</returns>
        public static T GetService<T, TKey>(this IServiceProvider serviceProvider, TKey key)
        {
            var service = serviceProvider.GetService(typeof(T), key);
            return (T) service;
        }

        /// <summary>
        /// Gets the service of type T by specified key comparer.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="serviceProvider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="keyComparer">A comparer function</param>
        /// <returns>A service object of type T that registered by specified key.
        /// If there are no service or key - default value will be returned</returns>
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
