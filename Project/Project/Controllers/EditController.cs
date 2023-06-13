using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace Project.Controllers
{
    public class EditController : Controller
    {
        private readonly ShopContext _context;

        public EditController(ShopContext context)
        {
            _context = context;
        }

        // GET: /Edit/Edit/{id}
        [HttpGet("/Edit/Edit/{id}")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productList = new List<Product> { product }; // Wrap the product in a list

            return View(productList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = _context.Products.FirstOrDefault(p => p.Id == model.Id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = model.Name;
                existingProduct.Category = model.Category;
                existingProduct.Description = model.Description;
                existingProduct.Price = model.Price;

                _context.Products.Update(existingProduct);
                _context.SaveChanges();

                return RedirectToAction("Details", "Details", new { id = model.Id });
            }

            return View(model);
        }
    }
}
