using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Data.Entities
{
    public class User:BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string? AvatarPath { get; set; }
        public string? Role { get; set; }

        public string? ConnectionId { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Group> Groups { get; set; }

    }
}
