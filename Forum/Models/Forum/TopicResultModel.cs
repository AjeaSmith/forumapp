using System;
using System.Collections.Generic;

namespace Forum.Models.Forum
{
    public class TopicResultModel
    {
        public ForumListingModel Forum { get; set; }
        public IEnumerable<ForumData.Models.Post> Posts { get; set; }
    }
}
