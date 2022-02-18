using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    [Area("admin")]
    public class OrderController : Controller
    {
        private EnstrumanShopContext database;

        public OrderController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            List<Order> orders = database.Orders.Include(i=>i.User).Include(i => i.OrderItems).ThenInclude(i=>i.Product).ToList();
            return View(orders);
        }
        public IActionResult Delete(int id)
        {
            Order order = database.Orders.Where(i => i.Id == id).FirstOrDefault();
            if (order != null)
            {
                List<OrderItem> orderItems = database.OrderItems.Where(i => i.OrderId == order.Id).ToList();
                database.OrderItems.RemoveRange(orderItems);
                database.SaveChanges();
                database.Orders.Remove(order);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
