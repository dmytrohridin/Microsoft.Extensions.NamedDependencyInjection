using System;

namespace DependencyInjectionNamedExtensions
{
    internal interface INamedServiceEnvelope<out TKey>
    {
        TKey Key { get; }

        Func<IServiceProvider, object> ImplementationFactory { get; }
    }
}
