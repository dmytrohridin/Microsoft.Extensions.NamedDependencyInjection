using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependecyInjectionNamedExtensions.Example.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DependecyInjectionNamedExtensions.Example.Controllers
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
                provider.GetService<IDataService, string>("DataServiceA").Get(),
                provider.GetService<IDataService, string>("DataServiceB").Get()
            };
            return array;
        }
    }
}
