using Microsoft.AspNetCore.Mvc;
using Nano.APIExample.Services;

namespace Nano.APIExample.Controllers
{
    [Route("v1/test")]
    public class TestController : Controller
    {
        private readonly ISingletonTestService _singletonTestService;
        private readonly IScopedTestService _scopedTestService;
        private readonly ITransientTestService _transientTestService;

        public TestController(ISingletonTestService singletonTestService, IScopedTestService scopedTestService,
            ITransientTestService transientTestService)
        {
            _singletonTestService = singletonTestService;
            _scopedTestService = scopedTestService;
            _transientTestService = transientTestService;
        }

        [HttpGet("singleton")]
        public string GetSingleton()
        {
            return _singletonTestService.Test();
        }

        [HttpGet("scoped")]
        public string GetScoped()
        {
            return _scopedTestService.Test();
        }

        [HttpGet("transient")]
        public string GetTransient()
        {
            return _transientTestService.Test();
        }
    }
}