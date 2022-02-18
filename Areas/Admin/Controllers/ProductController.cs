using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles ="admin")]
    public class ProductController : Controller
    {
        private EnstrumanShopContext database;

        public ProductController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            List<Product> products = database.Products.Include(i=>i.Category).ToList();
            return View(products);
        }
        public IActionResult Delete(int id)
        {
            Product product = database.Products.Where(i => i.Id == id).FirstOrDefault();
            if (product != null)
            {
                List<OrderItem> order = database.OrderItems.Where(i => i.ProductId == product.Id).ToList();
                database.OrderItems.RemoveRange(order);
                database.SaveChanges();

                List<UserBasketItem> items = database.UserBasketItems.Where(i => i.ProductId == product.Id).ToList();
                database.UserBasketItems.RemoveRange(items);
                database.SaveChanges();

                List<ProductImage> productImages = database.ProductImages.Where(i => i.ProductId == product.Id).ToList();
                database.ProductImages.RemoveRange(productImages);
                database.SaveChanges();

                database.Products.Remove(product);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Product product = database.Products.Include(i=>i.Category).Where(i => i.Id == id).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            ProductWithCategoriesModel model = new ProductWithCategoriesModel();
            model.Product = product;
            model.MainCategory = product.Category;
            model.Categories = database.Categories.ToList();
            model.Categories.Remove(product.Category);
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(ProductWithCategoriesModel model, int categoryId)
        {
            if(!ModelState.IsValid)
                return View(model);
            Product product = database.Products.Include(i => i.Category).Where(i => i.Id == model.Product.Id).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            product.Name = model.Product.Name;
            product.Description = model.Product.Description;
            product.Price = model.Product.Price;
            product.UnitsInStock = model.Product.UnitsInStock;
            product.CategoryId = categoryId;
            database.Products.Update(product);
            database.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Add()
        {
            ProductWithCategoriesModel model = new ProductWithCategoriesModel();
            model.Categories = database.Categories.ToList();
            return View(model);
        }
        [HttpPost]
        public IActionResult Add(ProductWithCategoriesModel model , int categoryId)
        {
            model.Categories = database.Categories.ToList();
            if (!ModelState.IsValid) return View(model);
            Product product = new Product()
            {
                Name = model.Product.Name,
                Description = model.Product.Description,
                Price=model.Product.Price,
                UnitsInStock=model.Product.UnitsInStock,
                CategoryId=categoryId
            };
            database.Products.Add(product);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
