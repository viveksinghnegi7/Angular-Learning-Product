using Demo.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Repos.Contract
{
    public interface IUserRepository //: IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetById(int id);
        Task<User> Authenticate(string email, string password);
        Task<User> RegisterUser(User user, string password);
        Task<User> CreateUser(User user);
        Task<User> UpdateUser(User user);

        Task<User> DeleteUser(int userId);



    }
}
