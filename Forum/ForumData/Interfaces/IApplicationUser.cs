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
        ApplicationUser GetById(string id);
        ApplicationUser UpdateUser(string id, string Email, string userDescription, string password);

        Task Create(ApplicationUser user);
        Task Remove(string id);
    }
}
