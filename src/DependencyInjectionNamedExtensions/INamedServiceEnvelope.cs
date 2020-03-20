using System;

namespace DependencyInjectionNamedExtensions
{
    internal interface INamedServiceEnvelope<TKey>
    {
        TKey Key { get; }

        Func<IServiceProvider, object> ImplementationFactory { get; }
    }

    internal interface INamedServiceEnvelope<TKey, TService> : INamedServiceEnvelope<TKey>
    {
        new Func<IServiceProvider, TService> ImplementationFactory { get; }
    }
}
