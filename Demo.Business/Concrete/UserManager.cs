using System;
using Demo.Business.Contract;
using Demo.Entities;
using Demo.Repos.Contract; 
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Demo.Utilities;

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
        public async Task<User> Authenticate(string email, string password)
        {

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user =await _userRepository.Authenticate(email,password);

            // return null if user not found
            if (user == null)
                return null;

            return user; 
        }


        public async Task<User> RegisterUser(User user, string password)
        {
            return await _userRepository.RegisterUser(user,password);
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<User> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await _userRepository.UpdateUser(user); 
        }

        public async Task<User> DeleteUser(int userId)
        {
            return await _userRepository.DeleteUser(userId);

        } 
    }
}
