using System;
using System.Data;
using Forum.ForumData.Interfaces;
using Forum.ForumData.Models;
using Forum.Models.Post;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IPost _postService;
        private readonly IForum _forumService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReplyController(IPost postService, IForum forumService, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forumService;
            _userManager = userManager;
        }
        // GET: /<controller>/
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public IActionResult Create(PostReplyModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = _userManager.GetUserId(User);
                    var user = _userManager.FindByIdAsync(userId).Result;
                    var post = _postService.GetById(model.Id);
                    var date = DateTime.Now;
                    var reply = new PostReply
                    {
                        Post = post,
                        Content = model.ReplyContent,
                        Created = date,
                        User = user
                    };
                    _postService.AddReply(reply);
                    return RedirectToAction("Index", "Post", new { id = model.Id });
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
