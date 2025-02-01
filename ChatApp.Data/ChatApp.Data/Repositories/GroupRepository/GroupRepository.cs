using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities;
using ChatApp.Data.Entities.Db;
using ChatApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repositories.GroupRepository
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IGenericRepository<Group> _genericRepository;
        private readonly ChatAppDbContext _context;
        public GroupRepository(IGenericRepository<Group> genericRepository, ChatAppDbContext context)
        {
            _genericRepository = genericRepository;
            _context = context;
        }

        public async Task<Group> GetByIdAsync(int id)
        {
           return await _genericRepository.GetByIdAsync(id);
        }

        public async Task<IReadOnlyList<Group>> ListAllAsync(PaginationModel paginationModel)
        {
           return await _genericRepository.ListAllAsync(paginationModel);
        }

        public async Task<Group> AddAsync(Group entity)
        {
            return await _genericRepository.AddAsync(entity);
        }

        public async Task<Group> UpdateAsync(Group entity)
        {
            return await _genericRepository.UpdateAsync(entity);
        }

        public async Task<Group> DeleteAsync(Group entity)
        {
            return await _genericRepository.DeleteAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            return await  _genericRepository.CountAsync();
        }
        public  async  Task<Group> GetGroupByNameAsync(string name)
        {
            return await _context.Groups.FirstOrDefaultAsync(u => u.Name == name);
        }
    }
}
