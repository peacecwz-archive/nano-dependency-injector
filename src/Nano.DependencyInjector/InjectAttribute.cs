using System;
using System.Collections.Generic;
using System.Text;

namespace Nano.DependencyInjector
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class InjectAttribute : Attribute
    {

    }
}
