using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities;
using ChatApp.Data.Entities.Db;
using ChatApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : BaseEntity
    {
        private readonly ChatAppDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ChatAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.FindAsync<T>(id);
            
        }
        public async Task<IReadOnlyList<T>> ListAllAsync(PaginationModel paginationModel)
        {
            if (paginationModel != null)
            {
                return await _dbSet.Skip((paginationModel.PageNumber - 1) * paginationModel.PageSize)
                    .Take(paginationModel.PageSize).ToListAsync();
            }
            return await _dbSet.Take(10).ToListAsync();  
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync()
        {
            
            return await _dbSet.CountAsync();
        }
    }
    
}
