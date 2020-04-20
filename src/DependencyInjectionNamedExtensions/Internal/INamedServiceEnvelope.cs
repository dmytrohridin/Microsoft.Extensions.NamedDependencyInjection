using System;

namespace DependencyInjectionNamedExtensions.Internal
{
    /// <summary>
    /// Interface that describe key and service factory that should be resolved by key
    /// </summary>
    /// <typeparam name="TKey">Type of key</typeparam>
    internal interface INamedServiceEnvelope<out TKey>
    {
        /// <summary>
        /// Key for registered service
        /// </summary>
        TKey Key { get; }

        /// <summary>
        /// Type of registered service
        /// </summary>
        Type ServiceType { get; }

        /// <summary>
        /// Factory that create the service
        /// </summary>
        Func<IServiceProvider, object> ImplementationFactory { get; }
    }
}
