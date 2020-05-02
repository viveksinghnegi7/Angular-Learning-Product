using Demo.Business.Contract;
using Demo.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Demo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("controller")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserManager _userManager; 
        public UsersController(ILogger<UsersController> logger, IUserManager userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            _logger.LogInformation("Authenticate Called");
            var user = _userManager.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userManager.GetAll();
            return Ok(users);
        }
         
    } 
}
