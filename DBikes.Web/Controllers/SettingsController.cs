using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBikes.Web.SettingsOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DBikes.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {

        private readonly MySettingsOptions mySettingsOptions;

        public SettingsController(MySettingsOptions options)
        {
            mySettingsOptions = options;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(mySettingsOptions);
        }
    }
}
