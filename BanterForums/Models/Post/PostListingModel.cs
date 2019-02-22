using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanterForums.Models.Post
{
    public class PostListingModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int AuthoRating { get; set; }
    }
}
