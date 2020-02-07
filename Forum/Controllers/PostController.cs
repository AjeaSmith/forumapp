using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Forum.ForumData.Interfaces;
using Forum.ForumData.Models;
using Forum.Models.Forum;
using Forum.Models.Post;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private readonly IApplicationUser _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(IPost postService, IForum forumService, IApplicationUser userService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userService = userService;
            _userManager = userManager;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index(int id)
        {
            var post = _postService.GetById(id);
            var model = new PostIndexModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Date = post.Created,
                Replies = BuildPostReplies(post.PostReplies),
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorRating = post.User.Rating,
                ForumId = post.Forum.Id,
                ForumName = post.Forum.Title
            };
            return View(model);
        }
        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            var ReplyModel = replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                ReplyContent = reply.Content,
                ReplyCreated = reply.Created,
                AuthorName = reply.User.UserName,
                AuthorId = reply.UserId,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
            });
            return ReplyModel;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public IActionResult Create(int id, PostCreateModel post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var forum = _forumService.GetById(id);
                    var userId = _userManager.GetUserId(User);
                    var user = _userManager.FindByIdAsync(userId).Result;
                    var postModel = new Post
                    {
                        Title = post.Title,
                        Content = post.Content,
                        Created = DateTime.Now,
                        Forum = forum,
                        User = user
                    };
                    _postService.Create(postModel);
                    return RedirectToAction("Index", "Forum");
                }
                return View();
            }
            catch (DataException ex)
            {
                throw ex;
            }
        }
    }
}
