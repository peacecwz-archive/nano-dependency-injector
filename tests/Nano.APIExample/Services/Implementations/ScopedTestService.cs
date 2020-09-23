using System;

namespace Nano.APIExample.Services.Implementations
{
    [Scoped]
    public class ScopedTestService : IScopedTestService
    {
        public ScopedTestService()
        {
            Console.WriteLine("Created scoped instance");
        }

        public string Test()
        {
            return "Scoped";
        }
    }
}