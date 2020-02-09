using System;
using System.Collections.Generic;
using Forum.ForumData.Models;
using Forum.ForumData.Interfaces;
using Forum.ForumData;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Forum.ForumServices
{
    public class UserService : IApplicationUser
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public Task Create(ApplicationUser user)
        {
            _context.ApplicationUsers.Add(user);
            return _context.SaveChangesAsync();

        }

        public IEnumerable<ApplicationUser> GetActiveUsers()
        {
            return _context.ApplicationUsers.Where(user => user.IsActive == true);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _context.ApplicationUsers;
        }

        public Task Remove(string id)
        {
            var userToRemove = GetById(id);
            _context.Remove(userToRemove);
            return _context.SaveChangesAsync();
        }

        public ApplicationUser GetByName(string name)
        {
            return _context.ApplicationUsers.Where(user => user.UserName == name).FirstOrDefault();
        }

        public ApplicationUser GetById(string id)
        {
            return _context.ApplicationUsers.Find(id);
        }

        public IEnumerable<ApplicationUser> GetAdminUsers()
        {
            return _context.ApplicationUsers.Where(user => user.IsAdmin == true);
        }

    }
}
