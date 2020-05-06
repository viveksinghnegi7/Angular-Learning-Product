using Demo.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Contract
{
   public interface IUserManager
    {
        Task<User> Authenticate(string username, string password);
        Task<User> RegisterUser(User user, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);  

    }
}
