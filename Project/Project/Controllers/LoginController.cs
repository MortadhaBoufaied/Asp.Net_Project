using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Project.Models;

namespace Project.Controllers
{
    public class LoginController : Controller
    {
        private readonly ShopContext _context;

        public LoginController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Validate the email and password against your user database
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user != null && VerifyPassword(password, user.Password))
            {
                // Assuming the user is valid, create the user's claims
                var claims = new[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    // Add any additional claims you need for your application
                };

                // Create the identity for the user
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Check if the user is an admin based on their email address
                if (user.Email == "boufaiedmortadha7@gmail.com")
                {
                    // Add the "Admin" role claim
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                }

                // Sign in the user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                // Set the login time in session
                HttpContext.Session.SetString("LoginTime", DateTime.Now.ToString());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid email or password");
                return View();
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Implement your password verification logic here
            // Compare the enteredPassword with the storedPassword hash
            // Return true if the passwords match, false otherwise

            // Example implementation using plain-text passwords (not recommended in production)
            return enteredPassword == storedPassword;
        }

        // Logout action
        public async Task<IActionResult> Logout()
        {
            // Clear the login time session variable
            HttpContext.Session.Remove("LoginTime");

            // Perform the logout operation
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        // Check if the session has expired
        private bool IsSessionExpired()
        {
            var loginTime = HttpContext.Session.GetString("LoginTime");
            if (loginTime != null)
            {
                var lastLoginTime = DateTime.Parse(loginTime);
                var currentTime = DateTime.Now;
                var sessionTimeout = TimeSpan.FromMinutes(10);
                return currentTime - lastLoginTime > sessionTimeout;
            }
            return true;
        }

        // Example protected action
        public IActionResult ProtectedAction()
        {
            if (IsSessionExpired())
            {
                // Session expired, log out the user
                return RedirectToAction("Logout");
            }

            // Continue with the protected action
            return View();
        }
    }
}
