using System;
using System.Collections.Generic;
using System.Text;

namespace BanterForums.Data.Models
{
    public class Post
    {
        int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Forum Forum { get; set; }
        public virtual IEnumerable<PostReply> Replies { get; set; }

    }
}
