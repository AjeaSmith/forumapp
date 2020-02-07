using System;
namespace Forum.Models.Forum
{
    public class ForumListingModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public string DatePosted { get; set; }

        public string ImageUrl { get; set; }
    }
}
