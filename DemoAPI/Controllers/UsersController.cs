using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Demo.Business.Contract;
using Demo.Entities;
using Demo.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Demo.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserManager _userManager;
        private readonly IOptions<AppSettings> _appSettings;
        private IMapper _mapper;
        public UsersController(ILogger<UsersController> logger, IMapper mapper, IUserManager userManager, IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
            _appSettings = appSettings;

        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateUser model)
        {
            try
            {
                _logger.LogInformation("Authenticate Called");
                var user = await _userManager.Authenticate(model.Email, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Username or password is incorrect" });

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Value.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                // return basic user info and authentication token
                _logger.LogInformation("Authenticate Completed");
                return Ok(new
                {
                    Id = user.UserId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = tokenString
                });
            }
            catch (AppException exception)
            {
                _logger.LogError(exception.Message);
                _logger.LogError(exception.InnerException.Message , exception);                
                return BadRequest(new { message = exception.Message });
            }

        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody]Register register)
        {
            // map model to entity
            var user = _mapper.Map<User>(register);
            try
            {
                // create user
                _userManager.RegisterUser(user, register.Password);
                return Ok();
            }
            catch (AppException ex)
            {
                _logger.LogError(ex.Message);

                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET api/users
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "API status Running", "Success" };
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userManager.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            // map model to entity
            var newUser = _mapper.Map<User>(user);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = await _userManager.CreateUser(newUser);
            return Ok(entity);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var entity = await _userManager.UpdateUser(user);
            return Ok(entity);
        }


        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var entity = await _userManager.DeleteUser(userId);
            return Ok(entity);
        }
    }
}
