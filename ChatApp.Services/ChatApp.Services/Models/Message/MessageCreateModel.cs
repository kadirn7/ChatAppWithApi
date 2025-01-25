using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Models.Message
{
    public record MessageCreateModel
    {
        public string Content { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }
    }
}
