using System;

namespace DependencyInjectionNamedExtensions
{
    /// <summary>
    /// Class that describe key and service factory that should be resolved by key
    /// </summary>
    /// <typeparam name="TKey">Type of key</typeparam>
    internal class NamedServiceEnvelope<TKey> : 
        INamedServiceEnvelope<TKey>
    {
        /// <summary>
        /// Key for registered service
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Type of registered service
        /// </summary>
        public Type ServiceType { get; }

        /// <summary>
        /// Factory that create the service
        /// </summary>
        public Func<IServiceProvider, object> ImplementationFactory { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="serviceType">Service type<</param>
        /// <param name="implementationFactory">Implementation factory</param>
        public NamedServiceEnvelope(
            TKey key,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory) =>
                (Key, ServiceType, ImplementationFactory) = (key,serviceType, implementationFactory);
    }
}
