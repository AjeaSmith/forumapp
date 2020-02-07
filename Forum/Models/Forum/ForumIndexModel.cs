using System;
using System.Collections.Generic;

namespace Forum.Models.Forum
{
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel> forums { get; set; }
    }
}
