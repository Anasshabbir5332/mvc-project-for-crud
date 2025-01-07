using Microsoft.AspNetCore.Mvc;
using mvc_project_for_crud.Models;
using mvc_project_for_crud.Services;
//using System.Net.Http.Headers;

namespace mvc_project_for_crud.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationdDbContext context;
        private readonly IWebHostEnvironment environment;

        //create constructer to read the data from database
        public ProductsController(ApplicationdDbContext context, IWebHostEnvironment environment) 
        {
            this.context = context;
            this.environment = environment;
        }
        public IActionResult Index()
        {
            //Create a varibale and with the database name of table products

            var products = context.products.ToList();
                return View(products);
        }
            public IActionResult Create()
            {
                    return View();
            }
        [HttpPost]

        public IActionResult Create(ProductDto productDto)
        {
            if(productDto.Imagefile == null)
            {
                ModelState.AddModelError("imageFile", "The image file is missing");
                return View(productDto);
            }

            //save the image file
            String newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(productDto.Imagefile!.FileName);

            String imageFullPath = environment.WebRootPath + "/products/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                productDto.Imagefile.CopyTo(stream);
            }
            //save the product in the database
            
            Product product = new Product()
            {
                Name = productDto.Name,
                Brand = productDto.Brand,
                Category = productDto.Category,
                Price = productDto.Price,
                Description = productDto.Description,
                ImageFilename = newFileName,
                CreatedAt = DateTime.Now,
            };
            context.products.Add(product);
            context.SaveChanges();

            return RedirectToAction("Index");

        }
        // Start-code  code for just pass data to edit page (to show in fields
        public ActionResult Edit(int id)
        {
            var product = context.products.Find(id);
            if (product == null)
            {
                return RedirectToAction("index", "product");
            }

            //create productdto from product
            var productdto = new ProductDto()
            {
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,

            };
            // create these to show id in Edit form
            ViewData["ProductId"] = product.Id;
            ViewData["imageFileName"] = product.ImageFilename;
            ViewData["CreatedAT"] = product.CreatedAt.ToString("MM/dd/yyyy");

            return View(productdto);
        }

        // End-code  code for just pass data to edit page (to show in fields
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductDto productdto)
        {
            // Validate Model State
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", new { id });
            }

            // Fetch Product
            var product = context.products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            // Image Handling (simplified)
            string newFileName = product.ImageFilename;
            if (productdto.Imagefile != null)
            {
                newFileName = UploadImage(productdto.Imagefile); // Call helper method
                DeleteImage(product.ImageFilename); // Call helper method
            }

            // Update Product
            product.Name = productdto.Name;
            product.Brand = productdto.Brand;
            product.Category = productdto.Category;
            product.Price = productdto.Price;
            product.Description = productdto.Description;
            product.ImageFilename = newFileName;

            context.SaveChanges();

            return RedirectToAction("Index", "Product");
        }

        // Helper Methods
        private string UploadImage(IFormFile file)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(environment.WebRootPath, "products", fileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        private void DeleteImage(string fileName)
        {
            string filePath = Path.Combine(environment.WebRootPath, "products", fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        // delete 
        public IActionResult Delete(int id)
        {
            var product = context.products.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            string imageFullPath = Path.Combine(environment.WebRootPath, "products", product.ImageFilename);
            if (System.IO.File.Exists(imageFullPath))
            {
                System.IO.File.Delete(imageFullPath);
            }
            context.products.Remove(product);

            context.SaveChanges();
            return RedirectToAction("Index");


        }


    }

}
