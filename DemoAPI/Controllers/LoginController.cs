using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Business.Contract;
using Demo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.API.Controllers
{
    [ApiController]
    [Route("controller/api")]
    public class LoginController:ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IUserManager _userManager;

        public LoginController(ILogger<LoginController> logger, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("login")]
        public Task<IEnumerable<Users>> Login()
        {
            var result = _userManager.GetAll();
            return result;
        }
    }
}
