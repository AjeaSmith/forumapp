using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.ForumData.Interfaces
{
    public interface IForum 
    {
        IEnumerable<Models.Forum> GetAll();
        Models.Forum GetById(int? id);

        Task Create(Models.Forum forum);
        Task Delete(int id);
        Task Edit(int id, string title, string description, string imageUrl);

    }
}
