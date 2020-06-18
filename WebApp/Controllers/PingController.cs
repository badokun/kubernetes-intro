using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            const string version = "V6";
            return Ok(new
            {
                version,
                nodeName = Environment.GetEnvironmentVariable("MY_NODE_NAME"),
                podName = Environment.GetEnvironmentVariable("MY_POD_NAME"),
                podNamespace = Environment.GetEnvironmentVariable("MY_POD_NAMESPACE"),
                podId = Environment.GetEnvironmentVariable("MY_POD_IP"),
                exchange = Environment.GetEnvironmentVariable("EXCHANGE"),
                apiKey = Environment.GetEnvironmentVariable("API_KEY")
            });
        }
    }
     
}
