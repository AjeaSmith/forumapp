using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.ForumData.Models;
using Forum.ForumData.Interfaces;
using Forum.ForumData;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Forum.ForumServices
{
    public class ReplyService : IPostReply
    {
        private readonly AppDbContext _context;
        public ReplyService(AppDbContext context)
        {
            _context = context;   
        }
        public Task Create(PostReply reply)
        {
            _context.PostReplies.Add(reply);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var reply = GetReplyById(id);
            _context.PostReplies.Remove(reply);
            return _context.SaveChangesAsync();
        }

        public Task Edit(int id, string newMessage)
        {
            var reply = GetReplyById(id);
            reply.Content = newMessage;
            _context.PostReplies.Update(reply);
            return _context.SaveChangesAsync();
        }

        public PostReply GetReplyById(int id)
        {
            return _context.PostReplies.Include(r => r.Post).ThenInclude(p => p.Forum).FirstOrDefault();
           
        }
    }
}
