using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nano.DependencyInjector
{
    public static class NanoDependencyInjectorExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, Assembly assembly)
        {
            var dependencies = DependencyInjectionConfigure.GetDependencies(assembly);

            foreach (var dependency in dependencies)
            {
                services.Add(
                    new ServiceDescriptor(dependency.InterfaceType, dependency.Implementation, dependency.Type));
            }

            return services;
        }
    }
}