using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities;
using ChatApp.Data.Models;

namespace ChatApp.Data.Repositories.MessageRepository
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        

        public Task<List<Message>> GetPrivateMessageHistory(MessageHistoryModel messageHistoryModel);
        public Task<List<Message>> GetGroupMessageHistory(MessageHistoryModel messageHistoryModel);

    }
}
