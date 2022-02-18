using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles="admin")]
    public class CategoryController : Controller
    {

        private EnstrumanShopContext database;

        public CategoryController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            List<Category> categories = database.Categories.ToList();
            return View(categories);
        }

        public IActionResult Delete(int id)
        {
            Category category = database.Categories.Where(i => i.Id == id).FirstOrDefault();
            if(category!=null)
            {
                database.Categories.Remove(category);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Category category = database.Categories.Where(i => i.Id == id).FirstOrDefault();
            if (category != null)
            {
                return View(category);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Update(Category category)
        {
            Category categoryToUpdate = database.Categories.Where(i => i.Id == category.Id).FirstOrDefault();
            if (category != null)
            {
                categoryToUpdate.Name = category.Name;
                database.Categories.Update(categoryToUpdate);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(string categoryName)
        {
            Category category = new Category()
            {
                Name = categoryName
            };
            database.Categories.Add(category);
            database.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
