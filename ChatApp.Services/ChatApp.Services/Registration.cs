
using AutoMapper;
using ChatApp.Services.Services.GroupService;
using ChatApp.Services.Services.MessageService;
using ChatApp.Services.Services.UserService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Services
{
    public static class Registration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IMessageService, MessageService>();


            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping.MyProfiles());
               
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
