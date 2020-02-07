using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Forum.ForumData.Models;

namespace Forum.ForumData.Interfaces
{
    public interface IPostReply
    {
        PostReply GetReplyById(int id);

        Task Create(PostReply reply);
        Task Delete(int id);
        Task Edit(int id, string newMessage);
    }
}
