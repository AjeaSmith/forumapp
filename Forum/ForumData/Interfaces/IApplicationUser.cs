using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.ForumData.Models;

namespace Forum.ForumData.Interfaces
{
    public interface IApplicationUser
    {
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> GetActiveUsers();
        IEnumerable<ApplicationUser> GetAdminUsers();
        ApplicationUser GetByName(string name);
        ApplicationUser GetById(int id);

        Task Create(ApplicationUser user);
        Task Remove(int id);
    }
}
