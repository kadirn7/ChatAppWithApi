using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Data.Entities
{
    public class Group:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? AvatarPath { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }

    }
}
