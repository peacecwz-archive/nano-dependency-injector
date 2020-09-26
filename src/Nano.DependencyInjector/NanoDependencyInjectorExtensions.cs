using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Nano.DependencyInjector
{
    public static class NanoDependencyInjectorExtensions
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services, Assembly assembly)
        {
            DependencyInjectionConfigure.GetDependencies(assembly)
                .If(d => d.HasPropertyInjectAttribute())
                .True(services.AddPropertyInjectableTypes)
                .False(f => services.Add(
                    new ServiceDescriptor(f.InterfaceType, f.ImplementationType, f.LifeTime)));
            return services;
        }

        private static void SetPropertyValue(IServiceProvider provider, object instance)
        {
            var injectableProperties = instance.GetType().GetProperties()
                .Where(p => p.GetCustomAttribute<InjectAttribute>() != null)
                .ToList();
            foreach (var property in injectableProperties)
            {
                var value = provider.GetService(property.PropertyType);
                property.SetValue(instance, value);
            }
        }

        private static object CreateInstance(IServiceProvider provider, Type type)
        {
            object instance;
            ConstructorInfo publicConstructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public,
                null, new[] {typeof(string)}, null);
            
            if (publicConstructor == null)
            {
                instance = System.Runtime.Serialization.FormatterServices
                    .GetUninitializedObject(type);
                SetPropertyValue(provider, instance);
                return instance;
            }

            var constructorPareParameters = publicConstructor.GetParameters();

            if (constructorPareParameters.Any())
            {
                var parameterInstances = constructorPareParameters
                    .Select(parameter => provider.GetRequiredService(parameter.ParameterType)).ToList();
                instance = Activator.CreateInstance(type, parameterInstances);
            }
            else
            {
                instance = Activator.CreateInstance(type);
            }

            SetPropertyValue(provider, instance);
            return instance;
        }

        internal static void AddPropertyInjectableTypes(this IServiceCollection services,
            DependencyInjectionServiceType dependency)
        {
            switch (dependency.LifeTime)
            {
                case ServiceLifetime.Scoped:
                    services.AddScoped(dependency.InterfaceType,
                        provider => CreateInstance(provider, dependency.ImplementationType));
                    break;
                case ServiceLifetime.Singleton:
                    services.AddSingleton(dependency.InterfaceType,
                        provider => CreateInstance(provider, dependency.ImplementationType));
                    break;
                case ServiceLifetime.Transient:
                    services.AddTransient(dependency.InterfaceType,
                        provider => CreateInstance(provider, dependency.ImplementationType));
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        internal static bool HasPropertyInjectAttribute(this DependencyInjectionServiceType dependency)
        {
            return dependency.ImplementationType.GetProperties()
                .Any(p => p.GetCustomAttribute<InjectAttribute>() != null);
        }
    }
}