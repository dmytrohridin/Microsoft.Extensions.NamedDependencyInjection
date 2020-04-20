# .NET Core Dependency Injection Named Extensions
Extensions for .NET Core Dependency Injection container, that allow to resolve dependencies by key

![.NET Core](https://github.com/dmytrohridin/DependencyInjectionNamedExtensions/workflows/.NET%20Core/badge.svg?branch=master)

## Why?

Current implementation of .NET Core Dependency Injection container does not support registration and resolving dependencies by name or key.

## Instalation

TODO: Add description after final nuget package will be ready

## Usage

Register named services with ```IServiceCollection```
```csharp
services.AddScoped<IEventBus, AzureServiceBusPersistance, string>("azureServiceBus");
services.AddScoped<IEventBus, RabbitMQServicePersistance, string>("rabbitMQ");
```

Inject ```IServiceProvider``` interface where you need to resolve dependency and call ```GetService``` method with key provided
```csharp
var eventBus = serviceProvider.GetService<IEventBus, string>(eventBusKey);
```

## Supported versions
