using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopContext _context;

        public AccountController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Account()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
        public IActionResult UserInfos()
        {
            string Username = User.Identity.Name;
            ViewBag.Username = Username;

            return View();
        }

    }

}
