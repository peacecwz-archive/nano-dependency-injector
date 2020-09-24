using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nano.DependencyInjector
{
    internal static class DependencyInjectionConfigure
    {
        public static List<DependencyInjectionServiceType> GetDependencies(Assembly assembly)
        {
            var dependencies = new List<DependencyInjectionServiceType>();
            foreach (var type in assembly.GetTypes())
            {
                var injectionType = GetInjectionType(type);
                if (!injectionType.Lifetime.HasValue)
                    continue;

                dependencies.Add(new DependencyInjectionServiceType
                {
                    LifeTime = injectionType.Lifetime.Value,
                    ImplementationType = type,
                    InterfaceType = injectionType.Self ? type : type.GetInterfaces()[0] // TODO (peacecwz): Detect multiple interface and interface inject selection
                });
            }

            return dependencies;
        }

        private static (ServiceLifetime? Lifetime, bool Self) GetInjectionType(Type type)
        {
            if (type.GetCustomAttribute<SingletonAttribute>() != null)
            {
                return (ServiceLifetime.Singleton, false);
            }

            if (type.GetCustomAttribute<SelfSingletonAttribute>() != null)
            {
                return (ServiceLifetime.Singleton, true);
            }

            if (type.GetCustomAttribute<ScopedAttribute>() != null)
            {
                return (ServiceLifetime.Scoped, false);
            }

            if (type.GetCustomAttribute<SelfScopedAttribute>() != null)
            {
                return (ServiceLifetime.Scoped, true);
            }

            if (type.GetCustomAttribute<TransientAttribute>() != null)
            {
                return (ServiceLifetime.Transient, false);
            }

            if (type.GetCustomAttribute<SelfTransientAttribute>() != null)
            {
                return (ServiceLifetime.Transient, true);
            }

            return (null, false);
        }
    }
}