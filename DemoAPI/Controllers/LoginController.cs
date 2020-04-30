using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("controller/api")]
    public class LoginController:ControllerBase
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}
