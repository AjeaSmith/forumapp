using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Forum.Models;
using Forum.ForumData.Interfaces;
using Forum.Models.Home;
using Forum.Models.Post;
using Forum.ForumData.Models;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPost _postService;

        public HomeController(ILogger<HomeController> logger, IPost postService)
        {
            _logger = logger;
            _postService = postService;
        }

        public IActionResult Index()
        {
            var posts = _postService.GetLatestPosts(10);
            var postModel = posts.Select(p => new PostListingModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Created = p.Created.ToString(),
                RepliesCount = p.PostReplies.Count()
            });
            var model = new HomeIndexModel
            {
                Latest = postModel
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
