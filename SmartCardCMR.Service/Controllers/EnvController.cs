using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace SmartCardCRM.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;

        public EnvController(IConfiguration config, IWebHostEnvironment env)
        {
            _config = config;
            _env = env;
        }

        [HttpGet]
        public ActionResult<string> GetEnv()
        {
            return _config.GetConnectionString("SmartCardCRM") + " " + _env.EnvironmentName;
        }
    }
}
