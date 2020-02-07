using System;
using System.Collections.Generic;
using Forum.ForumData.Models;
using Forum.Models.Forum;

namespace Forum.Models.Post
{
    public class PostListingModel
    {
        public virtual ForumListingModel Forum { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Created { get; set; }

        public string ForumImageUrl { get; set; }

        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorId { get; set; }

        public int RepliesCount { get; set; }
    }
}
