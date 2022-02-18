using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin")]
    public class ProductImageController : Controller
    {
        private EnstrumanShopContext database;

        public ProductImageController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            List<ProductImage> images = database.ProductImages.Include(i=>i.Product).ToList(); 
            return View(images);
        }

        public IActionResult Delete(int id)
        {
            ProductImage image = database.ProductImages.Where(i => i.Id == id).FirstOrDefault();
            if(image!= null)
            {
                database.ProductImages.Remove(image);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            List<Product> products = database.Products.ToList();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormFile file , int productId)
        {
            string fileName = Path.GetFileNameWithoutExtension(file.FileName) + DateTime.Now.ToString("yyyyMMddHHmmss") + Path.GetExtension(file.FileName);
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", fileName));
            var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            ProductImage image = new ProductImage()
            {
                IsMainImg = false,
                ProductId = productId,
                Path = fileName
            };
            database.ProductImages.Add(image);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
