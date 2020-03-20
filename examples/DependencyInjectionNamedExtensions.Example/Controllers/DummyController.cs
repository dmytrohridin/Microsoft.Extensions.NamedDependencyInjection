using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionNamedExtensions.Example.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjectionNamedExtensions.Example.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DummyController : ControllerBase
    {
        private readonly IServiceProvider provider;

        public DummyController(IServiceProvider provider)
        {
            this.provider = provider;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            var array = new[] 
            { 
                provider.GetService<IDataService, int>(key => key == 0).Get(),
                provider.GetService<IDataService, int>(1).Get()
            };
            return array;
        }
    }
}
