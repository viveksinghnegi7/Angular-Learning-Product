using Demo.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repos.Contract
{
    public interface IUserRepository //: IGenericRepository<User>
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetById(int id);

        Task<Users> CreateUsers(Users user);

    }
}
