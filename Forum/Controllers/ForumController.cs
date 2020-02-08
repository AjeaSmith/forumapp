using System.Data;
using System.IO;
using System.Linq;
using Forum.ForumData.Interfaces;
using Forum.ForumData.Models;
using Forum.Models.Forum;
using Forum.Models.Post;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;
        private readonly IPost _postService;
        private readonly IHostingEnvironment _env;

        private readonly IToastNotification _toastNotification;

        public ForumController(IForum forumService, IPost postService, IHostingEnvironment env,  IToastNotification toastNotification)
        {
            _forumService = forumService;
            _postService = postService;
            _env = env;
            _toastNotification = toastNotification;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var forums = _forumService.GetAll().Select(f => new ForumListingModel
            {
                Id = f.Id,
                Title = f.Title,
                Description = f.Description,
                ImageUrl = f.ImageUrl
            });
            var model = new ForumIndexModel
            {
                forums = forums
            };
            return View(model);
        }
        public IActionResult Create()
        {
            var model = new AddForumModel();
            return View(model);
        }
        [HttpPost, ActionName("Create")]
        public IActionResult Create(AddForumModel model)
        {
            // Upload Image functionality!
            if (ModelState.IsValid)
            {
                _toastNotification.AddSuccessToastMessage("Successfully added forum");
                string imageFile = null;
                if(model.ImageUpload != null)
                {
                    var uploadFolder = Path.Combine(_env.WebRootPath, "images");
                    imageFile = model.ImageUpload.FileName;

                    var filePath = Path.Combine(uploadFolder, imageFile);

                    model.ImageUpload.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                var forumModel = new ForumData.Models.Forum
                {
                    Title = model.Title,
                    Description = model.Description,
                    ImageUrl = imageFile
                };
                _forumService.Create(forumModel);
                return RedirectToAction("Index", "Forum");
            }
            _toastNotification.AddErrorToastMessage("Error: failed adding forum");
            return View();
        }
        public IActionResult Topic(int? id)
        {
            try
            {
                var forum = _forumService.GetById(id);
                var posts = forum.Posts;
                posts.Select(p => new PostListingModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    Created = p.Created.ToString(),
                    AuthorName = p.User.UserName,
                    ForumImageUrl = forum.ImageUrl,
                    Forum = BuildForumListing(p)
                });

                var model = new TopicResultModel
                {
                    Posts = posts,
                    Forum = BuildForum(forum)

                };
                return View(model);
            }
            catch (DataException ex)
            {
                throw ex;
            }

        }
        public ForumListingModel BuildForumListing(Post post)
        {
            var forums = post.Forum;

            return new ForumListingModel
            {
                Id = forums.Id,
                Title = forums.Title,
                Description = forums.Description,
                ImageUrl = forums.ImageUrl
            };
        }
        public ForumListingModel BuildForum(ForumData.Models.Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Title = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }
    }
}
