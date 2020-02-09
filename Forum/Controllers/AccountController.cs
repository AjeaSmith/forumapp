using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Forum.ForumData.Interfaces;
using Forum.ForumData.Models;
using Forum.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NToastNotify;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUser _userService;
        private readonly IToastNotification _toastNotification;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IApplicationUser userService, IToastNotification toastNotification)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
            _toastNotification = toastNotification;
        }
        // GET: /<controller>/
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost, ActionName("Register")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _toastNotification.AddSuccessToastMessage("Registration was Successful!");
                    var user = new ApplicationUser
                    {
                        UserName = model.Username,
                        Email = model.Email,
                        PasswordHash = model.Password,
                        MemberSince = DateTime.Now,
                        IsActive = true,
                        Rating = 0
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", "Home");
                }
                _toastNotification.AddErrorToastMessage("Error: Register Failed");
                return View();
            }
            catch (DataException ex)
            {
                throw ex;
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost, ActionName("Login")]
        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _toastNotification.AddSuccessToastMessage("Logged In Successfully");
                    var user = await _userManager.FindByNameAsync(model.Username);
                    var result = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (result)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", "Home");
                }
                _toastNotification.AddErrorToastMessage("Error: Login Failed");
                return View();
            }
            catch (DataException ex)
            {
                throw ex;
            }
        }
        public async Task<IActionResult> Logout()
        {
            // isActive is false at this point
            await _signInManager.SignOutAsync();
            _toastNotification.AddInfoToastMessage("Logged out Successfully");
            return RedirectToAction("Index", "Home");
        }
    }
}
