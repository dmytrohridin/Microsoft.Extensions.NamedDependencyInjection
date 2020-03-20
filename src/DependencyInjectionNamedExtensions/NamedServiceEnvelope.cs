using System;

namespace DependencyInjectionNamedExtensions
{
    internal class NamedServiceEnvelope<TKey> : 
        INamedServiceEnvelope<TKey>
    {
        public TKey Key { get; }

        public Func<IServiceProvider, object> ImplementationFactory { get; }

        public NamedServiceEnvelope(
            TKey key,
            Func<IServiceProvider, object> implementationFactory) =>
                (Key, ImplementationFactory) = (key, implementationFactory);
    }
}
