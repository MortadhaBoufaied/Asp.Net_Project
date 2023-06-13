using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Linq;

namespace Project.Controllers
{
    public class ListController : Controller
    {
        private readonly ShopContext _context;

        public ListController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult List()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();

            // Update the remaining product IDs


            return RedirectToAction("List");
        }

    }
}
