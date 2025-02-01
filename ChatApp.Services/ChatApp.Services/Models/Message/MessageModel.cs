﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services.Models.Message
{
    public record MessageModel
    {
        public string Content { get; set; }
        public int? UserId { get; set; }
        public int? GroupId { get; set; }


        public int? ReceiverUserId { get; set; }
        public int? ReceiverGroupId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Type { get; set; }
    }
}
