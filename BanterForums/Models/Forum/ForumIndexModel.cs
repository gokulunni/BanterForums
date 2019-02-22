using System;
using System.Collections.Generic;
using System.Text;

namespace BanterForums.Models.Forum
{   
    
    //represents an index of topics with a main topic (i.e. topics within a programming language category)
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}
