using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DependencyInjectionNamedExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddScoped<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType,
            TKey key)
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, object>(
                    key,
                    p => p.GetServices(serviceType).FirstOrDefault(s => s.GetType() == implementationType)));

            services.AddScoped(serviceType, implementationType);
            return services;
        }

        public static IServiceCollection AddScoped<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            TKey key)
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, object>(
                    key,
                    implementationFactory));

            services.AddScoped(serviceType, implementationFactory);
            return services;
        }

        public static IServiceCollection AddScoped<TService, TKey>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            TKey key) where TService : class
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, TService>(
                    key, 
                    implementationFactory));
            services.AddScoped(implementationFactory);
            return services;
        }

        public static IServiceCollection AddScoped<TService, TImplementation, TKey>(
            this IServiceCollection services,
            TKey key) where TService : class
                      where TImplementation : class, TService
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, TService>(
                    key,
                    (p) => p.GetServices<TService>().FirstOrDefault(s => s.GetType() == typeof(TImplementation))));

            services.AddScoped<TService, TImplementation>();
            return services;
        }

        public static IServiceCollection AddScoped<TService, TImplementation, TKey>(
            this IServiceCollection services, 
            Func<IServiceProvider, TImplementation> implementationFactory,
            TKey key)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddScoped<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey, TService>(
                    key,
                    implementationFactory));
            services.AddScoped<TService, TImplementation>(implementationFactory);
            return services;
        }
    }
}
