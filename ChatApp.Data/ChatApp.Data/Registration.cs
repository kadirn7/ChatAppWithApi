using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatApp.Data.Entities.Db;
using ChatApp.Data.Repositories;
using ChatApp.Data.Repositories.GroupRepository;
using ChatApp.Data.Repositories.MessageRepository;
using ChatApp.Data.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Data
{
    public static class Registration
    {
        public static void AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatAppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
        }
    }

}
