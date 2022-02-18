using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebUI.Extensions;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private EnstrumanShopContext database;

        public UserController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        public IActionResult Orders(string success="")
        {
            if (!string.IsNullOrEmpty(success)) ViewBag.Success = success;
            int userID = int.Parse(HttpContext.User.Claims(ClaimTypes.NameIdentifier)[0]);
            List<Order> orders = database.Orders.Include(i=>i.OrderItems).Where(i=>i.UserId==userID).ToList();
            return View(orders);
        }

        public IActionResult AddOrder()
        {
            int userID = int.Parse(HttpContext.User.Claims(ClaimTypes.NameIdentifier)[0]);
            UserBasket userBasket = database.UserBaskets.Include(i=>i.UserBasketItems).Where(i => i.UserId == userID).FirstOrDefault();
            Order newOrder = new Order()
            {
                OrderCode = Guid.NewGuid().ToString(),
                TotalPrice = userBasket.TotalPrice,
                OrderDate=DateTime.Now,
                UserId=userID
            };
            database.Orders.Add(newOrder);
            database.SaveChanges();
            foreach (var item in userBasket.UserBasketItems)
            {
                OrderItem orderItem = new OrderItem()
                {
                    ProductId = item.ProductId,
                    OrderId = newOrder.Id,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                database.OrderItems.Add(orderItem);
            }
            database.UserBasketItems.RemoveRange(database.UserBasketItems.Where(i => i.BasketId == userBasket.Id));
            database.SaveChanges();
            return RedirectToAction("Orders", new { success = "Siparişiniz bize başarıyla ulaşmıştır.Teşekkür Ederiz." });
        }
    }
}
