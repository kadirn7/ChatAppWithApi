using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities;

namespace ChatApp.Services.Services.UserService
{
    public interface IUserService:IGenericService<User>
    {
        public Task<User> GetByUsernameAndPasswordAsync(string username, string password);
        public Task<List<User>> GetUsersByNameAsync(string name);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
