using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.Data.Entities;
using ChatApp.Services.Models.User;
namespace ChatApp.Services.Mapping
{
    public class MyProfiles:Profile
    {
        public MyProfiles()
        {
            CreateMap<User, UserCreateModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User,UserUpdateModel>().ReverseMap();
        }
    }
}
