using System;

namespace DependencyInjectionNamedExtensions
{
    internal class NamedServiceEnvelope<TKey, TService> : 
        INamedServiceEnvelope<TKey, TService>
    {
        public TKey Key { get; }

        public Func<IServiceProvider, TService> ImplementationFactory { get; }

        Func<IServiceProvider, object> INamedServiceEnvelope<TKey>.ImplementationFactory => 
             ImplementationFactory as Func<IServiceProvider, object>;

        public NamedServiceEnvelope(
            TKey key,
            Func<IServiceProvider, TService> implementationFactory) =>
                (Key, ImplementationFactory) = (key, implementationFactory);
    }
}
