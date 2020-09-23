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
            foreach (Type type in assembly.GetTypes())
            {
                var injectionType = GetInjectionType(type);

                if (!injectionType.HasValue)
                {
                    continue;
                }

                dependencies.Add(new DependencyInjectionServiceType()
                {
                    LifeTime = injectionType.Value,
                    ImplementationType = type,
                    InterfaceType =
                        type.GetInterfaces()[
                            0] // TODO (peacecwz): Detect multiple interface and interface inject selection
                });
            }

            return dependencies;
        }

        private static ServiceLifetime? GetInjectionType(Type type)
        {
            if (type.GetCustomAttribute<SingletonAttribute>() != null)
            {
                return ServiceLifetime.Singleton;
            }

            if (type.GetCustomAttribute<ScopedAttribute>() != null)
            {
                return ServiceLifetime.Scoped;
            }

            if (type.GetCustomAttribute<TransientAttribute>() != null)
            {
                return ServiceLifetime.Transient;
            }

            return null;
        }
    }
}