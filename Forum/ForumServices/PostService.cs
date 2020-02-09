using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.ForumData.Models;
using Forum.ForumData.Interfaces;
using Forum.ForumData;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Forum.ForumServices
{
    public class PostService : IPost
    {
        private AppDbContext _context;
        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public Task Create(Post Post)
        {
             _context.Posts.Add(Post);
            return _context.SaveChangesAsync();
        }
        public Task AddReply(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            return _context.SaveChangesAsync();
        }
        public Task Delete(int id)
        {
           var postToDelete = _context.Posts.Find(id);
            _context.Posts.Remove(postToDelete);
            return _context.SaveChangesAsync();
        }

        public IEnumerable<Post> GetAll()
        {
            return _context.Posts.Include(p => p.User).Include(p => p.PostReplies).ThenInclude(r => r.User).Include(r => r.Forum);
        }

        public Post GetById(int? id)
        {
            return _context.Posts.Where(post => post.Id == id)
                .Include(post => post.Forum)
                .Include(post => post.User)
                .Include(post => post.PostReplies)
                .ThenInclude(r => r.User)
                .FirstOrDefault();
        }
        public int GetReplyCount(int? id)
        {
            return GetById(id).PostReplies.Count();
        }
        public IEnumerable<Post> GetLatestPosts(int num)
        {
            return GetAll().OrderByDescending(p => p.Created).Take(num);
        }

        // Get posts from signed in user
        public IEnumerable<Post> GetUserPost(string id)
        {
            return _context.Posts.Where(p => p.UserId == id);
        }
    }
}
