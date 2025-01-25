using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ChatApp.Data.Entities;
using ChatApp.Services.Models.Group;
using ChatApp.Services.Models.Message;
using ChatApp.Services.Models.User;

namespace ChatApp.Services.Mapping
{
    public class MyProfiles: Profile
    {
        public MyProfiles()
        {
            CreateMap<User, UserCreateModel>().ReverseMap();
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, UserUpdateModel>().ReverseMap();

            CreateMap<Message, MessageModel>().ReverseMap();
            CreateMap<Message, MessageCreateModel>().ReverseMap();
            CreateMap<Message, MessageUpdateModel>().ReverseMap();

            CreateMap<Group, GroupModel>().ReverseMap();
            CreateMap<Group, GroupCreateModel>().ReverseMap();
            CreateMap<Group, GroupUpdateModel>().ReverseMap();

        }
    }
}
