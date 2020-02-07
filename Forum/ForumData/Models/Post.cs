using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.ForumData.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [MinLength(4, ErrorMessage = "Title must be at least 4 characters long")]
        [Required(ErrorMessage = "This field is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Content { get; set; }

        public DateTime Created { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
        public IEnumerable<PostReply> PostReplies { get; set; }
        public virtual Forum Forum { get; set; }
    }
}
