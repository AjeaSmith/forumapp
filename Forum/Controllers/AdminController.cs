using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Forum.ForumData.Interfaces;
using Forum.ForumData.Models;
using Forum.Models.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IToastNotification _toastNotification;
        private readonly IApplicationUser _userService;
        public AdminController(RoleManager<IdentityRole> roleManager, 
        UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, 
        IApplicationUser userService, IToastNotification toastNotification)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService; 
            _toastNotification = toastNotification;  
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            var users = _userService.GetAdminUsers();
            var model = new AddRoleModel
            {
                AdminUsers = users
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserRole(AddRoleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _toastNotification.AddSuccessToastMessage("User role added successfully");
                    if(model.RoleName == "Admin")
                    {
                        var user = new ApplicationUser
                        {
                            UserName = model.UserName,
                            Email = model.Email,
                            PasswordHash = model.Password,
                            IsAdmin = true
                        };
                        await CreateUser(user, model);
                        await AddRole(user, model);
                    }
                    else
                    {
                        var user = new ApplicationUser
                        {
                            UserName = model.UserName,
                            Email = model.Email,
                            PasswordHash = model.Password,
                            IsAdmin = false
                        };
                        await CreateUser(user, model);
                        await AddRole(user, model);
                    }

                }
                _toastNotification.AddErrorToastMessage("Error: failed to add user role");
                return RedirectToAction("Index", "Forum");
            }
            catch (DataException ex)
            {
                throw ex;
            }
        }

        private async Task<RedirectToActionResult> CreateUser(ApplicationUser user, AddRoleModel model)
        {
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            return RedirectToAction("Index", "Forum");
        }

        private async Task<IActionResult> AddRole(ApplicationUser user, AddRoleModel model)
        {
            var role = new IdentityRole
            {
                Name = model.RoleName
            };
            await _roleManager.CreateAsync(role);
            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction("Index", "Admin");
        }
    }
}
