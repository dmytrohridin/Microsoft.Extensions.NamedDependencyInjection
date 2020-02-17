using System;

namespace DependecyInjectionNamedExtensions
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
