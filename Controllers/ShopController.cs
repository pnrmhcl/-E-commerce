using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private EnstrumanShopContext context;

        public ShopController(EnstrumanShopContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ShopPageViewModel model = new ShopPageViewModel();
            model.Categories = context.Categories.ToList();
            model.Products = context.Products.Include(i => i.ProductImages).ToList();
            return View(model);
        }

        public IActionResult Category(int id)
        {
            CategoryPageViewModel model = new CategoryPageViewModel();
            model.Category = context.Categories.Where(i => i.Id == id).FirstOrDefault();
            model.Categories = context.Categories.ToList();
            model.Products = context.Products.Include(i => i.ProductImages).Where(i => i.CategoryId == id).ToList();
            return View(model);
        }
    }
}
