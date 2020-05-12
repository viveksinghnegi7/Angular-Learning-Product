using System;
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
    [Route("controller")]
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
                var user =await _userManager.Authenticate(model.Email, model.Password);

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
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }



        [HttpGet("users")] 
        public async Task<IActionResult> GetAll()
        {
            var users =await _userManager.GetAll();
            return Ok(users);
        }

    }
}
