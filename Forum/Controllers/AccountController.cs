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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IApplicationUser _userService;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IApplicationUser userService)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
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
                    TempData["message"] = "Saved Succesfully";
                    ViewBag.Message = "Successful";
                    var user = new ApplicationUser
                    {
                        UserName = model.Username,
                        Email = model.Email,
                        PasswordHash = model.Password,
                        MemberSince = DateTime.Now,
                        IsActive = true
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {

                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }

                }
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
                ViewBag.Message = "";
                if (ModelState.IsValid)
                {
                    ViewBag.Message = "Logged In Succesfully";
                    var user = await _userManager.FindByNameAsync(model.Username);
                    var result = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (result)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }
                    return View();

                }
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
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Dismiss()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
