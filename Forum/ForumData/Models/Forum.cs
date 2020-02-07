using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.ForumData.Models
{
    public class Forum
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MinLength(3, ErrorMessage = "Must be at least 3 characters long")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage = "This field is required")]
        [Url(ErrorMessage = "Please use valid Url")]
        public string ImageUrl { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
