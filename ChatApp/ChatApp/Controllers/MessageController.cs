using AutoMapper;
using ChatApp.Data.Entities;
using ChatApp.Data.Models;
using ChatApp.Models;
using ChatApp.Services.Models.Message;
using ChatApp.Services.Services.MessageService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;
        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet("GetMessageHistory")] // Tek action
        public async Task<ReturnModel> GetMessageHistory([FromQuery] MessageHistoryModel messageHistoryModel)
        {
            List<Message> messages = null;
            string messageType = null;

            if (!string.IsNullOrEmpty(messageHistoryModel.GroupName))
            {
                messages = await _messageService.GetGroupMessageHistory(messageHistoryModel);
                messageType = "G";
            }
            else if (!string.IsNullOrEmpty(messageHistoryModel.SenderUsername) && !string.IsNullOrEmpty(messageHistoryModel.ReceiverUsername))
            {
                messages = await _messageService.GetPrivateMessageHistory(messageHistoryModel);
                messageType = "P"; // Veya "S" ve "R" olarak ayırabilirsiniz
            }
            else
            {
                return new ReturnModel
                {
                    Success = false,
                    Message = "Either GroupName or both SenderUsername and ReceiverUsername must be provided.",
                    StatusCode = 400
                };
            }

            var messageModels = _mapper.Map<List<MessageModel>>(messages);
            messageModels.ForEach(m => m.Type = messageType); // Mesaj tipini işaretle

            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                StatusCode = 200,
                Data = messageModels
            };
        }

        [HttpGet]
        public async Task<ReturnModel> Get([FromQuery] PaginationModel paginationModel)
        {
            var messages = await _messageService.ListAllAsync(paginationModel);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<List<MessageModel>>(messages),
                StatusCode = 200,
                TotalCount = await _messageService.CountAsync()
            };
        }
        [HttpGet("{id}")]
        public async Task<ReturnModel> Get(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<MessageModel>(message),
                StatusCode = 200
            };
        }
        [HttpPost]
        public async Task<ReturnModel> Post([FromBody] MessageCreateModel messageModel)
        {
            
            var message = _mapper.Map<Message>(messageModel);

                // Grup mesajı: alıcı bilgisi grup üzerinden ayarlanır.
                message.ReceiverGroupId = messageModel.GroupId;
                
            

            var messageResult = await _messageService.AddAsync(message);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<MessageModel>(messageResult),
                StatusCode = 200
            };
        }
        [HttpPut]
        public async Task<ReturnModel> Put([FromBody] MessageUpdateModel messageModel)
        {
            var message = _mapper.Map<Message>(messageModel);
            var messageResult = await _messageService.UpdateAsync(message);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<MessageModel>(messageResult),
                StatusCode = 200
            };
        }
        [HttpDelete("{id}")]
        public async Task<ReturnModel> Delete(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            await _messageService.DeleteAsync(message);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                StatusCode = 200
            };
        }

    }
}