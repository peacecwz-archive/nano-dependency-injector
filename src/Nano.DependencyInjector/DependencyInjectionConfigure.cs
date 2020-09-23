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
                    Type = injectionType.Value,
                    Implementation = type,
                    InterfaceType =
                        type.GetInterfaces()[
                            0] // TODO (peacecwz): Detect multiple interface and interface inject selection
                });
            }

            return dependencies;
        }

        private static ServiceLifetime? GetInjectionType(Type type)
        {
            if (type.GetCustomAttributes(typeof(SingletonAttribute), true).Length > 0)
            {
                return ServiceLifetime.Singleton;
            }

            if (type.GetCustomAttributes(typeof(ScopedAttribute), true).Length > 0)
            {
                return ServiceLifetime.Scoped;
            }

            if (type.GetCustomAttributes(typeof(TransientAttribute), true).Length > 0)
            {
                return ServiceLifetime.Transient;
            }

            return null;
        }
    }
}