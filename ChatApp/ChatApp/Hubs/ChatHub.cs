using ChatApp.Data.Entities;
using ChatApp.Services.Services.GroupService;
using ChatApp.Services.Services.MessageService;
using ChatApp.Services.Services.UserService;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub : Hub
{
    private readonly IDictionary<string, string> _connectedUsers;
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public ChatHub(IDictionary<string, string> connectedUsers, IMessageService messageService, IUserService userService, IGroupService groupService)
        {
            _connectedUsers = connectedUsers;
            _messageService = messageService;
            _userService = userService;
            _groupService = groupService;
        }

        public override async Task OnConnectedAsync()
    {


        HttpContext context = Context.GetHttpContext();
        var token = context.Request.Headers["Authorization"];
        var user = Context.User.Identity.Name;
        if (!_connectedUsers.ContainsKey(user))
            _connectedUsers.TryAdd(user, Context.ConnectionId);
        else
        {
            _connectedUsers[user] = Context.ConnectionId;
        }

        await Clients.Caller.SendAsync("UserConnected", Context.ConnectionId);
    }
    public async Task SendMessageToAll(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessageFromAll", user, message);
    }
    public async Task SendMessageToUser(string username, string message)
    {
        var userExists = _connectedUsers.TryGetValue(username, out var connectionId);
        var user = Context.User.Identity.Name;
        var userSender = await _userService.GetUserByUsernameAsync(user);
        var userReceiver = await _userService.GetUserByUsernameAsync(username);
        var newMessage = new Message
        {
            Content = message,
            UserId = userSender.Id,
            CreatedAt = DateTime.Now,
            GroupId = null,
            IsDeleted = false,
            UpdatedAt = DateTime.Now,
            ReceiverUserId = userReceiver.Id,
            ReceiverGroupId = null
        };
        if (userExists)
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessageFromUser", user, message);
        }
        else
        {
            await Clients.Caller.SendAsync("ReceiveMessageFromUser", "System", "User is not connected");
        }
        await _messageService.AddAsync(newMessage);

    }
        public async Task SendMessageToGroup(string group, string message)
        {
            var user = Context.User.Identity.Name;
            var userSender = await _userService.GetUserByUsernameAsync(user);
            var groupEntity = await _groupService.GetGroupByNameAsync(group);

            if (groupEntity != null)
            {
                var newMessage = new Message
                {
                    Content = message,
                    UserId = userSender.Id,
                    CreatedAt = DateTime.Now,
                    GroupId = groupEntity.Id,
                    IsDeleted = false,
                    UpdatedAt = DateTime.Now,
                    ReceiverUserId = null,
                    ReceiverGroupId = groupEntity.Id
                };

                await _messageService.AddAsync(newMessage);
                await Clients.Group(group).SendAsync("ReceiveMessageFromGroup", user, message);
            }
            else
            {
                await Clients.Caller.SendAsync("ReceiveMessageFromGroup", "System", "Group not found");
            }
        }
    }
} 