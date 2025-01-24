using AutoMapper;
using ChatApp.Data.Entities;
using ChatApp.Data.Models;
using ChatApp.Models;
using ChatApp.Services.Models.User;
using ChatApp.Services.Services.UserService;

using Microsoft.AspNetCore.Mvc;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ReturnModel> GetUsers([FromQuery] PaginationModel paginationModel)
        {
            var users = await _userService.ListAllAsync(paginationModel);

            return new ReturnModel
            {
                Success = true,
                Message = "Success",
                Data = _mapper.Map<List<UserModel>>(users),
                StatusCode = 200,
                TotalCount = await _userService.CountAsync()
            };
        }
        [HttpGet("{id}")]
        public async Task<ReturnModel> GetUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return new ReturnModel
                {
                    Success = false,
                    Message = "User not found",
                    StatusCode = 404
                };
            }
            return new ReturnModel
            {
                Success = true,
                Message = "User fetched successfully",
                Data = user,
                StatusCode = 200
            };
        }
        [HttpPost]
        public async Task<ReturnModel> CreateUser([FromBody] UserCreateModel userCreateModel)
        {
            var newUser = _mapper.Map<User>(userCreateModel);
            await _userService.AddAsync(newUser);
            return new ReturnModel
            {
                Success = true,
                Message = "User created successfully",
                Data = newUser,
                StatusCode = 201
            };
        }
        [HttpPut]
        public async Task<ReturnModel> UpdateUser([FromBody] UserUpdateModel userUpdateModel)
        {
            var user = _mapper.Map<User>(userUpdateModel);
            var updatedUser = await _userService.UpdateAsync(user);
            return new ReturnModel
            {
                Success = true,
                Message = "User updated successfully",
                Data = updatedUser,
                StatusCode = 200
            };

        }
        [HttpDelete("{id}")]
        public async Task<ReturnModel> DeleteUser(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return new ReturnModel
                {
                    Success = false,
                    Message = "User not found",
                    StatusCode = 404
                };
            }
            await _userService.DeleteAsync(user);
            return new ReturnModel
            {
                Success = true,
                Message = "User deleted successfully",
                StatusCode = 200
            };
        }
    }
}
