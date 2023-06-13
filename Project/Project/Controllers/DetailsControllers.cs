using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class DetailsController : Controller
    {
        private readonly ShopContext _context;

        public DetailsController(ShopContext context)
        {
            _context = context;
        }

        // GET: /Details/Details/5
        [HttpGet("/Details/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new DetailsViewModel
            {
                Products = new List<Product> { product }
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddOrder(int productId, string color, string size, int quantity)
        {
            // Create a new order
            var order = new Order
            {
                ProductId = productId,
                OrderDate = DateTime.Now,
                Quantity = quantity,
                Color = color,
                Size = size
            };

            // Save the order to the database
            _context.Orders.Add(order);
            _context.SaveChanges();

            // Optionally, you can perform additional operations or set a success message here

            // Return a JSON response indicating success
            return RedirectToAction("Cart", "Cart");
        }

        public class DetailsViewModel
        {
            public DetailsViewModel()
            {
                Orders = new List<Order>(); // Initialize the Orders property to an empty list
                Products = new List<Product>(); // Initialize the Products property to an empty list
            }

            public List<Order> Orders { get; set; }
            public List<Product> Products { get; set; }
        }
    }
}

