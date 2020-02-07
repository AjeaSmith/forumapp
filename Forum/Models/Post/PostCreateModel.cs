using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models.Post
{
    public class PostCreateModel
    {
        public int ForumId { get; set; }

        public string ForumName { get; set; }

        public string ForumImageUrl { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [MinLength(100, ErrorMessage = "Content is too short, must be at least 100 characters long")]
        public string Content { get; set; }

        public string Created { get; set; }

        public string AuthorName { get; set; }

    }
}
