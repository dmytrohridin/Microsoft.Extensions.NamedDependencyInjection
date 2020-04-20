using System;
using System.Linq;
using DependencyInjectionNamedExtensions.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionNamedExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a scoped service of the type specified in serviceType with an implementation
        /// of the type specified in implementationType to the specified 
        /// Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection 
        /// to add the service to.
        /// </param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddScoped<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType,
            TKey key)
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    serviceType,
                    p => p
                        .GetServices(serviceType)
                        .FirstOrDefault(s => s.GetType() == implementationType)));

            services.AddScoped(serviceType, implementationType);
            return services;
        }

        /// <summary>
        /// Adds a scoped service of the type specified in serviceType with a factory specified
        /// in implementationFactory to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection 
        /// to add the service to.
        /// </param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddScoped<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            TKey key)
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    serviceType,
                    implementationFactory));

            services.AddScoped(serviceType, implementationFactory);
            return services;
        }

        /// <summary>
        /// Adds a scoped service of the type specified in TService with a factory specified
        /// in implementationFactory to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection 
        /// to add the service to.
        /// </param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddScoped<TService, TKey>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            TKey key) where TService : class
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    typeof(TService),
                    implementationFactory));
            services.AddScoped(implementationFactory);
            return services;
        }

        /// <summary>
        /// Adds a scoped service of the type specified in TService with an implementation
        /// type specified in TImplementation to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection 
        /// to add the service to.
        /// </param>
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddScoped<TService, TImplementation, TKey>(
            this IServiceCollection services,
            TKey key) where TService : class
                      where TImplementation : class, TService
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    typeof(TService),
                    (p) => p.GetServices<TService>().FirstOrDefault(s => s.GetType() == typeof(TImplementation))));

            services.AddScoped<TService, TImplementation>();
            return services;
        }

        /// <summary>
        /// Adds a scoped service of the type specified in TService with an implementation
        /// type specified in TImplementation using the factory specified in implementationFactory
        /// to the specified Microsoft.Extensions.DependencyInjection.IServiceCollection.
        /// </summary>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <typeparam name="TKey">The type of key.</typeparam>
        /// <param name="services">The Microsoft.Extensions.DependencyInjection.IServiceCollection 
        /// to add the service to.
        /// </param>
        /// <param name="implementationFactory"> The factory that creates the service.</param>
        /// <param name="key">A key on which the dependency is registered.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddScoped<TService, TImplementation, TKey>(
            this IServiceCollection services, 
            Func<IServiceProvider, TImplementation> implementationFactory,
            TKey key)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    typeof(TService),
                    implementationFactory));
            services.AddScoped<TService, TImplementation>(implementationFactory);
            return services;
        }
    }
}
