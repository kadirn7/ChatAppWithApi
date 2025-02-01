using ChatApp.Data.Entities;
using ChatApp.Data.Models;

namespace ChatApp.Services.Services.MessageService
{
    public interface IMessageService : IGenericService<Message>
    {
       // Task<List<Message>> GetMessageHistory(MessageHistoryModel messageHistoryModel);

        Task<List<Message>> GetPrivateMessageHistory(MessageHistoryModel messageHistoryModel);
        Task<List<Message>> GetGroupMessageHistory(MessageHistoryModel messageHistoryModel);
    }
} 