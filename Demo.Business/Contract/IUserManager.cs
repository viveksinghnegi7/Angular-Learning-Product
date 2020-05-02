using Demo.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Contract
{
   public interface IUserManager
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetById(int id); 
        Task<Users> CreateUserRecord(Users userDetails);

    }
}
