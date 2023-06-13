using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Project.Models;
using System;
using System.IO;

namespace Project.Controllers
{
    public class FormController : Controller
    {
        private readonly ShopContext _context;

        public FormController(ShopContext context)
        {
            _context = context;
        }
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product model, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Generate a unique file name for the uploaded image
                    string uniqueFileName = GetUniqueFileName(imageFile.FileName);

                    // Set the file name to the `ImageFileName` property of the `Product` model
                    model.ImageFileName = uniqueFileName;

                    // Save the uploaded image to the server
                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", uniqueFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }
                }

                // Add the product to the database
                _context.Products.Add(model);
                _context.SaveChanges();

                // Redirect to a success page or perform other actions
                return RedirectToAction("List", "List");
            }

            // If the model state is not valid, return the form view with validation errors
            return View("Form", model);
        }


        private string GetUniqueFileName(string fileName)
        {
            string uniqueFileName = Path.GetFileNameWithoutExtension(fileName)
                + "_" + Guid.NewGuid().ToString().Substring(0, 8)
                + Path.GetExtension(fileName);
            return uniqueFileName;
        }
    }
}
