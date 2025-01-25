using AutoMapper;
using ChatApp.Data.Entities;
using ChatApp.Data.Models;
using ChatApp.Models;
using ChatApp.Services.Models.Group;
using ChatApp.Services.Models.Message;
using ChatApp.Services.Services.GroupService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IMapper mapper, IGroupService groupService)
        {
            _mapper = mapper;
            _groupService = groupService;
        }
        [HttpGet]
        public async Task<ReturnModel> GetGroup([FromQuery] PaginationModel paginationModel)
        {
            var messages = await _groupService.ListAllAsync(paginationModel);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<List<GroupModel>>(messages),
                StatusCode = 200,
                TotalCount = await _groupService.CountAsync()
            };
        }
        [HttpGet("{id}")]
        public async Task<ReturnModel> GetGroupById(int id)
        {
            var message = await _groupService.GetByIdAsync(id);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<GroupModel>(message),
                StatusCode = 200
            };
        }
        [HttpPost]
        public async Task<ReturnModel> CreateGroup([FromBody] GroupCreateModel groupCreateModel)
        {
            var group = _mapper.Map<Group>(groupCreateModel);
            var Result = await _groupService.AddAsync(group);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = Result,
                StatusCode = 201
            };
        }
        [HttpPut]
        public async Task<ReturnModel> UpdateGroup([FromBody] GroupUpdateModel groupUpdateModel)
        {
            var group = _mapper.Map<Group>(groupUpdateModel);
            var Result = await _groupService.UpdateAsync(group);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = Result,
                StatusCode = 200
            };
        }
        [HttpDelete("{id}")]
        public async Task<ReturnModel> DeleteGroup(int id)
        {

            var group = await _groupService.GetByIdAsync(id);
            await _groupService.DeleteAsync(group);
            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = group,
                StatusCode = 200
            };
        }
        
        }
    }
