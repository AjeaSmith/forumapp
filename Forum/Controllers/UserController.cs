using Forum.ForumData.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    public class UserController : Controller
    {
        private readonly IApplicationUser _userService;
        public UserController(IApplicationUser userService)
        {
            _userService = userService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
