using Demo.Business.Contract;
using Demo.Entities;
using Demo.Repos.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Concrete
{
    public class UserManager: IUserManager
    {
        readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        } 
        public async Task<IEnumerable<Users>> GetAll()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<Users> GetById(int id)
        {
            return await _userRepository.GetById(id);
        } 

        public async Task<Users> CreateUserRecord(Users userDetails)
        {
            return await _userRepository.CreateUsers(userDetails);
        }
    }
}
