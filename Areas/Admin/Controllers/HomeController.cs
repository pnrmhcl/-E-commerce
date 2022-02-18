using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Areas.Admin.Models;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="admin")]
    public class HomeController : Controller
    {

        private EnstrumanShopContext database;

        public HomeController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            DashboardModel model = new DashboardModel();
            model.CategoryCount = database.Categories.Count();
            model.ProductCount = database.Products.Count();
            model.UserCount = database.Users.Count();
            model.OrderCount = database.Orders.Count();
            return View(model);
        }
    }
}
