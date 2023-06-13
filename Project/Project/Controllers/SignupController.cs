using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class SignupController : Controller
    {
        private readonly ShopContext _context;

        public SignupController(ShopContext context)
        {
            _context = context;
        }

        // GET: /Signup/Signup
        public IActionResult Signup()
        {
            return View();
        }

        // POST: /Signup/Signup
        [HttpPost]
        public IActionResult Signup(string Username, string email, string password1, string password2)
        {
            if (ModelState.IsValid)
            {
                if (password1 == password2)
                {
                    var user = new User
                    {
                        Username = Username,
                        Email = email,
                        Password = password1,
                    };
                    _context.Users.Add(user);
                    _context.SaveChanges();

                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    ModelState.AddModelError("Password2", "Passwords do not match");
                }
            }

            // If the passwords don't match or the model is invalid, return to the signup page
            return View();
        }
    }
}
