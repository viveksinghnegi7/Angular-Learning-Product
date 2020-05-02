using Demo.Repos.GenericRepository;
using Demo.Repos.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Entities;
using Demo.Repos.Models;

namespace Demo.Repos.Concrete
{
    public class UserRepository : GenericRepository<User, DemoContext>, IUserRepository
    {
        DemoContext _demoContext;
        public UserRepository(DemoContext demoContext) : base(demoContext)
        {
            _demoContext = demoContext;
        }

        public async Task<User> CreateUsers(User user)
        {
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
    }
}
