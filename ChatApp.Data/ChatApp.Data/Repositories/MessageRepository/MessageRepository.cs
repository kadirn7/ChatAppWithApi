using ChatApp.Data.Entities;
using ChatApp.Data.Entities.Db;
using ChatApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

       

        public async Task<List<Message>> GetPrivateMessageHistory(MessageHistoryModel messageHistoryModel)
        {
            var senderUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == messageHistoryModel.SenderUsername);
            var receiverUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == messageHistoryModel.ReceiverUsername);

            if (senderUser == null || receiverUser == null)
            {
                throw new Exception("Sender or receiver user not found");
            }

            return await _context.Messages
                .AsNoTracking()
                .Where(m => (m.UserId == senderUser.Id && m.ReceiverUserId == receiverUser.Id) ||
                             (m.UserId == receiverUser.Id && m.ReceiverUserId == senderUser.Id))
                .OrderByDescending(m => m.CreatedAt)
                .Take(20) // İsteğe bağlı olarak pagination için parametrik hale getirilebilir
                .ToListAsync();
        }

        public async Task<List<Message>> GetGroupMessageHistory(MessageHistoryModel messageHistoryModel)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.Name == messageHistoryModel.GroupName);
            if (group == null)
            {
                throw new Exception($"Group '{messageHistoryModel.GroupName}' not found");
            }

            return await _context.Messages
                .AsNoTracking()
                .Where(m => m.GroupId == group.Id)
                .OrderByDescending(m => m.CreatedAt)
                .Take(20) // İsteğe bağlı olarak pagination için parametrik hale getirilebilir
                .ToListAsync();
        }
    }
}

