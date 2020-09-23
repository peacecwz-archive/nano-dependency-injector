using System;
using Microsoft.Extensions.DependencyInjection;

namespace Nano.DependencyInjector
{
    internal class DependencyInjectionServiceType
    {
        public Type ImplementationType { get; set; }
        public Type InterfaceType { get; set; }
        public ServiceLifetime LifeTime { get; set; }
    }
}