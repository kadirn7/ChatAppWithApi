using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Models.User
{
    public record UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
       // public string Password { get; set; }
        //public string Email { get; set; }
        public string FullName { get; set; }
        public string ConnectionId { get;set; }
    }
}
