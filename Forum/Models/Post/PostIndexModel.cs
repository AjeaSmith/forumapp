using System;
using System.Collections.Generic;
using Forum.ForumData.Models;

namespace Forum.Models.Post
{
    public class PostIndexModel
    {
        // Add Individual properties for posts
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public bool IsAuthorAdmin { get; set; }

        public DateTime Date { get; set; }
        public int ForumId { get; set; }
        public string ForumName { get; set; }

        public IEnumerable<PostReplyModel> Replies { get; set; }
    }
}
