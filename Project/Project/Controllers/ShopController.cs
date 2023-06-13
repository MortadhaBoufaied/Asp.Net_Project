using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopContext _context;

        public ShopController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Shop()
        {
            var products = _context.Products.ToList();
            return View(products);
        }
    }
}
