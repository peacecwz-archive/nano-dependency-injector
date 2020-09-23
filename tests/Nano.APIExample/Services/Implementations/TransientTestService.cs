using System;

namespace Nano.APIExample.Services.Implementations
{
    [Transient]
    public class TransientTestService : ITransientTestService
    {
        public TransientTestService()
        {
            Console.WriteLine("Created transient instance");
        }

        public string Test()
        {
            return "Transient";
        }
    }
}