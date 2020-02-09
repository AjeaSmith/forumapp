using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.ForumData.Models;

namespace Forum.ForumData.Interfaces
{
    public interface IPost
    {
        // Get all Posts
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetLatestPosts(int num);
        IEnumerable<Post> GetUserPost(string id);

        // Get Post by ID
        Post GetById(int? id);

        // Create Post
        Task Create(Post Post);

        Task AddReply(PostReply reply);

        // Delete Post by ID
        Task Delete(int id);
    }
}
