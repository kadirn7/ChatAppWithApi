using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Models
{
    public class TokenModel
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string SigninKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

    }
}
