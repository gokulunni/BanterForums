using System;
using System.Collections.Generic;
using System.Text;

namespace BanterForums.Models.Forum
{ //Each Forum listed within a Forum Index is display using this class ("Sub-forum")
    public class ForumListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
