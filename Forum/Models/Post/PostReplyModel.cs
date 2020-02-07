using System;
using System.ComponentModel.DataAnnotations;
using Forum.ForumData.Models;

namespace Forum.Models.Post
{
    public class PostReplyModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string ReplyContent { get; set; }

        public DateTime ReplyCreated { get; set; }

        // User it came from
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AuthorRating { get; set; }
        public string AuthorImageUrl { get; set; }
        public bool IsAuthorAdmin { get; set; }

        // Post it came from
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }

        // Forum it's related to
        public string ForumName { get; set; }
        public string ForumImageUrl { get; set; }
        public int ForumId { get; set; }
    }
}
