using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace DependencyInjectionNamedExtensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTransient<TService, TImplementation, TKey>(
            this IServiceCollection services, 
            Func<IServiceProvider, TImplementation> implementationFactory,
            TKey key)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    implementationFactory));
            
            services.AddTransient<TService, TImplementation>(implementationFactory);
            return services;
        }

        public static IServiceCollection AddTransient<TService, TKey>(
            this IServiceCollection services, 
            Func<IServiceProvider, TService> implementationFactory,
            TKey key) where TService : class
        {
            services.AddTransient<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    implementationFactory));
            services.AddTransient(implementationFactory);
            return services;
        }

        public static IServiceCollection AddTransient<TService, TImplementation, TKey>(
            this IServiceCollection services,
            TKey key)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddTransient<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    p => p.GetServices<TService>().FirstOrDefault(s => s.GetType() == typeof(TImplementation))));

            services.AddTransient<TService, TImplementation>();
            return services;
        }

        public static IServiceCollection AddTransient<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            TKey key)
        {
            services.AddTransient<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    implementationFactory));
            services.AddTransient(serviceType, implementationFactory);
            return services;
        }

        public static IServiceCollection AddTransient<TKey>(
            this IServiceCollection services, 
            Type serviceType, 
            Type implementationType,
            TKey key)
        {
            services.AddTransient<INamedServiceEnvelope<TKey>>(
                provider => new NamedServiceEnvelope<TKey>(
                    key,
                    p => p.GetServices(serviceType).FirstOrDefault(s => s.GetType() == implementationType)));
            services.AddTransient(serviceType, implementationType);
            return services;
        }
    }
}
