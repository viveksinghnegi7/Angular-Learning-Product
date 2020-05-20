using System;
using Demo.Repos.GenericRepository;
using Demo.Repos.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Entities;
using Demo.Repos.Models;
using Demo.Utilities;
using System.Linq;
namespace Demo.Repos.Concrete
{
    public class UserRepository : GenericRepository<User, DemoContext>, IUserRepository
    {
        readonly  DemoContext _demoContext;

        #region Constructor
        public UserRepository(DemoContext demoContext) : base(demoContext)
        {
            _demoContext = demoContext;
        }
        #endregion

        #region Public Methods
        public async Task<User> Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return null;

            var user = _demoContext.Users.SingleOrDefault(x => x.Email == email);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }

        public async Task<User> RegisterUser(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");

            if (_demoContext.Users.Any(x => x.Email == user.Email))
                throw new AppException("Username \"" + user.Email + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _demoContext.Users.Add(user);
            _demoContext.SaveChanges();

            //return user;
            return await base.Add(user);
        }


        async Task<IEnumerable<User>> IUserRepository.GetAllUsers()
        {
            return await base.GetAll();
        }

        async Task<User> IUserRepository.GetById(int id)
        {
            return await base.Get(id);
        }
        public async Task<User> CreateUser(User user)
        {
            return await base.Add(user);
        }

        public async Task<User> UpdateUser(User user)
        {
            return await base.Update(user); 
        }

        public async Task<User> DeleteUser(int userId)
        {
            return await base.Delete(userId);

        }
        #endregion


        #region Private Methods
        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        } 
        #endregion
    }
}
