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
        [HttpGet]
        public async Task<ReturnModel> GetMessages([FromQuery] PaginationModel paginationModel)
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
        public async Task<ReturnModel> GetMessage(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
            {
                return new ReturnModel
                {
                    Success = false,
                    Message = "Message not found",
                    StatusCode = 404
                };
            }
            return new ReturnModel
            {
                Success = true,
                Message = "Message fetched successfully",
                Data = message,
                StatusCode = 200
            };
        }
        [HttpPost]
        public async Task<ReturnModel> CreateMessage([FromBody] MessageCreateModel messageCreateModel)
        {
            var message = _mapper.Map<Message>(messageCreateModel);
            var messageResult = await _messageService.AddAsync(message);
            return new ReturnModel
            {
                Success = true,
                Message = "Message created successfully",
                Data = messageResult,
                StatusCode = 201
            };
        }
        [HttpPut]
        public async Task<ReturnModel> UpdateMessage([FromBody] MessageUpdateModel messageUpdateModel)
        {
            var message = _mapper.Map<Message>(messageUpdateModel);
            var messageResult = await _messageService.UpdateAsync(message);
            return new ReturnModel
            {
                Success = true,
                Message = "Message updated successfully",
                Data = messageResult,
                StatusCode = 200
            };
        }
        [HttpDelete("{id}")]
        public async Task<ReturnModel> DeleteMessage(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
            {
                return new ReturnModel
                {
                    Success = false,
                    Message = "Message not found",
                    StatusCode = 404
                };
            }
            await _messageService.DeleteAsync(message);
            return new ReturnModel
            {
                Success = true,
                Message = "Message deleted successfully",
                StatusCode = 200
            };
        }
    }
}
