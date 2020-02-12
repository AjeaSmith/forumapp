using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Forum.ForumData.Interfaces;
using Forum.ForumData.Models;
using Forum.Models.Post;
using Forum.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IApplicationUser _userService;
        private readonly IPost _postService;
        private readonly IToastNotification _toastNotification;
        private readonly UserManager<ApplicationUser> _userManager;
        public ProfileController(IApplicationUser userService, 
        UserManager<ApplicationUser> userManager, IPost postService, IToastNotification toastNotification)
        {
            _userService = userService;
            _userManager = userManager;
            _postService = postService;
            _toastNotification = toastNotification;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var posts = _postService.GetUserPost(userId);
            var user = _userService.GetById(userId);
            // var hasUserRoles = _userManager.IsInRoleAsync(user, "Admin").Result;
            var model = new UserViewModel(){
                UserName = user.UserName,
                UserDescription = user.UserDescription,
                Rating = user.Rating,
                IsActive = user.IsActive,
                IsAdmin = user.IsAdmin,
                MemberSince = user.MemberSince,
                UserPosts = BuildUserPosts(posts)
            };
            return View(model);
        }

        private IEnumerable<PostListingModel> BuildUserPosts(IEnumerable<Post> posts)
        {
            return posts.Select(p => new PostListingModel(){
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                Created = p.Created.ToString()
            });
        }
        public IActionResult Edit()
        {
          var userId = _userManager.GetUserId(User);
          var user = _userService.GetById(userId);

          var model = new UserViewModel(){
              UserDescription = user.UserDescription,
              Email = user.Email,
              Password = user.PasswordHash
          };
          return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UserViewModel model)
        {
            //update the user from DB with model values
            try
            {
                if(ModelState.IsValid){
                    _toastNotification.AddSuccessToastMessage("Updated successfully");
                    var userId = _userManager.GetUserId(User);
                    _userService.UpdateUser(userId, model.Email, model.UserDescription, model.Password);
                    return RedirectToAction("Index", "Profile");
                }
                _toastNotification.AddErrorToastMessage("Error: failed to make changes");
                return View();
            }
            catch (DataException ex)
            {
                
                throw ex;
            }
        }
        [HttpPost]
        public IActionResult Deactivate(string id)
        {
            try
            {
                if(id != null){
                    var user = _userService.GetById(id);
                    var userId = user.Id;
                    _userService.Remove(userId);
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Index", "Profile");
            }
            catch (DataException ex)
            {
                throw ex;
            }
        }
    }
}
