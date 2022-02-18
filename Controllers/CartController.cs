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
    public class CartController : Controller
    {
        private EnstrumanShopContext database;

        public CartController(EnstrumanShopContext database)
        {
            this.database = database;
        }

        public IActionResult Index()
        {
            int userID = int.Parse(HttpContext.User.Claims(ClaimTypes.NameIdentifier)[0]);
            UserBasket basket = database.UserBaskets.Include(i => i.UserBasketItems).ThenInclude(i => i.Product).ThenInclude(i=>i.ProductImages).Include(i => i.User).Where(i => i.UserId == userID).FirstOrDefault();
            if (basket == null)
            {
                createNewBasket(userID);
            }
            return View(basket);
        }

        public IActionResult AddToCart(int productId, int quantity)
        {
            Product product = database.Products.Where(i => i.Id == productId).FirstOrDefault();
            if (product == null) return RedirectToAction("Index", "Home");
            int userID = int.Parse(HttpContext.User.Claims(ClaimTypes.NameIdentifier)[0]);
            UserBasket basket = database.UserBaskets.Include(i=>i.UserBasketItems).Where(i => i.UserId == userID).FirstOrDefault();
            if (basket == null) basket = createNewBasket(userID);
            UserBasketItem userBasketItem = database.UserBasketItems.Where(i => i.BasketId == basket.Id && i.ProductId == product.Id).FirstOrDefault();
            if(userBasketItem != null)
            {
                userBasketItem.Quantity += quantity;
                userBasketItem.Price = product.Price * userBasketItem.Quantity;
                basket.TotalPrice = basket.UserBasketItems.Sum(i => i.Price);
                database.UserBasketItems.Update(userBasketItem);
                database.UserBaskets.Update(basket);
                database.SaveChanges();
            }
            else
            {
                userBasketItem = new UserBasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = product.Id,
                    Quantity = quantity,
                    Price = product.Price * quantity,
                };
                database.UserBasketItems.Add(userBasketItem);
                basket.TotalPrice = basket.UserBasketItems.Sum(i => i.Price);
                database.UserBaskets.Update(basket);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult DecreaseQuantity(int userBasketItemId)
        {
            UserBasketItem userBasketItem = database.UserBasketItems.Include(i=>i.Product).Where(i => i.Id == userBasketItemId).FirstOrDefault();
            if(userBasketItem.Quantity <= 1)
            {
                database.UserBasketItems.Remove(userBasketItem);
            }
            else
            {
                userBasketItem.Quantity--;
                userBasketItem.Price = userBasketItem.Product.Price * userBasketItem.Quantity;
                database.UserBasketItems.Update(userBasketItem);
            }
            UserBasket basket = database.UserBaskets.Include(i => i.UserBasketItems).Where(i => i.Id == userBasketItem.BasketId).FirstOrDefault();
            basket.TotalPrice = basket.UserBasketItems.Sum(i => i.Price);
            database.UserBaskets.Update(basket);
            database.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int userBasketItemId)
        {
            UserBasketItem userBasketItem = database.UserBasketItems.Where(i => i.Id == userBasketItemId).FirstOrDefault();
            database.UserBasketItems.Remove(userBasketItem);
            database.SaveChanges();
            UserBasket basket = database.UserBaskets.Include(i=>i.UserBasketItems).Where(i => i.Id == userBasketItem.BasketId).FirstOrDefault();
            basket.TotalPrice = basket.UserBasketItems.Sum(i => i.Price);
            database.UserBaskets.Update(basket);
            database.SaveChanges();
            return RedirectToAction("Index");
        }


        private UserBasket createNewBasket(int userId)
        {
            UserBasket basket = new UserBasket() { UserId = userId, TotalPrice = 0 };
            database.UserBaskets.Add(basket);
            database.SaveChanges();
            return basket;
        }
    }
}
