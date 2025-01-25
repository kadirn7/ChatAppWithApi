using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Models.Group
{
    public record GroupModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
