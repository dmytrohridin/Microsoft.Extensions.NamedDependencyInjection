using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.NamedDependencyInjection.Internal;

namespace Microsoft.Extensions.NamedDependencyInjection
{
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Gets the service object of the specified type by specified key of type TKey.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="serviceProvider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A service object of type serviceType that registered by specified key.
        /// If there are no service object or key - null will be returned.</returns>
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
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A service object of type T that registered by specified key.
        /// If there are no service or key - default value will be returned.</returns>
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
        /// <param name="keyComparer">A comparer function.</param>
        /// <returns>A service object of type T that registered by specified key.
        /// If there are no service or key - default value will be returned.</returns>
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

        /// <summary>
        /// Gets the service of type T by specified key of type TKey.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="serviceProvider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A service object of type T that registered by specified key.
        /// If there are no service or key - exception will be throwed.</returns>
        /// <exception cref="InvalidOperationException">There is no service of type serviceType that registered by specified key.</exception>
        public static T GetRequiredService<T, TKey>(this IServiceProvider serviceProvider, TKey key)
        {
            var service = serviceProvider.GetRequiredService(typeof(T), key);
            return (T)service;
        }

        /// <summary>
        /// Gets the service object of the specified type by specified key of type TKey.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="serviceProvider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="serviceType">An object that specifies the type of service object to get.</param>
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A service object of type serviceType that registered by specified key.
        /// If there are no service object or key - exception will be throwed.</returns>
        /// <exception cref="InvalidOperationException">There is no service of type serviceType that registered by specified key.</exception>
        public static object GetRequiredService<TKey>(this IServiceProvider serviceProvider, Type serviceType, TKey key)
        {
            var service = serviceProvider.GetService(serviceType, key);
            if (service == null)
            {
                throw new InvalidOperationException($"No service for type '{serviceType.FullName}' has been registered.");
            }
            return service;
        }

        /// <summary>
        /// Gets the service of type T by specified key comparer.
        /// </summary>
        /// <typeparam name="T">The type of service object to get.</typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="serviceProvider">The System.IServiceProvider to retrieve the service object from.</param>
        /// <param name="keyComparer">A comparer function.</param>
        /// <returns>A service object of type T that registered by specified key.
        /// If there are no service or key - exception will be throwed.</returns>
        /// <exception cref="InvalidOperationException">There is no service of type serviceType that registered by specified key.</exception>
        public static T GetRequiredService<T, TKey>(this IServiceProvider serviceProvider, Func<TKey, bool> keyComparer)
        {
            var service = serviceProvider.GetService<T, TKey>(keyComparer);
            if (service == null)
            {
                throw new InvalidOperationException($"No service for type '{typeof(T).FullName}' has been registered.");
            }
            return service;
        }
    }
}
