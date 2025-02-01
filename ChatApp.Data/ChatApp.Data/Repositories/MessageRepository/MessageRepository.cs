using ChatApp.Data.Entities;
using ChatApp.Data.Entities.Db;
using ChatApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data.Repositories.MessageRepository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IGenericRepository<Message> _genericRepository;
        private readonly ChatAppDbContext _context;

        public MessageRepository(IGenericRepository<Message> genericRepository, ChatAppDbContext context)
        {
            _genericRepository = genericRepository;
            _context = context;
        }

        public async Task<Message> AddAsync(Message entity)
        {
            return await _genericRepository.AddAsync(entity);
        }

        public async Task<int> CountAsync()
        {
            return await _genericRepository.CountAsync();
        }

        public async Task<Message> DeleteAsync(Message entity)
        {
            return await _genericRepository.DeleteAsync(entity);
        }
 

        public async Task<Message> UpdateAsync(Message entity)
        {
            return await _genericRepository.UpdateAsync(entity);
        }

        async Task<Message> IGenericRepository<Message>.GetByIdAsync(int id)
        {
            return await _genericRepository.GetByIdAsync(id);
            
        }

        async Task<IReadOnlyList<Message>> IGenericRepository<Message>.ListAllAsync(PaginationModel paginationModel)
        {
            return await _genericRepository.ListAllAsync(paginationModel);
        }

        public async Task<List<Message>> GetMessageHistory(MessageHistoryModel messageHistoryModel)
        {
            var senderUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == messageHistoryModel.SenderUsername);
            var receiverUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == messageHistoryModel.ReceiverUsername);

            if (senderUser == null || receiverUser == null)
            {
                throw new Exception("Sender or receiver user not found");
            }

            var query = _context.Messages.AsNoTracking();

            if (messageHistoryModel.MessageForPrivateChat)
            {
                query = query.Where(m => m.UserId == senderUser.Id && m.ReceiverUserId == receiverUser.Id);
            }
            else
            {
                if (string.IsNullOrEmpty(messageHistoryModel.GroupName) || messageHistoryModel.GroupName.ToLower() == "null")
                {
                    throw new Exception("Group name is required for non-private messages");
                }

                var receiverGroup = await _context.Groups.FirstOrDefaultAsync(g => g.Name == messageHistoryModel.GroupName);
                if (receiverGroup == null)
                {
                    throw new Exception($"Group '{messageHistoryModel.GroupName}' not found");
                }

                query = query.Where(m => m.UserId == senderUser.Id && m.GroupId == receiverGroup.Id);
            }

            var queryResult = await query.OrderByDescending(q => q.CreatedAt).Take(20).ToListAsync();
            return queryResult;
        }
    }
}

