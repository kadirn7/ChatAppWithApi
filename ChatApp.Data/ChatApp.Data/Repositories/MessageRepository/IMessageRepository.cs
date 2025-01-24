using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities;

namespace ChatApp.Data.Repositories.MessageRepository
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
    }
}
