using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.ForumData;
using Forum.ForumData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Forum.ForumServices
{
    public class ForumService : IForum
    {
        private AppDbContext _context;
        public ForumService(AppDbContext context)
        {
            _context = context;
        }

        public Task Create(ForumData.Models.Forum forum)
        {
            _context.Forums.Add(forum);
            return _context.SaveChangesAsync();
        }

        public Task Delete(int id)
        {
            var forumToDelete = _context.Forums.Find(id);
            _context.Remove(forumToDelete);
            return _context.SaveChangesAsync();
        }

        public Task Edit(int id, string title, string description, string imageUrl)
        {
            var forumToEdit = _context.Forums.Find(id);
            forumToEdit.Title = title;
            forumToEdit.Description = description;
            forumToEdit.ImageUrl = imageUrl;

            _context.Update(forumToEdit);
            return _context.SaveChangesAsync();
        }

        public IEnumerable<ForumData.Models.Forum> GetAll()
        {
            return _context.Forums.Include(f => f.Posts);
        }

        public ForumData.Models.Forum GetById(int? id)
        {
            var forum = _context.Forums.Where(f => f.Id == id).Include(f => f.Posts).ThenInclude(p => p.User).FirstOrDefault();
            return forum;
;        }
    }
}
