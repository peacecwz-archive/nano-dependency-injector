using System;
using Microsoft.Extensions.DependencyInjection;

namespace Nano.DependencyInjector
{
    internal class DependencyInjectionServiceType
    {
        public Type Implementation { get; set; }
        public Type InterfaceType { get; set; }
        public ServiceLifetime Type { get; set; }
    }
}