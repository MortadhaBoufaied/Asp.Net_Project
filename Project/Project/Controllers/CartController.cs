using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopContext _context;

        public CartController(ShopContext context)
        {
            _context = context;
        }

        public IActionResult Cart()
        {
            var viewModel = new CartViewModel
            {
                Orders = _context.Orders.ToList(),
                Products = _context.Products.ToList()
            };

            return View(viewModel);
        }

        public IActionResult Delete(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.ProductId == id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Cart");
        }

        public IActionResult OrderCount()
        {
            int orderCount = _context.Orders.Count();
            return Content("Total Orders: " + orderCount);
        }
    }

    public class CartViewModel
    {
        public List<Order> Orders { get; set; }
        public List<Product> Products { get; set; }
    }
}
