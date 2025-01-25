using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities;

namespace ChatApp.Data.Repositories.UserRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public Task<User> GetByUsernameAndPasswordAsync(string username,string password);
    }
}
