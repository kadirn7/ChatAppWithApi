using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities;
using ChatApp.Data.Models;
using ChatApp.Data.Repositories.UserRepository;

namespace ChatApp.Services.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddAsync(User entity)
        {
            return await _userRepository.AddAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            return await _userRepository.CountAsync();
        }

        public async Task<User> DeleteAsync(User entity)
        {
            return await _userRepository.DeleteAsync(entity);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetByUsernameAndPasswordAsync(string username, string password)
        {
            return await _userRepository.GetByUsernameAndPasswordAsync(username, password);
        }



        public async Task<List<User>> GetUsersByNameAsync(string name)
        {
            return await _userRepository.GetUsersByNameAsync(name);
        }

        public async Task<IReadOnlyList<User>> ListAllAsync(PaginationModel paginationModel)
        {
            return await _userRepository.ListAllAsync(paginationModel);
        }

        public async Task<User> UpdateAsync(User entity)
        {
            return await _userRepository.UpdateAsync(entity);
        }
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
    }
}
