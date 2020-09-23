using System;

namespace Nano.APIExample.Services.Implementations
{
    [Singleton]
    public class SingletonTestService : ISingletonTestService
    {
        public SingletonTestService()
        {
            Console.WriteLine("Created singleton instance");
        }

        public string Test()
        {
            return "Singleton";
        }
    }
}