using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities;

namespace ChatApp.Data.Repositories.GroupRepository
{
    public interface IGroupRepository : IGenericRepository<Group>
    {
        public Task<Group> GetGroupByNameAsync(string name);
    }
}
