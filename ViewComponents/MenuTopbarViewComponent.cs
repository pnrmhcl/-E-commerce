using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class MenuTopbarViewComponent : ViewComponent
    {
        private EnstrumanShopContext context;
        private IHttpContextAccessor contextAccessor;
        public MenuTopbarViewComponent(EnstrumanShopContext context, IHttpContextAccessor contextAccessor)
        {
            this.context = context;
            this.contextAccessor = contextAccessor;
        }
        public IViewComponentResult Invoke()
        {
            TopbarViewModel model = new TopbarViewModel();
            if (!contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                model.Authenticated = false;
                return View(model);
            }
            string email = this.contextAccessor.HttpContext.User.Claims(ClaimTypes.Email)[0];
            string fullName = this.contextAccessor.HttpContext.User.Claims(ClaimTypes.GivenName)[0];
            bool isAdmin = this.contextAccessor.HttpContext.User.Claims(ClaimTypes.Role).Where(i => i == "admin").FirstOrDefault() != null;
            model.Email = email;
            model.FullName = fullName;
            model.Authenticated = true;
            model.IsAdmin = isAdmin;
            return View(model);
        }
    }
}
