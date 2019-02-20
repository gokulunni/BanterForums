﻿using System;

namespace BanterForums.Data.Models
{
    public class PostReply
    {
        int Id { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Post Post { get; set; }

    }
}