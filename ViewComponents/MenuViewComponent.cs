using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private EnstrumanShopContext context;

        public MenuViewComponent(EnstrumanShopContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Category> categories = context.Categories.ToList();
            return View(categories);
        }
    }
}
