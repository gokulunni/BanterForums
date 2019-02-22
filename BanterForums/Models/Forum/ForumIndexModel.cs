using System;
using System.Collections.Generic;
using System.Text;

namespace BanterForums.Models.Forum
{
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}
