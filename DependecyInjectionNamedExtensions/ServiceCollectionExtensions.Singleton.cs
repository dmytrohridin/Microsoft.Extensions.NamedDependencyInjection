using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DependecyInjectionNamedExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSingleton<TService, TImplementation, TKey>(
            this IServiceCollection services, 
            Func<IServiceProvider, TImplementation> implementationFactory,
            TKey key)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddSingleton<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, TService>(
                    key,
                    implementationFactory));
            services.AddSingleton<TService, TImplementation>(implementationFactory);
            return services;
        }

        public static IServiceCollection AddSingleton<TService, TKey>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            TKey key) where TService : class
        {
            services.AddSingleton<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, TService>(
                    key,
                    implementationFactory));

            services.AddSingleton(implementationFactory);
            return services;
        }

        public static IServiceCollection AddSingleton<TService, TImplementation, TKey>(
            this IServiceCollection services,
            TKey key) where TService : class
                      where TImplementation : class, TService
        {
            services.AddSingleton<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, TService>(
                    key,
                    (p) => p.GetServices<TService>().FirstOrDefault(s => s.GetType() == typeof(TImplementation))));

            services.AddSingleton<TService, TImplementation>();
            return services;
        }

        public static IServiceCollection AddSingleton<TKey>(
            this IServiceCollection services, 
            Type serviceType, 
            Func<IServiceProvider, object> implementationFactory,
            TKey key)
        {
            services.AddSingleton<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, object>(
                    key,
                    implementationFactory));

            ServiceCollectionServiceExtensions.AddSingleton(services, serviceType, implementationFactory);
            return services;
        }

        public static IServiceCollection AddSingleton<TKey>(
            this IServiceCollection services, 
            Type serviceType, 
            Type implementationType,
            TKey key)
        {
            services.AddSingleton<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, object>(
                    key,
                    p => p.GetServices(serviceType).FirstOrDefault(s => s.GetType() == implementationType)));

            ServiceCollectionServiceExtensions.AddSingleton(services, serviceType, implementationType);
            return services;
        }

        public static IServiceCollection AddSingleton<TService, TKey>(
            this IServiceCollection services, 
            TService implementationInstance,
            TKey key) where TService : class
        {
            services.AddSingleton<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, TService>(
                    key,
                    _ => implementationInstance));

            services.AddSingleton(implementationInstance);
            return services;
        }

        public static IServiceCollection AddSingleton<TKey>(
            this IServiceCollection services, 
            Type serviceType, 
            object implementationInstance,
            TKey key)
        {
            services.AddSingleton<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, object>(
                    key,
                    _ => implementationInstance));

            ServiceCollectionServiceExtensions.AddSingleton(services, serviceType, implementationInstance);
            return services;
        }
    }
}
