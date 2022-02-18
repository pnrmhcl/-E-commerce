using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.ViewModels;

namespace WebUI.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private EnstrumanShopContext context;

        public HomeController(EnstrumanShopContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            HomepageViewModel model = new HomepageViewModel();
            model.Sliders = context.HomepageSliders.ToList();
            model.Categories = context.Categories.Include(i=>i.Products).ThenInclude(p=>p.ProductImages).ToList();
            model.Products = context.Products.Include(i=>i.ProductImages).ToList();
            return View(model);
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(ContactUsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ContactMessage contactMessage = new ContactMessage()
            {
                Message = model.Message,
                Email = model.Email,
                FullName = model.FullName,
                Title = model.Title
            };
            context.ContactMessages.Add(contactMessage);
            context.SaveChanges();
            ViewBag.Success = true;
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
