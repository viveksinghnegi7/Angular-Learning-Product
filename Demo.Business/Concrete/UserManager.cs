using System;
using Demo.Business.Contract;
using Demo.Entities;
using Demo.Repos.Contract;
using Demo.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Demo.Business.Concrete
{
    public class UserManager: IUserManager
    {
        readonly IUserRepository _userRepository;
        private readonly AppSettings _appSettings;
        public UserManager(IUserRepository userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;

        }
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<User> _users = new List<User>
        {
            new User { UserId = 1, FirstName = "Vivek", LastName = "Negi", Email = "viveksinghnegi7@gmail.com", Password = "12345" }
        };
        public User Authenticate(string email, string password)
        {
            var user = _users.SingleOrDefault(x => x.Email == email && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        } 

        public async Task<User> CreateUserRecord(User userDetails)
        {
            return await _userRepository.CreateUsers(userDetails);
        } 
    }
}
