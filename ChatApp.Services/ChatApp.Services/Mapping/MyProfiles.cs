using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.Services.Models.User;
namespace ChatApp.Services.Mapping
{
    public class MyProfiles:Profile
    {
        public MyProfiles()
        {
            CreateMap<ChatApp.Data.Entities.User, UserCreateModel>().ReverseMap();
            
        }
    }
}
