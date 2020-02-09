using System;
using System.Collections.Generic;
using Forum.Models.Post;

namespace Forum.Models.User
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string UserDescription { get; set; }
        public string ProfileImageUrl { get; set; }
        public int Rating { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public DateTime MemberSince { get; set; }
        public IEnumerable<PostListingModel> UserPosts { get; set; }
    }
}
