using Demo.Repos.GenericRepository;
using Demo.Repos.Contract;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Entities;
using Demo.Repos.Models;

namespace Demo.Repos.Concrete
{
    public class UserRepository : GenericRepository<Users, DemoContext>, IUserRepository
    {
        DemoContext _demoContext;
        public UserRepository(DemoContext demoContext) : base(demoContext)
        {
            _demoContext = demoContext;
        }

        public async Task<Users> CreateUsers(Users user)
        {
            return await base.Add(user); 
        }


        async Task<IEnumerable<Users>> IUserRepository.GetAllUsers()
        {
            return await base.GetAll(); 
        }

        async Task<Users> IUserRepository.GetById(int id)
        {
            return await base.Get(id); 
        }
    }
}
