using System;
using System.Collections.Generic;
using Forum.Models.Post;

namespace Forum.Models.Home
{
    public class HomeIndexModel
    {
        public IEnumerable<PostListingModel> Latest { get; set; }
    }
}
