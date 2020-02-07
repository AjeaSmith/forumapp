using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.ForumData.Models
{
    public class PostReply
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Cannot send empty message")]
        public string Content { get; set; }
        public DateTime Created { get; set; }
        public virtual Post Post { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

    }
}
